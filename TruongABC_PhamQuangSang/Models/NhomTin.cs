using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class NhomTin
{
    public int IdNhomTin { get; set; }

    public string TenNhomTin { get; set; } = null!;

    public virtual ICollection<TinTuc> TinTucs { get; set; } = new List<TinTuc>();
}
