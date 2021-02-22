namespace Exam_MusicalShop.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MusicalContext : DbContext
    {
        public MusicalContext()
            : base("name=MusicalContext")
        { }
        public virtual DbSet<Band> Bands { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DiscountClient> DiscountClients { get; set; }
        public virtual DbSet<DiscountDisk> DiscountDisks { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BuyHistory> BuyHistories { get; set; }

    }
}