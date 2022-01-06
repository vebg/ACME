using ACME.Persistence.Entities;
using ACME.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ACME.Business.Logic.Services.Implementations
{
    public class TextToClassService : ITextToClassService
    {
        private const string EQUAL_CHARACTER = "=";
        private const string COMMA_CHARACTER = ",";

        private readonly int CurrentYear;
        private readonly int CurrentMonth;

        public TextToClassService()
        {
            CurrentYear = DateTime.Now.Year;
            CurrentMonth = DateTime.Now.Month;
        }

        public List<Employee> TextToEmployees(string text)
        {
            try
            {
                List<Employee> employees = new();
                text = Regex.Replace(text, @"\s+", "");
                string[] splitValues = Regex.Split(text, "([a-zA-Z]{3,}=)");

                for (int i = 0; i < splitValues.Length; i++)
                {
                    if (splitValues[i].Length > 0)
                    {
                        if (splitValues[i].Contains(EQUAL_CHARACTER))
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
                                    InTime = new DateTime(CurrentYear, CurrentMonth, 1, hourIn, minuteIn, 0),
                                    OutTime = new DateTime(CurrentYear, CurrentMonth, 1, hourOut, minuteOut, 0)
                                });
                            }

                            employees.Add(new Employee
                            {
                                Name = splitValues[i].Replace(EQUAL_CHARACTER, ""),
                                OfficesHour = officeTimes
                            });

                            i++;
                        }
                    }
                }

                return employees;
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorMessages.FILE_BAD_FORMAT, ex);
                return Enumerable.Empty<Employee>().ToList();
            }
          
        }
    }
}
