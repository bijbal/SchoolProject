using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolProcessor.Tests
{
    [TestClass]
    public class ProcessorTest
    {
        SchoolDataService.ISchoolDataService dataService;
        Castle.Windsor.WindsorContainer container;
        [TestInitialize]
        public void Init()
        {
            container = new Castle.Windsor.WindsorContainer();
            container.Register(Castle.MicroKernel.Registration.Classes.FromThisAssembly());
        }

        [TestMethod]
        [ExpectedException( typeof(System.Exception))]
        public async void Test_Caculcate_StudentDetention()
        {
                SchoolProcessor.DetentionCalculator calculator = new SchoolProcessor.ConcurrentCalculator();
                var result = await calculator.CalculateResult(2, DateTime.Today);
                Assert.Equals(result.EffectiveResult, 0);
            
        }
    }
}
