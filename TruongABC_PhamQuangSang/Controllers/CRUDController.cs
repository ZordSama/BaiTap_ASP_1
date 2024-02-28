using Microsoft.AspNetCore.Mvc;
using TruongABC_PhamQuangSang.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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
            var model = new { ID_TIN = tintuc.IdTin, TenTin = tintuc.TenTin, TomTat = tintuc.TomTat, NoiDung = tintuc.NoiDung, Anh = tintuc.Anh, TenNhomTin = tintuc.NhomTinNavigation.TenNhomTin };
            response.Success = true;
            response.Data = JsonSerializer.Serialize(model);
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
            response.Data = JsonSerializer.Serialize(tintucs);
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
                int? idNhomTin = _db.NhomTins.FirstOrDefault(x => x.TenNhomTin == Convert.ToString(newTinTuc["TenNhomTin"]))?.IdNhomTin;
                if (idNhomTin != null)
                    tintuc.NhomTin = (int)idNhomTin;
                else
                {
                    tintuc.NhomTin =  CreateNhomTin(Convert.ToString(newTinTuc["TenNhomTin"])).IdNhomTin;

                }
                // Console.WriteLine(tintuc.NhomTin);
                tintuc.TomTat = Convert.ToString(newTinTuc["TomTat"]);
                // Console.WriteLine(tintuc.TomTat);
                tintuc.NoiDung = Convert.ToString(newTinTuc["NoiDung"]);
                // Console.WriteLine(tintuc.NoiDung);
                if (newTinTuc.Files.Count > 0)
                {
                    tintuc.Anh = SaveImage(newTinTuc.Files[0]);
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
        var tintuc = _db.TinTucs.FirstOrDefault(x => x.IdTin == Convert.ToInt32(newTinTuc["ID_TIN"]));
        Console.WriteLine(_db.TinTucs.FirstOrDefault(x => x.IdTin == Convert.ToInt32(newTinTuc["ID_TIN"])));
        try
        {
            if (tintuc != null)
            {
                tintuc.TenTin = Convert.ToString(newTinTuc["TenTin"]);
                // Console.WriteLine(tintuc.TenTin);
                int? idNhomTin = _db.NhomTins.FirstOrDefault(x => x.TenNhomTin == Convert.ToString(newTinTuc["TenNhomTin"]))?.IdNhomTin;
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
                    string? currentPath = _db.TinTucs.FirstOrDefault(t => t.IdTin == tintuc.IdTin)?.Anh;
                    if (currentPath != null)
                        tintuc.Anh = UpdateImage(newTinTuc.Files[0], currentPath);
                    else
                        tintuc.Anh = SaveImage(newTinTuc.Files[0]);
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
    private string SaveImage(IFormFile file)
    {
        // Generate a unique file name to avoid overwriting existing files
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        // Determine the path where the file will be saved
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "TinTuc", fileName);

        // Save the file to the server
        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return "~/assets/img/TinTuc/" + fileName;
    }
    private string UpdateImage(IFormFile file, string currentImagePath)
    {
        if (currentImagePath != null)
        {
            // Determine the path to the current image
            string currentImagePathFull = Path.Combine(_webHostEnvironment.WebRootPath, currentImagePath.Substring(2));

            // Check if the current image exists
            if (System.IO.File.Exists(currentImagePathFull))
            {
                // If the current image exists, delete it
                System.IO.File.Delete(currentImagePathFull);
            }
        }
        return SaveImage(file);
    }
}