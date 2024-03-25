using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TruongABC_PhamQuangSang.Models;

namespace TruongABC_PhamQuangSang.Controllers;

public class CRUDController : Controller
{
    private TruongAbcContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CRUDController(TruongAbcContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public ActionResult TinTuc()
    {
        return View("~/Views/Admin/CRUD/Tintuc.cshtml", _db);
    }

    public ActionResult GioiThieu()
    {
        return View("~/Views/Admin/CRUD/GioiThieu.cshtml", _db);
    }

    public ActionResult MonHoc()
    {
        return View("~/Views/Admin/CRUD/MonHoc.cshtml", _db);
    }

    public ActionResult Diem()
    {
        return View("~/Views/Admin/CRUD/Diem.cshtml", _db);
    }

    public ActionResult Lop()
    {
        return View("~/Views/Admin/CRUD/Lop.cshtml", _db);
    }

    public ActionResult VanBanPQ()
    {
        return View("~/Views/Admin/CRUD/VanBanPQ.cshtml", _db);
    }

    public ActionResult LienKet()
    {
        return View("~/Views/Admin/CRUD/LienKet.cshtml", _db);
    }

    public ActionResult LichCT()
    {
        return View("~/Views/Admin/CRUD/LichCT.cshtml", _db);
    }

    public ActionResult GiangVien()
    {
        return View("~/Views/Admin/CRUD/GiangVien.cshtml", _db);
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
            var model = new
            {
                ID_TIN = tintuc.IdTin,
                TenTin = tintuc.TenTin,
                TomTat = tintuc.TomTat,
                NoiDung = tintuc.NoiDung,
                Anh = tintuc.Anh,
                TenNhomTin = tintuc.NhomTinNavigation.TenNhomTin
            };
            response.Success = true;
            response.Data = JsonConvert.SerializeObject(model);
            response.Message = "Đã tải: " + tintuc.TenTin;
        }
        else
        {
            response.Success = false;
            response.Message = "Không tìm thấy tin tức này";
        }
        // Console.WriteLine(response.Message);
        return Json(response);
    }

    public JsonResult GetTinTucs()
    {
        var tintucs = _db.TinTucs.ToList();
        JsonResponseViewModel response = new JsonResponseViewModel();
        if (tintucs != null)
        {
            response.Success = true;
            response.Data = JsonConvert.SerializeObject(tintucs);
            response.Message = "Đã tải tất cả tin tức";
        }
        else
        {
            response.Success = false;
        }
        return Json(response);
    }

