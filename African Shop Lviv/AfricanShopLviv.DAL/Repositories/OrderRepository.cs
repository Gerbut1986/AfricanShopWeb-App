namespace AfricanShopLviv.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class OrderRepository : IRepository<Order>
    {
        readonly DataContext db;

        public OrderRepository(DataContext db) => this.db = db;

        public async Task Create(Order entity)
        {
            db.Orders.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Orders.FindAsync(id);
            db.Orders.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<Order> Get(int id)
        {
            return await db.Orders.FindAsync(id);
        }

        public IQueryable<Order> GetAll()
        {
            return db.Orders;
        }

        public async Task<System.Collections.Generic.List<Order>> GetAllAsync()
        {
            var lst = (from ci in db.Orders select ci);
            return await lst.ToListAsync();
        }

        public void Update(Order entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Orders.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}