using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class LichCt
{
    public int Id { get; set; }

    public string? NoiDung { get; set; }

    public string? DiaDiem { get; set; }

    public DateTime? ThoiGian { get; set; }
}
