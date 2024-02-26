using Microsoft.AspNetCore.Mvc;
using TruongABC_PhamQuangSang.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TruongABC_PhamQuangSang.Controllers.Admin;
public class CRUDController : Controller
{

    private TruongAbcContext _db;

    public CRUDController(TruongAbcContext db)
    {
        _db = db;
    }
    public ActionResult TinTuc()
    {
        return View("~/Views/Admin/CRUD/Tintuc.cshtml", _db);
    }

    public JsonResult GetTinTuc(int ID)
    {
        TinTuc? tintuc = _db.TinTucs.Find(ID);
        JsonResponseViewModel response = new JsonResponseViewModel();
        if (tintuc != null)
        {
            NhomTin? nhomTin = _db.NhomTins.Find(tintuc.NhomTin);
            string TenNhomTin;
            if (nhomTin != null)
                TenNhomTin = nhomTin.TenNhomTin;
            var model = new { ID_TIN = tintuc.IdTin, TenTin = tintuc.TenTin,TomTat = tintuc.TomTat,  NoiDung = tintuc.NoiDung, Anh = tintuc.Anh, TenNhomTin = tintuc.NhomTinNavigation.TenNhomTin };
            response.Success = true;
            response.Data = JsonSerializer.Serialize(model);
            response.Message = "Đã tải: " + tintuc.TenTin;
        }
        else
        {
            response.Success = false;
            response.Message = "Không tìm thấy tin tức này";
        }
        Console.WriteLine(response.Message);
        return Json(response);
    }
}