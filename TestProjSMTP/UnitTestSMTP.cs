using ConsoleAppMoqEasyStart.After;
using ConsoleAppMoqEasyStart.Services;
using Moq;
using NUnit.Framework;

namespace TestProjSMTP
{
    public class Tests
    {
        private Mock<ISmtpSender> _SMTPMock;
        [SetUp]
        public void Setup()
        {
           _SMTPMock = new Mock<ISmtpSender>(); 
        }

        [Test]
        public void Calling_SendMail_And_Sending_Mailmessage_should_return_True()
        {
            //arrange
            //The mock object is "dumb" - we have to tell it which method we want to call and what it SHOULD return
            _SMTPMock.Setup(x => x.SendMail(It.IsAny<string>())).Returns(true);

            //act
            OrderProcessorAfter Orderprocessor = new OrderProcessorAfter(_SMTPMock.Object);
            bool processed = Orderprocessor.Finalize();


            //Assert
            Assert.IsTrue(processed);
        }

        [TearDown]
        public void TearDown()
        {
            _SMTPMock = null;
        }
    }
}