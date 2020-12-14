using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class SubValidationController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: SubValidation
        public ActionResult Index()
        {
            var cR_Mas_User_Sub_Validation = db.CR_Mas_User_Sub_Validation.Include(c => c.CR_Mas_User_Information);
            return View(cR_Mas_User_Sub_Validation.ToList());
        }

        // GET: SubValidation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation = db.CR_Mas_User_Sub_Validation.Find(id);
            if (cR_Mas_User_Sub_Validation == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_User_Sub_Validation);
        }

        // GET: SubValidation/Create
        public ActionResult Create()
        {
            ViewBag.CR_Mas_User_Sub_Validation_Code = new SelectList(db.CR_Mas_User_Information, "CR_Mas_User_Information_Code", "CR_Mas_User_Information_Ar_Name");
            ViewBag.CR_Mas_Sup_System_Code = new SelectList(db.CR_Mas_Sys_System_Name, "CR_Mas_Sys_System_Code", "CR_Mas_Sys_System_Ar_Name");
            return View();
        }


        public JsonResult GetTaskList(string SystemCode)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CR_Mas_Sys_Tasks> SystemTask = db.CR_Mas_Sys_Tasks.Where(x => x.CR_Mas_Sys_System_Code == SystemCode).ToList();
            return Json(SystemTask,JsonRequestBehavior.AllowGet);
        }

        // POST: SubValidation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_User_Sub_Validation_Code,CR_Mas_User_Sub_Validation_Tasks_Code," +
            "CR_Mas_User_Sub_Validation_Insert,CR_Mas_User_Sub_Validation_UpDate,CR_Mas_User_Sub_Validation_Delete," +
            "CR_Mas_User_Sub_Validation_UnDelete,CR_Mas_User_Sub_Validation_Hold,CR_Mas_User_Sub_Validation_UnHold," +
            "CR_Mas_User_Sub_Validation_Print")] CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation, bool CR_Mas_User_Main_Validation)
        {
            
            
                if (ModelState.IsValid)
                {
                    var CheckExist = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code &&
                    x.CR_Mas_User_Sub_Validation_Tasks_Code == cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Tasks_Code);

                    if (CheckExist == null && cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code != null && cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Tasks_Code != null)
                    {

                    CR_Mas_User_Main_Validation mainval = new CR_Mas_User_Main_Validation();
                    mainval.CR_Mas_User_Main_Validation1 = CR_Mas_User_Main_Validation;
                    mainval.CR_Mas_User_Main_Validation_Code = cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code;
                    mainval.CR_Mas_User_Main_Validation_Tasks_Code = cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Tasks_Code;
                    db.CR_Mas_User_Main_Validation.Add(mainval);


                    db.CR_Mas_User_Sub_Validation.Add(cR_Mas_User_Sub_Validation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    }
                    else
                    {
                        if (CheckExist!=null)
                        ViewBag.exist = "هذا المستخدم له صلوحية محددة لهذه الشاشة ,الرجاء تعديلها";
                        if (cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code == null)
                        {
                            ViewBag.UserInfo = "رجاء تختار المستخدم";
                        }
                        if (cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Tasks_Code == null)
                        {
                            ViewBag.task = "رجاء تختار الشاشة";
                        }

                    }

                    

                }




            ViewBag.CR_Mas_User_Sub_Validation_Code = new SelectList(db.CR_Mas_User_Information, "CR_Mas_User_Information_Code", "CR_Mas_User_Information_Ar_Name");
            ViewBag.CR_Mas_Sup_System_Code = new SelectList(db.CR_Mas_Sys_System_Name, "CR_Mas_Sys_System_Code", "CR_Mas_Sys_System_Ar_Name");
            return View(cR_Mas_User_Sub_Validation);
        }


        ////amir test ajax
        //public IHtmlString List_select(string value)
        //{
        //    string htmlcode = "<label>list</label>" +
        //        "             < div class='col-md-12'>"+
        //        "@Html.DropDownList('CR_Mas_Sup_Task_Code', null, htmlAttributes: new { @class = 'input_form col-md-4', style = 'float:right' })"+
        //   " </ div >";
        //    return new HtmlString(htmlcode);
        //}

        // GET: SubValidation/Edit/5
        public ActionResult Edit(string id1, string id2)
        {
            if (id1 == null && id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code==id1 && x.CR_Mas_User_Sub_Validation_Tasks_Code==id2);
            //CR_Mas_Sup_Car_Model_Category cR_Mas_Sup_Car_Model_Category = db.CR_Mas_Sup_Car_Model_Category.FirstOrDefault(Mcat => Mcat.CR_Mas_Sup_Car_Model_Category_Code == id1 && Mcat.CR_Mas_Sup_Car_Category_Code == id2);
            if (cR_Mas_User_Sub_Validation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CR_Mas_User_Sub_Validation_Code = new SelectList(db.CR_Mas_User_Information, "CR_Mas_User_Information_Code", "CR_Mas_User_Information_PassWord", cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code);
            return View(cR_Mas_User_Sub_Validation);
        }

        // POST: SubValidation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_User_Sub_Validation_Code,CR_Mas_User_Sub_Validation_Tasks_Code,CR_Mas_User_Sub_Validation_Insert,CR_Mas_User_Sub_Validation_UpDate,CR_Mas_User_Sub_Validation_Delete,CR_Mas_User_Sub_Validation_UnDelete,CR_Mas_User_Sub_Validation_Hold,CR_Mas_User_Sub_Validation_UnHold,CR_Mas_User_Sub_Validation_Print")] CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cR_Mas_User_Sub_Validation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CR_Mas_User_Sub_Validation_Code = new SelectList(db.CR_Mas_User_Information, "CR_Mas_User_Information_Code", "CR_Mas_User_Information_PassWord", cR_Mas_User_Sub_Validation.CR_Mas_User_Sub_Validation_Code);
            return View(cR_Mas_User_Sub_Validation);
        }

        // GET: SubValidation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation = db.CR_Mas_User_Sub_Validation.Find(id);
            if (cR_Mas_User_Sub_Validation == null)
            {
                return HttpNotFound();
            }
            return View(cR_Mas_User_Sub_Validation);
        }

        // POST: SubValidation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CR_Mas_User_Sub_Validation cR_Mas_User_Sub_Validation = db.CR_Mas_User_Sub_Validation.Find(id);
            db.CR_Mas_User_Sub_Validation.Remove(cR_Mas_User_Sub_Validation);
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
