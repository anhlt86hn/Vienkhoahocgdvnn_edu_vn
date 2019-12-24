using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Data;
using System.IO;
using VKHGDDTNN.ViewModels;
using VKHGDDTNN.Models;



namespace VKHGDDTNN.Controllers.Admin.Menus
{
    public class MenuController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();

        #region Index
        public ActionResult Index(string sortOrder, string sortName, int? trang, string positionSearch, string sortPos, string getSortNameClass, string getSortOrderClass)
        {

            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "1"))
            {
                var menu = db.Menus.OrderBy(m => m.Level).ToList();
                int pageSize = 20;
                int pageNumber = (trang ?? 1);

                #region GetLastPage
                // begin [get last page]
                if (trang != null)
                    ViewBag.mPage = (int)trang;
                else
                    ViewBag.mPage = 1;

                int lastPage = menu.Count / pageSize;
                if (menu.Count % pageSize > 0)
                {
                    lastPage++;
                }
                ViewBag.LastPage = lastPage;

                ViewBag.PageSize = pageSize;
                //end [get last page]
                #endregion GetLastPage

                #region Order
                ViewBag.CurrentSortOrder = sortOrder;
                ViewBag.SortOrderParm = sortOrder == "ordAsc" ? "ordDesc" : "ordAsc";
                ViewBag.CurrentSortName = sortName;
                ViewBag.SortNameParm = sortName == "nameAsc" ? "nameDesc" : "nameAsc";

                switch (sortOrder)
                {
                    case "ordDesc":
                        menu = db.Menus.OrderByDescending(a => a.Ord).ToList();
                        break;
                    case "ordAsc":
                        menu = db.Menus.OrderBy(a => a.Ord).ToList();
                        break;
                    default:
                        break;
                }

                switch (sortName)
                {
                    case "nameDesc":
                        menu = db.Menus.OrderByDescending(a => a.Level).ToList();
                        break;
                    case "nameAsc":
                        menu = db.Menus.OrderBy(a => a.Level).ToList();
                        break;
                    default:
                        break;
                }
                #endregion Order

                #region PagedList
                PagedList<VKHGDDTNN.Models.Menu> pMenu = (PagedList<VKHGDDTNN.Models.Menu>)menu.ToPagedList(pageNumber, pageSize);

                /// Kiem tra co tim kiem theo vi tri va phan trang theo vi tri khong
                if ((String.IsNullOrEmpty(positionSearch) || positionSearch == "") && (sortPos == null))
                {
                    if (Request.IsAjaxRequest())
                        return PartialView("_Data", pMenu);
                    else
                        return View(pMenu);
                }
                else /// Truong hop co tim kiem theo vi tri
                {
                    int pos;
                    /// Kiem tra xem co phan trang khong
                    if (sortPos != null) /// Phan trang
                    {
                        ViewBag.currentPos = sortPos;
                        pos = Convert.ToInt32(sortPos);
                        if (sortName == "nameAsc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                        else if (sortName == "nameDesc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                        else if (sortOrder == "ordDesc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Ord).ToList();
                        else if (sortOrder == "ordAsc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderBy(a => a.Ord).ToList();
                        else
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                    }
                    else /// ko phan trang
                    {
                        ViewBag.currentPos = positionSearch;
                        pos = Convert.ToInt32(positionSearch);
                        if (sortName == "nameAsc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                        else if (sortName == "nameDesc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                        else if (sortOrder == "ordDesc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Ord).ToList();
                        else if (sortOrder == "ordAsc")
                            menu = db.Menus.Where(a => a.Position == pos).OrderBy(a => a.Ord).ToList();
                        else
                            menu = db.Menus.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                    }
                    pMenu = (PagedList<VKHGDDTNN.Models.Menu>)menu.ToPagedList(pageNumber, pageSize);
                #endregion PagedList

                    if (Request.IsAjaxRequest())
                        return PartialView("_Data", pMenu);
                    else
                        return View(pMenu);
                }
            }
           
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {    
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
             {
                 var groupproduct = db.GroupProducts.OrderBy(m => m.Level).ToList();
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();
            
            List<SelectListItem> listpage = new List<SelectListItem>();           
            listpage.Clear();           
                
            for (int i = 0; i < groupnews.Count; i++)
            {
                if (groupnews[i].Level.Length == 5)
                {
                    listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu/" + groupnews[i].Tag.ToString(), Selected = (groupnews[i].Level.Length==5) });
                }
                else if (groupnews[i].Level.Length > 5)
                {
                    listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu-con/" + groupnews[i].Tag.ToString(), Selected = (groupnews[i].Level.Length >5) });
                }
                }

            for (int j = 0; j < groupproduct.Count; j++)
            {
                if (groupproduct[j].Level.Length == 5)
                {
                    listpage.Add(new SelectListItem { Text = "Nhóm sản phẩm - " + StringClass.ShowNameLevel(groupproduct[j].Name, groupproduct[j].Level), Value = "/menu-sp/" + groupproduct[j].Tag.ToString(), Selected = (groupproduct[j].Level.Length == 5) });
                }
                else if(groupproduct[j].Level.Length > 5)
                {
                    listpage.Add(new SelectListItem { Text = "Nhóm sản phẩm - " + StringClass.ShowNameLevel(groupproduct[j].Name, groupproduct[j].Level), Value = "/menu-sp-con/" + groupproduct[j].Tag.ToString(), Selected = (groupproduct[j].Level.Length > 5) });
                }
            }
            var menuModel = db.Menus.OrderBy(m => m.Level).ToList();
            List<SelectListItem> lstMenu = new List<SelectListItem>();
            lstMenu.Clear();
            for (int i = 0; i < menuModel.Count; i++)
            {
                if (menuModel[i].Level.Length == 5)
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }
                else
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }
            }
           
            ViewBag.lstMenu = lstMenu;
            ViewBag.LinkModule = listpage;      
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");          
            return View();
             }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

       
        [HttpGet]
        public ActionResult CreateSub(int id)
        {         
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
             {
            var groupproduct = db.GroupProducts.OrderBy(m => m.Level).ToList();
            var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();
       
            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();
            for (int i = 0; i < groupnews.Count; i++)
            {
                listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu-con/" + groupnews[i].Tag.ToString() });
            }

            for (int j = 0; j < groupproduct.Count; j++)
            {

                listpage.Add(new SelectListItem { Text = "Nhóm sản phẩm - " + StringClass.ShowNameLevel(groupproduct[j].Name, groupproduct[j].Level), Value = "/menu-sp-con/" + groupproduct[j].Tag.ToString() });
            }
            var menuModel = db.Menus.OrderBy(m => m.Level).ToList();
            List<SelectListItem> lstMenu = new List<SelectListItem>();
            lstMenu.Clear();
            for (int i = 0; i < menuModel.Count; i++)
            {
                if (menuModel[i].Id == id)
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString(), Selected = (menuModel[i].Id == id) });
                }
                else
                {
                    lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });
                }               
            }
            ViewBag.LinkModule = listpage;
            ViewBag.lstMenu = lstMenu;
            ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");
            ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");
            ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
            ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
            //ViewBag.Ord = ord;
            return View();
             }
            else 
            { 
                return RedirectToAction("Default", "Admin"); 
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, VKHGDDTNN.Models.Menu m)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                m.Name = collection["Name"];
                m.Image = collection["Image"];
               
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        m.Link = "/menu/" + collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        m.Link = collection["LinkModule"];
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        m.Link = "/menu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {                 
                    m.Link = "/menu-tin/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }
               
                m.Position = Convert.ToInt32(collection["Position"]);
                m.Target = collection["Target"];
                m.Content = collection["Content"];
                m.Detail = collection["Detail"];
                m.Title = collection["Title"];
                m.Description = collection["Description"];
                m.Keyword = collection["Keyword"];
                string le = collection["lstMenu"];
                if (le.Length > 0)
                {
                    m.Level = le + "00000";
                }
                else
                {
                    m.Level = "00000";
                }
                m.Ord = Convert.ToInt32(collection["Ord"]);
                m.Active = (collection["Active"] == "false") ? false : true;
                m.Tag = VKHGDDTNN.Models.StringClass.NameToTag(collection["Name"]);
                db.Menus.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSub(FormCollection collection, VKHGDDTNN.Models.Menu m)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                
                m.Name = collection["Name"];
                m.Image = collection["Image"];

                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        m.Link = "/menu-con/" + collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        m.Link = collection["LinkModule"];
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        m.Link = "/menu-con/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    m.Link = "/menu-tin-con/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }
                
                m.Target = collection["Target"];
                m.Position = Convert.ToInt32(collection["Position"]);
                m.Content = collection["Content"];
                m.Detail = collection["Detail"];
                m.Title = collection["Title"];
                m.Description = collection["Description"];
                m.Keyword = collection["Keyword"];
                string le = collection["lstMenu"];
                if (le.Length > 0)
                {
                    m.Level = le + "00000";
                }
                else
                {
                    m.Level = "00000";
                }
                m.Ord = Convert.ToInt32(collection["Ord"]);
                m.Active = (collection["Active"] == "false") ? false : true;
                m.Tag = VKHGDDTNN.Models.StringClass.NameToTag(collection["Name"]);
                db.Menus.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

#endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var menu = db.Menus.Find(id);
                var menuId = menu.Id;
                var groupproduct = db.GroupProducts.OrderBy(m => m.Level).ToList();
                var groupnews = db.GroupNews.OrderBy(m => m.Level).ToList();               
              
                List<SelectListItem> listpage = new List<SelectListItem>();
                listpage.Clear();

                if (menu.Level.Length == 5)
                {
                    for (int i = 0; i < groupnews.Count; i++)
                    {
                        if(groupnews[i].Tag=="cap-1")
                        {
                            listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu/" + menu.Tag.ToString(), Selected = (groupnews[i].Level.Length == 5) });
                        }
                        else 
                        {                        
                        listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu/" + groupnews[i].Tag.ToString(), Selected = (groupnews[i].Level.Length==5)  });
                        }
                    }

                    for (int j = 0; j < groupproduct.Count; j++)
                    {

                        listpage.Add(new SelectListItem { Text = "Nhóm sản phẩm - " + StringClass.ShowNameLevel(groupproduct[j].Name, groupproduct[j].Level), Value = "/menu-sp/" + groupproduct[j].Tag.ToString(), Selected = (groupproduct[j].Level.Length == 5) });
                    }
                }
                else if(menu.Level.Length>5)
                {
                    for (int i = 0; i < groupnews.Count; i++)
                    {
                        listpage.Add(new SelectListItem { Text = "Nhóm bài viết - " + StringClass.ShowNameLevel(groupnews[i].Name, groupnews[i].Level), Value = "/menu-con/" + groupnews[i].Tag.ToString(), Selected = (groupnews[i].Level.Length>5)  });
                    }

                    for (int j = 0; j < groupproduct.Count; j++)
                    {

                        listpage.Add(new SelectListItem { Text = "Nhóm sản phẩm - " + StringClass.ShowNameLevel(groupproduct[j].Name, groupproduct[j].Level), Value = "/menu-sp-con/" + groupproduct[j].Tag.ToString(), Selected = (groupproduct[j].Level.Length > 5) });
                    }
                }

                var menuList = db.Menus.OrderBy(m => m.Level).ToList();
                foreach (var item in menuList)
                {
                    item.Name = StringClass.ShowNameLevel(item.Name, item.Level);
                }

                if (menu.Level.Length > 5)
                {
                    string t = menu.Level.Substring(0, menu.Level.Length - 5);
                    ViewBag.lstMenu = new SelectList(menuList, "Level", "Name", t);
                }
                else
                {
                    ViewBag.lstMenu = new SelectList(menuList, "Level", "Name");
                }

                //var menuModel = db.Menus.OrderBy(m => m.Level).ToList();
                //List<SelectListItem> lstMenu = new List<SelectListItem>();
                //lstMenu.Clear();
                //for (int i = 0; i < menuModel.Count; i++)
                //{                    
                //        lstMenu.Add(new SelectListItem { Text = StringClass.ShowNameLevel(menuModel[i].Name, menuModel[i].Level), Value = menuModel[i].Level.ToString() });                                      
                //}

                //ViewBag.lstMenu=lstMenu;
                ViewBag.LinkModule = listpage;
                ViewBag.Position = new SelectList(ShowPosition(), "value", "text");
                ViewBag.PageTypeDDL = new SelectList(PageTypeDDL(), "value", "text");
                ViewBag.LinkDDL = new SelectList(ShowLinkDDL(), "value", "text");              
                ViewBag.Target = new SelectList(ShowTarget(), "value", "text");
                ViewBag.MenuId = menuId;
                return View(menu);
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection collection, VKHGDDTNN.Models.Menu m, int id)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var menu = db.Menus.Find(id);
                m.Name = collection["Name"];
                m.Image = collection["Image"];
                m.Name = m.Name.Replace(".", "");
                m.Ord = Convert.ToInt32(collection["Ord"]);
                if(menu.Level.Length==5)
                {  
                if (collection["PageTypeDDL"] == "0")
                {
                    if (collection["LinkDDL"] == "0")
                    {
                        m.Link = collection["Link"];
                        //m.Link = "/menu/" + collection["Link"];
                    }
                    else if (collection["LinkDDL"] == "1")
                    {
                        m.Link = collection["LinkModule"];
                        //m.Link = collection["LinkModule"];
                    }
                    else if (collection["LinkDDL"] == "2")
                    {
                        m.Link = StringClass.NameToTag(collection["Name"].ToString()) + "";
                        //m.Link = "/menu/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }
                else
                {
                    m.Link = "/menu-tin/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                }
                }else if(menu.Level.Length>5)
                {
                    if (collection["PageTypeDDL"] == "0")
                    {
                        if (collection["LinkDDL"] == "0")
                        {
                            m.Link = collection["Link"];
                            //m.Link = "/menu-con/" + collection["Link"];
                        }
                        else if (collection["LinkDDL"] == "1")
                        {
                            m.Link = collection["LinkModule"];
                        }
                        else if (collection["LinkDDL"] == "2")
                        {
                            m.Link = StringClass.NameToTag(collection["Name"].ToString()) + "";
                            //m.Link = "/menu-con/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                        }
                    }
                    else
                    {
                        m.Link = "/menu-tin-con/" + StringClass.NameToTag(collection["Name"].ToString()) + "";
                    }
                }

                string le = collection["lstMenu"];
                m.Level = le + "00000";
                m.Target = collection["Target"];
                m.Position = Convert.ToInt32(collection["Position"]);
                m.Content = collection["Content"];
                m.Detail = collection["Detail"];
                m.Title = collection["Title"];
                m.Description = collection["Description"];
                m.Keyword = collection["Keyword"];
                m.Active = (collection["Active"] == "false") ? false : true;
                m.Tag = VKHGDDTNN.Models.StringClass.NameToTag(collection["Name"]);
                //menu.Tag = collection["Tag"];

                db.sp_Menu_Update(m.Id, m.Name, m.Title, m.Description, m.Keyword, m.Image, m.Link, m.Tag, m.Level, m.Position, m.Ord, m.Active, m.Target, m.Content, m.Detail, m.IdCategory);
                //UpdateModel(menu);
                //db.Entry(menu).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion Edit

        #region Delete
        [HttpGet]
        public ActionResult Delete(int id, int trang, int kichco)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var mDelete = db.Menus.Where(m => m.Id == id).SingleOrDefault();

                db.Menus.Remove(mDelete);
                db.SaveChanges();

                List<VKHGDDTNN.Models.Menu> adv = db.Menus.ToList();

                if ((adv.Count % kichco) == 0)
                {
                    if (trang > 1)
                    {
                        trang--;
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", new { page = trang });
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion DeleteItem

        #region MultiDelete
        public ActionResult MultiDelete(FormCollection collect)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                if (collect["btnDeleteAll"] != null)
                {
                    foreach (string key in Request.Form)
                    {
                        var checkbox = "";
                        if (key.StartsWith("chk"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                Int32 id = Convert.ToInt32(key.Remove(0, 3));
                                var Del = db.Menus.Where(sp => sp.Id == id).SingleOrDefault();
                                db.Menus.Remove(Del);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion 

        #region Search
        public ActionResult AutoComplete(string searchString, int? page)
        {
        
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                int pageSize = 2;
                int pageNumber = (page ?? 1);

                PagedList<VKHGDDTNN.Models.Menu> menu = (PagedList<VKHGDDTNN.Models.Menu>)db.Menus.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(a => a.Level).ToPagedList(pageNumber, pageSize);

                if (menu.Count != 0)
                    return PartialView("_Data", menu);
                else
                    return PartialView("ErrorSearch");
            }

            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

      

        #region UpdateDirect
        public ActionResult UpdateDirect(int id, string order, string link, string position)
        {
            var menu = db.Menus.Find(id);
            var result = string.Empty;
            if (menu != null)
            {
                if (order != null)
                {
                    menu.Ord = Convert.ToInt32(order);
                    result = "Thứ tự đã được thay đổi.";
                }
                else if (link != null)
                {
                    menu.Link = link;
                    result = "Link Menu đã được thay đổi.";
                }
                else if (position != null)
                {
                    menu.Position = Convert.ToInt32(position);
                    result = "Vị trí Menu đã được thay đổi.";
                }
                else
                {
                    menu.Active = menu.Active == true ? false : true;
                    result = "Trạng thái Menu đã được thay đổi.";
                }
            }

            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            //var result = "Dữ liệu đã được thay đổi.";// userModule.Active;
            return Json(result);
        }
        #endregion
        #region ChangeActive
        [HttpPost]
        public ActionResult ChangeActive(int id)
        {
            var m = db.Menus.Find(id);
            if (m != null)
            {
                m.Active = m.Active == true ? false : true;
            }
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            var results = "Trạng thái kích hoạt đã được thay đổi.";
            return Json(results);
        }
        #endregion
      
        #region[listDropDownList]
        private List<SelectListItem> listDropDownList()
        {
            List<SelectListItem> listpage = new List<SelectListItem>();
            listpage.Clear();

            var list = db.Menus.OrderBy(m => m.Level).ToList();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    listpage.Add(new SelectListItem { Text = StringClass.ShowNameLevel(list[i].Name, list[i].Level), Value = "/sanpham/sp/" + StringClass.NameToTag(list[i].Name) + "/" });
                }
            }
            //listpage.Add(new SelectListItem { Text = "Tài liệu", Value = "/thu-vien/" });
            //listpage.Add(new SelectListItem { Text = "Liên hệ", Value = "/hoc-cung-doanh-nghiep/" });
            //listpage.Add(new SelectListItem { Text = "Đăng ký Online", Value = "/dang-ky-online/" });
            return listpage;
        }
        #endregion

        #region showlistPagerodule
        public static List<DropDownList> showlistPagerodule()
        {
            VKHGDDTNNEntities db = new VKHGDDTNNEntities();
            var listg = db.Menus.OrderByDescending(m => m.Level).ToList();
            List<DropDownList> list = new List<DropDownList>();
            //list.Add(new DropDownList { value = "", text = "Trang chủ" });
            if (listg.Count > 0)
            {
                for (int i = 0; i < listg.Count; i++)
                {
                    list.Add(new DropDownList { value = listg[i].Link, text = StringClass.ShowNameLevel(listg[i].Name, listg[i].Level) });
                }

            }
            //list.Add(new DropDownList { value = "/thu-vien/", text = "Tài liệu" });
            //list.Add(new DropDownList { value = "/hoc-cung-doanh-nghiep/", text = "Liên hệ" });
            //list.Add(new DropDownList { value = "/dang-ky-online/", text = "Đăng ký Online" });

            return list;
        }
        #endregion

        #region ShowLinkDDL
        public static List<DropDownList> ShowLinkDDL()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Nhập liên kết" });
            list.Add(new DropDownList { value = "1", text = "Liên kết với nhóm" });
            list.Add(new DropDownList { value = "2", text = "Liên kết tự sinh" });
            return list;
        }
        #endregion

        #region PageTypeDDL
        public static List<DropDownList> PageTypeDDL()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Trang liên kết" });
            list.Add(new DropDownList { value = "1", text = "Trang nội dung" });
            return list;
        }
        #endregion

        #region ShowPosition
        public static List<DropDownList> ShowPosition()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "0", text = "Chưa đặt vị trí" });
            list.Add(new DropDownList { value = "1", text = "Menu chính (vị trí TRÊN CÙNG)" });
            list.Add(new DropDownList { value = "2", text = "Menu phụ (vị trí BÊN TRÁI)" });
            list.Add(new DropDownList { value = "3", text = "Menu phụ (vị trí BÊN PHẢI)" });
            list.Add(new DropDownList { value = "4", text = "Menu ở cả 2 vị trí (TRÊN CÙNG, BÊN PHẢI)" });
            

            return list;
        }
        #endregion

        #region ShowTarget
        public static List<DropDownList> ShowTarget()
        {
            List<DropDownList> list = new List<DropDownList>();
            list.Add(new DropDownList { value = "_self", text = "_self" });
            list.Add(new DropDownList { value = "_blank", text = "_blank" });
            return list;
        }
        #endregion

  }
}
