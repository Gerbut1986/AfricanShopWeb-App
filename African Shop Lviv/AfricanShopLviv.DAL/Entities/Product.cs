namespace AfricanShopLviv.DAL.Entities
{
    using System;

    public class Product
    {
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
