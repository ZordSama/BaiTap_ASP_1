using System;
using System.Collections.Generic;

namespace TruongABC_PhamQuangSang.Models;

public partial class LopMonhoc
{
    public int? IdMon { get; set; }

    public int? IdLop { get; set; }

    public virtual Lop? IdLopNavigation { get; set; }

    public virtual MonHoc? IdMonNavigation { get; set; }
}
