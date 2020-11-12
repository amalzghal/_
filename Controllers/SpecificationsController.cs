using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class SpecificationsController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Car_Specifications
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            return View(db.CR_Mas_Sup_Car_Specifications.ToList());
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post(String lang, String excelCall)
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
                var specificationsTable = new System.Data.DataTable("teste");

                specificationsTable.Columns.Add("المرجع", typeof(string));
                specificationsTable.Columns.Add("الحالة", typeof(string));
                specificationsTable.Columns.Add("الإسم العربي", typeof(string));
                specificationsTable.Columns.Add("الرمز", typeof(string));
                var Lrecord = db.CR_Mas_Sup_Car_Specifications.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        specificationsTable.Rows.Add(i.CR_Mas_Sup_Car_Specifications_Reasons, i.CR_Mas_Sup_Car_Specifications_Status, 
                            i.CR_Mas_Sup_Car_Specifications_Ar, i.CR_Mas_Sup_Car_Specifications_Code);
                    }
                }

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = specificationsTable;
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return View(db.CR_Mas_Sup_Car_Specifications.ToList());
        }

        //////// GET: Car_Specifications/Details/5
        //////public ActionResult Details(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Car_Specifications cR_Mas_Sup_Car_Specifications = db.CR_Mas_Sup_Car_Specifications.Find(id);
        //////    if (cR_Mas_Sup_Car_Specifications == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Car_Specifications);
        //////}
        public CR_Mas_Sup_Car_Specifications GetLastRecord()
        {
            var Lrecord = db.CR_Mas_Sup_Car_Specifications.Max(Lr => Lr.CR_Mas_Sup_Car_Specifications_Code);
            CR_Mas_Sup_Car_Specifications s = new CR_Mas_Sup_Car_Specifications();
            if (Lrecord != null)
            {
                int val = int.Parse(Lrecord) + 1;
                s.CR_Mas_Sup_Car_Specifications_Code = val.ToString();
            }
            else
            {
                s.CR_Mas_Sup_Car_Specifications_Code = "1001";
            }
            return s;
        }
        // GET: Car_Specifications/Create
        public ActionResult Create()
        {
            CR_Mas_Sup_Car_Specifications spec = new CR_Mas_Sup_Car_Specifications();
            spec = GetLastRecord();
            spec.CR_Mas_Sup_Car_Specifications_Status = "A";
            return View(spec);
        }

        // POST: Car_Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Car_Specifications_Code, CR_Mas_Sup_Car_Specifications_Ar, CR_Mas_Sup_Car_Specifications_En," +
        "CR_Mas_Sup_Car_Specifications_Fr, CR_Mas_Sup_Car_Specifications_Status, CR_Mas_Sup_Car_Specifications_Reasons")] CR_Mas_Sup_Car_Specifications 
        cR_Mas_Sup_Car_Specifications, string CR_Mas_Sup_Car_Specifications_Ar, string CR_Mas_Sup_Car_Specifications_Fr, string CR_Mas_Sup_Car_Specifications_En)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var LrecordExitArabe = db.CR_Mas_Sup_Car_Specifications.Any(Lr => Lr.CR_Mas_Sup_Car_Specifications_Ar == CR_Mas_Sup_Car_Specifications_Ar);
                    var LrecordExitEnglish = db.CR_Mas_Sup_Car_Specifications.Any(Lr => Lr.CR_Mas_Sup_Car_Specifications_En == CR_Mas_Sup_Car_Specifications_En);
                    var LrecordExitFrench = db.CR_Mas_Sup_Car_Specifications.Any(Lr => Lr.CR_Mas_Sup_Car_Specifications_Fr == CR_Mas_Sup_Car_Specifications_Fr);


                    if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Ar != null && cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_En != null &&
                        cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Fr != null && !LrecordExitArabe && !LrecordExitEnglish && !LrecordExitFrench &&
                        cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Ar.Length > 3 && cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_En.Length > 3 &&
                        cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Fr.Length > 3)
                    {
                        db.CR_Mas_Sup_Car_Specifications.Add(cR_Mas_Sup_Car_Specifications);
                        cR_Mas_Sup_Car_Specifications = new CR_Mas_Sup_Car_Specifications();
                        cR_Mas_Sup_Car_Specifications = GetLastRecord();
                        cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status = "A";
                        db.SaveChanges();
                        return RedirectToAction("Create", "Specifications");
                    }
                    else
                    {
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Ar == null)
                            ViewBag.LRExistAr = "عفوا إسم المواصفة بالعربي إجباري";
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_En == null)
                            ViewBag.LRExistEn = "عفوا إسم المواصفة بالإنجليزي إجباري";
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Fr == null)
                            ViewBag.LRExistFr = "عفوا إسم المواصفة بالفرنسي إجباري";
                        if (LrecordExitArabe)
                            ViewBag.LRExistAr = "عفوا هذه المواصفة موجودة";
                        if (LrecordExitEnglish)
                            ViewBag.LRExistEn = "عفوا هذه المواصفة موجودة";
                        if (LrecordExitFrench)
                            ViewBag.LRExistFr = "عفوا هذه المواصفة موجودة";
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Ar != null && cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Ar.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 50 حرفًا";
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_En != null && cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_En.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 50 حرفًا";
                        if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Fr != null && cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Fr.Length < 3)
                            ViewBag.LRExistFr = "عفوا الاسم يحتوي على ما بين 3 و 50 حرفًا";
                    }
                }
            }
            catch (Exception ex) { }
            return View(cR_Mas_Sup_Car_Specifications);
        }

        // GET: Car_Specifications/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Car_Specifications cR_Mas_Sup_Car_Specifications = db.CR_Mas_Sup_Car_Specifications.Find(id);
            if (cR_Mas_Sup_Car_Specifications == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "A" || 
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Activated" || 
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "1" || 
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Undeleted")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }
                if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "D" ||
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Deleted" ||
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "0")
                {
                    ViewBag.stat = "إسترجاع";
                    ViewBag.h = "تعطيل";
                }
                if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "H" ||
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Hold" || 
                    cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                }
                if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status;
            }
            return View(cR_Mas_Sup_Car_Specifications);
        }

        // POST: Car_Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_Car_Specifications_Code, CR_Mas_Sup_Car_Specifications_Ar, CR_Mas_Sup_Car_Specifications_En," +
            "CR_Mas_Sup_Car_Specifications_Fr, CR_Mas_Sup_Car_Specifications_Status, CR_Mas_Sup_Car_Specifications_Reasons")] 
             CR_Mas_Sup_Car_Specifications cR_Mas_Sup_Car_Specifications, string save, string delete, string hold)
        {
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status = "D";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Specifications).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (delete == "Activate" || delete == "إسترجاع")
            {
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Specifications).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status = "H";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Specifications).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Specifications).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Specifications).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "A" ||
            cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Activated" ||
            cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "1" ||
            cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "A";
            }
            if ((cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "D" ||
                 cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Deleted" ||
                 cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "D";
            }
            if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "H" ||
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "Hold" ||
                cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
                ViewBag.delete = "H";
            }
            if (cR_Mas_Sup_Car_Specifications.CR_Mas_Sup_Car_Specifications_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            return View(cR_Mas_Sup_Car_Specifications);
        }

        //////// GET: Car_Specifications/Delete/5
        //////public ActionResult Delete(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Car_Specifications cR_Mas_Sup_Car_Specifications = db.CR_Mas_Sup_Car_Specifications.Find(id);
        //////    if (cR_Mas_Sup_Car_Specifications == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Car_Specifications);
        //////}

        //////// POST: Car_Specifications/Delete/5
        //////[HttpPost, ActionName("Delete")]
        //////[ValidateAntiForgeryToken]
        //////public ActionResult DeleteConfirmed(string id)
        //////{
        //////    CR_Mas_Sup_Car_Specifications cR_Mas_Sup_Car_Specifications = db.CR_Mas_Sup_Car_Specifications.Find(id);
        //////    db.CR_Mas_Sup_Car_Specifications.Remove(cR_Mas_Sup_Car_Specifications);
        //////    db.SaveChanges();
        //////    return RedirectToAction("Index");
        //////}

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