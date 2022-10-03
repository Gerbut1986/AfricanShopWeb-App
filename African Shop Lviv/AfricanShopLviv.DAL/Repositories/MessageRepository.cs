namespace AfricanShopLviv.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class MessageRepository : IRepository<Message>
    {
        readonly DataContext db;

        public MessageRepository(DataContext db) => this.db = db;

        public async Task Create(Message entity)
        {
            db.Messages.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Messages.FindAsync(id);
            db.Messages.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<Message> Get(int id)
        {
            return await db.Messages.FindAsync(id);
        }

        public IQueryable<Message> GetAll()
        {
            return db.Messages;
        }

        public async Task<System.Collections.Generic.List<Message>> GetAllAsync()
        {
            var lst = (from ci in db.Messages select ci);
            return await lst.ToListAsync();
        }

        public void Update(Message entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            var res = db.SaveChanges();
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Messages.FindAsync(id)).State = EntityState.Modified;
            int r=await db.SaveChangesAsync();
        }
    }
}
