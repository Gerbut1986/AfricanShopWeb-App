namespace AfricanShopLviv.BLL.PartialModels.Email
{
    //this is a model to send data via SMPT protocol
    public class EmailData
    {
        public string Text { get; set; }          
        public string RecipientMail { get; set; } 
        public string Contant { get; set; }       
        public string Subject { get; set; }      
    }
}
