namespace DownyShoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIOHANG")]
    public partial class GIOHANG
    {
        [Key]
        public int IDGIOHANG { get; set; }

        public int? ID { get; set; }

        public int? QUANTITY { get; set; }

        public virtual GIAY GIAY { get; set; }
    }
}
