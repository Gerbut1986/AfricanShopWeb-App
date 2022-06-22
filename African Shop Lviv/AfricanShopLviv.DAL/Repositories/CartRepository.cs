namespace AfricanShopLviv.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class CartRepository : IRepository<ShopingCart>
    {
        readonly DataContext db;

        public CartRepository(DataContext db) => this.db = db;

        public async Task Create(ShopingCart entity)
        {
            db.ShopingCart.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.ShopingCart.FindAsync(id);
            db.ShopingCart.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<ShopingCart> Get(int id)
        {
            return await db.ShopingCart.FindAsync(id);
        }

        public IQueryable<ShopingCart> GetAll()
        {
            return db.ShopingCart;
        }

        public async Task<System.Collections.Generic.List<ShopingCart>> GetAllAsync()
        {
            var lst = (from ci in db.ShopingCart select ci);
            return await lst.ToListAsync();
        }

        public void Update(ShopingCart entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.ShopingCart.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
