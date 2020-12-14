using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class SupportingController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // GET: Supporting
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            var TypeList = new SelectList(new[] {
                                              new {ID="1",Name="شركات المساندة"},
                                              new{ID="2",Name="شركات التأمين"},
                                              new{ID="3",Name="البنوك"},
                                              }, "ID", "Name", 1);
            ViewData["list"] = TypeList;

            if (AccountController.ST_1402_unhold != true || AccountController.ST_1402_hold != true && AccountController.ST_1402_undelete != true || AccountController.ST_1402_delete != true)
            {
                var SuppLIst = from CR_Mas_Com_Supporting in db.CR_Mas_Com_Supporting
                                   where CR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status != "H" && CR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status != "D"
                                   select CR_Mas_Com_Supporting;
                return View(SuppLIst);
            }
            else
                if (AccountController.ST_1402_unhold != true || AccountController.ST_1402_hold != true)
            {
                var SuppLIst = db.CR_Mas_Com_Supporting.Where(x => x.CR_Mas_Com_Supporting_Status != "H");
                return View(db.CR_Mas_Com_Supporting.ToList());
            }
            else if (AccountController.ST_1402_undelete != true || AccountController.ST_1402_delete != true)
            {
                var SuppLIst = db.CR_Mas_Com_Supporting.Where(x => x.CR_Mas_Com_Supporting_Status != "D");
                return View(db.CR_Mas_Com_Supporting.ToList());
            }
            else
            {
                return View(db.CR_Mas_Com_Supporting.ToList());
            }

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
                var suppTable = new System.Data.DataTable("teste");

                suppTable.Columns.Add("المرجع", typeof(string));
                suppTable.Columns.Add("الحالة", typeof(string));
                suppTable.Columns.Add("الإسم الكامل العربي", typeof(string));
                suppTable.Columns.Add("الإسم المختصر العربي", typeof(string));
                suppTable.Columns.Add("الرمز", typeof(string));
                var Lrecord = db.CR_Mas_Com_Supporting.ToList();

                if (Lrecord != null)
                {
                    foreach (var i in Lrecord)
                    {
                        suppTable.Rows.Add(i.CR_Mas_Com_Supporting_Reasons, i.CR_Mas_Com_Supporting_Status,
                            i.CR_Mas_Com_Supporting_Ar_Long_Name, i.CR_Mas_Com_Supporting_Ar_Short_Name, i.CR_Mas_Com_Supporting_Code);
                    }
                }
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = suppTable;
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
            return View(db.CR_Mas_Com_Supporting.ToList());
        }

        //////// GET: Supporting/Details/5
        //////public ActionResult Details(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Com_Supporting cR_Mas_Com_Supporting = db.CR_Mas_Com_Supporting.Find(id);
        //////    if (cR_Mas_Com_Supporting == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Com_Supporting);
        //////}
        public CR_Mas_Com_Supporting GetLastRecord()
        {
            var Lrecord = db.CR_Mas_Com_Supporting.Max(Lr => Lr.CR_Mas_Com_Supporting_Code);
            CR_Mas_Com_Supporting s = new CR_Mas_Com_Supporting();
            if (Lrecord != null)
            {
                int val = int.Parse(Lrecord) + 1;
                s.CR_Mas_Com_Supporting_Code = val.ToString();
            }
            else
            {
                s.CR_Mas_Com_Supporting_Code = "1001";
            }
            return s;
        }
        // GET: Supporting/Create
        public ActionResult Create()
        {
            var TypeList = new SelectList(new[] {
                                              new {ID="1",Name="شركات المساندة"},
                                              new{ID="2",Name="شركات التأمين"},
                                              new{ID="3",Name="البنوك"},
                                              }, "ID", "Name", 1);
            ViewData["list"] = TypeList;
            ViewBag.CR_Mas_Com_Supporting_Type = new SelectList(db.CR_Mas_Com_Supporting, "CR_Mas_Com_Supporting_Type", "CR_Mas_Com_Supporting_Type");
            CR_Mas_Com_Supporting sup = new CR_Mas_Com_Supporting();
            sup = GetLastRecord();
            sup.CR_Mas_Com_Supporting_Status = "A";
            return View(sup);
        }

        // POST: Supporting/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CR_Mas_Com_Supporting_Code, CR_Mas_Com_Supporting_Type, CR_Mas_Com_Supporting_Ar_Long_Name, " +
        "CR_Mas_Com_Supporting_Ar_Short_Name, CR_Mas_Com_Supporting_En_Long_Name,CR_Mas_Com_Supporting_En_Short_Name, CR_Mas_Com_Supporting_Tax_Number," +
        " CR_Mas_Com_Supporting_Status, CR_Mas_Com_Supporting_Reasons")] CR_Mas_Com_Supporting cR_Mas_Com_Supporting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var LrecordExitArabeLng = db.CR_Mas_Com_Supporting.Any(Lr => Lr.CR_Mas_Com_Supporting_Ar_Long_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name);
                    var LrecordExitArabeSh = db.CR_Mas_Com_Supporting.Any(Lr => Lr.CR_Mas_Com_Supporting_Ar_Short_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name);
                    var LrecordExitEnglishLng = db.CR_Mas_Com_Supporting.Any(Lr => Lr.CR_Mas_Com_Supporting_En_Long_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name);
                    var LrecordExitEnglishSh = db.CR_Mas_Com_Supporting.Any(Lr => Lr.CR_Mas_Com_Supporting_En_Short_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name);

                    if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name != null &&
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name != null &&
                        !LrecordExitArabeLng && !LrecordExitArabeSh && !LrecordExitEnglishLng && !LrecordExitEnglishSh && 
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name.Length >= 3 && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name.Length >= 3 &&
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name.Length >= 3 && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name.Length >= 3)
                    {
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Code = GetLastRecord().CR_Mas_Com_Supporting_Code;
                        db.CR_Mas_Com_Supporting.Add(cR_Mas_Com_Supporting);
                        db.SaveChanges();
                        cR_Mas_Com_Supporting = new CR_Mas_Com_Supporting();
                        cR_Mas_Com_Supporting = GetLastRecord();
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status = "A";
                        TempData["TempModel"] = "تم الحفظ بنجاح";
                        return RedirectToAction("Create", "Supporting");
                    }
                    else
                    {
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name == null)
                            ViewBag.LRExistAr = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name == null)
                            ViewBag.LRExistEn = "الرجاء إدخال بيانات الحقل";
                        if (LrecordExitArabeLng)
                            ViewBag.LRExistArLng = "عفوا هذه الشركة موجودة";
                        if (LrecordExitArabeSh)
                            ViewBag.LRExistArSh = "عفوا هذه الشركة موجودة";
                        if (LrecordExitEnglishLng)
                            ViewBag.LRExistEn = "عفوا هذه الشركة موجودة";
                        if (LrecordExitEnglishSh)
                            ViewBag.LRExistEn = "عفوا هذه الشركة موجودة";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                    }
                }
            }
            catch (Exception) { }
            ViewBag.CR_Mas_Com_Supporting_Type = new SelectList(db.CR_Mas_Com_Supporting, "CR_Mas_Com_Supporting_Type", "CR_Mas_Com_Supporting_Type");
            return View(cR_Mas_Com_Supporting);
        }

        // GET: Supporting/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CR_Mas_Com_Supporting cR_Mas_Com_Supporting = db.CR_Mas_Com_Supporting.Find(id);
            if (cR_Mas_Com_Supporting == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "A" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Activated" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "1" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Undeleted")
                {
                    ViewBag.stat = "حذف";
                    ViewBag.h = "تعطيل";
                }

                if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "D" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Deleted" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "0")
                {
                    ViewBag.stat = "إسترجاع";
                    ViewBag.h = "تعطيل";
                    ViewData["ReadOnly"] = "true";
                }

                if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "H" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Hold" ||
                    cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "2")
                {
                    ViewBag.h = "تنشيط";
                    ViewBag.stat = "حذف";
                    ViewData["ReadOnly"] = "true";
                }

                if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == null)
                {
                    ViewBag.h = "تعطيل";
                    ViewBag.stat = "حذف";
                }
                ViewBag.delete = cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status;
                ViewBag.CR_Mas_Com_Supporting_Type = new SelectList(db.CR_Mas_Com_Supporting, "CR_Mas_Com_Supporting_Type", "CR_Mas_Com_Supporting_Type");
            }
            return View(cR_Mas_Com_Supporting);
        }

        // POST: Supporting/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CR_Mas_Com_Supporting_Code, CR_Mas_Com_Supporting_Type, CR_Mas_Com_Supporting_Ar_Long_Name, " +
        "CR_Mas_Com_Supporting_Ar_Short_Name, CR_Mas_Com_Supporting_En_Long_Name,CR_Mas_Com_Supporting_En_Short_Name, CR_Mas_Com_Supporting_Tax_Number," +
        " CR_Mas_Com_Supporting_Status, CR_Mas_Com_Supporting_Reasons")] CR_Mas_Com_Supporting cR_Mas_Com_Supporting, string save, string delete, string hold)
        {
            if (!string.IsNullOrEmpty(save))
            {
                if (ModelState.IsValid)
                {
                    var LrecordExitArabeLng = db.CR_Mas_Com_Supporting.Any(f => f.CR_Mas_Com_Supporting_Code != cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Code &&
                                                                          f.CR_Mas_Com_Supporting_Ar_Long_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name);
                    var LrecordExitArabeSh = db.CR_Mas_Com_Supporting.Any(f => f.CR_Mas_Com_Supporting_Code != cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Code &&
                                                                          f.CR_Mas_Com_Supporting_Ar_Short_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name);
                    var LrecordExitEnglishLng = db.CR_Mas_Com_Supporting.Any(f => f.CR_Mas_Com_Supporting_Code != cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Code &&
                                                                            f.CR_Mas_Com_Supporting_En_Long_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name);
                    var LrecordExitEnglishSh = db.CR_Mas_Com_Supporting.Any(f => f.CR_Mas_Com_Supporting_Code != cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Code &&
                                                                            f.CR_Mas_Com_Supporting_En_Short_Name == cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name);

                    if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name != null &&
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name != null &&
                        cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name.Length >= 3 && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name.Length >= 3 &&
                        !LrecordExitArabeLng && !LrecordExitArabeSh && !LrecordExitEnglishLng && !LrecordExitEnglishSh)
                    {
                        db.Entry(cR_Mas_Com_Supporting).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name == null)
                            ViewBag.LRExistAr = "الرجاء إدخال بيانات الحقل";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name == null)
                            ViewBag.LRExistEn = "الرجاء إدخال بيانات الحقل";
                        if (LrecordExitArabeLng)
                            ViewBag.LRExistArLng = "عفوا هذه الشركة موجودة";
                        if (LrecordExitArabeSh)
                            ViewBag.LRExistArSh = "عفوا هذه الشركة موجودة";
                        if (LrecordExitEnglishLng)
                            ViewBag.LRExistEnLng = "عفوا هذه الشركة موجودة";
                        if (LrecordExitEnglishSh)
                            ViewBag.LRExistEnSh = "عفوا هذه الشركة موجودة";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Long_Name.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Ar_Short_Name.Length < 3)
                            ViewBag.LRExistAr = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Long_Name.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                        if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name != null && cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_En_Short_Name.Length < 3)
                            ViewBag.LRExistEn = "عفوا الاسم يحتوي على ما بين 3 و 30 حرفًا";
                    }
                }
            }
            if (delete == "Delete" || delete == "حذف")
            {
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status = "D";
                db.Entry(cR_Mas_Com_Supporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (delete == "Activate" || delete == "إسترجاع")
            {
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status = "A";
                db.Entry(cR_Mas_Com_Supporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تعطيل" || hold == "hold")
            {
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status = "H";
                db.Entry(cR_Mas_Com_Supporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (hold == "تنشيط" || hold == "Activate")
            {
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status = "A";
                db.Entry(cR_Mas_Com_Supporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "A" ||
            cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Activated" ||
            cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "1" ||
            cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Undeleted")
            {
                ViewBag.stat = "حذف";
                ViewBag.h = "تعطيل";
            }
            if ((cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "D" ||
                 cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Deleted" ||
                 cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "0"))
            {
                ViewBag.stat = "إسترجاع";
                ViewBag.h = "تعطيل";
            }
            if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "H" ||
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "Hold" ||
                cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == "2")
            {
                ViewBag.h = "تنشيط";
                ViewBag.stat = "حذف";
            }
            if (cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status == null)
            {
                ViewBag.h = "تعطيل";
                ViewBag.stat = "حذف";
            }
            ViewBag.delete = cR_Mas_Com_Supporting.CR_Mas_Com_Supporting_Status;
            ViewBag.CR_Mas_Com_Supporting_Type = new SelectList(db.CR_Mas_Com_Supporting, "CR_Mas_Com_Supporting_Type", "CR_Mas_Com_Supporting_Type");
            return View(cR_Mas_Com_Supporting);
        }

        //////// GET: Supporting/Delete/5
        //////public ActionResult Delete(string id)
        //////{
        //////    if (id == null)
        //////    {
        //////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //////    }
        //////    CR_Mas_Com_Supporting cR_Mas_Com_Supporting = db.CR_Mas_Com_Supporting.Find(id);
        //////    if (cR_Mas_Com_Supporting == null)
        //////    {
        //////        return HttpNotFound();
        //////    }
        //////    return View(cR_Mas_Com_Supporting);
        //////}

        //////// POST: Car_Features/Delete/5
        //////[HttpPost, ActionName("Delete")]
        //////[ValidateAntiForgeryToken]
        //////public ActionResult DeleteConfirmed(string id)
        //////{
        //////    CR_Mas_Com_Supporting cR_Mas_Com_Supporting = db.CR_Mas_Com_Supporting.Find(id);
        //////    db.CR_Mas_Com_Supporting.Remove(cR_Mas_Com_Supporting);
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