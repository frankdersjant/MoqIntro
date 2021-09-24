namespace ConsoleAppMoqEasyStart.Services
{
    public interface ISmtpSender
    {
        bool SendMail(string message);
    }
}
