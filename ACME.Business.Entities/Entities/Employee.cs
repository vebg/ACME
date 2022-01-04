using System;
using System.Collections.Generic;

namespace ACME.Persistence.Entities
{
    public class Employee
    {
        public string Name { get; set; }
        public List<OfficesHours> OfficesHour { get; set; }
    }

    public class OfficesHours
    {
        public string DayOfWeek { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }

    }
}
