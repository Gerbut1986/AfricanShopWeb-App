namespace AfricanShopLviv.DAL.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<IEntity> where IEntity : class
    {
        Task Delete(int id);
        Task<IEntity> Get(int id);
        Task Create(IEntity entity);
        void Update(IEntity entity);
        Task UpdateAsync(int id);
        IQueryable<IEntity> GetAll();
        Task<System.Collections.Generic.List<IEntity>> GetAllAsync();
    }
}
