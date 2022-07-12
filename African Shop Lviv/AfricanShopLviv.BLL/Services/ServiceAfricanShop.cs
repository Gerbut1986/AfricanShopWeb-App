namespace AfricanShopLviv.BLL.Services
{
    using System;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;
    using AfricanShopLviv.BLL.DTO;
    using AfricanShopLviv.DAL.UOF;
    using System.Collections.Generic;
    using AfricanShopLviv.DAL.Entities;
    using AfricanShopLviv.DAL.Interfaces;

    public class ServiceAfricanShop : IServiceAfricanShop
    {
        readonly IUnitOfWork Db;

        #region Constructor:
        public ServiceAfricanShop(string strConn)
        {
            Db = new UnitOfWork(strConn);
        }
        #endregion

        #region Cart Service:
        public void Insert(CartDto dto)
        {
            try
            {
                var model = new ShopingCart
                {
                    ItemsPrice = dto.ItemsPrice,
                    OrderDate = dto.OrderDate,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    UserId = dto.UserId,
                    ProductName = dto.ProductName,
                    ProductPhoto = dto.ProductPhoto
                };
                Db.Carts.Create(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public IEnumerable<CartDto> ReadCarts()
        {
            var list = Db.Carts.GetAll().ToList();
            var listDTO = new List<CartDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CartDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].ItemsPrice = list[i].ItemsPrice;
                listDTO[i].OrderDate = list[i].OrderDate;
                listDTO[i].Price = list[i].Price;
                listDTO[i].Quantity = list[i].Quantity;
                listDTO[i].UserId = list[i].UserId;
                listDTO[i].ProductName = list[i].ProductName;
                listDTO[i].ProductPhoto = list[i].ProductPhoto;
            }
            return listDTO;
        }

        public async Task<List<CartDto>> ReadCartsAsync()
        {
            var l = await Db.Carts.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<CartDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CartDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].ItemsPrice = list[i].ItemsPrice;
                listDTO[i].OrderDate = list[i].OrderDate;
                listDTO[i].Price = list[i].Price;
                listDTO[i].Quantity = list[i].Quantity;
                listDTO[i].UserId = list[i].UserId;
                listDTO[i].ProductName = list[i].ProductName; 
                listDTO[i].ProductPhoto = list[i].ProductPhoto;
            }
            return listDTO;
        }

        public void Update(CartDto dto)
        {
            try
            {
                var model = new ShopingCart
                {
                    ItemsPrice = dto.ItemsPrice,
                    OrderDate = dto.OrderDate,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    UserId = dto.UserId,
                    ProductName = dto.ProductName,
                    ProductPhoto = dto.ProductPhoto
                };
                Db.Carts.Update(model);
            }
            catch { throw new Exception($"Method - '{nameof(Update)}({dto.GetType().Name})'"); }
        }

        public void DeleteCart(int id)
        {
            try
            {
                Db.Carts.Delete(id);
            }
            catch { throw new Exception($"Method - '{nameof(DeleteCart)}({id})'"); }
        }
        #endregion

        #region Product Service:
        public void Insert(ProductDto dto)
        {
            try
            {
                var model = new Product
                {
                    Name = dto.Name,
                    Code = dto.Code,
                    CategoryId = dto.CategoryId,
                    Date = dto.Date,
                    Description = dto.Description,
                    IsStock = dto.IsStock,
                    Photo = dto.Photo,
                    Price = dto.Price
                };
                Db.Products.Create(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public IEnumerable<ProductDto> ReadProducts()
        {
            var list = Db.Products.GetAll().ToList();
            var listDTO = new List<ProductDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new ProductDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].Code = list[i].Code;
                listDTO[i].CategoryId = list[i].CategoryId;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Description = list[i].Description;
                listDTO[i].IsStock = list[i].IsStock;
                listDTO[i].Photo = list[i].Photo;
                listDTO[i].Price = list[i].Price;
            }
            return listDTO;
        }

        public async Task<List<ProductDto>> ReadProductsAsync()
        {
            var l = await Db.Products.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<ProductDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new ProductDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].Code = list[i].Code;
                listDTO[i].CategoryId = list[i].CategoryId;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Description = list[i].Description;
                listDTO[i].IsStock = list[i].IsStock;
                listDTO[i].Photo = list[i].Photo;
                listDTO[i].Price = list[i].Price;
            }
            return listDTO;
        }

        public void Update(ProductDto dto)
        {
            try
            {
                var model = new Product
                {
                    Name = dto.Name,
                    Code = dto.Code,
                    CategoryId = dto.CategoryId,
                    Date = dto.Date,
                    Description = dto.Description,
                    IsStock = dto.IsStock,
                    Photo = dto.Photo,
                    Price = dto.Price
                };
                Db.Products.Update(model);
            }
            catch { throw new Exception($"Method - '{nameof(Update)}({dto.GetType().Name})'"); }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                Db.Products.Delete(id);
            }
            catch { throw new Exception($"Method - '{nameof(DeleteProduct)}({id})'"); }
        }
        #endregion

        #region Category Service:
        public void Insert(CategoryDto dto)
        {
            try
            {
                var model = new Category
                {
                    Name = dto.Name,
                    TagName = dto.TagName,
                    Photo = dto.Photo
                };
                Db.Categories.Create(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public IEnumerable<CategoryDto> ReadCategories()
        {
            var list = Db.Categories.GetAll().ToList();
            var listDTO = new List<CategoryDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CategoryDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].TagName = list[i].TagName;
                listDTO[i].Photo = list[i].Photo;
            }
            return listDTO;
        }

        public async Task<List<CategoryDto>> ReadCategoriesAsync()
        {
            var l = await Db.Categories.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<CategoryDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CategoryDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].TagName = list[i].TagName;
                listDTO[i].Photo = list[i].Photo;
            }
            return listDTO;
        }

        public void Update(CategoryDto dto)
        {
            try
            {
                var model = new Category
                {
                    Name = dto.Name,
                    TagName = dto.TagName,
                    Photo = dto.Photo
                };
                Db.Categories.Update(model);
            }
            catch { throw new Exception($"Method - '{nameof(Update)}({dto.GetType().Name})'"); }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                Db.Categories.Delete(id);
            }
            catch { throw new Exception($"Method - '{nameof(DeleteCategory)}({id})'"); }
        }
        #endregion   
    }
}
