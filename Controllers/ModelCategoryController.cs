using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentCar.Models;
using System.IO;
using System.Web.UI;
using System;

namespace RentCar.Controllers
{
    public class ModelCategoryController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: ModelCategory
        public ActionResult Index()
        {
            Session["POS"] = "1508";
            
            if (AccountController.ST_1508_unhold != true || AccountController.ST_1508_hold != true && AccountController.ST_1508_undelete != true || AccountController.ST_1508_delete != true)
            {
                var cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.Include(c => c.CR_Mas_Sup_Category_Car).Include(c => c.CR_Mas_Sup_Model).Where(stat => stat.CR_Mas_Sup_Car_Model_Category_Status != "H" && stat.CR_Mas_Sup_Car_Model_Category_Status != "D");
                return View(cR_Mas_Sup_Car_Model_Category.ToList());
            }
            else
                if (AccountController.ST_1508_unhold != true || AccountController.ST_1508_hold != true)
            {
                var cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.Include(c => c.CR_Mas_Sup_Category_Car).Include(c => c.CR_Mas_Sup_Model).Where(stat => stat.CR_Mas_Sup_Car_Model_Category_Status != "H");

                return View(cR_Mas_Sup_Car_Model_Category.ToList());
            }
            else if (AccountController.ST_1508_undelete != true || AccountController.ST_1508_delete != true)
            {
                var cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.Include(c => c.CR_Mas_Sup_Category_Car).Include(c => c.CR_Mas_Sup_Model).Where(stat => stat.CR_Mas_Sup_Car_Model_Category_Status != "D");
                return View(cR_Mas_Sup_Car_Model_Category.ToList());
            }
            else
            {
                var cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.Include(c => c.CR_Mas_Sup_Category_Car).Include(c => c.CR_Mas_Sup_Model);
                return View(cR_Mas_Sup_Car_Model_Category.ToList());
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post(string excelCall, string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                if (HomeController.Language == "1")
                {
                    HomeController.Language = "2";
                    Session["Lang"] = "Arabic";
                }
                else
                {
                    if (HomeController.Language == "2")
                    {
                        HomeController.Language = "1";
                        Session["Lang"] = "English";
                    }
                }
            }
            if (!string.IsNullOrEmpty(excelCall))
            {
                var ModelCat = new System.Data.DataTable("teste");

                ModelCat.Columns.Add("الحمولة", typeof(string));
                ModelCat.Columns.Add("عدد الأحصنة", typeof(string));
                ModelCat.Columns.Add("عدد الإسطوانات", typeof(string));
                ModelCat.Columns.Add("الوزن", typeof(string));
                ModelCat.Columns.Add("الركاب", typeof(string));
                ModelCat.Columns.Add("الشنط الصغيرة", typeof(string));
                ModelCat.Columns.Add("الشنط الكبيرة", typeof(string));
                ModelCat.Columns.Add("عدد الأبواب", typeof(string));
                ModelCat.Columns.Add("الفئة", typeof(string));
                ModelCat.Columns.Add("سنة الصنع", typeof(string));
                ModelCat.Columns.Add("الطراز", typeof(string));
                var Lrecord = db.CR_Mas_Sup_Car_Model_Category.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        
                        ModelCat.Rows.Add(
                            i.CR_Mas_Sup_Car_Model_Category_Payload,
                            i.CR_Mas_Sup_Car_Model_Category_Hourses,
                            i.CR_Mas_Sup_Car_Model_Category_Clinder,
                            i.CR_Mas_Sup_Car_Model_Category_Weight,
                            i.CR_Mas_Sup_Car_Model_Category_Passengers_No, 
                            i.CR_Mas_Sup_Car_Model_Category_Small_Bags, 
                            i.CR_Mas_Sup_Car_Model_Category_Bag_Bags,
                            i.CR_Mas_Sup_Car_Model_Category_Door_No, 
                            i.CR_Mas_Sup_Car_Category_Code, 
                            i.CR_Mas_Sup_Car_Model_Category_Year, 
                            i.CR_Mas_Sup_Car_Model_Category_Code);
                    }
                }

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = ModelCat;
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ModelDataTable.xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return View(db.CR_Mas_Sup_Model.ToList());
        }

