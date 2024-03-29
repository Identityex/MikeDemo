
using Microsoft.EntityFrameworkCore;
using MikeDemoDBEntities.Interfaces;
using MikeDemoDBEntities.Models;
using MikeDemoProject.DAL;
using MikeDemoProject.Models;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeDemoTests
{
    public class AutomobileTests
    {
        private Mock<MikeDBContext> _mikeDBContext;
        private Mock<DbSet<Automobiles>> _mockAutmobile;
        private Mock<DbSet<AutomobileTypes>> _mockTypes;
        private IAutomobileRepository<Automobiles> _automobileRepo;
        private IQueryable<AutomobileTypes> _automobileTypes;
        private IQueryable<Automobiles> _automobiles;
        private IWebDriver _webDriver;


        [SetUp]
        public void Setup()
        {
            _automobileTypes = new List<AutomobileTypes>
            {
                new AutomobileTypes { type = "Truck", wheels = 4 },
                new AutomobileTypes { type = "Car", wheels = 4 },
                new AutomobileTypes { type = "Motorbike", wheels = 2 }
            }.AsQueryable();

            _automobiles = new List<Automobiles>
            {
                new Automobiles { AutomobileId = 1 }
            }.AsQueryable();

            _mikeDBContext = new Mock<MikeDBContext>();
            _mockAutmobile = new Mock<DbSet<Automobiles>>();
            _mockTypes = new Mock<DbSet<AutomobileTypes>>();

            _mockTypes.As<IQueryable<AutomobileTypes>>().Setup(c => c.Provider).Returns(_automobileTypes.Provider);
            _mockTypes.As<IQueryable<AutomobileTypes>>().Setup(c => c.Expression).Returns(_automobileTypes.Expression);
            _mockTypes.As<IQueryable<AutomobileTypes>>().Setup(c => c.ElementType).Returns(_automobileTypes.ElementType);
            _mockTypes.As<IQueryable<AutomobileTypes>>().Setup(c => c.GetEnumerator()).Returns(_automobileTypes.GetEnumerator());



            _mockAutmobile.As<IQueryable<Automobiles>>().Setup(c => c.Provider).Returns(_automobiles.Provider);
            _mockAutmobile.As<IQueryable<Automobiles>>().Setup(c => c.Expression).Returns(_automobiles.Expression);
            _mockAutmobile.As<IQueryable<Automobiles>>().Setup(c => c.ElementType).Returns(_automobiles.ElementType);
            _mockAutmobile.As<IQueryable<Automobiles>>().Setup(c => c.GetEnumerator()).Returns(_automobiles.GetEnumerator());

            _mikeDBContext.Setup(c => c.AutomobileTypes)
                .Returns(_mockTypes.Object);
            _mikeDBContext.Setup(c => c.Automobiles)
                .Returns(_mockAutmobile.Object);

            _automobileRepo = new AutomobileRepository(_mikeDBContext.Object);

            _webDriver = new ChromeDriver("D:\\ChromeDriver2\\");
        }


        /// <summary>
        /// Verify both the addition and removal of a Automobile
        /// </summary>
        [Test]
        [Category("Database Test")]
        public void CanAddRemoveAutomobile()
        {
            _mikeDBContext.Setup(c => c.SaveChanges()).Verifiable();
            _automobileRepo.AddAutomobile(new Automobiles
            {
                Colour = "Red",
                DateAdded = DateTime.Now,
                Price = 2.00
            }, "Car");
            _mikeDBContext.Verify();

            _mikeDBContext.Setup(c => c.SaveChanges()).Verifiable();
            _automobileRepo.RemoveAutmobile(1);
            _mikeDBContext.Verify();

        }

        //Verify Automobile page works
        [Test]
        [Category("Web Test")]
        public void CanHitAutomobilePage()
        {
            _webDriver.Url = "https://localhost:44333/Automobile";
        }

        [TearDown]
        public void CloseBrowser()
        {
            _webDriver.Close();
        }
    }
}