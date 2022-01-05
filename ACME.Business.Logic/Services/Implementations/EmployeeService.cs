using ACME.Persistence.Entities;
using ACME.Persistence.Responses;
using ACME.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ACME.Business.Logic.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IIOService _iIOService;
        private readonly ITextToClassService _textToClassService;

        public EmployeeService(IIOService iOService, ITextToClassService textToClassService)
        {
            _iIOService = iOService;
            _textToClassService = textToClassService;

        }
        public async Task<List<Employee>> GetAllAsync(string path)
        {
            string values = await _iIOService.ReadTextFile(path);
            return _textToClassService.TextToEmployees(values);
        }

        public List<EmployeeTimeCoincidentResponse> GetEmployeesCoincidentOffice(List<Employee> employees)
        {
            List<EmployeeTimeCoincidentResponse> employeeTimeCoincidentResponse = new();
            FindCoincidents(employees, employeeTimeCoincidentResponse);
            return employeeTimeCoincidentResponse;
        }

        private static void FindCoincidents(List<Employee> employees, List<EmployeeTimeCoincidentResponse> employeeTimeCoincidentResponse)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                for (int y = i + 1; y < employees.Count; y++)
                {
                    var coincidents = employees[i].OfficesHour
                        .Where(x => employees[y].OfficesHour
                            .Any(y => y.DayOfWeek == x.DayOfWeek && x.InTime >= y.InTime && x.OutTime <= y.OutTime))
                        .Count();

                    if (coincidents > 0)
                    {
                        employeeTimeCoincidentResponse.Add(new EmployeeTimeCoincidentResponse
                        {
                            Names = $"{employees[i].Name}-{employees[y].Name}",
                            Count = coincidents
                        });
                    }
                }
            }
        }
    }
}
