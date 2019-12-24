using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using VKHGDDTNN.Models;
using System.Data.Entity;

namespace VKHGDDTNN.Controllers
{
    public class AdminController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();
        #region[Mặc định (Default)]
        public ActionResult Default()
        {
            if ((Request.Cookies["Username"] != null) && (Session["Member"] != null))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion   
     
        #region[Cấu hình (ConfigEditor)]
        public ActionResult CauHinh()
        {
            var config = db.Configs.FirstOrDefault();
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                if (config != null) { return View(config); }         
            }
            else { return RedirectToAction("Default", "Admin"); }
            return View();
        }
        #endregion

        #region[Cấu hình (ConfigEditor)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CauHinh(FormCollection collection)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var config = db.Configs.FirstOrDefault();
                var Contact = collection["Contact"];
                //var Mail_Port = collection["Mail_Port"];
                //var Mail_Info = collection["Mail_Info"];
                //var Mail_Noreply = collection["Mail_Noreply"];
                //var Mail_Password = collection["Mail_Password"];

                var Copyright = collection["Copyright"];
                var Payment = collection["Payment"];
                var Warning = collection["Warning"];
                //var GoogleMap = collection["GoogleMap"];
                //var Video = collection["Video"];

                var Title = collection["Title"];
                var Description = collection["Description"];
                var Keyword = collection["Keyword"];

                if (config != null)
                {
                    config.Contact = Contact;
                    //config.Mail_Port = short.Parse(Mail_Port);
                    //config.Mail_Info = Mail_Info;
                    //config.Mail_Noreply = Mail_Noreply;
                    //config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Payment = Payment;
                    config.Warning = Warning;
                    //config.GoogleMap = GoogleMap;
                    //config.Video = Video;

                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;
                    UpdateModel(config);
                    db.SaveChanges();
                    return RedirectToAction("CauHinh", "Admin");
                }
                else
                {
                    config = new Config();
                    config.Contact = Contact;
                    //config.Mail_Port = short.Parse(Mail_Port);
                    //config.Mail_Info = Mail_Info;
                    //config.Mail_Noreply = Mail_Noreply;
                    //config.Mail_Password = Mail_Password;
                    config.Copyright = Copyright;
                    config.Payment = Payment;
                    config.Warning = Warning;
                    config.Title = Title;
                    config.Description = Description;
                    config.Keyword = Keyword;
                    db.Entry(config).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("CauHinh", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }  
        }
        #endregion

        #region[Đăng nhập (Login)]
        public ActionResult Index()
        {
            if ((Request.Cookies["Username"] != null) && (Session["Member"] != null))
            {
                return RedirectToAction("Default");     
            }
            else
            {
                HttpCookie UserCookie = new HttpCookie("Username", "");
                UserCookie.Expires = DateTime.Now;
                Response.Cookies.Add(UserCookie);
                return View();
            }  
        }
        #endregion

        #region[Đăng nhập (Login)]
        [HttpPost]
        public ActionResult Index(FormCollection collect)
        {
            var user = collect["Username"];
            var pass = collect["Pass"];
            var list = db.Members.Where(n => n.Username == user && n.Password == pass).ToList();
            if (list.Count > 0)
            {
                HttpCookie UserCookie = new HttpCookie("Username");
                UserCookie.Values["UserNameText"] = user.ToString();
                UserCookie.Values["Permiss"] = list[0].Permiss.ToString();
                UserCookie.Values["PasswordText"] = pass.ToString();
                UserCookie.Values["FullName"] = Server.UrlEncode(list[0].Name.Trim());
                UserCookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(UserCookie);
                Session["Member"] = list;
                //Session["Member"] = UserCookie;           
                return RedirectToAction("Default", "Admin");
                //return RedirectToAction("Default", new { cookie = UserCookie });
            }
            else
            {
                ViewBag.Err = "Đăng nhập không thành công!";
                return View();
            }
        }
        #endregion

        #region[Đăng xuất (Logout)]
        public ActionResult DangXuat()
        {
            var cookie = new HttpCookie("Username");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);
            Session["Member"] = null;
            return RedirectToAction("Index");
        }
        #endregion
    }
}
