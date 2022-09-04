namespace AfricanShopLviv.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalAmount { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Products { get; set; }
    }
}
