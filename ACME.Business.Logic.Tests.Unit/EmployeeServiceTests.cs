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
        private readonly IIOService _iOService;
        private readonly ITextToClassService _textToClassService;
        private readonly IEmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _iOService = new IOService();
            _textToClassService = new TextToClassService();
            _employeeService = new EmployeeService(_iOService, _textToClassService);
        }

        [Theory]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.Business.Logic.Tests.Unit\\Inputs\\01-03-2022-Week1.txt", 0)]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.Business.Logic.Tests.Unit\\Inputs\\01-04-2022-Week1.txt", 0)]
        public async Task GetAll_ShouldReturnAList(string fullPath,int expectedCount)
        {

            // Act
            var values = await _employeeService.GetAllAsync(fullPath);
            // Assert
            Assert.True(values.Count > expectedCount);

        }

        [Theory]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.ConsoleApp\\Inputs\\01-03-2022-Week1.txt", "RENE-ASTRID:2,ASTRID-ANDRES:3,RENE-ANDRES:2")]
        public async Task GetEmployeesCoincident_BetweenThenIfExists(string fullPath, string expectedValues)
        {
            // Act
            var employess = await _employeeService.GetAllAsync(fullPath);
            var values = _employeeService.GetEmployeesCoincidentOffice(employess);
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
