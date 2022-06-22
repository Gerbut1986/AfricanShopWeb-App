namespace AfricanShopLviv.DAL.EF
{
    using System.Data.Entity;
    using AfricanShopLviv.DAL.Entities;

    public class DataContext : DbContext
    {
        public DataContext(string connection) : base(connection)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ShopingCart> ShopingCart { get; set; }
    }
}
