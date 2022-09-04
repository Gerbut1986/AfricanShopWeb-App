namespace AfricanShopLviv.BLL.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProductDto : Interfaces.IModel
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public bool IsStock { get; set; }
    }
}
