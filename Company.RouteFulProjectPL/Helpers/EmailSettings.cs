using System.Net;
using System.Net.Mail;
//zvkdymobozxsxliy

namespace Company.RouteFulProject.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            //SMTP:Simple Mail Transfer Protocol:protocol used to transfer emails

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl= true;
                client.Credentials = new NetworkCredential("www.wissamtarik2021@gmail.com", "zvkdymobozxsxliy");

                client.Send("www.wissamtarik2021@gmail.com", email.To, email.Subject, email.Body);

            }
            catch (Exception)
            {

                return false;
            }
            
            return true;
        }
    }
}
