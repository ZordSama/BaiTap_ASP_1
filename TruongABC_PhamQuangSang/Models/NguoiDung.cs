using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class NguoiDung
{
    public string Id { get; set; } = null!;

    public string TenNgDung { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public int? Quyen { get; set; }
}
