//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VocaPOS
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransaksiDetail
    {
        public int TransaksiDetailID { get; set; }
        public Nullable<int> TransaksiID { get; set; }
        public Nullable<int> InventoryID { get; set; }
        public int Jumlah { get; set; }
        public decimal Harga { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual Transaksi Transaksi { get; set; }
    }
}
