using ACME.Persistence.Entities;
using ACME.Persistence.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACME.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeTimeCoincidentResponse> GetEmployeesCoincidentOffice(List<Employee> employees);
        public Task<List<Employee>> GetAllAsync(string path);
    }
}
