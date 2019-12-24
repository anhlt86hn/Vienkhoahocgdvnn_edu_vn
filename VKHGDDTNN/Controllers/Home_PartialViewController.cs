using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Linq;
using VKHGDDTNN.Models;
using VKHGDDTNN.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Net;
using PagedList;
using PagedList.Mvc;


namespace VKHGDDTNN.Controllers
{
    public class Home_PartialViewController : Controller
    {
        //
        // GET: /Home_PartialView/
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();

        public ActionResult Index()
        {
            return View();
        }

         public ActionResult Statistic()
        {
            ViewBag.PageView = HttpContext.Application["DaTruyCap"].ToString();
            ViewBag.Online = HttpContext.Application["DangTruyCap"].ToString();
            return PartialView();
        }

         
        public ActionResult Register()
        {

            return PartialView();
        }
        public ActionResult _Logo()
        {
            var img = db.Pictures.Where(p => p.Active == true && p.Position == 1).SingleOrDefault();
            string imgLink = img.Link;
            string imgSource = img.Image;
            string imgAlt = img.Name;
            ViewBag.ImgLink = imgLink;
            ViewBag.ImgSource = imgSource;
            ViewBag.ImgAlt = imgAlt;
            return PartialView();
        }
        #region[Slide]
        public ActionResult _MainSlide()
        {
            var sl = db.Pictures.Where(s => s.Active == true && s.Position == 3).OrderBy(s => s.Ord).ToList();
            string[] str = { 
                               //"cubeRandom", 
        //"block", "cubeStop", "cubeStopRandom", "cubeHide", "cubeSize", "fadeFour", "paralell", "blind", "blindHeight", "blindWidth", "directionTop", "directionBottom", "directionRight", "directionLeft", "cubeSpread", "glassCube", "glassBlock", "circlesRotate", "cubeShow", "upBars", "downBars", "hideBars", "swapBars", "swapBarsBack", "swapBlocks", "cut" };
       "block", "cubeStop", "cubeStopRandom", "cubeHide", "cubeSize", "fadeFour", "paralell", "blind", "blindHeight", "blindWidth", "directionTop", "directionBottom", "directionRight", "directionLeft", "cubeSpread", "glassCube", "glassBlock", "circlesRotate", "cubeShow", "upBars", "downBars", "hideBars", "swapBars", "swapBarsBack", "swapBlocks", "cut" };
            string str2 = "";
            for (int i = 0; i < sl.Count; i++)
            {
                str2 += "<li><a target=\'_blank\' href=\'" + sl[i].Link + "\'><img class=\"" + str[i].ToString() + "\" src=\"" + sl[i].Image + "\" title=\'\'></a><div class=\'label_text\'><div class=\'lablecontent\'><a href=\'\' class=\'name\'></a><span class=\'clear\'></span><p class=\'des\'></p><div class=\'clear\'></div></div></div></li>";
            }
            ViewBag.MainSlide = str2;
            return PartialView();
        }
        #endregion Slide
        #region[Latest News]
        public ActionResult _LatestNews()
        {
            string ln = "";
            string topNewsName = "";
            string topNewsLink = "";
            string topNewsDesc = "";
            string topNewsImg = "";
            var latest = db.News.Where(n => n.Active == true && n.Index == true).OrderByDescending(n => n.MDate).Take(10).ToList();

            for (int i = 0; i < latest.Count; i++)
            {
                if (i == 0)
                {
                    topNewsName = latest[i].Name;
                    topNewsLink = latest[i].Tag;
                    topNewsDesc = latest[i].Content;
                    topNewsImg = latest[i].Image;
                }
                else
                {
                    ln += "<li><a href=\"/" + latest[i].Tag + "\" title=\"" + latest[i].Title + "\">" + latest[i].Name + "</a><span>  (" + latest[i].MDate.ToShortDateString() + ")</span></li>";
                }
            }
            ViewBag.LatestNews = ln;
            ViewBag.TopNewsName = topNewsName;
            ViewBag.TopNewsLink = topNewsLink;
            ViewBag.TopNewsDesc = topNewsDesc;
            ViewBag.TopNewsImg = topNewsImg;
            latest.Clear();
            return PartialView();
        }
        #endregion

