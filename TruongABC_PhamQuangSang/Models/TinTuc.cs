using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class TinTuc
{
    public int IdTin { get; set; }

    public string TenTin { get; set; } = null!;

    public string TomTat { get; set; } = null!;

    public string? NoiDung { get; set; }

    public int? NhomTin { get; set; }

    public string? Anh { get; set; }

    public virtual NhomTin? NhomTinNavigation { get; set; }
}
