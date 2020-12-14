using System;
using System.Collections.Generic;
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

namespace RentCar.Controllers
{
    public class AdditionalController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Additional
        public async Task<ActionResult> Index()
        {
            if (AccountController.ST_1507_unhold != true || AccountController.ST_1507_hold != true && AccountController.ST_1507_undelete != true || AccountController.ST_1507_delete != true)
            {
                var cR_Mas_Sup_Additional = db.CR_Mas_Sup_Additional.Include(c => c.CR_Mas_Sup_Group).Where(x => x.CR_Mas_Sup_Additional_Status != "D" && x.CR_Mas_Sup_Additional_Status != "H");
                return View(cR_Mas_Sup_Additional.ToList());
            }
            else
                if (AccountController.ST_1507_unhold != true || AccountController.ST_1507_hold != true)
            {
                var cR_Mas_Sup_Additional = db.CR_Mas_Sup_Additional.Include(c => c.CR_Mas_Sup_Group).Where(x => x.CR_Mas_Sup_Additional_Status != "H");
                return View(cR_Mas_Sup_Additional.ToList());
            }
            else if (AccountController.ST_1507_undelete != true || AccountController.ST_1507_delete != true)
            {
                var cR_Mas_Sup_Additional = db.CR_Mas_Sup_Additional.Include(c => c.CR_Mas_Sup_Group).Where(x => x.CR_Mas_Sup_Additional_Status != "D");
                return View(cR_Mas_Sup_Additional.ToList());
            }
            else
            {
                var cR_Mas_Sup_Additional = db.CR_Mas_Sup_Additional.Include(c => c.CR_Mas_Sup_Group);
                return View(await cR_Mas_Sup_Additional.ToListAsync());
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
                var colorTable = new System.Data.DataTable("teste");

                colorTable.Columns.Add("المرجع", typeof(string));
                colorTable.Columns.Add("الحالة", typeof(string));
                colorTable.Columns.Add("المجموعة", typeof(string));
                colorTable.Columns.Add("الإسم", typeof(string));
                colorTable.Columns.Add("الرمز", typeof(string));
                var Lrecord = db.CR_Mas_Sup_Additional.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        colorTable.Rows.Add(i.CR_Mas_Sup_Additional_Reasons, i.CR_Mas_Sup_Additional_Status, i.CR_Mas_Sup_Additional_Group_Code,
                        i.CR_Mas_Sup_Additional_Ar_Name, i.CR_Mas_Sup_Additional_Code);
                    }
                }
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = colorTable;
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
            return View(db.CR_Mas_Sup_Color.ToList());
        }

        // GET: Additional/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Additional cR_Mas_Sup_Additional = await db.CR_Mas_Sup_Additional.FindAsync(id);
            if (cR_Mas_Sup_Additional == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_Additional);
        }


        public CR_Mas_Sup_Additional GetLastRecord()
        {
            var Lrecord = db.CR_Mas_Sup_Additional.Max(Lr => Lr.CR_Mas_Sup_Additional_Code);
            CR_Mas_Sup_Additional A = new CR_Mas_Sup_Additional();
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                A.CR_Mas_Sup_Additional_Code = val.ToString();
            }
            else
            {
                A.CR_Mas_Sup_Additional_Code = "3500000001";
            }
            return A;
        }

        // GET: Additional/Create
        public ActionResult Create()
        {
            ViewBag.CR_Mas_Sup_Additional_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name");
            CR_Mas_Sup_Additional additional = new CR_Mas_Sup_Additional();
            additional = GetLastRecord();
            additional.CR_Mas_Sup_Additional_Status = "A";
            return View(additional);
        }
        // POST: Additional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CR_Mas_Sup_Additional_Code,CR_Mas_Sup_Additional_Group_Code, "+
            "CR_Mas_Sup_Additional_Ar_Name,CR_Mas_Sup_Additional_En_Name,CR_Mas_Sup_Additional_Fr_Name, "+
            "CR_Mas_Sup_Additional_Status,CR_Mas_Sup_Additional_Reasons")] CR_Mas_Sup_Additional cR_Mas_Sup_Additional,
            string CR_Mas_Sup_Additional_Ar_Name,string CR_Mas_Sup_Additional_En_Name, string CR_Mas_Sup_Additional_Fr_Name)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    var LrecordExitArabe = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_Ar_Name == CR_Mas_Sup_Additional_Ar_Name);
                    var LrecordExitEnglish = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_En_Name == CR_Mas_Sup_Additional_En_Name);
                    var LrecordExitFrench = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_Fr_Name == CR_Mas_Sup_Additional_Fr_Name);


                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name != null &&
                            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name != null && !LrecordExitArabe && !LrecordExitEnglish && !LrecordExitFrench &&
                            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name.Length >= 3 && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name.Length >= 3 &&
                            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name.Length >= 3)
                    {
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Code = GetLastRecord().CR_Mas_Sup_Additional_Code;
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Group_Code = "35";
                        db.CR_Mas_Sup_Additional.Add(cR_Mas_Sup_Additional);
                        await db.SaveChangesAsync();
                        cR_Mas_Sup_Additional = new CR_Mas_Sup_Additional();
                        cR_Mas_Sup_Additional = GetLastRecord();
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status = "A";
                        TempData["TempModel"] = "تم الحفظ بنجاح";
                        return RedirectToAction("Create", "Additional");
                    }
                    else
                    {
                        if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name == null)
                            ViewBag.LRExistAr = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name == null)
                            ViewBag.LRExistEn = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name == null)
                            ViewBag.LRExistFr = "الرجاء إدخال بيانات الحقل";
                        if (LrecordExitArabe)
                            ViewBag.LRExistAr = "عفوا هذالإضافة موجودة";
                        if (LrecordExitEnglish)
                            ViewBag.LRExistEn = "عفوا هذالإضافة موجودة";
                        if (LrecordExitFrench)
                            ViewBag.LRExistFr = "عفوا هذالإضافة موجودة";
                        if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name.Length < 3)
                            ViewBag.LRExistAr = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                        if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name.Length < 3)
                            ViewBag.LRExistEn = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name.Length < 3)
                            ViewBag.LRExistFr = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                }

                }
            //}
            //catch (Exception) { }

            ViewBag.CR_Mas_Sup_Additional_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Group_Code);
            return View(cR_Mas_Sup_Additional);
        }

        // GET: Additional/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Additional cR_Mas_Sup_Additional = await db.CR_Mas_Sup_Additional.FindAsync(id);
            if (cR_Mas_Sup_Additional == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "A" || cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "1")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }

                if ((cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "D" || cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "0"))
                {
                    ViewBag.stat = "إسترجاع";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "H" || cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                }

                if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status;
            }
            ViewBag.CR_Mas_Sup_Additional_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Group_Code);
            return View(cR_Mas_Sup_Additional);
        }

        // POST: Additional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CR_Mas_Sup_Additional_Code,CR_Mas_Sup_Additional_Group_Code, "+
            "CR_Mas_Sup_Additional_Ar_Name,CR_Mas_Sup_Additional_En_Name,CR_Mas_Sup_Additional_Fr_Name,CR_Mas_Sup_Additional_Status, "+
            "CR_Mas_Sup_Additional_Reasons")] CR_Mas_Sup_Additional cR_Mas_Sup_Additional, string save, string delete, string hold, string CR_Mas_Sup_Additional_Ar_Name,
            string CR_Mas_Sup_Additional_Fr_Name, string CR_Mas_Sup_Additional_En_Name)
        {
            if (ModelState.IsValid)
            {
                var LrecordExitArabe = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_Ar_Name == CR_Mas_Sup_Additional_Ar_Name);
                var LrecordExitEnglish = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_En_Name == CR_Mas_Sup_Additional_En_Name);
                var LrecordExitFrench = db.CR_Mas_Sup_Additional.Any(Lr => Lr.CR_Mas_Sup_Additional_Fr_Name == CR_Mas_Sup_Additional_Fr_Name);

                if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name != null &&
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name != null && !LrecordExitArabe && !LrecordExitEnglish && !LrecordExitFrench &&
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name.Length >= 3 && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name.Length >= 3 &&
                        cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name.Length >= 3)
                {
                    db.Entry(cR_Mas_Sup_Additional).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name == null)
                        ViewBag.LRExistAr = "الرجاء إدخال بيانات الحقل";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name == null)
                        ViewBag.LRExistEn = "الرجاء إدخال بيانات الحقل";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name == null)
                        ViewBag.LRExistFr = "الرجاء إدخال بيانات الحقل";
                    if (LrecordExitArabe)
                        ViewBag.LRExistAr = "عفوا هذالإضافة موجودة";
                    if (LrecordExitEnglish)
                        ViewBag.LRExistEn = "عفوا هذالإضافة موجودة";
                    if (LrecordExitFrench)
                        ViewBag.LRExistFr = "عفوا هذالإضافة موجودة";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Ar_Name.Length < 3)
                        ViewBag.LRExistAr = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_En_Name.Length < 3)
                        ViewBag.LRExistEn = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                    if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name != null && cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Fr_Name.Length < 3)
                        ViewBag.LRExistFr = "عفوا الحد الأدنى لطول هذا الحقل هو 3 حروف";
                }
            }
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status = "D";
                db.CR_Mas_Sup_Additional.Attach(cR_Mas_Sup_Additional);
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (delete == "Activate" || delete == "إسترجاع")
            {
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status = "A";
                db.CR_Mas_Sup_Additional.Attach(cR_Mas_Sup_Additional);
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status = "H";
                db.CR_Mas_Sup_Additional.Attach(cR_Mas_Sup_Additional);
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status = "A";
                db.CR_Mas_Sup_Additional.Attach(cR_Mas_Sup_Additional);
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Additional).Property(b => b.CR_Mas_Sup_Additional_Status).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "A" ||
            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "Activated" ||
            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "1" ||
            cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
            }

            if ((cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "D" ||
                 cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "Deleted" ||
                 cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
            }

            if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "H" ||
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "Hold" ||
                cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
            }

            if (cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }

            ViewBag.CR_Mas_Sup_Additional_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Additional.CR_Mas_Sup_Additional_Group_Code);
            return View(cR_Mas_Sup_Additional);
        }

        // GET: Additional/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Additional cR_Mas_Sup_Additional = await db.CR_Mas_Sup_Additional.FindAsync(id);
            if (cR_Mas_Sup_Additional == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_Additional);
        }

        // POST: Additional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CR_Mas_Sup_Additional cR_Mas_Sup_Additional = await db.CR_Mas_Sup_Additional.FindAsync(id);
            db.CR_Mas_Sup_Additional.Remove(cR_Mas_Sup_Additional);
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
