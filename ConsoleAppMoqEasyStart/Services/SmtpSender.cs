using System;

namespace ConsoleAppMoqEasyStart.Services
{
    public class SmtpSender : ISmtpSender
    {
        public bool SendMail(string message)
        {
            Console.WriteLine("Sending mail with " + message);
            return true;
        }
    }
}
