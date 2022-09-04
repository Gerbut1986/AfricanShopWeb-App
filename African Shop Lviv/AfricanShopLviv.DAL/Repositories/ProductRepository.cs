namespace AfricanShopLviv.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public class ProductRepository : IRepository<Product>
    {
        readonly DataContext db;

        public ProductRepository(DataContext db) => this.db = db;

        public async Task Create(Product entity)
        {
            db.Products.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Products.FindAsync(id);
            db.Products.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public IQueryable<Product> GetAll()
        {
            return db.Products;
        }

        public async Task<System.Collections.Generic.List<Product>> GetAllAsync()
        {
            var lst = (from ci in db.Products select ci);
            return await lst.ToListAsync();
        }

        public void Update(Product entity)
        {
            var res = db.Entry(entity).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch// (OptimisticConcurrencyException)
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Андрей\Desktop\C# Programming(Andrii)\Tanya Mark Website\AfricanShopWeb-App\African Shop Lviv\AfricanShopLviv.PL\App_Data\aspnet-AfricanShopLviv.PL-20220528031854.mdf';Integrated Security=True"))
                {
                    con.Open();
                    string query = "Update Products SET Name=@Name, Description=@Description , Price=@Price, " +
                        "Code=@Code, Date=@Date, Photo=@Photo, CategoryId=@CategoryId, IsStock=@IsStock where Id=" + entity.Id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Description", entity.Description);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.Parameters.AddWithValue("@Code", entity.Code);
                    cmd.Parameters.AddWithValue("@Date", entity.Date);
                    cmd.Parameters.AddWithValue("@Photo", entity.Photo);
                    cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                    cmd.Parameters.AddWithValue("@IsStock", entity.IsStock);
                    var result = cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Products.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
