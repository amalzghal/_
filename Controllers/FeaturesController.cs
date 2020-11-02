using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
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

        // GET: Car_Features/Create
        public ActionResult Create()
        {
            var Lrecord = db.CR_Mas_Sup_Car_Features.Max(Lr => Lr.CR_Mas_Sup_Car_Features_Code);
            CR_Mas_Sup_Car_Features ft = new CR_Mas_Sup_Car_Features();
            if (Lrecord != null)
            {
                int val = int.Parse(Lrecord) + 1;
                ft.CR_Mas_Sup_Car_Features_Code = val.ToString();
            }
            else
            {
                ft.CR_Mas_Sup_Car_Features_Code = "1001";
            }
            ft.CR_Mas_Sup_Car_Features_Status = "A";
            return View(ft);
        }

        // POST: Car_Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Car_Features_Code, CR_Mas_Sup_Car_Features_Ar, CR_Mas_Sup_Car_Features_En," +
            "CR_Mas_Sup_Car_Features_Fr, CR_Mas_Sup_Car_Features_Status, CR_Mas_Sup_Car_Features_Reasons")]
            CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features)
        {
            if (ModelState.IsValid)
            {
                db.CR_Mas_Sup_Car_Features.Add(cR_Mas_Sup_Car_Features);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
                    ViewBag.stat = "تفعيل";
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
             CR_Mas_Sup_Car_Features cR_Mas_Sup_Car_Features, string save, string delete, string hold)
        {
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "D";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (delete == "Activate" || delete == "تفعيل")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "H";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Car_Features.CR_Mas_Sup_Car_Features_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Car_Features).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
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