using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DownyShoes.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<GIAY> GIAYs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<LoaiGiay> LoaiGiays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiGiay>()
                .HasMany(e => e.GIAYs)
                .WithOptional(e => e.LoaiGiay)
                .HasForeignKey(e => e.IDLOAIGIAY);
        }
    }
}
