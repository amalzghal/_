using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class AccountController : Controller
    {
        private RentCarDBEntities db = new RentCarDBEntities();

        // Authentification varaibles:
        public static string UserLogin;
        public static string UserName;

        //////Main Tasks Access///////
        public static Boolean MT_1001= false;  // دخول النظام الإداري للشركة
        public static Boolean MT_1002=false;  //القائمة الرئيسية
        public static Boolean MT_1003=false;  //تغيير كلمة السر
        public static Boolean MT_1004 = false;  //عدد الشركات 
        public static Boolean MT_1005 = false;  //عدد العقود
        public static Boolean MT_1006 = false;  //عدد السيارات
        public static Boolean MT_1007;  //عدد العقود المنتهية
        public static Boolean MT_1008;  //عدد عقود التأجير
        public static Boolean MT_1009;  //عدد العقود على وشك الإنتهاء
        public static Boolean MT_1010;  //عدد عقود التأجير المنتهية
        public static Boolean MT_1011;  //عدد عقود التأجير القائمة

        public static Boolean MT_1101 = false;  //عقد تأجير السيارات
        public static Boolean MT_1102 = false;  //عقد تأجير السيارات التفصيلي
        public static Boolean MT_1103;  //عقود الشركات المساندات
        public static Boolean MT_1104;  //عقد تبادل البيانات

        public static Boolean MT_1201;  //تقارير إدارية مختصرة
        public static Boolean MT_1202;  //تقارير مالية مختصرة
        public static Boolean MT_1203;  //تقارير إدارية تفصيلية
        public static Boolean MT_1204;  //تقارير مالية تفصيلية
        public static Boolean MT_1205;  //تقارير إحصائية

        public static Boolean MT_1301;  //
        public static Boolean MT_1302;  //
        public static Boolean MT_1303;  //
        public static Boolean MT_1304;  //

        public static Boolean MT_1401;  //
        public static Boolean MT_1402;  //

        public static Boolean MT_1501;  //
        public static Boolean MT_1502;  //
        public static Boolean MT_1503;  //
        public static Boolean MT_1504;  //
        public static Boolean MT_1505;  //
        public static Boolean MT_1506;  //
        public static Boolean MT_1507;  //
        public static Boolean MT_1508;  //
        public static Boolean MT_1509;  //

        public static Boolean MT_1601;
        public static Boolean MT_1602;
        public static Boolean MT_1603;
        public static Boolean MT_1604;
        public static Boolean MT_1605;

        public static Boolean MT_1701;
        public static Boolean MT_1702;

        public static Boolean MT_1801;
        public static Boolean MT_1802;
        public static Boolean MT_1803;

        public static Boolean MT_1901;
        public static Boolean MT_1902;
        public static Boolean MT_1903;
        public static Boolean MT_1904;
        /////////end Main Access/////

        /////Sub Task Access/////////
        public static Boolean ST_1001 = false;  // دخول النظام الإداري للشركة
        public static Boolean ST_1002 = false;  //القائمة الرئيسية
        public static Boolean ST_1003 = false;  //تغيير كلمة السر
        public static Boolean ST_1004;  //عدد الشركات 
        public static Boolean ST_1005;  //عدد العقود
        public static Boolean ST_1006;  //عدد السيارات
        public static Boolean ST_1007;  //عدد العقود المنتهية
        public static Boolean ST_1008;  //عدد عقود التأجير
        public static Boolean ST_1009;  //عدد العقود على وشك الإنتهاء
        public static Boolean ST_1010;  //عدد عقود التأجير المنتهية
        public static Boolean ST_1011;  //عدد عقود التأجير القائمة

        public static Boolean ST_1101;  //عقد تأجير السيارات
        public static Boolean ST_1102;  //عقد تأجير السيارات التفصيلي
        public static Boolean ST_1103;  //عقود الشركات المساندات
        public static Boolean ST_1104;  //عقد تبادل البيانات

        public static Boolean ST_1201;  //تقارير إدارية مختصرة
        public static Boolean ST_1202;  //تقارير مالية مختصرة
        public static Boolean ST_1203;  //تقارير إدارية تفصيلية
        public static Boolean ST_1204;  //تقارير مالية تفصيلية
        public static Boolean ST_1205;  //تقارير إحصائية

        public static Boolean ST_1301;  //
        public static Boolean ST_1302;  //
        public static Boolean ST_1303;  //f
        public static Boolean ST_1304;  //f

        public static Boolean ST_1401;  //f
        public static Boolean ST_1402;  //f

        public static Boolean ST_1501;  //
        public static Boolean ST_1502;  //
        public static Boolean ST_1503;  //
        public static Boolean ST_1504;  //
        public static Boolean ST_1505;  //
        public static Boolean ST_1506;  //
        public static Boolean ST_1507;  //
        public static Boolean ST_1508;  //
        public static Boolean ST_1509;  //

        public static Boolean ST_1601;
        public static Boolean ST_1602;
        public static Boolean ST_1603;
        public static Boolean ST_1604;
        public static Boolean ST_1605;

        public static Boolean ST_1701;
        public static Boolean ST_1702;

        public static Boolean ST_1801;
        public static Boolean ST_1802;
        public static Boolean ST_1803;

        public static Boolean ST_1901;
        public static Boolean ST_1902;
        public static Boolean ST_1903;
        public static Boolean ST_1904;
        //////End Sub Task Access////



        //////Sub Validation////
        public static Boolean ST_1101_insert;
        public static Boolean ST_1101_update;
        public static Boolean ST_1101_delete;
        public static Boolean ST_1101_undelete;
        public static Boolean ST_1101_hold;
        public static Boolean ST_1101_unhold;
        public static Boolean ST_1101_print;

        public static Boolean ST_1102_insert;
        public static Boolean ST_1102_update;
        public static Boolean ST_1102_delete;
        public static Boolean ST_1102_undelete;
        public static Boolean ST_1102_hold;
        public static Boolean ST_1102_unhold;
        public static Boolean ST_1102_print;

        public static Boolean ST_1103_insert;
        public static Boolean ST_1103_update;
        public static Boolean ST_1103_delete;
        public static Boolean ST_1103_undelete;
        public static Boolean ST_1103_hold;
        public static Boolean ST_1103_unhold;
        public static Boolean ST_1103_print;

        public static Boolean ST_1104_insert;
        public static Boolean ST_1104_update;
        public static Boolean ST_1104_delete;
        public static Boolean ST_1104_undelete;
        public static Boolean ST_1104_hold;
        public static Boolean ST_1104_unhold;
        public static Boolean ST_1104_print;

        public static Boolean ST_1201_insert;
        public static Boolean ST_1201_update;
        public static Boolean ST_1201_delete;
        public static Boolean ST_1201_undelete;
        public static Boolean ST_1201_hold;
        public static Boolean ST_1201_unhold;
        public static Boolean ST_1201_print;

        public static Boolean ST_1202_insert;
        public static Boolean ST_1202_update;
        public static Boolean ST_1202_delete;
        public static Boolean ST_1202_undelete;
        public static Boolean ST_1202_hold;
        public static Boolean ST_1202_unhold;
        public static Boolean ST_1202_print;

        public static Boolean ST_1203_insert;
        public static Boolean ST_1203_update;
        public static Boolean ST_1203_delete;
        public static Boolean ST_1203_undelete;
        public static Boolean ST_1203_hold;
        public static Boolean ST_1203_unhold;
        public static Boolean ST_1203_print;

        public static Boolean ST_1204_insert;
        public static Boolean ST_1204_update;
        public static Boolean ST_1204_delete;
        public static Boolean ST_1204_undelete;
        public static Boolean ST_1204_hold;
        public static Boolean ST_1204_unhold;
        public static Boolean ST_1204_print;

        public static Boolean ST_1205_insert;
        public static Boolean ST_1205_update;
        public static Boolean ST_1205_delete;
        public static Boolean ST_1205_undelete;
        public static Boolean ST_1205_hold;
        public static Boolean ST_1205_unhold;
        public static Boolean ST_1205_print;

        public static Boolean ST_1301_insert;
        public static Boolean ST_1301_update;
        public static Boolean ST_1301_delete;
        public static Boolean ST_1301_undelete;
        public static Boolean ST_1301_hold;
        public static Boolean ST_1301_unhold;
        public static Boolean ST_1301_print;

        public static Boolean ST_1302_insert;
        public static Boolean ST_1302_update;
        public static Boolean ST_1302_delete;
        public static Boolean ST_1302_undelete;
        public static Boolean ST_1302_hold;
        public static Boolean ST_1302_unhold;
        public static Boolean ST_1302_print;

        public static Boolean ST_1303_insert;
        public static Boolean ST_1303_update;
        public static Boolean ST_1303_delete;
        public static Boolean ST_1303_undelete;
        public static Boolean ST_1303_hold;
        public static Boolean ST_1303_unhold;
        public static Boolean ST_1303_print;

        public static Boolean ST_1304_insert;
        public static Boolean ST_1304_update;
        public static Boolean ST_1304_delete;
        public static Boolean ST_1304_undelete;
        public static Boolean ST_1304_hold;
        public static Boolean ST_1304_unhold;
        public static Boolean ST_1304_print;


        public static Boolean ST_1401_insert;
        public static Boolean ST_1401_update;
        public static Boolean ST_1401_delete;
        public static Boolean ST_1401_undelete;
        public static Boolean ST_1401_hold;
        public static Boolean ST_1401_unhold;
        public static Boolean ST_1401_print;

        public static Boolean ST_1402_insert;
        public static Boolean ST_1402_update;
        public static Boolean ST_1402_delete;
        public static Boolean ST_1402_undelete;
        public static Boolean ST_1402_hold;
        public static Boolean ST_1402_unhold;
        public static Boolean ST_1402_print;


        public static Boolean ST_1501_insert;
        public static Boolean ST_1501_update;
        public static Boolean ST_1501_delete;
        public static Boolean ST_1501_undelete;
        public static Boolean ST_1501_hold;
        public static Boolean ST_1501_unhold;
        public static Boolean ST_1501_print;


        public static Boolean ST_1502_insert;
        public static Boolean ST_1502_update;
        public static Boolean ST_1502_delete;
        public static Boolean ST_1502_undelete;
        public static Boolean ST_1502_hold;
        public static Boolean ST_1502_unhold;
        public static Boolean ST_1502_print;

        public static Boolean ST_1503_insert;
        public static Boolean ST_1503_update;
        public static Boolean ST_1503_delete;
        public static Boolean ST_1503_undelete;
        public static Boolean ST_1503_hold;
        public static Boolean ST_1503_unhold;
        public static Boolean ST_1503_print;

        public static Boolean ST_1504_insert;
        public static Boolean ST_1504_update;
        public static Boolean ST_1504_delete;
        public static Boolean ST_1504_undelete;
        public static Boolean ST_1504_hold;
        public static Boolean ST_1504_unhold;
        public static Boolean ST_1504_print;

        public static Boolean ST_1505_insert;
        public static Boolean ST_1505_update;
        public static Boolean ST_1505_delete;
        public static Boolean ST_1505_undelete;
        public static Boolean ST_1505_hold;
        public static Boolean ST_1505_unhold;
        public static Boolean ST_1505_print;

        public static Boolean ST_1506_insert;
        public static Boolean ST_1506_update;
        public static Boolean ST_1506_delete;
        public static Boolean ST_1506_undelete;
        public static Boolean ST_1506_hold;
        public static Boolean ST_1506_unhold;
        public static Boolean ST_1506_print;

        public static Boolean ST_1507_insert;
        public static Boolean ST_1507_update;
        public static Boolean ST_1507_delete;
        public static Boolean ST_1507_undelete;
        public static Boolean ST_1507_hold;
        public static Boolean ST_1507_unhold;
        public static Boolean ST_1507_print;

        public static Boolean ST_1508_insert;
        public static Boolean ST_1508_update;
        public static Boolean ST_1508_delete;
        public static Boolean ST_1508_undelete;
        public static Boolean ST_1508_hold;
        public static Boolean ST_1508_unhold;
        public static Boolean ST_1508_print;

        public static Boolean ST_1509_insert;
        public static Boolean ST_1509_update;
        public static Boolean ST_1509_delete;
        public static Boolean ST_1509_undelete;
        public static Boolean ST_1509_hold;
        public static Boolean ST_1509_unhold;
        public static Boolean ST_1509_print;

        public static Boolean ST_1601_insert;
        public static Boolean ST_1601_update;
        public static Boolean ST_1601_delete;
        public static Boolean ST_1601_undelete;
        public static Boolean ST_1601_hold;
        public static Boolean ST_1601_unhold;
        public static Boolean ST_1601_print;

        public static Boolean ST_1602_insert;
        public static Boolean ST_1602_update;
        public static Boolean ST_1602_delete;
        public static Boolean ST_1602_undelete;
        public static Boolean ST_1602_hold;
        public static Boolean ST_1602_unhold;
        public static Boolean ST_1602_print;

        public static Boolean ST_1603_insert;
        public static Boolean ST_1603_update;
        public static Boolean ST_1603_delete;
        public static Boolean ST_1603_undelete;
        public static Boolean ST_1603_hold;
        public static Boolean ST_1603_unhold;
        public static Boolean ST_1603_print;

        public static Boolean ST_1604_insert;
        public static Boolean ST_1604_update;
        public static Boolean ST_1604_delete;
        public static Boolean ST_1604_undelete;
        public static Boolean ST_1604_hold;
        public static Boolean ST_1604_unhold;
        public static Boolean ST_1604_print;

        public static Boolean ST_1605_insert;
        public static Boolean ST_1605_update;
        public static Boolean ST_1605_delete;
        public static Boolean ST_1605_undelete;
        public static Boolean ST_1605_hold;
        public static Boolean ST_1605_unhold;
        public static Boolean ST_1605_print;

        public static Boolean ST_1701_insert;
        public static Boolean ST_1701_update;
        public static Boolean ST_1701_delete;
        public static Boolean ST_1701_undelete;
        public static Boolean ST_1701_hold;
        public static Boolean ST_1701_unhold;
        public static Boolean ST_1701_print;

        public static Boolean ST_1702_insert;
        public static Boolean ST_1702_update;
        public static Boolean ST_1702_delete;
        public static Boolean ST_1702_undelete;
        public static Boolean ST_1702_hold;
        public static Boolean ST_1702_unhold;
        public static Boolean ST_1702_print;

        public static Boolean ST_1801_insert;
        public static Boolean ST_1801_update;
        public static Boolean ST_1801_delete;
        public static Boolean ST_1801_undelete;
        public static Boolean ST_1801_hold;
        public static Boolean ST_1801_unhold;
        public static Boolean ST_1801_print;

        public static Boolean ST_1802_insert;
        public static Boolean ST_1802_update;
        public static Boolean ST_1802_delete;
        public static Boolean ST_1802_undelete;
        public static Boolean ST_1802_hold;
        public static Boolean ST_1802_unhold;
        public static Boolean ST_1802_print;

        public static Boolean ST_1803_insert;
        public static Boolean ST_1803_update;
        public static Boolean ST_1803_delete;
        public static Boolean ST_1803_undelete;
        public static Boolean ST_1803_hold;
        public static Boolean ST_1803_unhold;
        public static Boolean ST_1803_print;

        public static Boolean ST_1901_insert;
        public static Boolean ST_1901_update;
        public static Boolean ST_1901_delete;
        public static Boolean ST_1901_undelete;
        public static Boolean ST_1901_hold;
        public static Boolean ST_1901_unhold;
        public static Boolean ST_1901_print;

        public static Boolean ST_1902_insert;
        public static Boolean ST_1902_update;
        public static Boolean ST_1902_delete;
        public static Boolean ST_1902_undelete;
        public static Boolean ST_1902_hold;
        public static Boolean ST_1902_unhold;
        public static Boolean ST_1902_print;

        public static Boolean ST_1903_insert;
        public static Boolean ST_1903_update;
        public static Boolean ST_1903_delete;
        public static Boolean ST_1903_undelete;
        public static Boolean ST_1903_hold;
        public static Boolean ST_1903_unhold;
        public static Boolean ST_1903_print;

        public static Boolean ST_1904_insert;
        public static Boolean ST_1904_update;
        public static Boolean ST_1904_delete;
        public static Boolean ST_1904_undelete;
        public static Boolean ST_1904_hold;
        public static Boolean ST_1904_unhold;
        public static Boolean ST_1904_print;
        ////////End sub Validation///////

        // end Authentification varaibles




        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
     
        [ActionName("Login")]
        [HttpGet]
        public ActionResult Login_GET()
        {
            return View();
        }

        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login_Post(string LogEnter,string txtusername, string txtpassword)
        {
            if (LogEnter == "الدخول")
            {
                var q = db.CR_Mas_User_Information.FirstOrDefault(user => user.CR_Mas_User_Information_Code == txtusername && user.CR_Mas_User_Information_PassWord == txtpassword && user.CR_Mas_User_Information_Status=="A");
                if (q != null)
                {
                    UserLogin = q.CR_Mas_User_Information_Code;
                    UserName = q.CR_Mas_User_Information_Ar_Name;

                    Init();
                    Get_Authority();
                    Session["Hello"] = "....مرحبا " + UserName ;

                    return RedirectToAction("index", "home");
                }
                else
                {
                    //////ViewBag.LoginError = "الرجاء التأكد من إسم المستخدم";
                    //////ViewBag.PassError = "الرجاء التأكد من كلمة السر";

                    ViewBag.LoginError = "الرجاء التأكد من إسم المستخدم و  كلمة السر";
                }
            }
            return View();
        }

        public void Get_Authority()
        {
            //IList<CR_Mas_User_Main_Validation> MVALList = new List<CR_Mas_User_Main_Validation>();

            //List<CR_Mas_User_Main_Validation> MainVal = (from r in db.CR_Mas_User_Main_Validation
            //            where r.CR_Mas_User_Main_Validation_Code == UserLogin && r.CR_Mas_User_Main_Validation1 == true
            //            select new CR_Mas_User_Main_Validation { 
            //                CR_Mas_User_Main_Validation_Tasks_Code=r.CR_Mas_User_Main_Validation_Tasks_Code,
            //                CR_Mas_User_Main_Validation1=r.CR_Mas_User_Main_Validation1
            //            }).ToList();


            //foreach(var Authority in MainVal)
            //{
            //    if (Authority.CR_Mas_User_Main_Validation_Tasks_Code == "1004")
            //    {
            //        MT_1001 = true;
            //    }
            //}


            var Lrecord = db.CR_Mas_User_Main_Validation
                               .Where(t => t.CR_Mas_User_Main_Validation_Code.Contains(UserLogin));

            foreach(var record in Lrecord)
            {
                if (record.CR_Mas_User_Main_Validation_Tasks_Code=="1004" && record.CR_Mas_User_Main_Validation1==true)
                {
                    MT_1004 = true;
                    continue;
                }
              

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1005" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1005 = true;
                    continue;
                }
               

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1006" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1006 = true;
                    continue;
                }
               

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1007" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1007 = true;
                    continue;
                }
               

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1008" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1008 = true;
                    continue;
                }
               

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1009" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1009 = true;
                    continue;
                }
               
                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1010" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1010 = true;
                    continue;
                }
               
                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1011" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1011 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1101" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1101 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1102" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1102 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1103" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1103 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1104" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1104 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1201" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1201 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1202" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1202 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1203" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1203 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1204" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1204 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1205" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1205 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1301" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1301 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1302" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1302 = true;
                    continue;
                }
                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1303" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1303 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1304" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1304 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1401" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1401 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1402" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1402 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1501" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1501 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1502" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1502 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1503" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1503 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1504" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1504 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1505" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1505 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1506" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1506 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1507" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1507 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1508" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1508 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1509" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1509 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1601" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1601 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1602" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1602 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1603" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1603 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1604" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1604 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1605" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1605 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1701" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1701 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1702" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1702 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1801" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1801 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1802" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1802 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1803" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1803 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1901" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1901 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1902" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1902 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1903" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1903 = true;
                    continue;
                }

                if (record.CR_Mas_User_Main_Validation_Tasks_Code == "1904" && record.CR_Mas_User_Main_Validation1 == true)
                {
                    MT_1904 = true;
                    continue;
                }


                //List<CR_Mas_User_Main_Validation> MainVal = (from r in db.CR_Mas_User_Main_Validation
                //                                             where r.CR_Mas_User_Main_Validation_Code == UserLogin && r.CR_Mas_User_Main_Validation1 == true
                //                                             select new CR_Mas_User_Main_Validation
                //                                             {
                //                                                 CR_Mas_User_Main_Validation_Tasks_Code = r.CR_Mas_User_Main_Validation_Tasks_Code,
                //                                                 CR_Mas_User_Main_Validation1 = r.CR_Mas_User_Main_Validation1
                //                                             }).ToList();


                //var query = (from T in db.CR_Mas_Sys_Tasks
                //             from MainV in db.CR_Mas_User_Main_Validation
                //             from UserInfo in db.CR_Mas_User_Information
                //             where UserInfo.CR_Mas_User_Information_Code == UserLogin &&
                //             MainV.CR_Mas_User_Main_Validation_Code == UserInfo.CR_Mas_User_Information_Code &&
                //             MainV.CR_Mas_User_Main_Validation_Tasks_Code == T.CR_Mas_Sys_Tasks_Code
                //             select new CR_Mas_Sys_Tasks
                //             {
                //                 CR_Mas_Sys_Tasks_Code = T.CR_Mas_Sys_Tasks_Code,
                //                 CR_Mas_Sys_Tasks_Main_Validation = T.CR_Mas_Sys_Tasks_Main_Validation,
                //                 CR_Mas_Sys_Tasks_Sub_Validation = T.CR_Mas_Sys_Tasks_Sub_Validation

                //             }).ToList();

                //foreach (var q in query)
                //{
                //    if (q.CR_Mas_Sys_Tasks_Code=="1004")
                //    {
                //        ST_1004 = true;
                //        continue;
                //    }
                //}




            }



            var query = db.CR_Mas_User_Information.FirstOrDefault(user => user.CR_Mas_User_Information_Code == UserLogin).CR_Mas_User_Main_Validation.Select(x => x.CR_Mas_Sys_Tasks).ToList();

            foreach (var q in query)
            {
                
                if (q.CR_Mas_Sys_Tasks_Code == "1004")
                {
                    if (MT_1004 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1004 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }

                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1005")
                {
                    if (MT_1005 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1005 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1006")
                {
                    if (MT_1006 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1006 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1007")
                {
                    if (MT_1007 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1007 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1008")
                {
                    if (MT_1008 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1008 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1009")
                {
                    if (MT_1009 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1009 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1010")
                {
                    if (MT_1010 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1010 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1011")
                {
                    if (MT_1011 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1011 = true;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1101")
                {
                    if (MT_1101 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1101 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1101");
                            if (SubQuery != null)
                            {
                                ST_1101_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1101_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1101_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1101_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1101_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1101_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1101_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1102")
                {
                    if (MT_1102 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1102 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1102");
                            if (SubQuery != null)
                            {
                                ST_1102_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1102_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1102_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1102_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1102_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1102_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1102_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1103")
                {
                    if (MT_1103 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1103 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1103");
                            if (SubQuery != null)
                            {
                                ST_1103_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1103_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1103_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1103_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1103_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1103_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1103_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1104")
                {
                    if (MT_1104 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1104 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1104");
                            if (SubQuery != null)
                            {
                                ST_1104_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1104_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1104_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1104_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1104_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1104_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1104_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1201")
                {
                    if (MT_1201 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1201 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1201");
                            if (SubQuery != null)
                            {
                                ST_1201_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1201_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1201_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1201_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1201_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1201_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1201_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1202")
                {
                    if (MT_1202 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1202 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1202");
                            if (SubQuery != null)
                            {
                                ST_1202_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1202_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1202_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1202_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1202_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1202_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1202_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1203")
                {
                    if (MT_1203 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1203 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1203");
                            if (SubQuery != null)
                            {
                                ST_1203_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1203_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1203_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1203_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1203_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1203_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1203_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1204")
                {
                    if (MT_1204 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1204 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1204");
                            if (SubQuery != null)
                            {
                                ST_1204_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1204_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1204_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1204_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1204_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1204_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1204_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1205")
                {
                    if (MT_1205 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1205 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1205");
                            if (SubQuery != null)
                            {
                                ST_1205_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1205_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1205_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1205_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1205_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1205_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1205_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1301")
                {
                    if (MT_1301 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1301 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1301");
                            if (SubQuery != null)
                            {
                                ST_1301_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1301_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1301_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1301_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1301_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1301_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1301_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1302")
                {
                    if (MT_1302 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1302 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1302");
                            if (SubQuery != null)
                            {
                                ST_1302_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1302_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1302_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1302_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1302_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1302_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1302_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1303")
                {
                    if (MT_1303 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1303 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1303");
                            if (SubQuery != null)
                            {
                                ST_1303_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1303_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1303_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1303_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1303_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1303_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1303_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1304")
                {
                    if (MT_1304 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1304 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1304");
                            if (SubQuery != null)
                            {
                                ST_1304_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1304_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1304_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1304_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1304_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1304_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1304_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1401")
                {
                    if (MT_1401 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1401 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1401");
                            if (SubQuery != null)
                            {
                                ST_1401_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1401_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1401_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1401_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1401_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1401_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1401_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1402")
                {
                    if (MT_1402 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1402 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1402");
                            if (SubQuery != null)
                            {
                                ST_1402_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1402_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1402_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1402_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1402_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1402_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1402_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1501")
                {
                    if (MT_1501 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1501 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1501");
                            if (SubQuery != null)
                            {
                                ST_1501_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1501_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1501_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1501_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1501_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1501_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1501_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1502")
                {
                    if (MT_1502 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1502 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1502");
                            if (SubQuery != null)
                            {
                                ST_1502_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1502_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1502_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1502_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1502_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1502_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1502_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1503")
                {
                    if (MT_1503 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1503 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1503");
                            if (SubQuery != null)
                            {
                                ST_1503_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1503_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1503_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1503_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1503_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1503_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1503_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1504")
                {
                    if (MT_1504 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1504 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1504");
                            if (SubQuery != null)
                            {
                                ST_1504_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1504_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1504_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1504_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1504_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1504_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1504_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1505")
                {
                    if (MT_1505 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1505 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1505");
                            if (SubQuery != null)
                            {
                                ST_1505_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1505_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1505_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1505_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1505_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1505_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1505_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1506")
                {
                    if (MT_1506 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1506 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1506");
                            if (SubQuery != null)
                            {
                                ST_1506_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1506_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1506_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1506_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1506_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1506_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1506_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1507")
                {
                    if (MT_1507 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1507 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1507");
                            if (SubQuery != null)
                            {
                                ST_1507_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1507_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1507_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1507_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1507_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1507_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1507_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1508")
                {
                    if (MT_1508 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1508 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1508");
                            if (SubQuery != null)
                            {
                                ST_1508_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1508_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1508_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1508_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1508_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1508_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1508_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1509")
                {
                    if (MT_1509 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1509 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1509");
                            if (SubQuery != null)
                            {
                                ST_1509_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1509_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1509_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1509_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1509_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1509_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1509_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                
                if (q.CR_Mas_Sys_Tasks_Code == "1601")
                {
                    if (MT_1601 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1601 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1601");
                            if (SubQuery != null)
                            {
                                ST_1601_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1601_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1601_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1601_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1601_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1601_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1601_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1602")
                {
                    if (MT_1602 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1602 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1602");
                            if (SubQuery != null)
                            {
                                ST_1602_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1602_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1602_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1602_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1602_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1602_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1602_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1603")
                {
                    if (MT_1603 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1603 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1603");
                            if (SubQuery != null)
                            {
                                ST_1603_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1603_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1603_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1603_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1603_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1603_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1603_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1604")
                {
                    if (MT_1604 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1604 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1604");
                            if (SubQuery != null)
                            {
                                ST_1604_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1604_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1604_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1604_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1604_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1604_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1604_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1605")
                {
                    if (MT_1605 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1605 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1605");
                            if (SubQuery != null)
                            {
                                ST_1605_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1605_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1605_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1605_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1605_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1605_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1605_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1701")
                {
                    if (MT_1701 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1701 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1701");
                            if (SubQuery != null)
                            {
                                ST_1701_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1701_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1701_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1701_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1701_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1701_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1701_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1702")
                {
                    if (MT_1702 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1702 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1702");
                            if (SubQuery != null)
                            {
                                ST_1702_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1702_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1702_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1702_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1702_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1702_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1702_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1801")
                {
                    if (MT_1801 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1801 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1801");
                            if (SubQuery != null)
                            {
                                ST_1801_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1801_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1801_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1801_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1801_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1801_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1801_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1802")
                {
                    if (MT_1802 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1802 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1802");
                            if (SubQuery != null)
                            {
                                ST_1802_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1802_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1802_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1802_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1802_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1802_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1802_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1803")
                {
                    if (MT_1803 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1803 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1803");
                            if (SubQuery != null)
                            {
                                ST_1803_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1803_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1803_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1803_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1803_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1803_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1803_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1901")
                {
                    if (MT_1901 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1901 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1901");
                            if (SubQuery != null)
                            {
                                ST_1901_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1901_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1901_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1901_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1901_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1901_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1901_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1902")
                {
                    if (MT_1902 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1902 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1902");
                            if (SubQuery != null)
                            {
                                ST_1902_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1902_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1902_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1902_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1902_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1902_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1902_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1903")
                {
                    if (MT_1903 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1903 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1903");
                            if (SubQuery != null)
                            {
                                ST_1903_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1903_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1903_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1903_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1903_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1903_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1903_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                
                if (q.CR_Mas_Sys_Tasks_Code == "1904")
                {
                    if (MT_1904 == true)
                    {
                        if (q.CR_Mas_Sys_Tasks_Sub_Validation == true)
                        {
                            ST_1904 = true;
                            var SubQuery = db.CR_Mas_User_Sub_Validation.FirstOrDefault(x => x.CR_Mas_User_Sub_Validation_Code == UserLogin && x.CR_Mas_User_Sub_Validation_Tasks_Code == "1904");
                            if (SubQuery != null)
                            {
                                ST_1904_insert = (bool)SubQuery.CR_Mas_User_Sub_Validation_Insert;
                                ST_1904_update = (bool)SubQuery.CR_Mas_User_Sub_Validation_UpDate;
                                ST_1904_delete = (bool)SubQuery.CR_Mas_User_Sub_Validation_Delete;
                                ST_1904_undelete = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnDelete;
                                ST_1904_hold = (bool)SubQuery.CR_Mas_User_Sub_Validation_Hold;
                                ST_1904_unhold = (bool)SubQuery.CR_Mas_User_Sub_Validation_UnHold;
                                ST_1904_print = (bool)SubQuery.CR_Mas_User_Sub_Validation_Print;
                            }
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

        }

        public void Init()
        {
            MT_1001 = false;  // دخول النظام الإداري للشركة
            MT_1002 = false;  //القائمة الرئيسية
            MT_1003 = false;  //تغيير كلمة السر
            MT_1004 = false;  //عدد الشركات 
            MT_1005 = false;  //عدد العقود
            MT_1006 = false;  //عدد السيارات
            MT_1007 = false;  //عدد العقود المنتهية
            MT_1008 = false;  //عدد عقود التأجير
            MT_1009 = false;  //عدد العقود على وشك الإنتهاء
            MT_1010 = false;  //عدد عقود التأجير المنتهية
            MT_1011 = false;  //عدد عقود التأجير القائمة

            MT_1101 = false;  //عقد تأجير السيارات
            MT_1102 = false;  //عقد تأجير السيارات التفصيلي
            MT_1103 = false;  //عقود الشركات المساندات
            MT_1104 = false;  //عقد تبادل البيانات

            MT_1201 = false;  //تقارير إدارية مختصرة
            MT_1202 = false;  //تقارير مالية مختصرة
            MT_1203 = false;  //تقارير إدارية تفصيلية
            MT_1204 = false;  //تقارير مالية تفصيلية
            MT_1205 = false;  //تقارير إحصائية

            MT_1301 = false;  //
            MT_1302 = false;  //
            MT_1303 = false;  //
            MT_1304 = false;  //
            MT_1401 = false;  //
            MT_1402 = false;  //

            MT_1501 = false;  //
            MT_1502 = false;  //
            MT_1503 = false;  //
            MT_1504 = false;  //
            MT_1505 = false;  //
            MT_1506 = false;  //
            MT_1507 = false; //
            MT_1508 = false;  //
            MT_1509 = false;  //

            MT_1601 = false;
            MT_1602 = false;
            MT_1603 = false;
            MT_1604 = false;
            MT_1605 = false;

            MT_1701 = false;
            MT_1702 = false;

            MT_1801 = false;
            MT_1802 = false;
            MT_1803 = false;

            MT_1901 = false;
            MT_1902 = false;
            MT_1903 = false;
            MT_1904 = false;




            ST_1001 = false;  // دخول النظام الإداري للشركة
            ST_1002 = false;  //القائمة الرئيسية
            ST_1003 = false;  //تغيير كلمة السر
            ST_1004 = false;   //عدد الشركات 
            ST_1005 = false;   //عدد العقود
            ST_1006 = false;   //عدد السيارات
            ST_1007 = false;   //عدد العقود المنتهية
            ST_1008 = false;   //عدد عقود التأجير
            ST_1009 = false;   //عدد العقود على وشك الإنتهاء
            ST_1010 = false;   //عدد عقود التأجير المنتهية
            ST_1011 = false;   //عدد عقود التأجير القائمة

            ST_1101 = false;   //عقد تأجير السيارات
            ST_1102 = false;   //عقد تأجير السيارات التفصيلي
            ST_1103 = false;   //عقود الشركات المساندات
            ST_1104 = false;   //عقد تبادل البيانات

            ST_1201 = false;   //تقارير إدارية مختصرة
            ST_1202 = false;   //تقارير مالية مختصرة
            ST_1203 = false;   //تقارير إدارية تفصيلية
            ST_1204 = false;   //تقارير مالية تفصيلية
            ST_1205 = false;   //تقارير إحصائية

            ST_1301 = false;   //
            ST_1302 = false;   //
            ST_1303 = false;   //f
            ST_1304 = false;  //f

            ST_1401 = false;  //f
            ST_1402 = false;  //f

            ST_1501 = false;  //
            ST_1502 = false;  //
            ST_1503 = false;  //
            ST_1504 = false;  //
            ST_1505 = false;  //
            ST_1506 = false;  //
            ST_1507 = false;  //
            ST_1508 = false;  //
            ST_1509 = false;  //

            ST_1601 = false;
            ST_1602 = false;
            ST_1603 = false;
            ST_1604 = false;
            ST_1605 = false;

            ST_1701 = false;
            ST_1702 = false;

            ST_1801 = false;
            ST_1802 = false;
            ST_1803 = false;

            ST_1901 = false;
            ST_1902 = false;
            ST_1903 = false;
            ST_1904 = false;


            ST_1101_insert = false;
            ST_1101_update = false;
            ST_1101_delete = false;
            ST_1101_undelete = false;
            ST_1101_hold = false;
            ST_1101_unhold = false;
            ST_1101_print = false;

            ST_1102_insert = false;
            ST_1102_update = false;
            ST_1102_delete = false;
            ST_1102_undelete = false;
            ST_1102_hold = false;
            ST_1102_unhold = false;
            ST_1102_print = false;

            ST_1103_insert = false;
            ST_1103_update = false;
            ST_1103_delete = false;
            ST_1103_undelete = false;
            ST_1103_hold = false;
            ST_1103_unhold = false;
            ST_1103_print = false;

            ST_1104_insert = false;
            ST_1104_update = false;
            ST_1104_delete = false;
            ST_1104_undelete = false;
            ST_1104_hold = false;
            ST_1104_unhold = false;
            ST_1104_print = false;

            ST_1201_insert = false;
            ST_1201_update = false;
            ST_1201_delete = false;
            ST_1201_undelete = false;
            ST_1201_hold = false;
            ST_1201_unhold = false;
            ST_1201_print = false;

            ST_1202_insert = false;
            ST_1202_update = false;
            ST_1202_delete = false;
            ST_1202_undelete = false;
            ST_1202_hold = false;
            ST_1202_unhold = false;
            ST_1202_print = false;

            ST_1203_insert = false;
            ST_1203_update = false;
            ST_1203_delete = false;
            ST_1203_undelete = false;
            ST_1203_hold = false;
            ST_1203_unhold = false;
            ST_1203_print = false;

            ST_1204_insert = false;
            ST_1204_update = false;
            ST_1204_delete = false;
            ST_1204_undelete = false;
            ST_1204_hold = false;
            ST_1204_unhold = false;
            ST_1204_print = false;

            ST_1205_insert = false;
            ST_1205_update = false;
            ST_1205_delete = false;
            ST_1205_undelete = false;
            ST_1205_hold = false;
            ST_1205_unhold = false;
            ST_1205_print = false;

            ST_1301_insert = false;
            ST_1301_update = false;
            ST_1301_delete = false;
            ST_1301_undelete = false;
            ST_1301_hold = false;
            ST_1301_unhold = false;
            ST_1301_print = false;

            ST_1302_insert = false;
            ST_1302_update = false;
            ST_1302_delete = false;
            ST_1302_undelete = false;
            ST_1302_hold = false;
            ST_1302_unhold = false;
            ST_1302_print = false;

            ST_1303_insert = false;
            ST_1303_update = false;
            ST_1303_delete = false;
            ST_1303_undelete = false;
            ST_1303_hold = false;
            ST_1303_unhold = false;
            ST_1303_print = false;

            ST_1304_insert = false;
            ST_1304_update = false;
            ST_1304_delete = false;
            ST_1304_undelete = false;
            ST_1304_hold = false;
            ST_1304_unhold = false;
            ST_1304_print = false;


            ST_1401_insert = false;
            ST_1401_update = false;
            ST_1401_delete = false;
            ST_1401_undelete = false;
            ST_1401_hold = false;
            ST_1401_unhold = false;
            ST_1401_print = false;

            ST_1402_insert = false;
            ST_1402_update = false;
            ST_1402_delete = false;
            ST_1402_undelete = false;
            ST_1402_hold = false;
            ST_1402_unhold = false;
            ST_1402_print = false;


            ST_1501_insert = false;
            ST_1501_update = false;
            ST_1501_delete = false;
            ST_1501_undelete = false;
            ST_1501_hold = false;
            ST_1501_unhold = false;
            ST_1501_print = false;

            ST_1502_insert = false;
            ST_1502_update = false;
            ST_1502_delete = false;
            ST_1502_undelete = false;
            ST_1502_hold = false;
            ST_1502_unhold = false;
            ST_1502_print = false;

            ST_1503_insert = false;
            ST_1503_update = false;
            ST_1503_delete = false;
            ST_1503_undelete = false;
            ST_1503_hold = false;
            ST_1503_unhold = false;
            ST_1503_print = false;

            ST_1504_insert = false;
            ST_1504_update = false;
            ST_1504_delete = false;
            ST_1504_undelete = false;
            ST_1504_hold = false;
            ST_1504_unhold = false;
            ST_1504_print = false;

            ST_1505_insert = false;
            ST_1505_update = false;
            ST_1505_delete = false;
            ST_1505_undelete = false;
            ST_1505_hold = false;
            ST_1505_unhold = false;
            ST_1505_print = false;

            ST_1506_insert = false;
            ST_1506_update = false;
            ST_1506_delete = false;
            ST_1506_undelete = false;
            ST_1506_hold = false;
            ST_1506_unhold = false;
            ST_1506_print = false;

            ST_1507_insert = false;
            ST_1507_update = false;
            ST_1507_delete = false;
            ST_1507_undelete = false;
            ST_1507_hold = false;
            ST_1507_unhold = false;
            ST_1507_print = false;

            ST_1508_insert = false;
            ST_1508_update = false;
            ST_1508_delete = false;
            ST_1508_undelete = false;
            ST_1508_hold = false;
            ST_1508_unhold = false;
            ST_1508_print = false;

            ST_1509_insert = false;
            ST_1509_update = false;
            ST_1509_delete = false;
            ST_1509_undelete = false;
            ST_1509_hold = false;
            ST_1509_unhold = false;
            ST_1509_print = false;

            ST_1601_insert = false;
            ST_1601_update = false;
            ST_1601_delete = false;
            ST_1601_undelete = false;
            ST_1601_hold = false;
            ST_1601_unhold = false;
            ST_1601_print = false;

            ST_1602_insert = false;
            ST_1602_update = false;
            ST_1602_delete = false;
            ST_1602_undelete = false;
            ST_1602_hold = false;
            ST_1602_unhold = false;
            ST_1602_print = false;

            ST_1603_insert = false;
            ST_1603_update = false;
            ST_1603_delete = false;
            ST_1603_undelete = false;
            ST_1603_hold = false;
            ST_1603_unhold = false;
            ST_1603_print = false;

            ST_1604_insert = false;
            ST_1604_update = false;
            ST_1604_delete = false;
            ST_1604_undelete = false;
            ST_1604_hold = false;
            ST_1604_unhold = false;
            ST_1604_print = false;

            ST_1605_insert = false;
            ST_1605_update = false;
            ST_1605_delete = false;
            ST_1605_undelete = false;
            ST_1605_hold = false;
            ST_1605_unhold = false;
            ST_1605_print = false;

            ST_1701_insert = false;
            ST_1701_update = false;
            ST_1701_delete = false;
            ST_1701_undelete = false;
            ST_1701_hold = false;
            ST_1701_unhold = false;
            ST_1701_print = false;

            ST_1702_insert = false;
            ST_1702_update = false;
            ST_1702_delete = false;
            ST_1702_undelete = false;
            ST_1702_hold = false;
            ST_1702_unhold = false;
            ST_1702_print = false;

            ST_1801_insert = false;
            ST_1801_update = false;
            ST_1801_delete = false;
            ST_1801_undelete = false;
            ST_1801_hold = false;
            ST_1801_unhold = false;
            ST_1801_print = false;

            ST_1802_insert = false;
            ST_1802_update = false;
            ST_1802_delete = false;
            ST_1802_undelete = false;
            ST_1802_hold = false;
            ST_1802_unhold = false;
            ST_1802_print = false;

            ST_1803_insert = false;
            ST_1803_update = false;
            ST_1803_delete = false;
            ST_1803_undelete = false;
            ST_1803_hold = false;
            ST_1803_unhold = false;
            ST_1803_print = false;

            ST_1901_insert = false;
            ST_1901_update = false;
            ST_1901_delete = false;
            ST_1901_undelete = false;
            ST_1901_hold = false;
            ST_1901_unhold = false;
            ST_1901_print = false;

            ST_1902_insert = false;
            ST_1902_update = false;
            ST_1902_delete = false;
            ST_1902_undelete = false;
            ST_1902_hold = false;
            ST_1902_unhold = false;
            ST_1902_print = false;

            ST_1903_insert = false;
            ST_1903_update = false;
            ST_1903_delete = false;
            ST_1903_undelete = false;
            ST_1903_hold = false;
            ST_1903_unhold = false;
            ST_1903_print = false;

            ST_1904_insert = false;
            ST_1904_update = false;
            ST_1904_delete = false;
            ST_1904_undelete = false;
            ST_1904_hold = false;
            ST_1904_unhold = false;
            ST_1904_print = false;
        }
        
    }
}