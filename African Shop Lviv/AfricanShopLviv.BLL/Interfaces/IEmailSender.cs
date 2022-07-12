namespace AfricanShopLviv.BLL.Interfaces
{
    public interface IEmailSender
    {
        string SendMessage(Services.MessageType msgType, string ownerRecepient, string attachment);
    }
}