        #region[Training]
        public ActionResult _Training()
        {
            var rootGroupNews = db.GroupNews.Where(r => r.Active == true && r.Index == true && r.Name == "Đào tạo").SingleOrDefault();
            var menu = db.Menus.Where(m => m.Active == true && m.Tag == "dao-tao").SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            string rootLevel = rootGroupNews.Level;
            string newsList = "";
            string groupNewsList = "";
            string topNewsLink = "";
            string topNewsName = "";
            string topNewsDesc = "";
            string topNewsImg = "";
            string groupNewsIDs = "";
            string strNews = "";

            List<News> list = new List<News>();

            var unsortedNews = db.News.Where(n => n.Active == true && n.Index == true).OrderByDescending(n => n.SDate).ToList();
            var group = db.GroupNews.Where(g => g.Active == true & g.Index == true && g.Level.Length == 10 && g.Level.Substring(0, 5) == rootLevel).OrderBy(g => g.Ord).ToList();
            for (int i = 0; i < group.Count; i++)
            {
                groupNewsIDs += group[i].Id.ToString();
                groupNewsList += "<li><a href=\"/menu-con/" + group[i].Tag + "\" title=\"" + group[i].Title + "\">" + group[i].Name + "</a></li>";

            }
            for (int j = 0; j < unsortedNews.Count; j++)
            {
                strNews = unsortedNews[j].IdGroup.ToString();
                if (groupNewsIDs.Contains(strNews))
                {
                    list.Add(unsortedNews[j]);
                }
            }
            list=list.OrderByDescending(l => l.MDate).Take(4).ToList();
            for (int k = 0; k < list.Count; k++)
            {
                if (k == 0)
                {
                    topNewsName = list[k].Name;
                    topNewsLink = list[k].Tag;
                    topNewsDesc = list[k].Content;
                    topNewsImg = list[k].Image;
                }
                else
                {
                    newsList += "<li><a href=\"/" + list[k].Tag + "\" title=\"" + list[k].Title + "\">" + list[k].Name + "</a><span>  (" + list[k].MDate.ToShortDateString() + ")</span></li>";
                }
            }
            ViewBag.TopNewsName = topNewsName;
            ViewBag.TopNewsLink = topNewsLink;
            ViewBag.TopNewsDesc = topNewsDesc;
            ViewBag.TopNewsImg = topNewsImg;
            ViewBag.NewsList1 = newsList;
            ViewBag.GroupNewsList = groupNewsList;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            unsortedNews.Clear();
            unsortedNews = null;
            list.Clear();
            list = null;
            newsList = null;
            return PartialView();
        }
        #endregion


        #region[Admissions]
        public ActionResult _Admissions()
        {
            var rootGroupNews = db.GroupNews.Where(r => r.Active == true && r.Index == true && r.Name == "Tuyển sinh").SingleOrDefault();
            var menu = db.Menus.Where(m => m.Active == true && m.Tag == "tuyen-sinh").SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            string rootLevel = rootGroupNews.Level;
            string newsList = "";
            string groupNewsList = "";
            string topNewsLink = "";
            string topNewsName = "";
            string topNewsDesc = "";
            string topNewsImg = "";
            string groupNewsIDs = "";
            string strNews = "";

            List<News> list = new List<News>();

            var unsortedNews = db.News.Where(n => n.Active == true && n.Index == true).OrderByDescending(n => n.SDate).ToList();
            var group = db.GroupNews.Where(g => g.Active == true & g.Index == true && g.Level.Length == 10 && g.Level.Substring(0, 5) == rootLevel).OrderBy(g => g.Ord).ToList();
            for (int i = 0; i < group.Count; i++)
            {
                groupNewsIDs += group[i].Id.ToString();
                groupNewsList += "<li><a href=\"/menu-con/" + group[i].Tag + "\" title=\"" + group[i].Title + "\">" + group[i].Name + "</a></li>";

            }
            for (int j = 0; j < unsortedNews.Count; j++)
            {
                strNews = unsortedNews[j].IdGroup.ToString();
                if (groupNewsIDs.Contains(strNews))
                {
                    list.Add(unsortedNews[j]);
                }
            }
            list=list.OrderByDescending(l => l.MDate).Take(4).ToList();
            for (int k = 0; k < list.Count; k++)
            {
                if (k == 0)
                {
                    topNewsName = list[k].Name;
                    topNewsLink = list[k].Tag;
                    topNewsDesc = list[k].Content;
                    topNewsImg = list[k].Image;
                }
                else
                {
                    newsList += "<li><a href=\"/" + list[k].Tag + "\" title=\"" + list[k].Title + "\">" + list[k].Name + "</a><span>  (" + list[k].MDate.ToShortDateString() + ")</span></li>";
                }
            }
            ViewBag.TopNewsName = topNewsName;
            ViewBag.TopNewsLink = topNewsLink;
            ViewBag.TopNewsDesc = topNewsDesc;
            ViewBag.TopNewsImg = topNewsImg;
            ViewBag.NewsList2 = newsList;
            ViewBag.GroupNewsList = groupNewsList;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            unsortedNews.Clear();
            unsortedNews = null;
            list.Clear();
            list = null;
            newsList = null;
            return PartialView();
        }
        #endregion

       

