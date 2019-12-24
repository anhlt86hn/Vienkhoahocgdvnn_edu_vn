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

namespace VKHGDDTNN.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();

        #region Index
        public ActionResult Index(string sortDate, string sortName, int? trang, string dateSearch, string sortDatePagelist)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                //var all = db.GroupNews.OrderByDescending(g => g.Ord).ToList();
                var all = db.Contacts.OrderBy(g => g.SDate).ToList();
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
                
                #region OrderDate
                
                ViewBag.CurrentSortDate = sortDate;
                ViewBag.SortDateParm = sortDate == "dateAsc" ? "dateDesc" : "dateAsc";

                switch (sortDate)
                {
                    case "dateDesc":
                        all = db.Contacts.OrderByDescending(a => a.SDate).ToList();
                        break;
                    case "dateAsc":
                        all = db.Contacts.OrderBy(a => a.SDate).ToList();
                        break;
                    default:
                        break;
                }

               
                #endregion Order

                #region PagedList
                PagedList<Contact> ct = (PagedList<Contact>)all.ToPagedList(pageNumber, pageSize);
                /// Kiem tra co tim kiem theo vi tri va phan trang theo vi tri khong
                
                    ct = (PagedList<VKHGDDTNN.Models.Contact>)all.ToPagedList(pageNumber, pageSize);
                    #endregion

                    if (Request.IsAjaxRequest())
                    return PartialView("_Data", ct);                

                return View(ct);
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion


        [HttpGet]
        public ActionResult Create()
        {        
                return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, Contact contact)
        {
            try
            {
                var menu = db.Menus.Where(m => m.Active == true && m.Name == "Liên hệ").SingleOrDefault();
                string menuName = menu.Name; ViewBag.MenuName = menuName;
                string menuLink = menu.Link; ViewBag.MenuLink = menuLink;

                contact.Name = collection["name"];
                contact.Email = collection["email"];
                contact.Title = collection["title"];
                contact.Detail = collection["content"];
                contact.SDate = System.DateTime.Now;
                contact.Status = (collection["Status"] == "false") ? false : true;
                db.sp_Contact_Insert(contact.Name, contact.Title, contact.Email, contact.Phone, contact.Detail, contact.SDate, contact.Status);
                db.SaveChanges();
                #region[Title, Keyword, Deskription]
                if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
                if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
                if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
                #endregion

                return RedirectToAction("Create","Contact");
            }
            catch
            {
                return View();
            }

          
        }


       


        public ActionResult ContactCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactCreate(FormCollection collection, Contact contact)
        {
            try
            {
                var menu = db.Menus.Where(m => m.Active == true && m.Name == "Liên hệ").SingleOrDefault();
                string menuName = menu.Name; ViewBag.MenuName = menuName;
                string menuLink = menu.Link; ViewBag.MenuLink = menuLink;


                     #region[Title, Keyword, Deskription]
                if (menu.Title.Length > 0) { ViewBag.tit = menu.Title; } else { ViewBag.tit = menu.Name; }
                if (menu.Description.Length > 0) { ViewBag.des = menu.Description; } else { ViewBag.des = menu.Name; }
                if (menu.Keyword.Length > 0) { ViewBag.key = menu.Keyword; } else { ViewBag.key = menu.Name; }
                #endregion

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ContactEdit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactEdit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("ContactIndex");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ContactDelete(int id)
        {
            return View();
        }

        #region MultiCommand
        public ActionResult MultiCommand(FormCollection collect)
        {
            int m = int.Parse(collect["mPage"]);
            int pagesize = int.Parse(collect["PageSize"]);

            List<Contact> ct = db.Contacts.ToList();
            int lastpage = ct.Count / pagesize;
            if (ct.Count % pagesize > 0)
            {
                lastpage++;
            }
            if (Request.Cookies["Username"] != null)
            {

                if (collect["btnDelete"] != null)
                {
                    //string str = "";
                    foreach (string key in Request.Form)
                    {
                        var checkbox = "";
                        if (key.Contains("chk"))
                        {
                            if (key.Contains("chkIndex") || key.Contains("chkStatus"))
                            {

                            }
                            else
                            {
                                checkbox = Request.Form["" + key];
                                if (checkbox != "false")
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 3));
                                    var Del = (from del in db.Contacts where del.Id == id select del).SingleOrDefault();
                                    db.Contacts.Remove(Del);
                                    db.SaveChanges();
                                }
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

          

            }          
                return RedirectToAction("Index", "Admin");           
        }
        #endregion

        #region ChangeStatus
        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            var m = db.Contacts.Find(id);
            if (m != null)
            {
                m.Status = m.Status == true ? false : true;
            }
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            var results = "Trạng thái đã được thay đổi.";
            return Json(results);
        }
        #endregion




        #region Delete
        [HttpGet]
        public ActionResult Delete(int id, int trang, int kichco)
        {
            if ((Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Values["Permiss"].ToString() == "3") || (Request.Cookies["Username"].Values["Permiss"].ToString() == "2"))
            {
                var mDelete = db.Contacts.Where(m => m.Id == id).SingleOrDefault();

                db.Contacts.Remove(mDelete);
                db.SaveChanges();

                List<VKHGDDTNN.Models.Contact> adv = db.Contacts.ToList();

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


        public ActionResult View(int id)
        {
            var c = db.Contacts.Find(id);
            
            return View(c);
        }
    }
}
