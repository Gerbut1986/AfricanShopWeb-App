namespace AfricanShopLviv.BLL.Services
{
    using System.Linq;
    using DTO.EmailEntity;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Collections.Generic;
    using AfricanShopLviv.BLL.Interfaces;
    using AfricanShopLviv.BLL.PartialModels.Email;

    public enum MessageType { NewOrder, NewOrderHtmlBody, WriteUs, AcceptToClient }

    public class EmailSender : IEmailSender
    {
        private readonly EmailData data;
        private readonly string htmlBody;

        public EmailSender(EmailData data, string htmlBody = null) 
        {
            this.htmlBody = htmlBody; this.data = data;
        }

        public string SendMessage(MessageType msgType, string attachFile = null, string ownerRecepient = null)
        {
            try
            {
                // [0] - returnMsg, [1] - subject, [2] - body
                var param = htmlBody == null ? GetConcreteMsg(msgType).ToList() : GetConcreteMsg(msgType, htmlBody).ToList();
                var recepient = ownerRecepient != null ? ownerRecepient : "africanshoplviv@gmail.com"; 
                MailAddress from = new MailAddress(data.Email, data.UserName);
                MailAddress to = new MailAddress(recepient); // email to admin
                MailMessage m = new MailMessage(from, to); 
                m.IsBodyHtml = true;
                m.Subject = param[1];
                AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(htmlBody, System.Text.Encoding.UTF8, "text/html");
                m.AlternateViews.Add(htmlView); // And a html attachment to make sure.
                m.Body = htmlBody;
                if (attachFile != null)
                    m.Attachments.Add(Attach(attachFile));
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(Credential.Mail, Credential.Pass);
                smtp.EnableSsl = true;
                smtp.Send(m);
                return param[0];
            }
            catch(System.Exception ex)
            {
                return ex.Message;// + " | " + ex.StackTrace;
            }
        }

        private Attachment Attach(string file)
        {
            var attach = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = attach.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            return attach;
        }

        private IEnumerable<string> GetConcreteMsg(MessageType typeMsg, string htmlBody = "")
        {
            var returnLst = new List<string>();
            string returnMsg = string.Empty, subject = string.Empty, body = string.Empty;
            switch (typeMsg)
            {
                case MessageType.NewOrder:
                    returnMsg = "Thank you for your order. We will contact you soon!";
                    subject = $"Request for New Order {data.SendingDate}";
                    body = $"<h1 style='color:green;font-weight:bold'>Hello MAOTS ent Best Buy Online!</h1><br/>" +
                           $"<h2 style='color:green;font-weight:bold'>I'm '{data.UserName}'. I maked the following order.</h2><br/><br/>" +
                           $"<p><i style='color:black;font-weight:bold'>{data.Comment}.</i></p>" +
                           $"<h2 style='color:violet;font-weight:bold'>It is on  attach.</h2><br/><br/><h3>Best Regards, {data.UserName}.</h3>";
                    break;
                case MessageType.WriteUs:
                    returnMsg = "Your message has been successfully sent to our email address.";
                    subject = $"New Message from AfricanShopLviv.com";
                    body = $"<h1 style='font-weight:bold'>Hello MAOTS ent Best Buy Online! I'm '{data.UserName}'</h1>" +
                           $"<h2 style='color:green;font-weight:bold'>{data.Comment}</h2><br/><br/>" +
                           $"<h3 style='color:green;font-weight:bold'>Phone: {data.Phone}</h3>" +
                           $"<h3 style='color:green;font-weight:bold'>Email: {data.Email}</h3><br/><br/><br/><br/>" +
                           $"<h4 style='color:darkred;font-weight:bold'>Best Regards, {data.UserName}.</h4>";
                    break;
                case MessageType.AcceptToClient:
                    returnMsg = "Thank you for your order! We sent you a message! Check it out!";
                    subject = $"Your Order from AfricanShopLviv.com";
                    body = "<p><blockquote>Info 'bout your place Order!</blockquote><br/>" +
                            "<h2 style='colog:green;font-weight:bold;font-family:Palatino Linotype'>Thank you for your order, " +
                            "we are there to serve you better, please PAYMENT" +
                            "BELOW 1000 GRIVNA WILL HENCEFORTH REQUIRE PRE PAYMENT OF 300 GRIVNA TO OUR ACCOUNT," +
                            "ORDERS ABOVE 1000 GRIVNA WILL REQUIRE PRE PAYMENT OF 300 GRIVNA TO OUR ACCOUNT," +
                            "FULL PRE PAYMENT IS VERY WELCOME 🙏🏽 FRESH VEGETABLES OR FROZEN MEAT ORDERS REQUIRE " +
                            "FULL PAYMENT BEFORE SHIPPING</h2></p><br/><br/>" +
                            "<h4 colog:green;font-weight:bold>PRIVAT BANK 🏦</h4><br/>" +
                            "<h3 colog:violet;font-weight:bold>Tetyana Syrytsia-Okoye: 5363 5420 1653 2278</h3>";
                    break;
                case MessageType.NewOrderHtmlBody:
                    returnMsg = "Thank you for your order. We will contact you soon!";
                    subject = $"Request for New Order {data.SendingDate}";
                    body = htmlBody;
                    break;
            }
            returnLst.AddRange(new string[] { returnMsg, subject, body });
            return returnLst;
        }
    }
}