        #region[Announcement]
        public ActionResult _Announcement()
        {
            string l = "";
            var group = db.GroupNews.Where(g => g.Name == ("Sức khỏe học đường")).SingleOrDefault();
            string gTag=group.Tag;
            var menu = db.Menus.Where(m => m.Tag == gTag).SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            int id = group.Id;
            var news = db.News.Where(n => n.Active == true && n.Index == true && n.IdGroup ==id).OrderByDescending(n => n.SDate).Take(10).ToList();

            for (int i = 0; i < news.Count; i++)
            {
                l += "<li><a href=\"/" + news[i].Tag + "\" title=\"" + news[i].Title + "\">" + news[i].Name + "</a></li>";
            }
            ViewBag.Announcement = l;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            return PartialView();
        }
        #endregion

        #region[Announcement]
        public ActionResult _Announcement2()
        {
            string l = "";
            var group = db.GroupNews.Where(g => g.Name == ("Tin giáo dục trong nước")).SingleOrDefault();
            string gTag = group.Tag;
            var menu = db.Menus.Where(m => m.Tag == gTag).SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            int id = group.Id;
            var news = db.News.Where(n => n.Active == true && n.Index == true && n.IdGroup == id).OrderByDescending(n => n.SDate).Take(10).ToList();

            for (int i = 0; i < news.Count; i++)
            {
                l += "<li><a href=\"/" + news[i].Tag + "\" title=\"" + news[i].Title + "\">" + news[i].Name + "</a></li>";
            }
            ViewBag.Announcement = l;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            return PartialView();
        }
        #endregion

        #region[Announcement]
        public ActionResult _Announcement3()
        {
            string l = "";
            var group = db.GroupNews.Where(g => g.Name == ("Tin tức xã hội")).SingleOrDefault();
            string gTag = group.Tag;
            var menu = db.Menus.Where(m => m.Tag == gTag).SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            int id = group.Id;
            var news = db.News.Where(n => n.Active == true && n.Index == true && n.IdGroup == id).OrderByDescending(n => n.SDate).Take(10).ToList();

            for (int i = 0; i < news.Count; i++)
            {
                l += "<li><a href=\"/" + news[i].Tag + "\" title=\"" + news[i].Title + "\">" + news[i].Name + "</a></li>";
            }
            ViewBag.Announcement = l;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            return PartialView();
        }
        #endregion

        #region[Announcement]
        public ActionResult _Announcement4()
        {
            string l = "";
            var group = db.GroupNews.Where(g => g.Name == ("Hướng nghiệp")).SingleOrDefault();
            string gTag = group.Tag;
            var menu = db.Menus.Where(m => m.Tag == gTag).SingleOrDefault();
            string menuName = menu.Name;
            string menuLink = menu.Link;
            int id = group.Id;
            var news = db.News.Where(n => n.Active == true && n.Index == true && n.IdGroup == id).OrderByDescending(n => n.SDate).Take(10).ToList();

            for (int i = 0; i < news.Count; i++)
            {
                l += "<li><a href=\"/" + news[i].Tag + "\" title=\"" + news[i].Title + "\">" + news[i].Name + "</a></li>";
            }
            ViewBag.Announcement = l;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            return PartialView();
        }
        #endregion

