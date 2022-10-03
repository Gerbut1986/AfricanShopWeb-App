﻿namespace AfricanShopLviv.BLL.Services
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
    using System.Data.SqlClient;

    public class ServiceAfricanShop : IServiceAfricanShop
    {
        readonly IUnitOfWork Db;
        private string connectStrCpy;

        #region Constructor:
        public ServiceAfricanShop(string strConn)
        {
            connectStrCpy = strConn;
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
        public async Task Insert(ProductDto dto)
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
                await Db.Products.Create(model);
            }
            catch(Exception ex) { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
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

        public async Task UpdateAsync(int id)
        {
            try
            {
                await Db.Products.UpdateAsync(id);
            }
            catch (Exception ex) { var error = ex.Message; }
        }

        public void Update(ProductDto dto)
        {
            try
            {
                var model = new Product
                {
                    Id = dto.Id,
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
            catch(Exception ex) { throw new Exception($"Method - '{nameof(Update)}({dto.GetType().Name})'"); }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                Db.Products.Delete(id);
            }
            catch(Exception ex) { throw new Exception($"Method - '{nameof(DeleteProduct)}({id})'"); }
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
                    Id = dto.Id,
                    Name = dto.Name,
                    TagName = dto.TagName,
                    Photo = dto.Photo
                };
                Db.Categories.Update(model);
            }
            catch(Exception ex) { throw new Exception($"Method - '{nameof(Update)}({dto.GetType().Name})'"); }
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

        #region Order Service:
        public void Insert(OrderDto dto)
        {
            try
            {
                var model = new Order
                {
                    OrderDate = dto.OrderDate,
                    TotalAmount = dto.TotalAmount,
                    Products = dto.Products,
                    UserId = dto.UserId
                };
                Db.Orders.Create(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public IEnumerable<OrderDto> ReadOrders()
        {
            var list = Db.Orders.GetAll().ToList();
            var listDTO = new List<OrderDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new OrderDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].TotalAmount = list[i].TotalAmount;
                listDTO[i].OrderDate = list[i].OrderDate;
                listDTO[i].UserId = list[i].UserId;
                listDTO[i].Products = list[i].Products;
            }
            return listDTO;
        }

        public async Task<List<OrderDto>> ReadOrdersAsync()
        {
            var list = await Db.Orders.GetAllAsync();
            var listDTO = new List<OrderDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new OrderDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].TotalAmount = list[i].TotalAmount;
                listDTO[i].OrderDate = list[i].OrderDate;
                listDTO[i].UserId = list[i].UserId;
                listDTO[i].Products = list[i].Products;
            }
            return listDTO;
        }

        public void Update(OrderDto dto)
        {
            try
            {
                var model = new Order
                {
                    OrderDate = dto.OrderDate,
                    TotalAmount = dto.TotalAmount,
                    Products = dto.Products,
                    UserId = dto.UserId
                };
                Db.Orders.Update(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public void DeleteOrder(int id)
        {
            try
            {
                Db.Orders.Delete(id);
            }
            catch { throw new Exception($"Method - '{nameof(DeleteOrder)}({id})'"); }
        }
        #endregion

        #region Message Service:
        public void Insert(MessageDto dto)
        {
            try
            {
                var model = new Message
                {
                    DateMessage = dto.DateMessage,
                    IsReviwed = dto.IsReviwed,
                    RecipientId = dto.RecipientId,
                    SenderId = dto.SenderId,
                    TextMessage = dto.TextMessage,
                    Title = dto.Title,
                    TypeMessage = dto.TypeMessage
                };
                Db.Messages.Create(model);
            }
            catch { throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'"); }
        }

        public IEnumerable<MessageDto> ReadMessages()
        {
            var list = Db.Messages.GetAll().ToList();
            var listDTO = new List<MessageDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new MessageDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].DateMessage = list[i].DateMessage;
                listDTO[i].IsReviwed = list[i].IsReviwed;
                listDTO[i].RecipientId = list[i].RecipientId;
                listDTO[i].SenderId = list[i].SenderId;
                listDTO[i].TextMessage = list[i].TextMessage;
                listDTO[i].Title = list[i].Title;
                listDTO[i].TypeMessage = list[i].TypeMessage;
            }
            return listDTO;
        }

        public async Task<List<MessageDto>> ReadMessagesAsync()
        {
            var list = await Db.Messages.GetAllAsync();
            var listDTO = new List<MessageDto>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new MessageDto());
                listDTO[i].Id = list[i].Id;
                listDTO[i].DateMessage = list[i].DateMessage;
                listDTO[i].IsReviwed = list[i].IsReviwed;
                listDTO[i].RecipientId = list[i].RecipientId;
                listDTO[i].SenderId = list[i].SenderId;
                listDTO[i].TextMessage = list[i].TextMessage;
                listDTO[i].Title = list[i].Title;
                listDTO[i].TypeMessage = list[i].TypeMessage;
            }
            return listDTO;
        }

        public void Update(MessageDto dto)
        {
            //
            try
            {
                var model = new Message
                {
                    Id = dto.Id,
                    DateMessage = dto.DateMessage,
                    IsReviwed = dto.IsReviwed,
                    RecipientId = dto.RecipientId,
                    SenderId = dto.SenderId,
                    TextMessage = dto.TextMessage,
                    Title = dto.Title,
                    TypeMessage = dto.TypeMessage
                };
                Db.Messages.UpdateAsync(model.Id);
            }
            catch(Exception ex)
            {
                // {
                // "Attaching an entity of type 'AfricanShopLviv.DAL.Entities.Message'
                // failed because another entity of the same type already has the same primary key value.
                // This can happen when using the 'Attach' method or setting the state of an entity
                // to 'Unchanged' or 'Modified' if any entities in the graph have conflicting key values.
                // This may be because some entities are new and have not yet received database-generated key values.
                // In this case use the 'Add' method or the 'Added' entity state to track the graph and then
                // set the state of non-new entities to 'Unchanged' or 'Modified' as appropriate."
                // }
                throw new Exception($"Method - '{nameof(Insert)}({dto.GetType().Name})'");
            }
        }

        public string UpdateADO(MessageDto msg)
        {
            string qwery = $"update messages set IsReviwed=@IsReviwed where Id={msg.Id}";
            using (var conn = new SqlConnection(connectStrCpy)) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qwery, conn))
                {
                    cmd.Parameters.AddWithValue("@IsReviwed", msg.IsReviwed);
                    try
                    {
                        var res = cmd.ExecuteNonQuery();
                        return res == 1 ? "Update Success!" : "Something went wrong...";
                    }
                    catch(Exception ex) { return ex.Message; }
                }
            }
        }

        public void DeleteMessage(int id)
        {
            try
            {
                Db.Messages.Delete(id);
            }
            catch { throw new Exception($"Method - '{nameof(DeleteOrder)}({id})'"); }
        }
        #endregion
    }
}
