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
    public class BrandController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Brand
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            return View(db.CR_Mas_Sup_Brand.ToList());
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post(String lang,String excelCall)
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
                var brandTable = new System.Data.DataTable("teste");

                brandTable.Columns.Add("المرجع", typeof(string));
                brandTable.Columns.Add("الحالة", typeof(string));
                brandTable.Columns.Add("الإسم العربي", typeof(string));
                brandTable.Columns.Add("الرمز", typeof(string));
                var Lrecord = db.CR_Mas_Sup_Brand.ToList();


                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        brandTable.Rows.Add(i.CR_Mas_Sup_Brand_Reasons,i.CR_Mas_Sup_Brand_Status,i.CR_Mas_Sup_Brand_Ar_Name,i.CR_Mas_Sup_Brand_Code);
                    }
                }

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = brandTable;
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

            return View(db.CR_Mas_Sup_Brand.ToList());
        }

        // GET: Brand/Details/5
        //////public ActionResult Details(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Brand cR_Mas_Sup_Brand = db.CR_Mas_Sup_Brand.Find(id);
        //////    if (cR_Mas_Sup_Brand == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Brand);
        //////}

        // GET: Brand/Create
        public ActionResult Create()
        {
            var Lrecord = db.CR_Mas_Sup_Brand.Max(Lr => Lr.CR_Mas_Sup_Brand_Code);
            CR_Mas_Sup_Brand b = new CR_Mas_Sup_Brand();
            if (Lrecord!=null)
            {
                int val = int.Parse(Lrecord) + 1;
                b.CR_Mas_Sup_Brand_Code = val.ToString();
            }else
            {
                b.CR_Mas_Sup_Brand_Code = "1001";
            }
                b.CR_Mas_Sup_Brand_Status = "A";

            return View(b);
        }

        // POST: Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_Brand_Code,CR_Mas_Sup_Brand_Ar_Name,CR_Mas_Sup_Brand_En_Name,CR_Mas_Sup_Brand_Fr_Name,CR_Mas_Sup_Brand_Status,CR_Mas_Sup_Brand_Reasons")] CR_Mas_Sup_Brand cR_Mas_Sup_Brand)
        {
            if (ModelState.IsValid)
            {
                db.CR_Mas_Sup_Brand.Add(cR_Mas_Sup_Brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cR_Mas_Sup_Brand);
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_Brand cR_Mas_Sup_Brand = db.CR_Mas_Sup_Brand.Find(id);
            if (cR_Mas_Sup_Brand == null)
            {
                return HttpNotFound();
            }else
            {
                if (cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status=="A" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "Activated" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "1" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "Undeleted")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }

                if ((cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "D" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "Deleted" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "0"))
                {
                    ViewBag.stat = "تفعيل";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "H" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "Hold" || cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                }

                if (cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status;
                

            }
            return View(cR_Mas_Sup_Brand);
        }

        // POST: Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_Brand_Code,CR_Mas_Sup_Brand_Ar_Name,CR_Mas_Sup_Brand_En_Name,CR_Mas_Sup_Brand_Fr_Name,CR_Mas_Sup_Brand_Status,CR_Mas_Sup_Brand_Reasons")] CR_Mas_Sup_Brand cR_Mas_Sup_Brand,string save, string delete, string hold)
        {
            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cR_Mas_Sup_Brand).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cR_Mas_Sup_Brand);
            }

           
            if (delete=="Delete" || delete=="حذف")
            {
                cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status = "D";
                ViewBag.stat = "تفعيل";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "D";
            }


            if (delete == "Activate" || delete == "تفعيل")
            {
                cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status = "A";
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
                ViewBag.delete = "A";
            }



            if (hold == "تعطيل"|| hold == "hold")
            {      
                ViewBag.h = "تنشيط";
                cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status = "H";
                ViewBag.delete = "H";
                ViewBag.stat = "حذف";
            }
        

            if (hold == "تنشيط" || hold == "Activate")
            {
                ViewBag.h = "تعطيل";
                cR_Mas_Sup_Brand.CR_Mas_Sup_Brand_Status = "A";
                ViewBag.delete = "A";
                ViewBag.stat = "حذف";
            }
           

            return View(cR_Mas_Sup_Brand);

        }



        // GET: Brand/Delete/5
        //////public ActionResult Delete(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Sup_Brand cR_Mas_Sup_Brand = db.CR_Mas_Sup_Brand.Find(id);
        //////    if (cR_Mas_Sup_Brand == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Sup_Brand);
        //////}

        //////// POST: Brand/Delete/5
        //////[HttpPost, ActionName("Delete")]
        //////[ValidateAntiForgeryToken]
        //////public ActionResult DeleteConfirmed(string id)
        //////{
        //////    CR_Mas_Sup_Brand cR_Mas_Sup_Brand = db.CR_Mas_Sup_Brand.Find(id);
        //////    db.CR_Mas_Sup_Brand.Remove(cR_Mas_Sup_Brand);
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
