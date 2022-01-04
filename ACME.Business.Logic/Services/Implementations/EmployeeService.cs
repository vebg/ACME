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
        private const string EQUAL_CHARACTER = "=";
        private const string COMMA_CHARACTER = ",";

        private readonly int CurrentYear;
        private readonly int CurrentMonth;

        private readonly IIOService _iIOService;
        public EmployeeService(IIOService iOService)
        {
            _iIOService = iOService;
            CurrentYear = DateTime.Now.Year;
            CurrentMonth = DateTime.Now.Month;

        }
        public async Task<List<Employee>> GetAllAsync(string path)
        {
            List<Employee> employees = new();
            string values = await _iIOService.ReadFile(path);

            values = Regex.Replace(values, @"\s+","");
            string[] splitValues = Regex.Split(values, "([a-zA-Z]{3,}=)");

            for (int i = 0; i < splitValues.Length; i++)
            {
                if(splitValues[i].Length > 0)
                {
                    if(splitValues[i].Contains(EQUAL_CHARACTER))
                    {
                        List<OfficesHours> officeTimes = new();
                        foreach (var item in splitValues[i + 1].Split(COMMA_CHARACTER))
                        {
                            string dayOfWeek = item.Substring(0, 2);

                            int hourIn = int.Parse(item.Substring(2, 2));
                            int minuteIn = int.Parse(item.Substring(5, 2));


                            int hourOut = int.Parse(item.Substring(8, 2));
                            int minuteOut = int.Parse(item.Substring(11, 2));

                            officeTimes.Add(new OfficesHours
                            {
                                 DayOfWeek = dayOfWeek,
                                 InTime = new DateTime(CurrentYear, CurrentMonth,1, hourIn, minuteIn,0),
                                 OutTime = new DateTime(CurrentYear, CurrentMonth,1, hourOut, minuteOut,0)
                            });
                        }


                        employees.Add(new Employee
                        {
                            Name = splitValues[i].Replace(EQUAL_CHARACTER,""),
                            OfficesHour = officeTimes
                        });

                        i++;
                    }
                }
            }

            return employees;


        }

        public List<EmployeeTimeCoincidentResponse> GetEmployeesCoincidentOffice(List<Employee> employees)
        {
            List<EmployeeTimeCoincidentResponse> employeeTimeCoincidentResponse = new();

            for (int i = 0; i < employees.Count; i++)
            {
                for (int y = i + 1; y < employees.Count; y++)
                {
                    var coincidents = employees[i].OfficesHour
                        .Where(x => employees[y].OfficesHour
                            .Any(y => y.DayOfWeek == x.DayOfWeek && y.InTime >= x.InTime && y.OutTime <= x.OutTime))
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

            return employeeTimeCoincidentResponse;
        }
    }
}
