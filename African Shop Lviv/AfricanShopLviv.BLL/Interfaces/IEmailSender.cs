namespace AfricanShopLviv.BLL.Interfaces
{
    public interface IEmailSender
    {
        string SendMessage(string email, string fname, string lname, string ownerRecepient, string attachment);
    }
}
