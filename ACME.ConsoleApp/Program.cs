using ACME.Business.Logic.Services.Implementations;
using ACME.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ACME.ConsoleApp
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //setup our DI
            IIOService iOService = new IOService();
            IEmployeeService employeeService = new EmployeeService(iOService);
            var path = @"c:\Users\victor\source\repos\ACME\ACME.ConsoleApp\Inputs\01-03-2022-Week1.txt";
            var data = await employeeService.GetAllAsync(path);
            foreach (var item in employeeService.GetEmployeesCoincidentOffice(data))
            {
                Console.WriteLine($"{item.Names}:{ item.Count}");
            }
            Console.ReadLine();

            //Console.WriteLine("Hello World!");
        }
    }
}
