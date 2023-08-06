using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace employment_api.Dto
{
	public class DepartmentDto
	{
        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        [RegularExpression(@"[A-Z]+")]
        public string Code { get; set; } = "";

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z-\s]+")]
        [DefaultValue("Technology Department")]
        public string Name { get; set; } = "";
	}

    public class DepartmentUpdateDto
    {
        [MinLength(2)]
        [MaxLength(8)]
        [RegularExpression(@"[A-Z]+")]
        public string? Code { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z-\s]+")]
        [DefaultValue("Technology Department")]
        public string? Name { get; set; }
    }
}

