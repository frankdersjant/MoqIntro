using ConsoleAppMoqEasyStart.Services;

namespace ConsoleAppMoqEasyStart.After
{
    public class OrderProcessorAfter
    {
        private readonly ISmtpSender _smtpSender;

        //Class is now decoupled from the SmtpSender through ctor injection
        //Loosely coupled architecture!
        //Hollywood principle
        public OrderProcessorAfter(ISmtpSender smtpSender)
        {
            _smtpSender = smtpSender;
        }

        public bool Finalize()
        {
            _smtpSender.SendMail("message");

            return true;
        }
    }
}
