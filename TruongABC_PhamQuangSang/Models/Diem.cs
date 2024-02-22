using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class Diem
{
    public int? IdLop { get; set; }

    public int? IdMon { get; set; }

    public string AnhDiem { get; set; } = null!;

    public virtual MonHoc? IdLop1 { get; set; }

    public virtual Lop? IdLopNavigation { get; set; }
}
