using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VKHGDDTNN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Home", url: "", defaults: new { controller = "Home", action = "Index" });


            #region[Quản trị]
            routes.MapRoute("Quản trị - đăng nhập", url: "admin", defaults: new { controller = "Admin", action = "Index" });
            routes.MapRoute("Quản trị - cấu hình", url: "admin/cau-hinh", defaults: new { controller = "Admin", action = "CauHinh" });
            routes.MapRoute("Quản trị - đăng xuất", url: "admin/dang-xuat", defaults: new { controller = "Admin", action = "DangXuat" });
            routes.MapRoute("Quản trị - mặc định", url: "admin/default", defaults: new { controller = "Admin", action = "Default" });
            #endregion


            routes.MapRoute("Liên hệ", "lien-he", defaults: new { controller = "Contact", action = "Create" });
            routes.MapRoute("Hỏi đáp", "menu/hoi-dap", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("htvdv", "menu/hop-tac-va-dich-vu", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("lkw", "menu/lien-ket-website", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("ttnoibo", "menu/thong-tin-noi-bo", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("tv", "menu/thu-vien", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("sp", "menu/san-pham", defaults: new { controller = "NewsView", action = "Updating" });
            routes.MapRoute("gtt", "menu/gioi-thieu", defaults: new { controller = "NewsView", action = "MenuList" });
            routes.MapRoute("MENU", "menu/{tag}", defaults: new { controller = "NewsView", action = "List" });
            routes.MapRoute("MENU tin", "menu-tin/{tag}", defaults: new { controller = "NewsView", action = "RootMenuDetail" });
            routes.MapRoute("MENU con tin", "menu-tin-con/{tag}", defaults: new { controller = "NewsView", action = "MenuDetail" });
            routes.MapRoute("danh sach tin", "menu-con/{tag}", defaults: new { controller = "NewsView", action = "ListNews" });
            routes.MapRoute("danh sach tin page", "menu-con/{tag}/{page}", defaults: new { controller = "NewsView", action = "ListNews" });
            routes.MapRoute("tin", "{tag}", defaults: new { controller = "NewsView", action = "NewsDetail" });
          

            #region[Quản trị Module Danh mục sản phẩm]
            routes.MapRoute("NSP - index", url: "admin/danh-muc-san-pham", defaults: new { controller = "GroupProduct", action = "Index" });
            routes.MapRoute("NSP - them", url: "admin/danh-muc-san-pham/them", defaults: new { controller = "GroupProduct", action = "Create" });
            routes.MapRoute("NSP - sua", url: "admin/danh-muc-san-pham/sua", defaults: new { controller = "GroupProduct", action = "Edit" });
            routes.MapRoute("NSP - xoa", url: "admin/danh-muc-san-pham/xoa", defaults: new { controller = "GroupProduct", action = "Delete" });
            routes.MapRoute("NSP - them cap con", url: "admin/danh-muc-san-pham/them-cap-con", defaults: new { controller = "GroupProduct", action = "CreateSub" });
            #endregion

            #region[Quản trị Module Sản phẩm]
            routes.MapRoute("sp - Danh sách sản phẩm", url: "admin/san-pham", defaults: new { controller = "Product", action = "Index" });
            routes.MapRoute("sp - Thêm sản phẩm", url: "admin/san-pham/them", defaults: new { controller = "Product", action = "Create" });
            routes.MapRoute("sp - them 2", url: "admin/san-pham/them2", defaults: new { controller = "Product", action = "Create2" });
            routes.MapRoute("sp - Sửa sản phẩm", url: "admin/san-pham/sua", defaults: new { controller = "Product", action = "Edit" });
            routes.MapRoute("sp - Xóa sản phẩm", url: "admin/san-pham/xoa", defaults: new { controller = "Product", action = "Delete" });
            //routes.MapRoute("sp - Xem ảnh sản phẩm", url: "admin/san-pham/xem-Image", defaults: new { controller = "Product", action = "ProductViewImgot" });
            routes.MapRoute("sp - Xem ảnh sản phẩm 2", url: "admin/san-pham/xem-Image", defaults: new { controller = "Product", action = "ProductViewImg" });
            routes.MapRoute("sp - Sửa ảnh sản phẩm", url: "admin/san-pham/sua-Image", defaults: new { controller = "Product", action = "ProductEditImg" });
            routes.MapRoute("sp - Thêm ảnh sản phẩm", url: "admin/san-pham/them-Image", defaults: new { controller = "Product", action = "ProductAddImg" });
            routes.MapRoute("sp - Xóa ảnh sản phẩm", url: "admin/san-pham/xoa-Image", defaults: new { controller = "Product", action = "ProductDelImg" });
            #endregion

            #region[Quản trị Module Menu]
            routes.MapRoute("Menu - danh sách", url: "admin/menu", defaults: new { controller = "Menu", action = "Index" });
            routes.MapRoute("Menu - thêm", url: "admin/menu/them", defaults: new { controller = "Menu", action = "Create" });
            routes.MapRoute("Menu - sửa", url: "admin/menu/sua", defaults: new { controller = "Menu", action = "Edit" });
            routes.MapRoute("Menu - xóa", url: "admin/menu/xoa", defaults: new { controller = "Menu", action = "Delete" });
            //routes.MapRoute("Menu - nhiều lệnh", url: "admin/menu/nhieu-lenh", defaults: new { controller = "Menu", action = "MultiCommand" });
            routes.MapRoute("Menu - tìm kiếm", url: "admin/menu/tim-kiem", defaults: new { controller = "Menu", action = "AutoComplete" });
            //routes.MapRoute("Menu - thêm nhiều", url: "admin/menu/them-nhieu", defaults: new { controller = "Menu", action = "ThemNhieu"});
            routes.MapRoute("Menu - xóa nhiều", url: "admin/menu/xoa-nhieu", defaults: new { controller = "Menu", action = "MultiDelete" });
            routes.MapRoute("Menu - sửa trực tiếp", url: "admin/menu/sua-truc-tiep", defaults: new { controller = "Menu", action = "UpdateDirect" });
            routes.MapRoute("Menu - sửa trạng thái", url: "admin/menu/sua-trang-thai", defaults: new { controller = "Menu", action = "ChangeActive" });
            routes.MapRoute("Menu - thêm cấp con", url: "admin/menu/them-cap-con", defaults: new { controller = "Menu", action = "CreateSub" });
            routes.MapRoute("Menu - hiển thị số lượng bai-viet", url: "admin/menu/hien-thi-so-luong-bai-viet", defaults: new { controller = "Menu", action = "HienThiSoLuong" });
            routes.MapRoute("Menu - sắp xếp theo tên", url: "admin/menu/sap-xep-theo-ten", defaults: new { controller = "Menu", action = "SortByName" });
            routes.MapRoute("Menu - sửa hiển thị trang chủ", url: "admin/menu/sua-hien-thi-trang-chu", defaults: new { controller = "Menu", action = "SuaHienThiTrangChu" });
            routes.MapRoute("Menu - sửa tên", url: "admin/bai-viet/sua-ten", defaults: new { controller = "Menu", action = "ChangeNews" });
            #endregion

            #region[Quản trị Module Quảng cáo]
            routes.MapRoute("Nhóm quảng cáo - danh sách", url: "admin/quang-cao", defaults: new { controller = "QuangCao", action = "Index" });
            routes.MapRoute("Nhóm quảng cáo - thêm", url: "admin/quang-cao/them", defaults: new { controller = "QuangCao", action = "Them" });
            routes.MapRoute("Nhóm quảng cáo - sửa", url: "admin/quang-cao/sua", defaults: new { controller = "QuangCao", action = "Sua" });
            routes.MapRoute("Nhóm quảng cáo - xóa", url: "admin/quang-cao/xoa", defaults: new { controller = "QuangCao", action = "Xoa" });
            routes.MapRoute("Nhóm quảng cáo - nhiều lệnh", url: "admin/quang-cao/nhieu-lenh", defaults: new { controller = "QuangCao", action = "NhieuLenh" });
            routes.MapRoute("Nhóm quảng cáo - tìm kiếm", url: "admin/quang-cao/tim-kiem", defaults: new { controller = "QuangCao", action = "TimKiem" });
            routes.MapRoute("Nhóm quảng cáo - thêm nhiều", url: "admin/quang-cao/them-nhieu", defaults: new { controller = "QuangCao", action = "ThemNhieu" });
            //routes.MapRoute("Nhóm quảng cáo - xóa nhiều", url: "admin/quang-cao/xoa-nhieu", defaults: new { controller = "QuangCao", action = "XoaNhieu" });
            routes.MapRoute("Nhóm quảng cáo - sửa trực tiếp", url: "admin/quang-cao/sua-truc-tiep", defaults: new { controller = "QuangCao", action = "SuaTrucTiep" });
            //routes.MapRoute("Nhóm quảng cáo - sửa trạng thái", url: "admin/quang-cao/sua-trang-thai", defaults: new { controller = "QuangCao", action = "SuaTrangThai" });
            //routes.MapRoute("Nhóm quảng cáo - thêm cấp con", url: "admin/quang-cao/them-cap-con", defaults: new { controller = "QuangCao", action = "ThemCapCon" });
            #endregion

            #region [Quản trị Module Nhóm ảnh]
            routes.MapRoute("Nhóm ảnh - danh sách", url: "admin/nhom-Image", defaults: new { controller = "GroupPicture", action = "Index" });
            routes.MapRoute("Nhóm ảnh - thêm", url: "admin/nhom-Image/them", defaults: new { controller = "GroupPicture", action = "Create" });
            routes.MapRoute("Nhóm ảnh - sửa", url: "admin/nhom-Image/sua", defaults: new { controller = "GroupPicture", action = "Edit" });
            routes.MapRoute("Nhóm ảnh - xóa", url: "admin/nhom-Image/xoa", defaults: new { controller = "GroupPicture", action = "Delete" });
            //routes.MapRoute("Nhóm ảnh - nhiều lệnh", url: "admin/nhom-Image/nhieu-lenh", defaults: new { controller = "GroupPicture", action = "MultiCommand" });
            //routes.MapRoute("Nhóm ảnh - tìm kiếm", url: "admin/nhom-Image/tim-kiem", defaults: new { controller = "GroupPicture", action = "AutoComplete" });
            ////routes.MapRoute("Nhóm ảnh - thêm nhiều", url: "admin/nhom-Image/them-nhieu", defaults: new { controller = "GroupPicture", action = "ThemNhieu"});
            //routes.MapRoute("Nhóm ảnh - xóa nhiều", url: "admin/nhom-Image/xoa-nhieu", defaults: new { controller = "GroupPicture", action = "MultiDelete" });
            //routes.MapRoute("Nhóm ảnh - sửa trực tiếp", url: "admin/nhom-Image/sua-truc-tiep", defaults: new { controller = "GroupPicture", action = "UpdateDirect" });
            //routes.MapRoute("Nhóm ảnh - sửa trạng thái", url: "admin/nhom-Image/sua-trang-thai", defaults: new { controller = "GroupPicture", action = "ChangeActive" });
            routes.MapRoute("Nhóm ảnh - thêm cấp con", url: "admin/nhom-Image/them-cap-con", defaults: new { controller = "GroupPicture", action = "CreateSub" });
            #endregion

            #region[Quản trị Module ảnh]
            routes.MapRoute("Ảnh - danh sách", url: "admin/anh", defaults: new { controller = "Picture", action = "Index" });
            routes.MapRoute("Ảnh - thêm", url: "admin/anh/them", defaults: new { controller = "Picture", action = "Create" });
            routes.MapRoute("Ảnh - sửa", url: "admin/anh/sua", defaults: new { controller = "Picture", action = "Edit" });
            routes.MapRoute("Ảnh - xóa", url: "admin/anh/xoa", defaults: new { controller = "Picture", action = "Delete" });
            routes.MapRoute("Ảnh - nhiều lệnh", url: "admin/anh/nhieu-lenh", defaults: new { controller = "Picture", action = "MultiCommand" });
            routes.MapRoute("Ảnh - tìm kiếm", url: "admin/anh/tim-kiem", defaults: new { controller = "Picture", action = "Search" });
            routes.MapRoute("Ảnh - thêm nhiều", url: "admin/anh/them-nhieu", defaults: new { controller = "Picture", action = "AddMulti" });
            routes.MapRoute("Ảnh - xóa nhiều", url: "admin/anh/xoa-nhieu", defaults: new { controller = "Picture", action = "MultiCommand" });
            routes.MapRoute("Ảnh - sửa trực tiếp", url: "admin/anh/sua-truc-tiep", defaults: new { controller = "Picture", action = "UpdateDirect" });
            //routes.MapRoute("Ảnh - sửa trạng thái", url: "admin/anh/sua-trang-thai", defaults: new { controller = "Picture", action = "SuaTrangThai" });
            //routes.MapRoute("Ảnh - thêm cấp con", url: "admin/anh/them-cap-con", defaults: new { controller = "Picture", action = "ThemCapCon" });
            #endregion

            #region[Quản trị Module Phản hồi]
            routes.MapRoute("Phản hồi - dphan-hoi sách", url: "admin/phan-hoi", defaults: new { controller = "Contact", action = "Index" });
            routes.MapRoute("Phản hồi - thêm", url: "admin/phan-hoi/them", defaults: new { controller = "Picture", action = "Create" });
            routes.MapRoute("Phản hồi - sửa", url: "admin/phan-hoi/xem-chi-tiet", defaults: new { controller = "Contact", action = "View" });
            routes.MapRoute("Phản hồi - xóa", url: "admin/phan-hoi/xoa", defaults: new { controller = "Contact", action = "Delete" });
            routes.MapRoute("Phản hồi - nhiều lệnh", url: "admin/phan-hoi/nhieu-lenh", defaults: new { controller = "Contact", action = "ChangeStatus" });
            routes.MapRoute("Phản hồi - tìm kiếm", url: "admin/phan-hoi/tim-kiem", defaults: new { controller = "Contact", action = "Search" });
            routes.MapRoute("Phản hồi - thêm nhiều", url: "admin/phan-hoi/them-nhieu", defaults: new { controller = "Contact", action = "AddMulti" });
            routes.MapRoute("Phản hồi - xóa nhiều", url: "admin/phan-hoi/xoa-nhieu", defaults: new { controller = "Contact", action = "MultiCommand" });
            routes.MapRoute("Phản hồi - sửa trực tiếp", url: "admin/phan-hoi/sua-truc-tiep", defaults: new { controller = "Contact", action = "UpdateDirect" });
            //routes.MapRoute("Ảnh - sửa trạng thái", url: "admin/phan-hoi/sua-trang-thai", defaults: new { controller = "Contact", action = "SuaTrangThai" });
            //routes.MapRoute("Ảnh - thêm cấp con", url: "admin/phan-hoi/them-cap-con", defaults: new { controller = "Contact", action = "ThemCapCon" });
            #endregion

            #region [Quản trị Module nhóm bài viết]
            routes.MapRoute("Nhóm bài viết - danh sách", url: "admin/nhom-bai-viet", defaults: new { controller = "GroupNew", action = "Index" });
            routes.MapRoute("Nhóm bài viết - thêm", url: "admin/nhom-bai-viet/them", defaults: new { controller = "GroupNew", action = "Create" });
            routes.MapRoute("Nhóm bài viết - sửa", url: "admin/nhom-bai-viet/sua", defaults: new { controller = "GroupNew", action = "Edit" });
            routes.MapRoute("Nhóm bài viết - xóa", url: "admin/nhom-bai-viet/xoa", defaults: new { controller = "GroupNew", action = "Delete" });
            routes.MapRoute("Nhóm bài viết - nhiều lệnh", url: "admin/nhom-bai-viet/nhieu-lenh", defaults: new { controller = "GroupNew", action = "MultiCommand" });
            routes.MapRoute("Nhóm bài viết - tìm kiếm", url: "admin/nhom-bai-viet/tim-kiem", defaults: new { controller = "GroupNew", action = "AutoComplete" });
            //routes.MapRoute("Nhóm bài viết - thêm nhiều", url: "admin/nhom-bai-viet/them-nhieu", defaults: new { controller = "GroupNew", action = "ThemNhieu"});
            routes.MapRoute("Nhóm bài viết - xóa nhiều", url: "admin/nhom-bai-viet/xoa-nhieu", defaults: new { controller = "GroupNew", action = "MultiDelete" });
            routes.MapRoute("Nhóm bài viết - sửa trực tiếp", url: "admin/nhom-bai-viet/sua-truc-tiep", defaults: new { controller = "GroupNew", action = "UpdateDirect" });
            routes.MapRoute("Nhóm bài viết - sửa trạng thái", url: "admin/nhom-bai-viet/sua-trang-thai", defaults: new { controller = "GroupNew", action = "ChangeActive" });
            routes.MapRoute("Nhóm bài viết - thêm cấp con", url: "admin/nhom-bai-viet/them-cap-con", defaults: new { controller = "GroupNew", action = "CreateSub" });
            #endregion

            #region[Quản trị Module Bài viết]
            routes.MapRoute("Bài viết - danh sách", url: "admin/bai-viet", defaults: new { controller = "News", action = "Index" });
            routes.MapRoute("Bài viết - thêm", url: "admin/bai-viet/them", defaults: new { controller = "News", action = "Create" });
            routes.MapRoute("Bài viết - sửa", url: "admin/bai-viet/sua", defaults: new { controller = "News", action = "Edit" });
            routes.MapRoute("Bài viết - xóa", url: "admin/bai-viet/xoa", defaults: new { controller = "News", action = "Delete" });
            routes.MapRoute("Bài viết - nhiều lệnh", url: "admin/bai-viet/nhieu-lenh", defaults: new { controller = "News", action = "MultiCommand" });
            routes.MapRoute("Bài viết - tìm kiếm", url: "admin/bai-viet/tim-kiem", defaults: new { controller = "News", action = "Search" });
            //routes.MapRoute("Bài viết - xóa nhiều", url: "admin/bai-viet/xoa-nhieu", defaults: new { controller = "News", action = "MultiDelete" });
            routes.MapRoute("Bài viết - sửa trực tiếp", url: "admin/bai-viet/sua-truc-tiep", defaults: new { controller = "News", action = "UpdateDirect" });
            routes.MapRoute("Bài viết - sửa trạng thái", url: "admin/bai-viet/sua-trang-thai", defaults: new { controller = "News", action = "ChangeActive" });
            routes.MapRoute("Bài viết - hiển thị số lượng bai-viet", url: "admin/bai-viet/hien-thi-so-luong-bai-viet", defaults: new { controller = "News", action = "LoadNewsAmount" });
            routes.MapRoute("Bài viết - sắp xếp theo tên", url: "admin/bai-viet/sap-xep-theo-ten", defaults: new { controller = "News", action = "SortByName" });
            routes.MapRoute("Bài viết - sửa hiển thị trang chủ", url: "admin/bai-viet/sua-hien-thi-trang-chu", defaults: new { controller = "News", action = "ChangeIndex" });
            routes.MapRoute("Bài viết - sửa tên", url: "admin/bai-viet/sua-ten", defaults: new { controller = "News", action = "ChangeNews" });
            #endregion

            #region[Quản trị Module thành viên]
            routes.MapRoute("Thành viên - danh sách", url: "admin/thanh-vien", defaults: new { controller = "Member", action = "Index" });
            routes.MapRoute("Thành viên - thêm", url: "admin/thanh-vien/them", defaults: new { controller = "Member", action = "Create" });
            routes.MapRoute("Thành viên - sửa", url: "admin/thanh-vien/sua", defaults: new { controller = "Member", action = "Edit" });
            routes.MapRoute("Thành viên - xóa", url: "admin/thanh-vien/xoa", defaults: new { controller = "Member", action = "Delete" });
            routes.MapRoute("Thành viên - nhiều lệnh", url: "admin/thanh-vien/nhieu-lenh", defaults: new { controller = "Member", action = "MultiCommand" });
            routes.MapRoute("Thành viên - tìm kiếm", url: "admin/thanh-vien/tim-kiem", defaults: new { controller = "Member", action = "Search" });
            //routes.MapRoute("Thành viên - thêm nhiều", url: "admin/thanh-vien", defaults: new { controller = "Member", action = "Index" });
            //routes.MapRoute("Thành viên - xóa nhiều", url: "admin/thanh-vien", defaults: new { controller = "Member", action = "Index" });
            routes.MapRoute("Thành viên - sửa trực tiếp", url: "admin/thanh-vien/sua-truc-tiep", defaults: new { controller = "Member", action = "UpdateDirect" });
            //routes.MapRoute("Thành viên - sửa trạng thái", url: "admin/thanh-vien", defaults: new { controller = "Member", action = "Index" });
            //routes.MapRoute("Thành viên - thêm cấp con", url: "admin/thanh-vien", defaults: new { controller = "Member", action = "Index" });
            #endregion

           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}