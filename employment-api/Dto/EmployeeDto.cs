using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace employment_api.Dto
{
	public class EmployeeDto
	{
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z-]+")]
        [DefaultValue("John")]
        public string FirstName { get; set; } = "";

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z-]+")]
        [DefaultValue("Doe")]
        public string LastName { get; set; } = "";

        [Required]
        [RegularExpression(@"^([1-3]?\d{1}\/[0-1]?\d{1}\/\d{4}){1}")]
        [DefaultValue("16/10/2005")]
        public string DOB { get; set; } = "";

        [Required]
        [StringLength(11)]
        [RegularExpression(@"(^(0[7-9][0-1])\d{8}){1}")]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        [RegularExpression(@"[A-Z]+")]
        public string DepartmentCode { get; set; } = "";
	}

    public class EmployeeUpdateDto
    {
        [MinLength(1)]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z-]+")]
        [DefaultValue("John")]
        public string? FirstName { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z-]+")]
        [DefaultValue("Doe")]
        public string? LastName { get; set; }

        [RegularExpression(@"^([1-3]?\d{1}\/[0-1]?\d{1}\/\d{4}){1}")]
        [DefaultValue("16/10/2005")]
        public string? DOB { get; set; }

        [StringLength(11)]
        [RegularExpression(@"(^(0[7-9][0-1])\d{8}){1}")]
        public string? PhoneNumber { get; set; }

        [MinLength(2)]
        [MaxLength(8)]
        [RegularExpression(@"[A-Z]+")]
        public string? DepartmentCode { get; set; }
    }
}

