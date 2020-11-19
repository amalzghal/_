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
    public class FeaturesController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Car_Features
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            return View(db.CR_Mas_Sup_Car_Features.ToList());
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
                var featuresTable = new System.Data.DataTable("teste");

                featuresTable.Columns.Add("المرجع", typeof(string));
                featuresTable.Columns.Add("الحالة", typeof(string));
                featuresTable.Columns.Add("الإسم العربي", typeof(string));
                featuresTable.Columns.Add("الرمز", typeof(string));
                var Lrecord = db.CR_Mas_Sup_Car_Features.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        featuresTable.Rows.Add(i.CR_Mas_Sup_Car_Features_Reasons, i.CR_Mas_Sup_Car_Features_Status,
                            i.CR_Mas_Sup_Car_Features_Ar, i.CR_Mas_Sup_Car_Features_Code);
                    }
                }
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = featuresTable;
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
            return View(db.CR_Mas_Sup_Car_Features.ToList());
        }

        //////// GET: Car_Features/Details/5
        //////public ActionResult Details(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features = db.CR_Mas_Sup_Car_Features.Find(id);
        //////    if (cR_Mas_Sup_Car_Features == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Car_Features);
        //////}
        public CR_Mas_Sup_Car_Features GetLastRecord()
        {
            var Lrecord = db.CR_Mas_Sup_Car_Features.Max(Lr => Lr.CR_Mas_Sup_Car_Features_Code);
            CR_Mas_Sup_Car_Features f = new CR_Mas_Sup_Car_Features();
            if (Lrecord != null)
            {
                int val = int.Parse(Lrecord) + 1;
                f.CR_Mas_Sup_Car_Features_Code = val.ToString();
            }
            else
            {
                f.CR_Mas_Sup_Car_Features_Code = "1001";
            }
            return f;
        }
        // GET: Car_Features/Create
        public ActionResult Create()
        {
            CR_Mas_Sup_Car_Features feature = new CR_Mas_Sup_Car_Features();
            feature = GetLastRecord();
            feature.CR_Mas_Sup_Car_Features_Status = "A";
            return View(feature);
        }

        // POST: Car_Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Car_Features_Code, CR_Mas_Sup_Car_Features_Ar, CR_Mas_Sup_Car_Features_En," +
        "CR_Mas_Sup_Car_Features_Fr, CR_Mas_Sup_Car_Features_Status, CR_Mas_Sup_Car_Features_Reasons")] CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features,
        string CR_Mas_Sup_Car_Features_Ar, string CR_Mas_Sup_Car_Features_Fr, string CR_Mas_Sup_Car_Features_En)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var LrecordExitArabe = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_Ar == CR_Mas_Sup_Car_Features_Ar);
                    var LrecordExitEnglish = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_En == CR_Mas_Sup_Car_Features_En);
                    var LrecordExitFrench = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_Fr == CR_Mas_Sup_Car_Features_Fr);


                    if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En != null &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr != null && !LrecordExitArabe && !LrecordExitEnglish && !LrecordExitFrench &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar.Length >= 3 && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En.Length >= 3 &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr.Length >= 3)
                    {
                        db.CR_Mas_Sup_Car_Features.Add(cR_Mas_Sup_Car_Features);
                        db.SaveChanges();
                        cR_Mas_Sup_Car_Features = new CR_Mas_Sup_Car_Features();
                        cR_Mas_Sup_Car_Features = GetLastRecord();
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "A";
                        return RedirectToAction("Create", "Features");
                    }
                    else
                    {
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar == null)
                            ViewBag.LRExistAr = "عفوا إسم الميزة بالعربي إجباري";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En == null)
                            ViewBag.LRExistEn = "عفوا إسم الميزة بالإنجليزي إجباري";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr == null)
                            ViewBag.LRExistFr = "عفوا إسم الميزة بالفرنسي إجباري";
                        if (LrecordExitArabe)
                            ViewBag.LRExistAr = "عفوا هذه الميزة موجودة";
                        if (LrecordExitEnglish)
                            ViewBag.LRExistEn = "عفوا هذه الميزة موجودة";
                        if (LrecordExitFrench)
                            ViewBag.LRExistFr = "عفوا هذه الميزة موجودة";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr.Length < 3)
                            ViewBag.LRExistFr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                    }
                }
            }
            catch (Exception){}
            return View(cR_Mas_Sup_Car_Features);
        }

        // GET: Car_Features/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features = db.CR_Mas_Sup_Car_Features.Find(id);
            if (cR_Mas_Sup_Car_Features == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "A" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Activated" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "1" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Undeleted")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "D" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Deleted" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "0")
                {
                    ViewBag.stat = "إسترجاع";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "H" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Hold" ||
                    cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                }

                if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status;
            }
            return View(cR_Mas_Sup_Car_Features);
        }

        // POST: Car_Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_Car_Features_Code, CR_Mas_Sup_Car_Features_Ar, CR_Mas_Sup_Car_Features_En," +
            "CR_Mas_Sup_Car_Features_Fr, CR_Mas_Sup_Car_Features_Status, CR_Mas_Sup_Car_Features_Reasons")]
             CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features, string save, string delete, string hold, string CR_Mas_Sup_Car_Features_Ar, 
            string CR_Mas_Sup_Car_Features_En, string CR_Mas_Sup_Car_Features_Fr)
        {
            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    var LrecordExitArabe = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_Ar == CR_Mas_Sup_Car_Features_Ar);
                    var LrecordExitEnglish = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_En == CR_Mas_Sup_Car_Features_En);
                    var LrecordExitFrench = db.CR_Mas_Sup_Car_Features.Any(Lr => Lr.CR_Mas_Sup_Car_Features_Fr == CR_Mas_Sup_Car_Features_Fr);


                    if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En != null &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr != null && !LrecordExitArabe && !LrecordExitEnglish && !LrecordExitFrench &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar.Length >= 3 && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En.Length >= 3 &&
                        cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr.Length >= 3)
                    {
                        db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar == null)
                            ViewBag.LRExistAr = "عفوا إسم الميزة بالعربي إجباري";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En == null)
                            ViewBag.LRExistEn = "عفوا إسم الميزة بالإنجليزي إجباري";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr == null)
                            ViewBag.LRExistFr = "عفوا إسم الميزة بالفرنسي إجباري";
                        if (LrecordExitArabe)
                            ViewBag.LRExistAr = "عفوا هذه الميزة موجودة";
                        if (LrecordExitEnglish)
                            ViewBag.LRExistEn = "عفوا هذه الميزة موجودة";
                        if (LrecordExitFrench)
                            ViewBag.LRExistFr = "عفوا هذه الميزة موجودة";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Ar.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_En.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr != null && cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Fr.Length < 3)
                            ViewBag.LRExistFr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                    }
                }
            }
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "D";
                db.CR_Mas_Sup_Car_Features.Attach(cR_Mas_Sup_Car_Features);
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Reasons).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (delete == "Activate" || delete == "إسترجاع")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "A";
                db.CR_Mas_Sup_Car_Features.Attach(cR_Mas_Sup_Car_Features);
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Reasons).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "H";
                db.CR_Mas_Sup_Car_Features.Attach(cR_Mas_Sup_Car_Features);
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Reasons).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "A";
                db.CR_Mas_Sup_Car_Features.Attach(cR_Mas_Sup_Car_Features);
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Status).IsModified = true;
                db.Entry(cR_Mas_Sup_Car_Features).Property(b => b.CR_Mas_Sup_Car_Features_Reasons).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "A" ||
            cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Activated" ||
            cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "1" ||
            cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
            }
            if ((cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "D" ||
                 cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Deleted" ||
                 cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
            }
            if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "H" ||
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "Hold" ||
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
            }
            if (cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            ViewBag.delete = cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status;
            return View(cR_Mas_Sup_Car_Features);
        }

        //////// GET: Car_Features/Delete/5
        //////public ActionResult Delete(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features = db.CR_Mas_Sup_Car_Features.Find(id);
        //////    if (cR_Mas_Sup_Car_Features == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Car_Features);
        //////}

        //////// POST: Car_Features/Delete/5
        //////[HttpPost, ActionName("Delete")]
        //////[ValidateAntiForgeryToken]
        //////public ActionResult DeleteConfirmed(string id)
        //////{
        //////    CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features = db.CR_Mas_Sup_Car_Features.Find(id);
        //////    db.CR_Mas_Sup_Car_Features.Remove(cR_Mas_Sup_Car_Features);
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