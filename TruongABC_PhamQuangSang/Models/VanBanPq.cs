using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class VanBanPq
{
    public string MaVb { get; set; } = null!;

    public string TenVb { get; set; } = null!;

    public string? TomTat { get; set; }

    public string? Link { get; set; }
}
