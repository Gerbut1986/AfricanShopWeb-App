namespace AfricanShopLviv.BLL.DTO
{
    public class CategoryDto : Interfaces.IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagName { get; set; }
        public string Photo { get; set; }

        public override string ToString()
        {
            return $"{Id},{Name},{TagName},{Photo}";
        }
    }
}
