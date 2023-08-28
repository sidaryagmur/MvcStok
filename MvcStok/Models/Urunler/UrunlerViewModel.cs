using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStok.Models.Urunler
{
    public class UrunlerViewModel
    {
        public int UrünId { get; set; }
        public string UrünAd { get; set; }
        public string Marka { get; set; }
        public Nullable<short> UrünKategori { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public string KategoriAdi { get; set; }
        public Nullable<byte> Stok { get; set; }

    }
}