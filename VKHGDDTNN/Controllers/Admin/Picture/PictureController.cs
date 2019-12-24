using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VKHGDDTNN.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Data;
using System.IO;


namespace VKHGDDTNN.Controllers.Admin.Picture
{
    public class PictureController : Controller
    {
        VKHGDDTNNEntities db = new VKHGDDTNNEntities();

        #region[Danh sách (Index)]
        public ActionResult Index(string sortOrder, string sortName, int? trang, string positionSearch, string sortPos, string getSortNameClass, string getSortOrderClass)
        {
            var all = db.Pictures.OrderByDescending(a => a.Ord).ToList();

            #region Chọn vị trí đặt ảnh (Controls SelectListItem)
            List<SelectListItem> sllTar = new List<SelectListItem>();
            List<SelectListItem> sllPos = new List<SelectListItem>();
            sllTar = CreateTarget("_blank", "_self", "_parient", "_top");
            sllPos = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide","Thumb Dịch vụ");
            ViewBag.Target = sllTar;
            ViewBag.Position = sllPos;
            #endregion SelectListItem

            int pageSize = 10;

            int pageNumber = (trang ?? 1);

            #region Lấy trang cuối (GetLastPage)
            // begin [get last page]
            if (trang != null)
                ViewBag.mPage = (int)trang;
            else
                ViewBag.mPage = 1;

            int lastPage = all.Count / pageSize;
            if (all.Count % pageSize > 0)
            {
                lastPage++;
            }
            ViewBag.LastPage = lastPage;

            ViewBag.PageSize = pageSize;
            //end [get last page]
            #endregion GetLastPage

            #region Thứ tự (Order)
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.SortOrderParm = sortOrder == "ordAsc" ? "ordDesc" : "ordAsc";
            ViewBag.CurrentSortName = sortName;
            ViewBag.SortNameParm = sortName == "nameAsc" ? "nameDesc" : "nameAsc";
            
            switch (sortOrder)
            {
                case "ordDesc":
                    all = db.Pictures.OrderByDescending(a => a.Ord).ToList();
                    break;
                case "ordAsc":
                    all = db.Pictures.OrderBy(a => a.Ord).ToList();
                    break;
                default:
                    break;
            }

            switch (sortName)
            {
                case "nameDesc":
                    all = db.Pictures.OrderByDescending(a => a.Name).ToList();
                    break;
                case "nameAsc":
                    all = db.Pictures.OrderBy(a => a.Name).ToList();
                    break;
                default:
                    break;
            }
            #endregion Order

            PagedList<VKHGDDTNN.Models.Picture> img = (PagedList<VKHGDDTNN.Models.Picture>)all.ToPagedList(pageNumber, pageSize);

            /// Kiem tra co tim kiem theo vi tri va phan trang theo vi tri khong
            if ((String.IsNullOrEmpty(positionSearch) || positionSearch == "") && (sortPos == null))
            {
                if (Request.IsAjaxRequest())
                    return PartialView("_Data", img);
                else
                    return View(img);
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
                        all = db.Pictures.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                    else if (sortName == "nameDesc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                    else if (sortOrder == "ordDesc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Ord).ToList();
                    else if (sortOrder == "ordAsc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderBy(a => a.Ord).ToList();
                    else
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                }
                else /// ko phan trang
                {
                    ViewBag.currentPos = positionSearch;
                    pos = Convert.ToInt32(positionSearch);
                    if (sortName == "nameAsc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderBy(a => a.Name).ToList();
                    else if (sortName == "nameDesc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Name).ToList();
                    else if (sortOrder == "ordDesc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Ord).ToList();
                    else if (sortOrder == "ordAsc")
                        all = db.Pictures.Where(a => a.Position == pos).OrderBy(a => a.Ord).ToList();
                    else
                        all = db.Pictures.Where(a => a.Position == pos).OrderByDescending(a => a.Id).ToList();
                }

                //sllPos[pos].Selected = true;

                img = (PagedList<VKHGDDTNN.Models.Picture>)all.ToPagedList(pageNumber, pageSize);

                if (Request.IsAjaxRequest())
                    return PartialView("_Data", img);
                else
                    return View(img);
            }
        }
        #endregion

        #region[Thêm (Create)]

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Position = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide", "Thumb Dịch vụ");
            ViewBag.Target = CreateTarget("_blank", "_self", "_parient", "_top");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, VKHGDDTNN.Models.Picture img)
        {
            if (Request.Cookies["Username"] != null)
            {
                img.Name = collection["Name"];
                img.Image = collection["Image"];
                img.Width = Convert.ToInt32(collection["Width"]);
                img.Height = Convert.ToInt32(collection["Height"]);
                img.Ord = Convert.ToInt32(collection["Ord"]);
                img.Link = collection["Link"];
                img.Click = Convert.ToInt32(collection["Click"]);
                img.Active = (collection["Active"] == "false") ? false : true;
                img.Position = Convert.ToInt32(collection["Position"]);
                img.Description = collection["Description"];

                if (Convert.ToInt32(collection["Target"]) == 0)
                {
                    img.Target = "_blank";
                }
                else if (Convert.ToInt32(collection["Target"]) == 1)
                {
                    img.Target = "_self";
                }
                else if (Convert.ToInt32(collection["Target"]) == 2)
                {
                    img.Target = "_Parient";
                }
                else if (Convert.ToInt32(collection["Target"]) == 3)
                {
                    img.Target = "_top";
                }
                img.Click = 0;
                db.Pictures.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion ImageCreate

        #region [Sửa (Edit)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var edit = db.Pictures.SingleOrDefault(m => m.Id == id);
            int idimg = edit.Id;
            ViewBag.Id = idimg;
            ViewBag.Position = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide", "Thumb Dịch vụ");
            ViewBag.Target = CreateTarget("_blank", "_self", "_parient", "_top");

            return View(edit);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection collection, VKHGDDTNN.Models.Picture img)
        {
            if (Request.Cookies["Username"] != null)
            {
                img.Name = collection["Name"];
                img.Image = collection["Image"];
                img.Height = Convert.ToInt32(collection["Height"]);
                img.Width = Convert.ToInt32(collection["Width"]);
                img.Position = Convert.ToInt32(collection["Position"]);
                img.Link = collection["Link"];
                img.Target = collection["Target"];
                img.Ord = Convert.ToInt32(collection["Ord"]);
                img.Active = (collection["Active"] == "false") ? false : true;
                img.Description = collection["Description"];             
                db.Entry(img).State = EntityState.Modified;
                db.sp_Picture_Update(img.Id, img.Name, img.Description, img.Image, img.Link, img.Position, img.Ord, img.Active, img.Width, img.Height, img.Click, img.Target );
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion Edit

        #region [Xóa (Delete)]
        public ActionResult Delete(int id, int trang, int cotrang)
        {
            if (Request.Cookies["Username"] != null)
            {
                var del = db.Pictures.Where(m => m.Id == id).SingleOrDefault();

                db.Pictures.Remove(del);
                db.SaveChanges();

                List<VKHGDDTNN.Models.Picture> img = db.Pictures.ToList();

                if ((img.Count % cotrang) == 0)
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

        #region[Nhiều lệnh (MultiCommand)]
        [HttpPost]
        public ActionResult MultiCommand(FormCollection collect)
        {
            if (Request.Cookies["Username"] != null)
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
                                var del = db.Pictures.Where(sp => sp.Id == id).SingleOrDefault();
                                db.Pictures.Remove(del);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else if (collect["Position"] != null)
                {
                    //string i = collect["sllPossition"];
                    Session["Position"] = collect["Position"];
                    return RedirectToAction("Index", "Picture");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion

        #region[Tìm kiếm (Search)]
        [HttpPost]
        public ActionResult Search(string searchString, int? page)
        {
            if (Request.Cookies["Username"] != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);

                PagedList<VKHGDDTNN.Models.Picture> img = (PagedList<VKHGDDTNN.Models.Picture>)db.Pictures.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(a => a.Name).ToPagedList(pageNumber, pageSize);

                if (img.Count > 0)
                    return PartialView("_Data", img);
                else
                    return PartialView("ErrorSearch");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }
        #endregion Search
         
        #region [Thêm nhiều (AddMulti)]
        public ActionResult AddMulti()
        {
            ViewBag.Position = CreateSSL("Chưa đặt vị trí", "Logo", "Thumb Menu", "Slide","Thumb Dịch vụ");
            return View();
        }

        [HttpPost]
        public ActionResult AddMulti(IEnumerable<HttpPostedFileBase> fileImg, FormCollection aForm)
        {
            if (Request.Cookies["Username"] != null)
            {
                int ID;
                var tmp = db.Pictures.Select(a => (int?)a.Id).Max();
                //if (tmp == null)
                //    tmp = 1;
                foreach (var file in fileImg)
                {
                    if (tmp == null)
                    { ID = 1; tmp = 1; }
                    else
                        ID = db.Pictures.Select(a => a.Id).Max();

                    if (file.ContentLength > 0)
                    {
                        //var b = (from k in db.ProImages select k.Id).Max();
                        var ab = Request.Files["fileImg"];
                        String FileExtn = System.IO.Path.GetExtension(file.FileName).ToLower();
                        if (!(FileExtn == ".jpg" || FileExtn == ".png" || FileExtn == ".gif"))
                        {
                            ViewBag.error = "Only jpg, gif and png files are allowed!";
                        }
                        else
                        {
                            VKHGDDTNN.Models.Picture img = new VKHGDDTNN.Models.Picture();
                            var Filename = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath(Url.Content("/Content/Images/")), Filename);
                            file.SaveAs(path);

                            if (Convert.ToInt32(aForm["Position"]) == 0)
                                img.Position = 0;
                            else if (Convert.ToInt32(aForm["Position"]) == 1)
                                img.Position = 1;
                            else if (Convert.ToInt32(aForm["Position"]) == 2)
                                img.Position = 2;
                            else if (Convert.ToInt32(aForm["Position"]) == 3)
                                img.Position = 3;
                            img.Id = ID + 1;
                            img.Name = "Ảnh thứ " + ID;
                            img.Ord = 0;
                            img.Click = 0;
                            img.Target = "_top";
                            img.Width = 0;
                            img.Height = 0;
                            //aImage.Position = 0;
                            img.Image = "/Content/Images/" + Filename;
                            //img.Date = DateTime.Now;
                            db.Pictures.Add(img);
                            db.SaveChanges();
                        }
                    }
                    var fd = file;
                }         
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Default", "Admin");
            }
        }

        #endregion AddMulti

        #region[Sửa trực tiếp (UpdateDirect)]
        public ActionResult UpdateDirect(int id, string order, string width, string height, string name, string image)
        {
            var img = db.Pictures.Find(id);
            var result = string.Empty;
            if (img != null)
            {
                if (order != null)
                {
                    img.Ord = Convert.ToInt32(order);
                    result = "Thứ tự đã được thay đổi.";
                }
                else if (width != null)
                {
                    img.Width = Convert.ToInt32(width);
                    result = "Độ rộng ảnh đã được thay đổi.";
                }
                else if (height != null)
                {
                    img.Height = Convert.ToInt32(height);
                    result = "Chiều cao ảnh đã được thay đổi.";
                }
                else if (name != null)
                {
                    img.Name = name;
                    result = "Tên ảnh đã được thay đổi.";
                }
                else if (image != null)
                {
                    img.Image = image;
                    result = "ảnh đã được thay đổi.";
                }
                else
                {
                    img.Active = img.Active == true ? false : true;
                    result = "Trạng thái ảnh đã được thay đổi.";
                }
            }

            db.Entry(img).State = EntityState.Modified;
            db.SaveChanges();
            //var result = "Dữ liệu đã được thay đổi.";// userModule.Active;
            return Json(result);
        }
        #endregion

        #region [Các phương thức khác (OtherMethods)]

        public List<SelectListItem> CreateSSL(string v1, string v2, string v3, string v4, string v5)
        {
            List<SelectListItem> sll = new List<SelectListItem>();
            sll.Add(new SelectListItem { Value = "0", Text = v1 });
            sll.Add(new SelectListItem { Value = "1", Text = v2 });
            sll.Add(new SelectListItem { Value = "2", Text = v3 });
            sll.Add(new SelectListItem { Value = "3", Text = v4 });
            sll.Add(new SelectListItem { Value = "4", Text = v5 });
            return sll;
        }

        public List<SelectListItem> CreateTarget(string v1, string v2, string v3, string v4)
        {
            List<SelectListItem> sll = new List<SelectListItem>();

            sll.Add(new SelectListItem { Value = "0", Text = v1 });
            sll.Add(new SelectListItem { Value = "1", Text = v2 });
            sll.Add(new SelectListItem { Value = "2", Text = v3 });
            sll.Add(new SelectListItem { Value = "3", Text = v4 });
            return sll;
        }

        #region Target
        public List<SelectListItem> Target()
        {
            List<SelectListItem> Target = new List<SelectListItem>();
            Target.Add(new SelectListItem { Value = "0", Text = "_blank" });
            Target.Add(new SelectListItem { Value = "1", Text = "_self" });
            Target.Add(new SelectListItem { Value = "2", Text = "_parient" });
            Target.Add(new SelectListItem { Value = "3", Text = "_top" });
            return Target;
        }
        #endregion Target

        #region Vị trí (Position)
        public List<SelectListItem> Position()
        {
            List<SelectListItem> Position = new List<SelectListItem>();
            Position.Add(new SelectListItem { Value = "0", Text = "Chưa đặt vị trí" });
            Position.Add(new SelectListItem { Value = "1", Text = "Logo" });        
            Position.Add(new SelectListItem { Value = "2", Text = "Slide" });

            return Position;
        }
        #endregion Position
        #endregion       
    }
}
