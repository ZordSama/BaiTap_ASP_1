using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TruongABC_PhamQuangSang.Models;

namespace TruongABC_PhamQuangSang.Controllers;

public class AuthController : Controller
{
    private TruongAbcContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AuthController(TruongAbcContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }


    public ActionResult Index()
    {
        return View("~/Views/Admin/Auth.cshtml");
    }

    public JsonResult Login(IFormCollection req)
    {
        var username = req["TenNgDung"];
        var password = req["Passwd"];
        NguoiDung? user = _db.NguoiDungs.FirstOrDefault(u => u.TenNgDung == username && u.Passwd == password);
        if (user == null)
            return Json(new {success = false, message = "Invalid username or password!"});
        // Your login logic here
        return Json(new { success = true, message = "Login successful!" });
    }
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Auth");
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
