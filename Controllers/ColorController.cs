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
using RentCar.Models;

namespace RentCar.Controllers
{
    public class ColorController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Model
        public ActionResult Index()
        {
            var cR_Mas_Sup_Color = db.CR_Mas_Sup_Color.Include(c => c.CR_Mas_Sup_Group);
            return View(cR_Mas_Sup_Color.ToList());
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
                var Lrecord = db.CR_Mas_Sup_Color.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        colorTable.Rows.Add(i.CR_Mas_Sup_Color_Reasons, i.CR_Mas_Sup_Color_Status, i.CR_Mas_Sup_Color_Group_Code,
                        i.CR_Mas_Sup_Color_Ar_Name, i.CR_Mas_Sup_Color_Code);
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

        //////// GET: Model/Details/5
        //////public ActionResult Details(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Color cR_Mas_Sup_Color = db.CR_Mas_Sup_Color.Find(id);
        //////    if (cR_Mas_Sup_Color == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Color);
        //////}

        // GET: Model/Create
        public ActionResult Create()
        {
            ViewBag.CR_Mas_Sup_Color_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", 
            "CR_Mas_Sup_Group_Ar_Name");

            var Lrecord = db.CR_Mas_Sup_Color.Max(Lr => Lr.CR_Mas_Sup_Color_Code);
            CR_Mas_Sup_Color b = new CR_Mas_Sup_Color();
            if (Lrecord != null)
            {
                double val = double.Parse(Lrecord) + 1;
                b.CR_Mas_Sup_Color_Code = val.ToString();
            }
            else
            {
                b.CR_Mas_Sup_Color_Code = "3100000001";
            }
            b.CR_Mas_Sup_Color_Status = "A";
            return View(b);

        }

        // POST: Model/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Color_Code, CR_Mas_Sup_Color_Group_Code, CR_Mas_Sup_Color_Ar_Name, " +
        "CR_Mas_Sup_Color_En_Name, CR_Mas_Sup_Color_Fr_Name, CR_Mas_Sup_Color_Status, CR_Mas_Sup_Color_Reasons")] 
        CR_Mas_Sup_Color cR_Mas_Sup_Color)
        {
            if (ModelState.IsValid)
            {
                db.CR_Mas_Sup_Color.Add(cR_Mas_Sup_Color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CR_Mas_Sup_Color_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", 
                "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Color.CR_Mas_Sup_Color_Group_Code);
            //ViewBag.CR_Mas_Sup_Color_Group_Code = "31";
            return View(cR_Mas_Sup_Color);
        }

        // GET: Model/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Color cR_Mas_Sup_Color = db.CR_Mas_Sup_Color.Find(id);
            if (cR_Mas_Sup_Color == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "A" || cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "1")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }

                if ((cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "D" || cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "0"))
                {
                    ViewBag.stat = "تفعيل";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "H" || cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                }

                if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status;
            }

            ViewBag.CR_Mas_Sup_Color_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", 
            "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Color.CR_Mas_Sup_Color_Group_Code);
            return View(cR_Mas_Sup_Color);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_Color_Code, CR_Mas_Sup_Color_Group_Code, CR_Mas_Sup_Color_Ar_Name, " +
        "CR_Mas_Sup_Color_En_Name, CR_Mas_Sup_Color_Fr_Name, CR_Mas_Sup_Color_Status, CR_Mas_Sup_Color_Reasons")] 
        CR_Mas_Sup_Color cR_Mas_Sup_Color, string save, string delete, string hold)
        {
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status = "D";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Color).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (delete == "Activate" || delete == "تفعيل")
            {
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Color).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status = "H";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Color).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status = "A";
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Color).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Color).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CR_Mas_Sup_Color_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_Color.CR_Mas_Sup_Color_Group_Code);
            if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "A" ||
            cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "Activated" ||
            cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "1" ||
            cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "A";
            }

            if ((cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "D" ||
                 cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "Deleted" ||
                 cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "0"))
            {
                ViewBag.stat = "تفعيل";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "D";
            }

            if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "H" ||
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "Hold" ||
                cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
                ViewBag.delete = "H";
            }

            if (cR_Mas_Sup_Color.CR_Mas_Sup_Color_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            return View(cR_Mas_Sup_Color);
        }

        //////// GET: Model/Delete/5
        //////public ActionResult Delete(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Color cR_Mas_Sup_Color = db.CR_Mas_Sup_Color.Find(id);
        //////    if (cR_Mas_Sup_Color == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Color);
        //////}

        //////// POST: Model/Delete/5
        //////[HttpPost, ActionName("Delete")]
        //////[ValidateAntiForgeryToken]
        //////public ActionResult DeleteConfirmed(string id)
        //////{
        //////    CR_Mas_Sup_Color cR_Mas_Sup_Color = db.CR_Mas_Sup_Color.Find(id);
        //////    db.CR_Mas_Sup_Color.Remove(cR_Mas_Sup_Color);
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