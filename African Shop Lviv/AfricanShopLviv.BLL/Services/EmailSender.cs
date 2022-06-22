namespace AfricanShopLviv.BLL.Services
{
    using System.Net.Mail;
    using System.Net.Mime;
    using AfricanShopLviv.BLL.Interfaces;
    using AfricanShopLviv.BLL.PartialModels.Email;

    public class EmailSender : IEmailSender
    {
        public string SendMessage(string email, string fname, string lname, string ownerRecepient, string attachFile = null)
        {
            try
            {
                MailAddress from = new MailAddress(email, "Request for Passwords");
                MailAddress to = new MailAddress(ownerRecepient); // email to admin
                MailMessage m = new MailMessage(from, to);
                m.Subject = $"I Forgot My Email From '{email}'";
                m.Body = $"Hi Administrator! I'm '{fname} {lname}' and forgot my password." +
                "\nPlease provide me with my password.\n\nRegards.";
                if (attachFile != null)
                    m.Attachments.Add(Attach(attachFile));
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(Credential.Mail, Credential.Pass);
                smtp.EnableSsl = true;
                smtp.Send(m);
                return "Your e-mail was sent successfully to Admin.";
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
    }
}
