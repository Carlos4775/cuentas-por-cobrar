using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace WebAppCxC.Email
{
    public class Email
    {
        MailAddress to = new MailAddress("elizabeth@westminster.co.uk");
        MailAddress from = new MailAddress("piotr@mailtrap.io");

        MailMessage message = new MailMessage(from, to);
        message.Subject = "Good morning, Elizabeth";
        message.Body = "Elizabeth, Long time no talk. Would you be up for lunch in Soho on Monday? I'm paying.;";  
    }
}