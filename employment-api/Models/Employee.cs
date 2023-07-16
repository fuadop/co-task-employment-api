using System;
using Microsoft.EntityFrameworkCore;

namespace employment_api.Models
{
    [PrimaryKey(nameof(ID))]
    public class Employee
    {
        public Guid ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public bool Validated { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

