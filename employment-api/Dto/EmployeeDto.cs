using System;
namespace employment_api.Dto
{
	public class EmployeeDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DOB { get; set; }
		public string PhoneNumber { get; set; }
		public string DepartmentCode { get; set; }
	}

    public class EmployeeUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DepartmentCode { get; set; }
    }
}

