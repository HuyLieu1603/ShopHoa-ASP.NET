//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopHoa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public System.Guid MaHD { get; set; }
        public Nullable<System.DateTime> NgayLapHD { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<System.Guid> MaNV { get; set; }
        public Nullable<System.Guid> MaKH { get; set; }
        public Nullable<System.Guid> MaTK { get; set; }
        public Nullable<System.Guid> MaSP { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Product Product { get; set; }
        public virtual Statistical Statistical { get; set; }
    }
}
