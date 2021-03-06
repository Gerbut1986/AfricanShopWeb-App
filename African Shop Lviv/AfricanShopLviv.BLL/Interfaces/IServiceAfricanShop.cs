namespace AfricanShopLviv.BLL.Interfaces
{
    using DTO;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IServiceAfricanShop
    {
        #region Category Service:
        void Insert(CategoryDto model);
        IEnumerable<CategoryDto> ReadCategories();
        Task<List<CategoryDto>> ReadCategoriesAsync();
        void Update(CategoryDto model);
        void DeleteCategory(int id);
        #endregion

        #region Product Service:
        void Insert(ProductDto model);
        IEnumerable<ProductDto> ReadProducts();
        Task<List<ProductDto>> ReadProductsAsync();
        void Update(ProductDto model);
        void DeleteProduct(int id);
        #endregion

        #region Cart Service:
        void Insert(CartDto model);
        IEnumerable<CartDto> ReadCarts();
        Task<List<CartDto>> ReadCartsAsync();
        void Update(CartDto model);
        void DeleteCart(int id);
        #endregion
      
    }
}
