using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VKHGDDTNN.Models;

namespace VKHGDDTNN.Controllers
{
    public class CategoryController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();
        #region[CategoryIndex]
        public ActionResult CategoryIndex()
        {
            string page = "1";//so phan trang hien tai
            var pagesize = 25;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.Categories.OrderBy(c=>c.Level).ToList();
            var pages = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            //var pages = db.sp_Category_Phantrang(page, productize, "", "[Level] asc").ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            if (numOfNews > 0)
            {
                ViewBag.Pager = VKHGDDTNN.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            else
            {
                ViewBag.Pager = "";
            }
            return View(pages);
        }
        #endregion
        #region[CategoryCreate]
        public ActionResult CategoryCreate()
        {
            var brand = db.GroupProducts.ToList();
            for (int i = 0; i < brand.Count; i++)
            {
                ViewBag.Brand = new SelectList(brand, "Id", "Name");
            }
            return View();
        }
        #endregion
        #region[CategoryCreate]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CategoryCreate(FormCollection collection, Category catego)
        {
            if (Request.Cookies["Username"] != null)
            {
                var Name = collection["Name"];
                var Content = collection["Content"];
                var Ord = collection["Ord"];
                catego.Tag = StringClass.NameToTag(Name);
                catego.Name = Name;
                catego.Ord = Convert.ToInt32(Ord);
                catego.IdGroupPro = Convert.ToInt32(collection["Brand"]);
                catego.Active = (collection["Active"] == "false") ? false : true;
                catego.Priority = (collection["Priority"] == "false") ? false : true;
                catego.Index = (collection["Index"] == "false") ? false : true;
                catego.Description = collection["Description"];
                catego.Keyword = collection["Keyword"];
                catego.Title = collection["Title"];
                catego.Level = "00000";
                db.Categories.Add(catego);
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion
        #region[CategoryEdit]
        public ActionResult CategoryEdit(int id)
        {
            var Edit = db.Categories.First(m => m.Id == id);
            var brand = db.GroupProducts.ToList();
            for (int i = 0; i < brand.Count; i++)
            {
                ViewBag.Brand = new SelectList(brand, "Id", "Name", Edit.IdGroupPro);
            }
            return View(Edit);
        }
        #endregion
        #region[CategoryEdit]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CategoryEdit(int id, FormCollection collection, string Level)
        {
            if (Request.Cookies["Username"] != null)
            {
                var catego = db.Categories.First(model => model.Id == id);
                var Name = collection["Name"];
                var Content = collection["Content"];
                var Ord = collection["Ord"];
                catego.Tag = StringClass.NameToTag(Name);
                catego.Name = Name;
                catego.Ord = Convert.ToInt32(Ord);
                if (catego.Level.Length < 10)
                {
                    catego.IdGroupPro = Convert.ToInt32(collection["Brand"]);
                }
                else
                {
                    var grp = db.Categories.Where(m => m.Level == catego.Level.Substring(0, 5)).FirstOrDefault();
                    catego.IdGroupPro = Convert.ToInt32(grp.Level);
                }
                catego.Active = (collection["Active"] == "false") ? false : true;
                catego.Priority = (collection["Priority"] == "false") ? false : true;
                catego.Index = (collection["Index"] == "false") ? false : true;
                catego.Description = collection["Description"];
                catego.Keyword = collection["Keyword"];
                catego.Title = collection["Title"];

                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion
        #region[CategoryAddSub]
        public ActionResult CategoryAddSub(string level)
        {
            ViewBag.Level = level;
            return View();
        }
        #endregion
        #region[CategoryAddSub]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CategoryAddSub(FormCollection collection, Category catego, string level)
        {
            if (Request.Cookies["Username"] != null)
            {
                var grp = db.Categories.Where(m => m.Level == level).FirstOrDefault();
                var Name = collection["Name"];
                var Content = collection["Content"];
                var Ord = collection["Ord"];
                catego.Tag = StringClass.NameToTag(Name);
                catego.Name = Name;
                catego.Ord = Convert.ToInt32(Ord);
                catego.IdGroupPro = Convert.ToInt32(grp.IdGroupPro);
                catego.Active = (collection["Active"] == "false") ? false : true;
                catego.Priority = (collection["Priority"] == "false") ? false : true;
                catego.Index = (collection["Index"] == "false") ? false : true;
                catego.Description = collection["Description"];
                catego.Keyword = collection["Keyword"];
                catego.Title = collection["Title"];
                catego.Level = level + "00000";
                db.Categories.Add(catego);
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion
        #region[CategoryDelete]
        public ActionResult CategoryDelete(int id)
        {
            if (Request.Cookies["Username"] != null)
            {
                var del = (from categaa in db.Categories where categaa.Id == id select categaa).Single();
                db.Categories.Remove(del);
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion
        #region[CategoryActive]
        public ActionResult CategoryActive(int id)
        {
            if (Request.Cookies["Username"] != null)
            {
                var act = (from catego in db.Categories where catego.Id == id select catego).Single();
                if (act.Active == true)
                {
                    act.Active = false;
                }
                else { act.Active = true; }
                db.SaveChanges();
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion
        #region[MultiDelete]
        public ActionResult MultiDelete()
        {
            if (Request.Cookies["Username"] != null)
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
                            var Del = (from emp in db.Categories where emp.Id == id select emp).SingleOrDefault();
                            db.Categories.Remove(Del);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion



    }
}
