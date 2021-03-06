namespace AfricanShopLviv.DAL.Interfaces
{
    using Entities;

    public interface IUnitOfWork : System.IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<ShopingCart> Carts { get; }
        System.Threading.Tasks.Task<int> SaveAsync();
    }
}
