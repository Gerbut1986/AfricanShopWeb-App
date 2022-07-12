namespace AfricanShopLviv.BLL.DTO
{
    public class CartDto : Interfaces.IModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public double ItemsPrice { get; set; }
    }
}
