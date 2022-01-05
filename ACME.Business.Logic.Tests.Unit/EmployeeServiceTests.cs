using ACME.Business.Logic.Services.Implementations;
using ACME.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ACME.Business.Logic.Tests.Unit
{
    public class EmployeeServiceTests
    {
        //Arrange
        private readonly IIOService iOService = new IOService();
        private readonly ITextToClassService textToClassService = new TextToClassService();
        private readonly IEmployeeService employeeService;

        public EmployeeServiceTests()
        {
            employeeService = new EmployeeService(iOService, textToClassService);
        }

        [Theory]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.ConsoleApp\\Inputs\\01-03-2022-Week1.txt",0)]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.ConsoleApp\\Inputs\\01-04-2022-Week1.txt", 0)]
        public async Task GetAllShouldReturnAList(string fullPath,int expectedCount)
        {

            // Act
            var values = await employeeService.GetAllAsync(fullPath);
            // Assert
            Assert.True(values.Count > expectedCount);

        }

        [Theory]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.ConsoleApp\\Inputs\\01-03-2022-Week1.txt", "RENE-ASTRID:2,ASTRID-ANDRES:3,RENE-ANDRES:2")]
        public async Task GetEmployeesCoincidentBetweenThen(string fullPath, string expectedValues)
        {
            // Act
            var employess = await employeeService.GetAllAsync(fullPath);
            var values = employeeService.GetEmployeesCoincidentOffice(employess);
            var jaggetArray = expectedValues.Split(",").Select(x => x.Split(":")).ToArray();

            // Assert

            for (int i = 0; i < jaggetArray.Count(); i++)
            {
                Assert.Contains(jaggetArray[i][0], values.Select(x => x.Names.ToUpper()));
                var employee = values.Where(x => x.Names.ToUpper() == jaggetArray[i][0]).SingleOrDefault();
                Assert.Equal(int.Parse(jaggetArray[i][1]), employee.Count);
            }
        


        }
    }
}
