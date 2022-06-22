namespace AfricanShopLviv.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

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
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Products.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
