using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Linq;
using VKHGDDTNN.Models;
using PagedList;
using PagedList.Mvc;

namespace VKHGDDTNN.Controllers.NewsView
{
    public class NewsViewController : Controller
    {
        VKHGDDTNNEntities data = new VKHGDDTNNEntities();
        #region[Danh sách tin tức]
        public ActionResult List(int? page, string tag)
        {
            if (Request.HttpMethod == "GET")
            {
            }
            else
            {
                page = 1;
            }

            #region[Phân trang]
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            // Thiết lập phân trang
            PagedListRenderOptions ship = new PagedListRenderOptions();

            ship.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToLastPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToNextPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToIndividualPages = true;
            ship.DisplayPageCountAndCurrentLocation = false;
            ship.MaximumPageNumbersToDisplay = 5;
            ship.DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            ship.EllipsesFormat = "&#8230;";
            ship.LinkToFirstPageFormat = "Trang đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Trang cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "pagination" };
            ship.LiElementClasses = Enumerable.Empty<string>();

            ViewBag.ship = ship;
            #endregion

            var rootGroupNews = data.GroupNews.Where(r => r.Active == true && r.Index == true && r.Tag == tag).SingleOrDefault();
            var menu = data.Menus.Where(m => m.Active == true && m.Tag == tag).SingleOrDefault();

            //string rootMenuLevel = menu.Level;
            string menuName = menu.Name;
            string menuLink = menu.Link;

            //var mlist = data.Menus.Where(mm => mm.Active == true && mm.Detail!=null && mm.Level.Length==10 && mm.Level.Substring(0, 5) == rootMenuLevel).OrderByDescending(mm => mm.Ord).ToList();
            //for (int h=0;h<mlist.Count;h++)
            //{

            //}

            string rootLevel = rootGroupNews.Level;          
            string groupNewsIDs = "";
            string strNews = "";

            List<News> list = new List<News>();

            var unsortedNews = data.News.Where(n => n.Active == true && n.Index == true).OrderByDescending(n => n.SDate).ToList();
            var group = data.GroupNews.Where(g => g.Active == true & g.Index == true && g.Level.Length == 10 && g.Level.Substring(0, 5) == rootLevel).OrderBy(g => g.Ord).ToList();
            for (int i = 0; i < group.Count; i++)
            {
                groupNewsIDs += group[i].Id.ToString();             
            }

            for (int j = 0; j < unsortedNews.Count; j++)
            {
                strNews = unsortedNews[j].IdGroup.ToString();
                if (groupNewsIDs.Contains(strNews))
                {
                    list.Add(unsortedNews[j]);
                }
            }
            list = list.OrderByDescending(l => l.SDate).Take(12).ToList();
        //    string strList="";
        //    for (int k = 0; k < list.ToPagedList(pageNumber, pageSize).Count; k++)
        //    {

        //strList="<div class=\"container list-news\"><div class=\"col-4 mid-thumbnail\"><div class=\"mid-thumbnail-inner\">";
        //    strList+="<img src=\""+list[k].Image+"\" alt=\""+list[k].Name+"\" /></div></div>";
        //        strList+="<div class=\"col-8 mid-content\"><div class=\"sub-title\"><a href=\""+list[k].Tag+"\" class=\"menu"+(k+1)+"\">"+list[k].Name+"</a></div>";
        //           strList+="<p>"+list[k].Content+"</p></div></div>";                  
        //    }
        //    ViewBag.StrList = strList;
        
            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion

            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            unsortedNews.Clear();
            unsortedNews = null;          
            return View(list.ToPagedList(pageNumber, pageSize));
           
        }
        #endregion
        #region[Danh sách tin theo nhóm]
        public ActionResult ListNews(int? page,string tag)
        {
            if (Request.HttpMethod == "GET")
            {
            }
            else
            {
                page = 1;
            }

            #region[Phân trang]
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            // Thiết lập phân trang
            PagedListRenderOptions ship = new PagedListRenderOptions();

            ship.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToLastPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToNextPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToIndividualPages = true;
            ship.DisplayPageCountAndCurrentLocation = false;
            ship.MaximumPageNumbersToDisplay = 5;
            ship.DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            ship.EllipsesFormat = "&#8230;";
            ship.LinkToFirstPageFormat = "Trang đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Trang cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "pagination" };
            ship.LiElementClasses = Enumerable.Empty<string>();

            ViewBag.ship = ship;
            #endregion
            var g = data.GroupNews.Where(gr => gr.Tag == tag).SingleOrDefault();
            var menu = data.Menus.Where(m => m.Tag == tag).SingleOrDefault();
            string menulvl = menu.Level.Substring(0,5);
            string menuName = menu.Name;
            string menuLink = menu.Link;
            
          var rootmenu = data.Menus.Where(rm => rm.Active == true && rm.Level.Length==5 && rm.Level==menulvl).SingleOrDefault();
          string rootmenuName = rootmenu.Name;
          string rootmenuLink = rootmenu.Link;
            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            ViewBag.RootMenuName = rootmenuName;
            ViewBag.RootMenuLink = rootmenuLink;
    
                int gid = g.Id;
                var list = (from n in data.News where n.Active == true && n.IdGroup == gid orderby n.Id descending select n).ToList();
                //string strList = "";
                //for (int k = 0; k < list.ToPagedList(pageNumber, pageSize).Count; k++)
                //{

                //    strList = "<div class=\"container list-news\"><div class=\"col-4 mid-thumbnail\"><div class=\"mid-thumbnail-inner\">";
                //    strList += "<img src=\"" + list[k].Image + "\" alt=\"" + list[k].Name + "\" /></div></div>";
                //    strList += "<div class=\"col-8 mid-content\"><div class=\"sub-title\"><a href=\"" + list[k].Tag + "\" class=\"menu" + (k + 1) + "\">" + list[k].Name + "</a></div>";
                //    strList += "<p>" + list[k].Content + "</p></div></div>";
                //}
                //ViewBag.StrList = strList;

                #region[Title, Keyword, Deskription]
                if (g.Title.Length > 0) { ViewBag.tit = g.Title; } else { ViewBag.tit = g.Name; }
                if (g.Description.Length > 0) { ViewBag.des = g.Description; } else { ViewBag.des = g.Name; }
                if (g.Keyword.Length > 0) { ViewBag.key = g.Keyword; } else { ViewBag.key = g.Name; }
                #endregion
                
                return View(list.ToPagedList(pageNumber, pageSize));
               
               
                   
        }
        #endregion
        #region[Chi tiết tin]
        public ActionResult NewsDetail(string tag)
        {
            int IdGroup = 0;
            string newsName="";
            string newsLink="";
            string newsContent="";
            DateTime newsDate;
            string newsDetail="";
            var news = (from n in data.News where n.Tag == tag select n).SingleOrDefault();
            IdGroup = news.IdGroup;
            newsName=news.Name;
            newsLink=news.Tag;
            newsContent=news.Content;
            newsDetail=news.Detail;
            newsDate=news.MDate;
            var group = data.GroupNews.Where(gr => gr.Active == true && gr.Id == IdGroup).SingleOrDefault();
            string grTag = group.Tag;
            var menu = data.Menus.Where(m => m.Tag == grTag).SingleOrDefault();
            string rootmenuName = "";
            string rootmenuLink = "";
            string menulvl = "";
            string menuName = "";
            string menuLink = "";
            if(menu.Level.Length==5)
            {
                rootmenuName = menu.Name;
                rootmenuLink = menu.Link;
                ViewBag.RootMenuName = rootmenuName;
                ViewBag.RootMenuLink = rootmenuLink;
            }
            else if (menu.Level.Length>5)
            {
                menulvl = menu.Level.Substring(0, 5);
                var rootmenu = data.Menus.Where(rm => rm.Active == true && rm.Level.Length == 5 && rm.Level == menulvl).SingleOrDefault();
                rootmenuName = rootmenu.Name;
                rootmenuLink = rootmenu.Link;
                ViewBag.RootMenuName = rootmenuName;
                ViewBag.RootMenuLink = rootmenuLink;
                menuName = menu.Name;
                menuLink = menu.Link;
                ViewBag.MenuName = menuName;
                ViewBag.MenuLink = menuLink;
            }
            
            ViewBag.NewsName = newsName; ViewBag.NewsLink = newsLink; ViewBag.NewsContent = newsContent; ViewBag.NewsDetail = newsDetail; ViewBag.NewsDate = newsDate;
                #region[Title, Keyword, Deskription]
               
                if (news.Title != null && news.Title.Length > 0) { ViewBag.tit = news.Title; } else { ViewBag.tit = news.Name; }
                if (news.Description != null && news.Description.Length > 0) { ViewBag.des = news.Description; } else { ViewBag.des = news.Name; }
                if (news.Keyword != null && news.Keyword.Length > 0) { ViewBag.key = news.Keyword; } else { ViewBag.key = news.Name; }
                #endregion    
            return View();
        }
        #endregion

