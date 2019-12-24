using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Data.Mapping;
using VKHGDDTNN.Models;
using VKHGDDTNN.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace VKHGDDTNN.Controllers
{
    public class HomeController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();
        public ActionResult Index()
        {
            #region[Title, Keyword, Deskription]
            var listconfig = (from p in db.Configs select p).ToList();
            if (listconfig.Count > 0) { ViewBag.tit = listconfig[0].Title; ViewBag.des = listconfig[0].Description; ViewBag.key = listconfig[0].Keyword; }
            listconfig.Clear();
            listconfig = null;
            #endregion    
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NewTab()
        {
            return View();
        }
    }
}