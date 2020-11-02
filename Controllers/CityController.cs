using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class CityController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: City
        public ActionResult Index()
        {
            var cR_Mas_Sup_City = db.CR_Mas_Sup_City.Include(c => c.CR_Mas_Sup_Regions).Include(c => c.CR_Mas_Sup_Group);
            return View(cR_Mas_Sup_City.ToList());
        }

        // GET: City/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_City cR_Mas_Sup_City = db.CR_Mas_Sup_City.Find(id);
            if (cR_Mas_Sup_City == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_City);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            ViewBag.CR_Mas_Sup_City_Regions_Code = new SelectList(db.CR_Mas_Sup_Regions, "CR_Mas_Sup_Regions_Code", "CR_Mas_Sup_Regions_Ar_Name");
            ViewBag.CR_Mas_Sup_City_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Sup_City_Code,CR_Mas_Sup_City_Group_Code,CR_Mas_Sup_City_Ar_Name,CR_Mas_Sup_City_En_Name,CR_Mas_Sup_City_Fr_Name,CR_Mas_Sup_City_Status,CR_Mas_Sup_City_Reasons,CR_Mas_Sup_City_Regions_Code")] CR_Mas_Sup_City cR_Mas_Sup_City)
        {
            if (ModelState.IsValid)
            {
                db.CR_Mas_Sup_City.Add(cR_Mas_Sup_City);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CR_Mas_Sup_City_Regions_Code = new SelectList(db.CR_Mas_Sup_Regions, "CR_Mas_Sup_Regions_Code", "CR_Mas_Sup_Regions_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Regions_Code);
            ViewBag.CR_Mas_Sup_City_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Group_Code);
            return View(cR_Mas_Sup_City);
        }

        // GET: City/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_City cR_Mas_Sup_City = db.CR_Mas_Sup_City.Find(id);
            if (cR_Mas_Sup_City == null)
            {
                return HttpNotFound();
            }
            ViewBag.CR_Mas_Sup_City_Regions_Code = new SelectList(db.CR_Mas_Sup_Regions, "CR_Mas_Sup_Regions_Code", "CR_Mas_Sup_Regions_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Regions_Code);
            ViewBag.CR_Mas_Sup_City_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Group_Code);
            return View(cR_Mas_Sup_City);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Sup_City_Code,CR_Mas_Sup_City_Group_Code,CR_Mas_Sup_City_Ar_Name,CR_Mas_Sup_City_En_Name,CR_Mas_Sup_City_Fr_Name,CR_Mas_Sup_City_Status,CR_Mas_Sup_City_Reasons,CR_Mas_Sup_City_Regions_Code")] CR_Mas_Sup_City cR_Mas_Sup_City)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cR_Mas_Sup_City).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CR_Mas_Sup_City_Regions_Code = new SelectList(db.CR_Mas_Sup_Regions, "CR_Mas_Sup_Regions_Code", "CR_Mas_Sup_Regions_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Regions_Code);
            ViewBag.CR_Mas_Sup_City_Group_Code = new SelectList(db.CR_Mas_Sup_Group, "CR_Mas_Sup_Group_Code", "CR_Mas_Sup_Group_Ar_Name", cR_Mas_Sup_City.CR_Mas_Sup_City_Group_Code);
            return View(cR_Mas_Sup_City);
        }

        // GET: City/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Sup_City cR_Mas_Sup_City = db.CR_Mas_Sup_City.Find(id);
            if (cR_Mas_Sup_City == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_Sup_City);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CR_Mas_Sup_City cR_Mas_Sup_City = db.CR_Mas_Sup_City.Find(id);
            db.CR_Mas_Sup_City.Remove(cR_Mas_Sup_City);
            db.SaveChanges();
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