        #region[Main Menu]
        public ActionResult _MainMenu()
        {
            string menu = "";
            var cat = db.Menus.Where(c => c.Active == true && c.Level.Length == 5 && c.Position == 1 || c.Position == 4).OrderBy(c => c.Ord).ToList();
            for (int i = 0; i < cat.Count; i++)
            {
                List<Menu> menus = db.Menus.ToList();
                List<Menu> catsub = new List<Menu>();
                string levelm = cat[i].Level;
                catsub = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == levelm && m.Active == true).OrderBy(m => m.Level).ToList();
                if (catsub.Count > 0)
                {
                    if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/"))
                        //if(Request.Url.ToString()=="")
                    {
                        if (i == 0)
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                            //chuoi += "<li style=\"background: none\"><a class=\"active\" href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a>";
                        }
                        else
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                            //chuoi += "<li style=\"background: none\"><a href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a>";
                        }
                        else
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                        }
                    }
                    menu += "<ul class=\"sub-menu\">";
                    for (int k = 0; k < catsub.Count; k++)
                    {
                        string levelm10 = catsub[k].Level;
                        List<Menu> catsub10 = new List<Menu>();
                        catsub10 = db.Menus.Where(m => m.Level.Length == 15 && m.Level.Substring(0, 10) == levelm10 && m.Active == true).OrderBy(m => m.Level).ToList();
                        if (catsub10.Count == 0)
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a></li>";
                        }
                        else
                        {
                            menu += "<li class=\"sub\"><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a>";
                            menu += "<ul>";
                            for (int n = 0; n < catsub10.Count; n++)
                            {
                                string levelm15 = catsub10[n].Level;
                                List<Menu> catsub15 = new List<Menu>();
                                catsub15 = db.Menus.Where(m => m.Level.Length == 20 && m.Level.Substring(0, 15) == levelm15 && m.Active == true).OrderBy(m => m.Level).ToList();
                                if (catsub15.Count == 0)
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a></li>";
                                }
                                else
                                {
                                    menu += "<li class=\"sub\"><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a>";
                                    menu += "<ul>";
                                    for (int m = 0; m < catsub15.Count; m++)
                                    {
                                        menu += "<li><a href=\"" + catsub15[m].Link + "\"><span>" + catsub15[m].Name + "</span></a></li>";
                                    }
                                    menu += "</ul></li>";
                                }
                            }
                            menu += "</ul></li>";
                        }
                    }
                    menu += "</ul></li>";
                }
                else
                {
                    if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/"))
                    //if ((Request.Url.ToString().IndexOf(cat[i].Link) > 0 && cat[i].Link != "/") || (cat[i].Link == "/" && (Request.Url.ToString() == "http://localhost:1584/" || Request.Url.ToString() == "http://ilovestyle.vn/")))
                    {
                        if (i == 0)
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                            //chuoi += "<li style=\"background: none\"><a class=\"active\" href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a></li>";
                        }
                        else
                        {

                            menu += "<li><a class=\"active\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                            //chuoi += "<li style=\"background: none\"><a href=\"" + cat[i].Link + "\">" + cat[i].Name + "</a></li>";
                        }
                        else
                        {

                            menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                        }
                    }
                }
            }
            ViewBag.Menu = menu;
            cat.Clear();
            cat = null;
            return PartialView();
        }
        #endregion

        #region[Left Menu 1]
        public ActionResult _LeftMenu1()
        {
            string menu = "";
            var cat = db.Menus.Where(c => c.Active == true && c.Level.Length == 5 && c.Tag=="dao-tao").OrderBy(c => c.Ord).Single();

                List<Menu> menus = db.Menus.ToList();
                List<Menu> catsub = new List<Menu>();
                string levelm = cat.Level;
                catsub = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == levelm && m.Active == true).OrderBy(m => m.Level).ToList();
                if (catsub.Count > 0)
                {

                    //menu += "<li><a class=\"has-submenu\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                    //menu += "<ul>";
                    for (int k = 0; k < catsub.Count; k++)
                    {
                        string levelm10 = catsub[k].Level;
                        List<Menu> catsub10 = new List<Menu>();
                        catsub10 = db.Menus.Where(m => m.Level.Length == 15 && m.Level.Substring(0, 10) == levelm10 && m.Active == true).OrderBy(m => m.Level).ToList();
                        if (catsub10.Count == 0)
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a></li>";
                        }
                        else
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a>";
                            menu += "<ul>";
                            for (int n = 0; n < catsub10.Count; n++)
                            {
                                string levelm15 = catsub10[n].Level;
                                List<Menu> catsub15 = new List<Menu>();
                                catsub15 = db.Menus.Where(m => m.Level.Length == 20 && m.Level.Substring(0, 15) == levelm15 && m.Active == true).OrderBy(m => m.Level).ToList();
                                if (catsub15.Count == 0)
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a></li>";
                                }
                                else
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a>";
                                    menu += "<ul>";
                                    for (int m = 0; m < catsub15.Count; m++)
                                    {
                                        menu += "<li><a href=\"" + catsub15[m].Link + "\"><span>" + catsub15[m].Name + "</span></a></li>";
                                    }
                                    menu += "</ul></li>";
                                }
                            }
                            menu += "</ul></li>";
                        }
                    }
                    //menu += "</ul></li>";
                }
                //else
                //{
                //    menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                //}
            ViewBag.LeftMenu1 = menu;
            //cat.Clear();
            //cat = null;
            return PartialView();
        }
        #endregion

        #region[Left Menu 2]
        public ActionResult _LeftMenu2()
        {
            string menu = "";
            var cat = db.Menus.Where(c => c.Active == true && c.Level.Length == 5 && c.Tag == "tuyen-sinh").OrderBy(c => c.Ord).Single();

            List<Menu> menus = db.Menus.ToList();
            List<Menu> catsub = new List<Menu>();
            string levelm = cat.Level;
            catsub = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == levelm && m.Active == true).OrderBy(m => m.Level).ToList();
            if (catsub.Count > 0)
            {

                //menu += "<li><a class=\"has-submenu\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                //menu += "<ul>";
                for (int k = 0; k < catsub.Count; k++)
                {
                    string levelm10 = catsub[k].Level;
                    List<Menu> catsub10 = new List<Menu>();
                    catsub10 = db.Menus.Where(m => m.Level.Length == 15 && m.Level.Substring(0, 10) == levelm10 && m.Active == true).OrderBy(m => m.Level).ToList();
                    if (catsub10.Count == 0)
                    {
                        menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a></li>";
                    }
                    else
                    {
                        menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a>";
                        menu += "<ul>";
                        for (int n = 0; n < catsub10.Count; n++)
                        {
                            string levelm15 = catsub10[n].Level;
                            List<Menu> catsub15 = new List<Menu>();
                            catsub15 = db.Menus.Where(m => m.Level.Length == 20 && m.Level.Substring(0, 15) == levelm15 && m.Active == true).OrderBy(m => m.Level).ToList();
                            if (catsub15.Count == 0)
                            {
                                menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a></li>";
                            }
                            else
                            {
                                menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a>";
                                menu += "<ul>";
                                for (int m = 0; m < catsub15.Count; m++)
                                {
                                    menu += "<li><a href=\"" + catsub15[m].Link + "\"><span>" + catsub15[m].Name + "</span></a></li>";
                                }
                                menu += "</ul></li>";
                            }
                        }
                        menu += "</ul></li>";
                    }
                }
                //menu += "</ul></li>";
            }
            //else
            //{
            //    menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
            //}
            ViewBag.LeftMenu2 = menu;
            //cat.Clear();
            //cat = null;
            return PartialView();
        }
        #endregion

        #region[Right Menu]
        public ActionResult _RightMenu()
        {
            string menu = "";
            var cat = db.Menus.Where(c => c.Active == true && c.Level.Length == 5 && c.Position==3 || c.Position==4).OrderBy(c => c.Ord).ToList();
            for (int i = 0; i < cat.Count; i++)
            {
                List<Menu> menus = db.Menus.ToList();
                List<Menu> catsub = new List<Menu>();
                string levelm = cat[i].Level;
                catsub = db.Menus.Where(m => m.Level.Length == 10 && m.Level.Substring(0, 5) == levelm && m.Active == true).OrderBy(m => m.Level).ToList();
                if (catsub.Count > 0)
                {
                  
                    menu += "<li><a class=\"has-submenu\" href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a>";
                    menu += "<ul>";
                    for (int k = 0; k < catsub.Count; k++)
                    {
                        string levelm10 = catsub[k].Level;
                        List<Menu> catsub10 = new List<Menu>();
                        catsub10 = db.Menus.Where(m => m.Level.Length == 15 && m.Level.Substring(0, 10) == levelm10 && m.Active == true).OrderBy(m => m.Level).ToList();
                        if (catsub10.Count == 0)
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a></li>";
                        }
                        else
                        {
                            menu += "<li><a href=\"" + catsub[k].Link + "\"><span>" + catsub[k].Name + "</span></a>";
                            menu += "<ul>";
                            for (int n = 0; n < catsub10.Count; n++)
                            {
                                string levelm15 = catsub10[n].Level;
                                List<Menu> catsub15 = new List<Menu>();
                                catsub15 = db.Menus.Where(m => m.Level.Length == 20 && m.Level.Substring(0, 15) == levelm15 && m.Active == true).OrderBy(m => m.Level).ToList();
                                if (catsub15.Count == 0)
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a></li>";
                                }
                                else
                                {
                                    menu += "<li><a href=\"" + catsub10[n].Link + "\"><span>" + catsub10[n].Name + "</span></a>";
                                    menu += "<ul>";
                                    for (int m = 0; m < catsub15.Count; m++)
                                    {
                                        menu += "<li><a href=\"" + catsub15[m].Link + "\"><span>" + catsub15[m].Name + "</span></a></li>";
                                    }
                                    menu += "</ul></li>";
                                }
                            }
                            menu += "</ul></li>";
                        }
                    }
                    menu += "</ul></li>";
                }
                else
                {
                    menu += "<li><a href=\"" + cat[i].Link + "\"><span>" + cat[i].Name + "</span></a></li>";
                }
            }
            ViewBag.RightMenu = menu;
            cat.Clear();
            cat = null;
            return PartialView();
        }
        #endregion

       



        //#region[Thực đơn]
        //public ActionResult _ThucDon()
        //{
        //    var pro = db.Products.Where(p => p.Active == true && p.Index == true).OrderByDescending(p => p.Ord).ToList();
       
        //        return PartialView(pro);
        //}
        //#endregion

        #region[Thực đơn]
        public ActionResult _ThucDon()
        {
            var pro = db.Products.Where(p => p.Active == true && p.Index == true).OrderByDescending(p => p.Ord).ToList();
            string str = "";
            for (int i = 0; i < pro.Count; i++)
            {
                str += "<li class=\'item\'>";                
                str += "<section class=\'imgframe\'><a class=\'image\' href=\'\' title='A'><img alt=\"" + pro[i].Name + "\" class=\"\" src=\"" + pro[i].Image + "\" /></a></section>";
                str += "<section id=\"LineBottomProductHome\"></section>";
                str += "<a class=\'name\' href=\"/" + pro[i].Tag + "\" title=\"" + pro[i].Name + "\"><p id=\"itemname\">" + pro[i].Name + "</p></a>";
                //str += "<section class=\"itemdes\"><a class=\'name\' href=\"/" + pro[i].Tag + "\" title=\"" + pro[i].Name + "\"><p id=\"itemname\">" + pro[i].Name + "</p></a>";
                double checkPrice=0;
                if (double.TryParse(pro[i].PricePromotion, out checkPrice))
                {
                    str += "<p id=\"itemprice\">Giá: " + pro[i].PricePromotion + " VNĐ</p>";
                    //str += "<p id=\"itemprice\">Giá: " + pro[i].PricePromotion + " VNĐ</p></section>";
                }
                else
                {
                    str += "<p id=\"itemprice\">Giá: " + pro[i].PricePromotion + "</p>";
                    //str += "<p id=\"itemprice\">Giá: " + pro[i].PricePromotion + " VNĐ</p></section>";
                }
                
                str += "</li>";
            }
            ViewBag.ThucDon = str;
            return PartialView();
        }
        #endregion

        #region quang cao
        public ActionResult _QuangCao()
        {
            return PartialView();
        }
        #endregion

        #region gioi thieu
        public ActionResult _GioiThieu()
        {
            var group=db.GroupNews.Where(g=>g.Active==true&&g.Name=="Giới thiệu").Single();
            int gId=group.Id;
            var article = db.News.Where(n => n.Active == true && n.Index == true && n.IdGroup == gId).FirstOrDefault();
            string name = article.Name;
            string content = article.Content;
            string image = article.Image;
            string detail = article.Detail;
            ViewBag.Name = name;
            ViewBag.Content = content;
            ViewBag.Image = image;
            ViewBag.Detail = detail;
            return PartialView();
        }
        #endregion

        #region bo suu tap
        public ActionResult _BoSuuTap()
        {
            var group = db.GroupPictures.Where(g => g.Active == true && g.Name == "Giới thiệu").Single();
            return PartialView();
        }
        #endregion

        #region doi tac
        public ActionResult _DoiTac()
        {
            return PartialView();
        }
        #endregion

        //#region[ListNewsHome]
        //public ActionResult _ListNewsHome(string tag)
        //{
        //    var rootGroupNews = db.GroupNews.Where(r => r.Active == true && r.Index == true && r.Tag == tag).SingleOrDefault();
        //    var menu = db.Menus.Where(m => m.Active == true && m.Tag == tag).SingleOrDefault();
        //    string menuName = menu.Name;
        //    string menuLink = menu.Link;
        //    string rootLevel = rootGroupNews.Level;
        //    string newsList = "";
        //    string groupNewsList = "";
        //    string topNewsLink = "";
        //    string topNewsName = "";
        //    string topNewsDesc = "";
        //    string groupNewsIDs = "";
        //    List<News> list = new List<News>();
        //    var group = db.GroupNews.Where(g => g.Active == true & g.Index == true && g.Level.Length == 10 && g.Level.Substring(0, 5) == rootLevel).OrderBy(g => g.Ord).ToList();
        //    for (int i = 0; i < group.Count; i++)
        //    {
        //        groupNewsIDs += group[i].Id.ToString();
        //        groupNewsList += "<li><a href=\"/danh-muc-tin/" + group[i].Tag + "\" title=\"" + group[i].Title + "\">" + group[i].Name + "</a></li>";
        //        var unsortedNews = db.News.Where(n => n.Active == true && n.Index == true).ToList();
        //        for (int j = 0; j < unsortedNews.Count; j++)
        //        {
        //            if (unsortedNews[j].IdGroup.ToString().Contains(groupNewsIDs))
        //            {
        //                list.Add(unsortedNews[j]);
        //            }
        //        }
        //    }
        //    list = (from n in db.News where n.Active == true && n.Index == true orderby n.MDate descending select n).ToList();
        //    for (int k = 0; k < list.Count; k++)
        //    {
        //        if (k == 0)
        //        {
        //            topNewsName = list[k].Name;
        //            topNewsLink = list[k].Tag;
        //            topNewsDesc = list[k].Content;
        //        }
        //        else
        //        {
        //            newsList += "<li><a href=\"/" + list[k].Tag + "\" title=\"" + list[k].Title + "\">" + list[k].Name + "</a></li>";
        //        }
        //    }
        //    ViewBag.TopNewsName = topNewsName;
        //    ViewBag.TopNewsLink = topNewsLink;
        //    ViewBag.TopNewsDesc = topNewsDesc;
        //    ViewBag.NewsList = newsList;
        //    ViewBag.GroupNewsList = groupNewsList;
        //    ViewBag.MenuName = menuName;
        //    ViewBag.MenuLink = menuLink;
        //    return PartialView();
        //}
        //#endregion

    }
}
