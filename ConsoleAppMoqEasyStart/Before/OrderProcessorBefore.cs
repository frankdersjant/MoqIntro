using ConsoleAppMoqEasyStart.Services;

namespace ConsoleAppMoqEasyStart.Before
{
    public class OrderProcessorBefore
    {
        //During testing I only want to test the method Finalize
        //However the mehod is dependent on the class SmtpSender()
        //Effctively I cannot test the method Finalize without sending mails.. 
        //My class is highly coupled with a dependency (SmtpSender)
        public bool Finalize()
        {
            var smtpSend = new SmtpSender();
            smtpSend.SendMail("message");

            //everything went well - order proecessed
            return true;
        }
    }
}
