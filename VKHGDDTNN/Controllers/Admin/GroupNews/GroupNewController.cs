using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VKHGDDTNN.Models;
using PagedList;
using PagedList.Mvc;
using System.Collections;
using System.Data;
using System.Data.Entity;

namespace VKHGDDTNN.Controllers.Admin.GroupNews
{
    public class GroupNewController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();
        
        #region Index
        public ActionResult Index(string sortOrder, string sortName, int? trang, string getSortNameClass, string getSortOrderClass)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                //var all = db.GroupNews.OrderByDescending(g => g.Ord).ToList();
                var all = db.GroupNews.OrderBy(g => g.Level).ToList();
                int pageSize = 10;
            int pageNumber = (trang ?? 1);

             //begin [get last page]
            if (trang != null)
            {
                ViewBag.mPage = (int)trang;
            }
            else
            {
                ViewBag.mPage = 1;
            }


            int lastPage = all.Count / pageSize;
            if (all.Count % pageSize > 0)
            {
                lastPage++;
            }
            ViewBag.LastPage = lastPage;

            ViewBag.PageSize = pageSize;
                //end [get last page]
                #region Order
                ViewBag.CurrentSortOrder = sortOrder;
                ViewBag.SortOrderParm = sortOrder == "ordAsc" ? "ordDesc" : "ordAsc";
                ViewBag.CurrentSortName = sortName;
                ViewBag.SortNameParm = sortName == "nameAsc" ? "nameDesc" : "nameAsc";

                switch (sortOrder)
                {
                    case "ordDesc":
                        all = db.GroupNews.OrderByDescending(a => a.Ord).ToList();
                        break;
                    case "ordAsc":
                        all = db.GroupNews.OrderBy(a => a.Ord).ToList();
                        break;
                    default:
                        break;
                }

                switch (sortName)
                {
                    case "nameDesc":
                        all = db.GroupNews.OrderByDescending(a => a.Level).ToList();
                        break;
                    case "nameAsc":
                        all = db.GroupNews.OrderBy(a => a.Level).ToList();
                        break;
                    default:
                        break;
                }
                #endregion Order

