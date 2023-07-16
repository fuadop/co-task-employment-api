using System;
namespace employment_api.Dto
{
	public class DepartmentDto
	{
        public string Code { get; set; }
        public string Name { get; set; }
	}

    public class DepartmentUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}

