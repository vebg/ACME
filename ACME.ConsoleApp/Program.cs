using ACME.Business.Logic.Services.Implementations;
using ACME.Services.Interfaces;
using System;
using System.Threading.Tasks;
using static ACME.ConsoleApp.Constans;

namespace ACME.ConsoleApp
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IIOService iOService = new IOService();
            IEmployeeService employeeService = new EmployeeService(iOService);
            bool start = true;
            while (start)
            {
                WelcomeMessages();
                var fileName = Console.ReadLine();
                Console.Clear();
                start = await ProccessEmployeeFile(iOService, employeeService, start, fileName);
            }
        }

        private static async Task<bool> ProccessEmployeeFile(IIOService iOService, IEmployeeService employeeService, bool start, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine(ErrorMessages.FILE_NAME_BAD_FORMAT_TRY_AGAIN);
                await Loading(10);
            }
            else
            {
                if (!iOService.ValidPath(fileName))
                {
                    Console.WriteLine(ErrorMessages.FILE_NOT_FOUND_TRY_AGAIN);
                    await Loading();
                }
                else
                {
                    var data = await employeeService.GetAllAsync(iOService.GetFilePath(fileName));
                    foreach (var item in employeeService.GetEmployeesCoincidentOffice(data))
                    {
                        Console.WriteLine($"{item.Names}:{ item.Count}");
                    }
                    start = false;
                }
            }

            return start;
        }

        private static async Task Loading(int waiting = 5)
        {
            for (int i = 0; i < waiting; i++)
            {
                string loadingIndicator = "";
                Console.Write(loadingIndicator += ".");
                await Task.Delay(500);
            }
            Console.Clear();
        }

        private static void WelcomeMessages()
        {
            Console.WriteLine("********************");
            Console.WriteLine("**** - ACME - ******");
            Console.WriteLine("******System********");
            Console.WriteLine("********************");
            Console.WriteLine("\r");
            Console.WriteLine("Insert the name of the file:");
        }
    }
}