                #region PagedList
                PagedList<GroupNew> GroupNews = (PagedList<GroupNew>)all.ToPagedList(pageNumber, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_Data", GroupNews);
                #endregion

                return View(GroupNews);
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            { 
            var GroupNews = db.GroupNews.Where(g => g.Active == true).OrderBy(g => g.Level).ToList();
            if (GroupNews.Count == 0)
            {
                GroupNews.Add(new GroupNew { Level = "", Name = "" });
            }

            foreach (var item in GroupNews)
            {
                item.Name = VKHGDDTNN.Models.StringClass.ShowNameLevel(item.Name, item.Level);
            }

            for (int i = 0; i < GroupNews.Count; i++)
            {
                ViewBag.GroupNew = new SelectList(GroupNews, "Id", "Name");
            }
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
            return View();
        }
   
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, GroupNew catego)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            { 
                // Lấy dữ liệu từ view
                int parentId = 0;
                string level = "00000";
                if (collection["GroupNew"] != "0")
                {
                    if (collection["GroupNew"] != "")
                    {
                        parentId = Int32.Parse(collection["GroupNew"]);
                        string parentLevel = db.GroupNews.Where(g => g.Id == parentId).SingleOrDefault().Level;
                        level = parentLevel + "00000";
                    }

                }
                catego.Level = level;
                catego.Name = collection["Name"];
                catego.Title = collection["Title"];
                catego.Description = collection["Description"];
                catego.Keyword = collection["Keyword"];
                catego.Ord = Convert.ToInt32(collection["Ord"]);
                catego.Tag = StringClass.NameToTag(collection["Name"]);
                catego.Priority = (collection["Priority"] == "false") ? false : true;
                catego.Index = (collection["Index"] == "false") ? false : true;
                catego.Active = (collection["Active"] == "false") ? false : true;


                db.Entry(catego).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

        public ActionResult CreateSub(int id)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var GroupNews = db.GroupNews.OrderBy(g => g.Level).ToList();
                //var GroupNews = db.GroupNews.Where(g => g.Active == true).OrderBy(g => g.Level).ToList();
                foreach (var item in GroupNews)
                {
                    item.Name = VKHGDDTNN.Models.StringClass.ShowNameLevel(item.Name, item.Level);
                }

                for (int i = 0; i < GroupNews.Count; i++)
                {
                    //if (GroupNews[i].Level.Length == 5)
                    //{
                    //    lstgr.Add(new SelectListItem { Text = StringClass.ShowNameLevel(GroupNews[i].Name, GroupNews[i].Level), Value = GroupNews[i].Level.ToString() });
                    //}
                    //else
                    //{
                    //    lstgr.Add(new SelectListItem { Text = StringClass.ShowNameLevel(GroupNews[i].Name, GroupNews[i].Level), Value = GroupNews[i].Level.ToString() });
                    //}
                    ViewBag.GroupNew = new SelectList(GroupNews, "Id", "Name", id);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSub(FormCollection collection, GroupNew catego)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                // Lấy dữ liệu từ view
                int parentId = 0;
                string level = "00000";
                if (collection["GroupNew"] != "")
                {
                    parentId = Int32.Parse(collection["GroupNew"]);
                    string parentLevel = db.GroupNews.Where(g => g.Id == parentId).SingleOrDefault().Level;
                    level = parentLevel + "00000";
                }
                catego.Level = level;
                catego.Name = collection["Name"];
                catego.Title = collection["Title"];
                catego.Description = collection["Description"];
                catego.Keyword = collection["Keyword"];
                catego.Ord = Convert.ToInt32(collection["Ord"]);
                catego.Tag = StringClass.NameToTag(collection["Name"]);
                catego.Priority = (collection["Priority"] == "false") ? false : true;
                catego.Index = (collection["Index"] == "false") ? false : true;
                catego.Active = (collection["Active"] == "false") ? false : true;


                db.Entry(catego).State = EntityState.Added;
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
        public ActionResult Edit(int id)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            { 
            var GroupNew = db.GroupNews.Find(id);
            int idGn = GroupNew.Id;
            ViewBag.Id = idGn;
                var GroupNews = db.GroupNews.OrderBy(g => g.Level).ToList();
            //var GroupNews = db.GroupNews.Where(g => g.Active == true).OrderBy(g => g.Level).ToList();

            foreach (var item in GroupNews)
            {
                item.Name = VKHGDDTNN.Models.StringClass.ShowNameLevel(item.Name, item.Level);

            }
                //if (GroupNew.Level.Length > 5)
                //{
                //    string t = GroupNew.Level.Substring(0, GroupNew.Level.Length - 5);
                //    ViewBag.GroupNews = new SelectList(GroupNews, "Level", "Name", t);
                //}
                //else
                //{
                //    ViewBag.GroupNews = new SelectList(GroupNews, "Level", "Name");
                //}

                for (int i = 0; i < GroupNews.Count; i++)
                {
                    ViewBag.GroupNew = new SelectList(GroupNews, "Id", "Name", GroupNew.Level);
                }
                return View(GroupNew);
            }
             else
             {
                 return RedirectToAction("Default", "Admin");
             }          
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection, VKHGDDTNN.Models.GroupNew m)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var ob = db.GroupNews.Find(id);
                m.Name = collection["Name"];
                m.Name = m.Name.Replace(".", "");
                m.Ord = Convert.ToInt32(collection["Ord"]);
                string le = collection["GroupPost"];
                m.Level = le + "00000";
                m.Title = collection["Title"];
                m.Description = collection["Description"];
                m.Keyword = collection["Keyword"];
                m.Tag = VKHGDDTNN.Models.StringClass.NameToTag(collection["Name"]);
                m.Priority = (collection["Priority"] == "false") ? false : true;
                m.Index = (collection["Index"] == "false") ? false : true;
                m.Active = (collection["Active"] == "false") ? false : true;
                db.sp_GroupNews_Update(m.Id, m.Name, m.Title, m.Description, m.Keyword, m.Tag, m.Level, m.Ord, m.Active, m.Index, m.Priority);
                // Lấy dữ liệu từ view
                //int parentId = 0;
                //string level = "00000";
                //if (collection["GroupNew"] != "")
                //{
                //    parentId = Int32.Parse(collection["GroupNew"]);
                //    string parentLevel = db.GroupNews.Where(g => g.Id == parentId).SingleOrDefault().Level;
                //    level = parentLevel + "00000";
                //}
                //catego.Level = level;
                //catego.Name = collection["Name"];
                ////if (catego.Level.Length > 5)
                ////{
                ////    catego.Name = name.Substring(catego.Level.Length - 5, name.Length - (catego.Level.Length - 5));
                ////}
                ////else
                ////{
                ////    catego.Name = name;
                ////}
                //catego.Title = collection["Title"];
                //catego.Description = collection["Description"];
                //catego.Keyword = collection["Keyword"];
                //catego.Ord = Convert.ToInt32(collection["Ord"]);
                //catego.Tag = StringClass.NameToTag(collection["Name"]);
                //catego.Priority = (collection["Priority"] == "false") ? false : true;
                //catego.Index = (collection["Index"] == "false") ? false : true;
                //catego.Active = (collection["Active"] == "false") ? false : true;


                //db.Entry(catego).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id, int trang, int kichco)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                //var del = (from categaa in db.GroupNews where categaa.Id == id select categaa).Single();
                var del = db.GroupNews.First(p => p.Id == id);
                ViewBag.Id = del.Id;
                db.GroupNews.Remove(del);
                db.SaveChanges();

                List<GroupNew> GroupNews = db.GroupNews.ToList();

                if ((GroupNews.Count % kichco) == 0)
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
        #endregion

        #region MultiCommand
        [HttpPost]
        public ActionResult MultiCommand(FormCollection collect)
        {
            int m = int.Parse(collect["mPage"]);
            int pagesize = int.Parse(collect["PageSize"]);

            List<GroupNew> GroupNews = db.GroupNews.ToList();
            int lastpage = GroupNews.Count / pagesize;
            if (GroupNews.Count % pagesize > 0)
            {
                lastpage++;
            }
            //int lastPage = int.Parse(collect["LastPage"]);

            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {

                if (collect["btnDelete"] != null)
                {
                    //string str = "";
                    foreach (string key in Request.Form)
                    {
                        var checkbox = "";
                        if (key.Contains("chk"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                int id = Convert.ToInt32(key.Remove(0, 3));
                                var Del = (from del in db.GroupNews where del.Id == id select del).SingleOrDefault();
                                //if (Del.SpTon == 0)
                                //{
                                db.GroupNews.Remove(Del);
                                db.SaveChanges();
                                //}
                                //else
                                //{
                                //    str += Del.Name + ",";
                                //    Session["DeletePro"] = "Sản phẩm " + str + "  vẫn còn trong kho! Không được xóa!";
                                //}
                            }
                        }
                    }

                    if (collect["checkAll"] != null)
                    {
                        if (m == 1)
                        {
                            return RedirectToAction("Index");
                        }

                        if (m == lastpage)
                        {
                            m--;
                        }
                    }
                    return RedirectToAction("Index", new { page = m });
                }
                else
                {
                    foreach (string key in Request.Form)
                    {
                        if (key.StartsWith("Ord"))
                        {
                            Int32 id = Convert.ToInt32(key.Remove(0, 3));
                            var Up = db.GroupNews.Where(e => e.Id == id).FirstOrDefault();

                            if (Up != null)
                            {
                                if (!collect["Ord" + id].Equals(""))
                                {
                                    Up.Ord = int.Parse(collect["Ord" + id]);
                                }

                                db.Entry(Up).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                    return RedirectToAction("Index", new { page = m });
                }
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

        #region MultiDelete
        public ActionResult MultiDelete()
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
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
                            var Del = (from emp in db.GroupNews where emp.Id == id select emp).SingleOrDefault();
                            db.GroupNews.Remove(Del);
                            db.SaveChanges();
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

        #region UpdateDirect
        [HttpPost]
        public ActionResult UpdateDirect(int id, string ord, string name)
        {
            var results = "";
            var GroupNew = db.GroupNews.Find(id);
            if (GroupNew != null)
            {
                if (ord != null)
                {
                    GroupNew.Ord = Int32.Parse(ord);
                    results = "Thứ tự đã được thay đổi.";
                }
                else if (name != null)
                {
                    GroupNew.Name = name;
                    results = "Tên Nhóm bài viết đã được thay đổi.";
                }
            }
            db.Entry(GroupNew).State = EntityState.Modified;
            db.SaveChanges();
            return Json(results);
        }
        #endregion

        #region ChangeActive
        [HttpPost]
        public ActionResult ChangeActive(int id)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var GroupNew = db.GroupNews.Find(id);
                if (GroupNew != null)
                {
                    GroupNew.Active = GroupNew.Active == true ? false : true;
                }
                db.Entry(GroupNew).State = EntityState.Modified;
                db.SaveChanges();

                var results = "Trạng thái kích hoạt đã được thay đổi.";
                return Json(results);
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion    

        
    
  
    }
}
