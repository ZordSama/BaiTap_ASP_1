using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class GiangVien
{
    public int IdGv { get; set; }

    public string TenGv { get; set; } = null!;

    public string? DienThoai { get; set; }
}
