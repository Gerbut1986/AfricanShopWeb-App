namespace AfricanShopLviv.PL.Models
{
    using AfricanShopLviv.BLL.DTO;
    using System.Collections.Generic;

    public class StaticTables
    {
        public static IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public static IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
        public static IEnumerable<CartDto> Carts { get; set; } = new List<CartDto>();

        public static void ClearAllTbls()
        {
            Categories = new List<CategoryDto>();
            Products = new List<ProductDto>();
            Carts = new List<CartDto>();
        }
    }
}