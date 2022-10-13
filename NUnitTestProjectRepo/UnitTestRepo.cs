using DAL;
using Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProjectRepo
{
    public class Tests
    {
        private List<Dive> _mockLstDives;
        private Mock<IDiveRepository> _diveMock;
        private IDiveRepository _diveRepo;

        [SetUp]
        public void Setup()
        {
            Dive diveOne = new Dive(1, "Cyprus", 15, 30);
            Dive diveTwo = new Dive(2, "Crete", 17, 100);

            _mockLstDives = new List<Dive>
            {
                diveOne,
                diveTwo
            };

            _diveMock = new Mock<IDiveRepository>();

            //All Dives
            _diveMock.Setup(m => m.GetAll()).Returns(_mockLstDives);

            //GetByID
            _diveMock.Setup(m =>
                m.GetSingle(It.Is<int>(i => i == 1 || i == 2 || i == 3 || i == 4))).Returns<int>(r => new Dive
                   {
                       Id = r,
                       Location = string.Format("Fake Dive {0}", r)
                   });

            //Add Dive
            _diveMock.Setup(m => m.Add(It.IsAny<Dive>())).Callback(new Action<Dive>(
                        x =>
                        {
                            _mockLstDives.Add(x);
                        }
                    ));


            //Delete Dive
            _diveMock.Setup(x => x.Delete(It.IsAny<Dive>())).Callback(new Action<Dive>(x =>
            {
                _mockLstDives.RemoveAll(d => d.Id == x.Id);
            }
            ));


            //Update Dive
            _diveMock.Setup(x => x.Update(It.IsAny<Dive>())).Callback(new Action<Dive>(x =>
                {
                    var found = _mockLstDives.Find(c => c.Id == x.Id);
                    found.Location = x.Location;
                }));

            _diveRepo = _diveMock.Object;
        }

        [Test]
        public void GetAll_Should_Return_All_MockLstDives()
        {
            //Arrange

            //Act
            var testDives = (IList<Dive>)_diveRepo.GetAll();

            //Assert
            Assert.AreEqual(2, testDives.Count);
        }

        [Test]
        public void GetById_Should_Return_Correct_Dive()
        {
            // Arrange

            //Act
            Dive testDive = _diveRepo.GetSingle(1);

            //Assert
            Assert.AreEqual(1, testDive.Id);
        }

        [Test]
        public void Insert_Should_Return_Increased_MockLstDives()
        {
            // Arrange
            Dive testDive = new Dive(3, "TestDive", 25, 300);

            //Act
            _diveRepo.Add(testDive);
           var after = (IList<Dive>)_diveRepo.GetAll();

            //Assert
            Assert.AreEqual(3, after.Count);
        }


        [Test]
        public void Delete_Should_Return_Decreased_MockLstDives()
        {
            // Arrange
            Dive testDive = _mockLstDives.First(i => i.Id == 1);

            //Act
            _diveRepo.Delete(testDive);

            //Assert
            Assert.AreEqual(1, _mockLstDives.Count);
        }

        [Test]
        public void Update_Should_ChangeDive()
        {
            // Arrange
            Dive testDive = new Dive(2, "Update", 12, 12);

            //Act
            _diveRepo.Update(testDive);

            //Assert
            Assert.AreEqual("Update", _mockLstDives[1].Location);
        }


        [TearDown]
        public void TestCleanUp()
        {
            _diveMock = null;
            _mockLstDives = null;
            _diveRepo = null;
        }
    }
}