        // GET: ModelCategory/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = await db.CR_Mas_Sup_Car_Model_Category.FindAsync(id);
            if (cR_Mas_Sup_Car_Model_Category == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_Car_Model_Category);
        }

        // GET: ModelCategory/Create
        public ActionResult Create()
        {

            //////////////// retrieve category car list switch status////////////
            if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true && AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D" && x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
                if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else if (AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car, "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            // ////////////////////////////////////////////////////////////////////

            //////////////// retrieve Model list switch status////////////
            if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true && AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D" && x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
                if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else if (AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model, "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            ///////////////////////////////////////////////////////////////////////
            CR_Mas_Sup_Car_Model_Category mod = new CR_Mas_Sup_Car_Model_Category();
            mod.CR_Mas_Sup_Car_Model_Category_Status = "A";
            ////ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car, "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            ////ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model, "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            return View(mod);
        }

        // POST: ModelCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Car_Model_Category_Code,CR_Mas_Sup_Car_Model_Category_Year," +
            "CR_Mas_Sup_Car_Category_Code,CR_Mas_Sup_Car_Model_Category_Door_No,CR_Mas_Sup_Car_Model_Category_Bag_Bags," +
            "CR_Mas_Sup_Car_Model_Category_Small_Bags,CR_Mas_Sup_Car_Model_Category_Passengers_No,CR_Mas_Sup_Car_Model_Category_Weight, "+
            "CR_Mas_Sup_Car_Model_Category_Clinder,CR_Mas_Sup_Car_Model_Category_Hourses,CR_Mas_Sup_Car_Model_Category_Payload, "+
            "CR_Mas_Sup_Car_Model_Category_Picture,CR_Mas_Sup_Car_Model_Category_Status,CR_Mas_Sup_Car_Model_Category_Reasons")] 
            CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category , HttpPostedFileBase img,
            string CR_Mas_Sup_Car_Model_Category_Code,int CR_Mas_Sup_Car_Model_Category_Year,string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //ViewBag.b = db.CR_Mas_Sup_Model.FirstOrDefault(m => m.CR_Mas_Sup_Model_Code == CR_Mas_Sup_Car_Model_Category_Code).CR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Ar_Name.ToString();
                    var LrecordExitCatCode = db.CR_Mas_Sup_Car_Model_Category.Any(Lr => Lr.CR_Mas_Sup_Car_Model_Category_Code == CR_Mas_Sup_Car_Model_Category_Code);
                    var LrecordExitYear = db.CR_Mas_Sup_Car_Model_Category.Any(Lr => Lr.CR_Mas_Sup_Car_Model_Category_Year == CR_Mas_Sup_Car_Model_Category_Year);
                    if(cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year<2000 ||
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year > DateTime.Now.Year + 1  ||
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Category_Code==null || LrecordExitYear==true && LrecordExitCatCode==true
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Bag_Bags == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Clinder == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Door_No == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Hourses == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Passengers_No == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Passengers_No == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Payload == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Small_Bags == null
                        || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Weight == null)

                    {
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Bag_Bags == null)
                            ViewBag.LRExistBigBags = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Clinder == null)
                            ViewBag.LRExistCylinder = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Door_No == null)
                            ViewBag.LRExistDoor = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Hourses == null)
                            ViewBag.LRExistHorses = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Passengers_No == null)
                            ViewBag.LRExistPassenger = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Payload == null)
                            ViewBag.LRExistLoad = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Small_Bags == null)
                            ViewBag.LRExistSmallBags = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Weight == null)
                            ViewBag.LRExistWeight = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year < 2000 || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year > DateTime.Now.Year + 1)
                            ViewBag.LRExistyear = "الرجاء التأكد من البيانات";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year.ToString()==null)
                        ViewBag.LRExistyear = "الرجاء إدخال بيانات الحقل";
                        if (LrecordExitCatCode && LrecordExitYear)
                            ViewBag.LRExistAr = "عفوا هذ النوع موجود";
                        //if(cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Picture==null)
                        //    ViewBag.LRExistpic = "الرجاء إدخال بيانات الحقل";
                    }
                    else
                    {
                        string filepath = "";
                        if (img!=null)
                        {
                            filepath = "~/images" + Path.GetFileName(img.FileName);
                            img.SaveAs(HttpContext.Server.MapPath(filepath));
                        }
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Picture = filepath;
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "A";
                        db.CR_Mas_Sup_Car_Model_Category.Add(cR_Mas_Sup_Car_Model_Category);
                        db.SaveChanges();
                        cR_Mas_Sup_Car_Model_Category = new CR_Mas_Sup_Car_Model_Category();
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "A";
                        TempData["TempModel"] = "تم الحفظ بنجاح";
                        return RedirectToAction("Create", "ModelCategory");
                    }

                    
                }
            }
            catch (Exception) { }

            //////////////// retrieve category car list switch status////////////
            if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true && AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D" && x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
                if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else if (AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car, "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            // ////////////////////////////////////////////////////////////////////

            //////////////// retrieve Model list switch status////////////
            if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true && AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D" && x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
                if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else if (AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model, "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            ///////////////////////////////////////////////////////////////////////
            return View(cR_Mas_Sup_Car_Model_Category);
        }

        // GET: ModelCategory/Edit/5
        public ActionResult Edit(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //////CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = await db.CR_Mas_Sup_Car_Model_Category.FindAsync(id);
            CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.FirstOrDefault(Mcat => Mcat.CR_Mas_Sup_Car_Model_Category_Code == id1 && Mcat.CR_Mas_Sup_Car_Category_Code == id2);
            if (cR_Mas_Sup_Car_Model_Category == null)
            {
                return HttpNotFound();
            }

            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "A" || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "1")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
            }
            if ((cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "D" || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
                ViewData["ReadOnly"] = "true";
            }
            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "H" || cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
                ViewData["ReadOnly"] = "true";
            }
            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            ViewBag.delete = cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status;

            //ViewBag.path=cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Picture.ToString();
            ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car, "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model, "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            return View(cR_Mas_Sup_Car_Model_Category);
        }

        // POST: ModelCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_Car_Model_Category_Code," +
            "CR_Mas_Sup_Car_Model_Category_Year,CR_Mas_Sup_Car_Category_Code," +
            "CR_Mas_Sup_Car_Model_Category_Door_No,CR_Mas_Sup_Car_Model_Category_Bag_Bags," +
            "CR_Mas_Sup_Car_Model_Category_Small_Bags,CR_Mas_Sup_Car_Model_Category_Passengers_No," +
            "CR_Mas_Sup_Car_Model_Category_Weight,CR_Mas_Sup_Car_Model_Category_Clinder," +
            "CR_Mas_Sup_Car_Model_Category_Hourses,CR_Mas_Sup_Car_Model_Category_Payload," +
            "CR_Mas_Sup_Car_Model_Category_Picture,CR_Mas_Sup_Car_Model_Category_Status," +
            "CR_Mas_Sup_Car_Model_Category_Reasons")] CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category, HttpPostedFileBase img, string save, string delete, string hold)
        {

            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Year > 2000 && 
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Code != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Bag_Bags != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Clinder != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Door_No != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Hourses != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Passengers_No != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Payload != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Small_Bags != null &&
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Weight != null)
                    {
                        if (img != null)
                        {
                            string filepath = "";
                            if (img.FileName.Length > 0)
                            {
                                filepath = "~/images" + Path.GetFileName(img.FileName);
                                img.SaveAs(HttpContext.Server.MapPath(filepath));
                                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Picture = filepath;
                            }
                        }
                        cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "A";
                        db.Entry(cR_Mas_Sup_Car_Model_Category).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Bag_Bags == null)
                            ViewBag.LRExistBigBags = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Clinder == null)
                            ViewBag.LRExistCylinder = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Door_No == null)
                            ViewBag.LRExistDoor = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Hourses == null)
                            ViewBag.LRExistHorses = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Passengers_No == null)
                            ViewBag.LRExistPassenger = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Payload == null)
                            ViewBag.LRExistLoad = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Small_Bags == null)
                            ViewBag.LRExistSmallBags = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Weight == null)
                            ViewBag.LRExistWeight = "الرجاء إدخال بيانات الحقل";
                        
                    }


                }
            }

            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "D";
                db.Entry(cR_Mas_Sup_Car_Model_Category).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            if (delete == "Activate" || delete == "إسترجاع")
            {
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "A";
                db.Entry(cR_Mas_Sup_Car_Model_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "H";
                db.Entry(cR_Mas_Sup_Car_Model_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status = "A";
                db.Entry(cR_Mas_Sup_Car_Model_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "A" ||
            cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "Activated" ||
            cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "1" ||
            cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
            }
            if ((cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "D" ||
                 cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "Deleted" ||
                 cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
            }
            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "H" ||
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "Hold" ||
                cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
            }
            if (cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            //////////////// retrieve category car list switch status////////////
            if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true && AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D" && x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
                if (AccountController.ST_1503_unhold != true || AccountController.ST_1503_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "H"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else if (AccountController.ST_1503_undelete != true || AccountController.ST_1503_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car.Where(x => x.CR_Mas_Sup_Category_Car_Status != "D"), "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Category_Code = new SelectList(db.CR_Mas_Sup_Category_Car, "CR_Mas_Sup_Category_Car_Code", "CR_Mas_Sup_Category_Car_Ar_Name");
            }
            // ////////////////////////////////////////////////////////////////////

            //////////////// retrieve Model list switch status////////////
            if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true && AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D" && x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
                if (AccountController.ST_1502_unhold != true || AccountController.ST_1502_hold != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "H"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else if (AccountController.ST_1502_undelete != true || AccountController.ST_1502_delete != true)
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model.Where(x => x.CR_Mas_Sup_Model_Status != "D"), "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            else
            {
                ViewBag.CR_Mas_Sup_Car_Model_Category_Code = new SelectList(db.CR_Mas_Sup_Model, "CR_Mas_Sup_Model_Code", "CR_Mas_Sup_Model_Ar_Name");
            }
            ///////////////////////////////////////////////////////////////////////
            ViewBag.delete = cR_Mas_Sup_Car_Model_Category.CR_Mas_Sup_Car_Model_Category_Status;
            return View(cR_Mas_Sup_Car_Model_Category);
        }

        // GET: ModelCategory/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = await db.CR_Mas_Sup_Car_Model_Category.FindAsync(id);
            if (cR_Mas_Sup_Car_Model_Category == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_Car_Model_Category);
        }

        // POST: ModelCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = await db.CR_Mas_Sup_Car_Model_Category.FindAsync(id);
            db.CR_Mas_Sup_Car_Model_Category.Remove(cR_Mas_Sup_Car_Model_Category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
