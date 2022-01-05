
using ACME.Persistence.Entities;
using System.Collections.Generic;

namespace ACME.Services.Interfaces
{
    public interface ITextToClassService
    {
        public List<Employee> TextToEmployees(string text);
    }
}
