namespace DownyShoes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAY")]
    public partial class GIAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIAY()
        {
            GIOHANGs = new HashSet<GIOHANG>();
        }

        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string NAME { get; set; }

        public int? COST { get; set; }

        [Column(TypeName = "ntext")]
        public string IMAGE { get; set; }

        [Column(TypeName = "ntext")]
        public string DETAILS { get; set; }

        public int? IDLOAIGIAY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }

        public virtual LoaiGiay LoaiGiay { get; set; }
    }
}
