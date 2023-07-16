using System;
using Microsoft.EntityFrameworkCore;

namespace employment_api.Models
{
    [PrimaryKey(nameof(ID))]
    public class Department
	{
		public Guid ID { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }

		public ICollection<Employee> Employees { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

