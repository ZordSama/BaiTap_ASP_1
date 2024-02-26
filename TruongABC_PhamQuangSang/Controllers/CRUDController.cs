using Microsoft.AspNetCore.Mvc;
using TruongABC_PhamQuangSang.Models;

namespace TruongABC_PhamQuangSang.Controllers.Admin;
public class CRUDController : Controller
{

    private TruongAbcContext _db;

    public CRUDController(TruongAbcContext db){
        _db = db;
    }
    public ActionResult TinTuc(){
        return View("~/Views/Admin/CRUD/Tintuc.cshtml", _db);
    }

    public JsonResult GetTinTuc(int ID){
        return Json(_db.TinTucs.Where(x => x.IdTin == ID).FirstOrDefault());
    }
}