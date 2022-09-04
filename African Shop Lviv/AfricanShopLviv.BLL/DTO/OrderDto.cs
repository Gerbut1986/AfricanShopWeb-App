namespace AfricanShopLviv.BLL.DTO
{
    public class OrderDto : Interfaces.IModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalAmount { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Products { get; set; }
    }
}