        #region[Chi tiết menu tin]
        public ActionResult RootMenuDetail(string tag)
        {
            
            var menu = data.Menus.Where(m => m.Tag == tag).SingleOrDefault();
            string rootmenuName = "";
            string rootmenuLink = "";
            string rootmenuContent="";
            string rootmenuDetail="";       

                rootmenuName = menu.Name;
                rootmenuLink = menu.Link;
                rootmenuContent = menu.Content;
                rootmenuDetail = menu.Detail;
                ViewBag.RootMenuName = rootmenuName;
                ViewBag.RootMenuLink = rootmenuLink;
                ViewBag.RootMenuContent = rootmenuContent;
                ViewBag.RootMenuDetail = rootmenuDetail;
      

            #region[Title, Keyword, Deskription]
            if (menu.Title != null && menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description != null && menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword != null && menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion
            return View();
        }
        #endregion

        #region[Chi tiết menu con tin]
        public ActionResult MenuDetail(string tag)
        {

            var menu = data.Menus.Where(m => m.Tag == tag).SingleOrDefault();
            string rootmenuName = "";
            string rootmenuLink = "";
            string rootmenuContent = "";
            string rootmenuDetail = "";
            string menulvl = "";
            string menuName = "";
            string menuLink = "";
            string menuContent = "";
            string menuDetail = "";
            
                menulvl = menu.Level.Substring(0, 5);
                var rootmenu = data.Menus.Where(rm => rm.Active == true && rm.Level.Length == 5 && rm.Level == menulvl).SingleOrDefault();
                rootmenuName = rootmenu.Name;
                rootmenuLink = rootmenu.Link;
                rootmenuContent = rootmenu.Content;
                rootmenuDetail = rootmenu.Detail;
                ViewBag.RootMenuName = rootmenuName;
                ViewBag.RootMenuLink = rootmenuLink;
                ViewBag.RootMenuContent = rootmenuContent;
                ViewBag.RootMenuDetail = rootmenuDetail;
                menuName = menu.Name;
                menuLink = menu.Link;
                menuContent = menu.Content;
                menuDetail = menu.Detail;
                ViewBag.MenuName = menuName;
                ViewBag.MenuLink = menuLink;
                ViewBag.MenuContent = menuContent;
                ViewBag.MenuDetail = menuDetail;
        
            #region[Title, Keyword, Deskription]
            if (menu.Title != null && menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description != null && menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword != null && menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion
            return View();
        }
        #endregion

        #region[Danh sách tin tức]
        public ActionResult MenuList(int? page)
        {
            if (Request.HttpMethod == "GET")
            {
            }
            else
            {
                page = 1;
            }

            #region[Phân trang]
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            // Thiết lập phân trang
            PagedListRenderOptions ship = new PagedListRenderOptions();

            ship.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToLastPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToNextPage = PagedListDisplayMode.Always;
            ship.DisplayLinkToIndividualPages = true;
            ship.DisplayPageCountAndCurrentLocation = false;
            ship.MaximumPageNumbersToDisplay = 5;
            ship.DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            ship.EllipsesFormat = "&#8230;";
            ship.LinkToFirstPageFormat = "Trang đầu";
            ship.LinkToPreviousPageFormat = "«";
            ship.LinkToIndividualPageFormat = "{0}";
            ship.LinkToNextPageFormat = "»";
            ship.LinkToLastPageFormat = "Trang cuối";
            ship.PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
            ship.ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
            ship.FunctionToDisplayEachPageNumber = null;
            ship.ClassToApplyToFirstListItemInPager = null;
            ship.ClassToApplyToLastListItemInPager = null;
            ship.ContainerDivClasses = new[] { "pagination-container" };
            ship.UlElementClasses = new[] { "pagination" };
            ship.LiElementClasses = Enumerable.Empty<string>();

            ViewBag.ship = ship;
            #endregion

            
            var menu = data.Menus.Where(m => m.Active == true && m.Tag == "gioi-thieu").SingleOrDefault();

            string rootMenuLevel = menu.Level;
            string menuName = menu.Name;
            string menuLink = menu.Link;

            var mlist = data.Menus.Where(mm => mm.Active == true && mm.Detail != null && mm.Level.Length == 10 && mm.Level.Substring(0, 5) == rootMenuLevel).OrderByDescending(mm => mm.Ord).ToList();
            for (int h = 0; h < mlist.Count; h++)
            {

            }

           

            #region[Title, Keyword, Deskription]
            if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
            if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
            if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
            #endregion

            ViewBag.MenuName = menuName;
            ViewBag.MenuLink = menuLink;
            
            return View(mlist.ToPagedList(pageNumber, pageSize));

        }
        #endregion

        #region[404]
        public ActionResult Updating()
        {
            var t = data.Pictures.Where(p => p.Name == "DCN").SingleOrDefault();
            string imgSrc = t.Image;
            string imgAlt = t.Name;
            ViewBag.ImageSource = imgSrc;
            ViewBag.ImageAlt = imgAlt;
            return View();
        }
        #endregion

    }
}
