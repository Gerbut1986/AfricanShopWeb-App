﻿namespace AfricanShopLviv.DAL.UOF
{
    using Repositories;
    using Interfaces;
    using Entities;
    using EF;

    public class UnitOfWork : IUnitOfWork
    {
        readonly DataContext db; 

        #region All Repos:    
        CartRepository cartRepo;
        OrderRepository orderRepo;
        MessageRepository messageRepo;
        ProductRepository productRepo;
        CategoryRepository categoryRepo;
        #endregion

        public UnitOfWork(string conn) => db = new DataContext(conn);

        public IRepository<Product> Products
        {
            get
            {
                if (productRepo == null)
                    productRepo = new ProductRepository(db);
                return productRepo;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepo == null)
                    categoryRepo = new CategoryRepository(db);
                return categoryRepo;
            }
        }

        public IRepository<ShopingCart> Carts
        {
            get
            {
                if (cartRepo == null)
                    cartRepo = new CartRepository(db);
                return cartRepo;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepository(db);
                return orderRepo;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepo == null)
                    messageRepo = new MessageRepository(db);
                return messageRepo;
            }
        }

        #region Dispose:
        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion

        public async System.Threading.Tasks.Task<int> SaveAsync() => await db.SaveChangesAsync();
    }
}