    [HttpPostAttribute]
    public JsonResult CreateTinTuc(IFormCollection newTinTuc)
    {
        JsonResponseViewModel response = new JsonResponseViewModel();
        TinTuc tintuc = new TinTuc();

        try
        {
            if (newTinTuc != null)
            {
                tintuc.TenTin = Convert.ToString(newTinTuc["TenTin"]);
                // Console.WriteLine(tintuc.TenTin);
                int? idNhomTin = _db
                    .NhomTins.FirstOrDefault(x =>
                        x.TenNhomTin == Convert.ToString(newTinTuc["TenNhomTin"])
                    )
                    ?.IdNhomTin;
                if (idNhomTin != null)
                    tintuc.NhomTin = (int)idNhomTin;
                else
                {
                    tintuc.NhomTin = CreateNhomTin(
                        Convert.ToString(newTinTuc["TenNhomTin"])
                    ).IdNhomTin;
                }
                // Console.WriteLine(tintuc.NhomTin);
                tintuc.TomTat = Convert.ToString(newTinTuc["TomTat"]);
                // Console.WriteLine(tintuc.TomTat);
                tintuc.NoiDung = Convert.ToString(newTinTuc["NoiDung"]);
                // Console.WriteLine(tintuc.NoiDung);
                if (newTinTuc.Files.Count > 0)
                {
                    tintuc.Anh = SaveImage(newTinTuc.Files[0], "TinTuc/");
                }
                _db.TinTucs.Add(tintuc);
                _db.SaveChanges();
                response.Success = true;
                response.Message = "Đã thêm tin tức";
            }
            else
            {
                response.Success = false;
                response.Message = "Không thể thêm tin tức";
            }
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return Json(response);
    }

    [HttpPut]
    public JsonResult UpdateTinTuc(IFormCollection newTinTuc)
    {
        JsonResponseViewModel response = new JsonResponseViewModel();
        // Console.WriteLine(newTinTuc["ID_TIN"]);
        var tintuc = _db.TinTucs.FirstOrDefault(x =>
            x.IdTin == Convert.ToInt32(newTinTuc["ID_TIN"])
        );
        Console.WriteLine(
            _db.TinTucs.FirstOrDefault(x => x.IdTin == Convert.ToInt32(newTinTuc["ID_TIN"]))
        );
        try
        {
            if (tintuc != null)
            {
                tintuc.TenTin = Convert.ToString(newTinTuc["TenTin"]);
                // Console.WriteLine(tintuc.TenTin);
                int? idNhomTin = _db
                    .NhomTins.FirstOrDefault(x =>
                        x.TenNhomTin == Convert.ToString(newTinTuc["TenNhomTin"])
                    )
                    ?.IdNhomTin;
                if (idNhomTin != null)
                    tintuc.NhomTin = (int)idNhomTin;
                else
                {
                    tintuc.NhomTin = _db.NhomTins.Count() + 1;
                    CreateNhomTin(Convert.ToString(newTinTuc["TenNhomTin"]));
                }
                // Console.WriteLine(tintuc.NhomTin);
                tintuc.TomTat = Convert.ToString(newTinTuc["TomTat"]);
                // Console.WriteLine(tintuc.TomTat);
                tintuc.NoiDung = Convert.ToString(newTinTuc["NoiDung"]);
                // Console.WriteLine(tintuc.NoiDung);
                if (newTinTuc.Files.Count > 0)
                {
                    string? currentPath = _db
                        .TinTucs.FirstOrDefault(t => t.IdTin == tintuc.IdTin)
                        ?.Anh;
                    if (currentPath != null)
                        tintuc.Anh = UpdateImage(newTinTuc.Files[0], currentPath, "TinTuc/");
                    else
                        tintuc.Anh = SaveImage(newTinTuc.Files[0], "TinTuc");
                }
                _db.Entry(tintuc).State = EntityState.Modified;
                _db.SaveChanges();
                response.Success = true;
                response.Message = "Đã cập nhật tin tức";
            }
            else
            {
                response.Success = false;
                response.Message = "Không thể cập nhật tin tức";
            }
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return Json(response);
    }

    [HttpDelete]
    public JsonResult DeleteTinTuc(IFormCollection newTinTuc)
    {
        JsonResponseViewModel response = new JsonResponseViewModel();
        int idTin = Convert.ToInt32(newTinTuc["idTin"]);
        try
        {
            Console.WriteLine(idTin);
            var tinTuc = _db.TinTucs.FirstOrDefault(t => t.IdTin == idTin);
            if (tinTuc != null)
            {
                _db.TinTucs.Remove(tinTuc);
                _db.SaveChanges();
                response.Success = true;
                response.Message = "Đã xóa tin tức";
            }
            else
            {
                response.Success = false;
                response.Message = "Không tìm thấy tin tức";
            }
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return Json(response);
    }

    public NhomTin CreateNhomTin(string TenNhomTin)
    {
        NhomTin nhomTin = new NhomTin();
        try
        {
            nhomTin.TenNhomTin = TenNhomTin;
            _db.NhomTins.Add(nhomTin);
            _db.SaveChanges();
            return nhomTin;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return nhomTin;
        }
    }

    public JsonResult GetGioiThieu(int ID)
    {
        GioiThieu? gioithieu = _db.GioiThieus.Find(ID);
        JsonResponseViewModel response = new JsonResponseViewModel();
        if (gioithieu != null)
        {
            var model = new
            {
                IdGt = gioithieu.IdGt,
                TenGt = gioithieu.TenGt,
                NoiDungGt = gioithieu.NoiDungGt
            };
            response.Success = true;
            response.Data = JsonConvert.SerializeObject(model);
            response.Message = "Đã tải: " + gioithieu.TenGt;
        }
        else
        {
            response.Success = false;
            response.Message = "Không tìm thấy giới thiệu này";
        }
        return Json(response);
    }

    [HttpPost]
    public JsonResult CreateGioiThieu(IFormCollection newGioiThieu)
    {
        GioiThieu? gioithieu = JsonConvert.DeserializeObject<GioiThieu>(
            StringifyFormCollection(newGioiThieu)
        );
        try
        {
            // gioithieu = JsonConvert.DeserializeObject<GioiThieu>(JsonConvert.SerializeObject(newGioiThieu));
            if (gioithieu != null)
            {
                System.Console.WriteLine(JsonConvert.SerializeObject(gioithieu));
                _db.Add(gioithieu);
                _db.SaveChanges();
                return Json(new { Success = true, Message = "Added record: " + gioithieu.TenGt });
            }
            return Json(
                new { Success = false, Message = "Có vẻ phrase dữ liện ổn... còn db thì không..." }
            );
        }
        catch (Exception e)
        {
            return Json(new { Success = false, Message = e.Message });
        }
    }

    [HttpPut]
    public JsonResult UpdateGioiThieu(IFormCollection newGioiThieu)
    {
        string data = StringifyFormCollection(newGioiThieu);
        Console.WriteLine(data);
        try
        {
            // Console.WriteLine(newGioiThieu);
            GioiThieu? gioiThieu = JsonConvert.DeserializeObject<GioiThieu>(data);
            // Console.WriteLine(JsonConvert.SerializeObject(gioiThieu));
            // Console.WriteLine(serializedForm);
            // GioiThieu? gioithieu = JsonConvert.DeserializeObject<GioiThieu>(serializedForm);
            // GioiThieu? gioiThieu;
            // Console.WriteLine(JsonConvert.SerializeObject(gioithieu));
            if (gioiThieu != null)
            {
                _db.GioiThieus.Update(gioiThieu);
                _db.SaveChanges();
                return Json(
                    new { success = true, Message = "Updated record \"" + gioiThieu.TenGt + "\"!" }
                );
            }
            return Json(new { success = false, Message = "something isn't right..." });
        }
        catch (Exception e)
        {
            return Json(new { success = false, Message = e.Message });
        }
        // return Json(new { st = "testing" });
    }

    [HttpDelete]
    public JsonResult DeleteGioiThieu(IFormCollection req)
    {
        int id = Convert.ToInt32(req["id"]);
        try
        {
            System.Console.WriteLine(id);
            var gt = _db.GioiThieus.Find(id);
            if (gt != null)
            {
                _db.GioiThieus.Remove(gt);
                _db.SaveChanges();
                return Json(
                    new { success = true, Message = "Deleted record \"" + gt.TenGt + "\"!" }
                );
            }
            return Json(new { success = false, Message = "something isn't right..." });
        }
        catch (Exception e)
        {
            return Json(new { success = false, Message = e.Message });
        }
    }

    public JsonResult GetLichCt(int ID)
    {
        LichCt? gioithieu = _db.LichCts.Find(ID);
        JsonResponseViewModel response = new JsonResponseViewModel();
        if (gioithieu != null)
        {
            response.Success = true;
            response.Data = JsonConvert.SerializeObject(gioithieu);
            response.Message = "Đã tải: ";
        }
        else
        {
            response.Success = false;
            response.Message = "Không tìm thấy";
        }
        return Json(response);
    }

    [HttpPost]
    public JsonResult CreateLichCt(IFormCollection newLichCt)
    {
        LichCt? lichct = JsonConvert.DeserializeObject<LichCt>(StringifyFormCollection(newLichCt));
        try
        {
            // lichct = JsonConvert.DeserializeObject<LichCt>(JsonConvert.SerializeObject(newLichCt));
            if (lichct != null)
            {
                System.Console.WriteLine(JsonConvert.SerializeObject(lichct));
                _db.Add(lichct);
                _db.SaveChanges();
                return Json(new { Success = true, Message = "Added record: " + lichct.Id });
            }
            return Json(
                new { Success = false, Message = "Có vẻ phrase dữ liện ổn... còn db thì không..." }
            );
        }
        catch (Exception e)
        {
            return Json(new { Success = false, Message = e.Message });
        }
    }

    [HttpPut]
    public JsonResult UpdateLichCt(IFormCollection newLichCt)
    {
        string data = StringifyFormCollection(newLichCt);
        Console.WriteLine(data);
        try
        {
            // Console.WriteLine(newLichCt);
            LichCt? lichct = JsonConvert.DeserializeObject<LichCt>(data);
            // Console.WriteLine(JsonConvert.SerializeObject(lichct));
            // Console.WriteLine(serializedForm);
            // LichCt? lichct = JsonConvert.DeserializeObject<LichCt>(serializedForm);
            // LichCt? lichct;
            // Console.WriteLine(JsonConvert.SerializeObject(lichct));
            if (lichct != null)
            {
                _db.LichCts.Update(lichct);
                _db.SaveChanges();
                return Json(
                    new { success = true, Message = "Updated record \"" + lichct.Id + "\"!" }
                );
            }
            return Json(new { success = false, Message = "something isn't right..." });
        }
        catch (Exception e)
        {
            return Json(new { success = false, Message = e.Message });
        }
        // return Json(new { st = "testing" });
    }

    [HttpDelete]
    public JsonResult DeleteLichCt(IFormCollection req)
    {
        int id = Convert.ToInt32(req["id"]);
        try
        {
            System.Console.WriteLine(id);
            var gt = _db.LichCts.Find(id);
            if (gt != null)
            {
                _db.LichCts.Remove(gt);
                _db.SaveChanges();
                return Json(new { success = true, Message = "Deleted record \"" + gt.Id + "\"!" });
            }
            return Json(new { success = false, Message = "something isn't right..." });
        }
        catch (Exception e)
        {
            return Json(new { success = false, Message = e.Message });
        }
    }

    private string SaveImage(IFormFile file, string folder)
    {
        // Generate a unique file name to avoid overwriting existing files
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        // Determine the path where the file will be saved
        string uploadPath = Path.Combine(
            _webHostEnvironment.WebRootPath,
            "assets",
            "img",
            folder,
            fileName
        );

        // Save the file to the server
        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return "~/assets/img/" + folder + fileName;
    }

    private string UpdateImage(IFormFile file, string currentImagePath, string folder)
    {
        if (currentImagePath != null)
        {
            // Determine the path to the current image
            string currentImagePathFull = Path.Combine(
                _webHostEnvironment.WebRootPath,
                currentImagePath.Substring(2)
            );

            // Check if the current image exists
            if (System.IO.File.Exists(currentImagePathFull))
            {
                // If the current image exists, delete it
                System.IO.File.Delete(currentImagePathFull);
            }
        }
        return SaveImage(file, folder);
    }

    public string StringifyFormCollection(IFormCollection data)
    {
        var output = new Dictionary<string, string?>();
        foreach (var (key, value) in data.AsEnumerable())
        {
            Console.WriteLine($"{key} = {value}");
            output[key] = value;
        }
        return JsonConvert.SerializeObject(output);
    }
}
