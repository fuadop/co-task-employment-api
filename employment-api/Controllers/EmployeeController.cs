using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using employment_api.Dto;
using employment_api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace employment_api.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private DatabaseContext _db;
        public EmployeeController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("")]
        public ResponseBase<Employee> CreateEmployee([FromBody]EmployeeDto input)
        {
            try
            {
                var firstName = input.FirstName.Trim();
                var lastName = input.LastName.Trim();
                var phoneNumber = input.PhoneNumber.Trim();
                var dob = input.DOB.Trim();
                var departmentCode = input.DepartmentCode.Trim().ToUpper();

                // validate dob
                DateTime? _dob = null;
                try
                {
                    _dob = DateTime.ParseExact(dob, "dd/MM/yyyy", null);
                }
                catch { };
                if (_dob == null)
                {
                    Response.StatusCode = 422;
                    return new ResponseBase<Employee>(422, "Validation Error: Invalid dob provided", null);
                }
                if (_dob > DateTime.Now)
                {
                    Response.StatusCode = 422;
                    return new ResponseBase<Employee>(422, "Validation Error: 'dob' cannot be in the future", null);
                }

                // validate department code
                var department = _db.Departments
                        .Where(x => x.Code == departmentCode)
                        .FirstOrDefault();
                if (department == null)
                {
                    Response.StatusCode = 422;
                    return new ResponseBase<Employee>(422, $"Validation Error: Department with code '{departmentCode}' was not found.", null);
                }

                var employee = new Employee
                {
                    Validated = false,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    DOB = (DateTime)_dob,
                    Department = department,
                };


                var entity = _db.Employees.Add(employee);
                _db.SaveChanges();


                return new ResponseBase<Employee>(200, "Employee created", entity.Entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Employee>(500, e.Message, null);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public ResponseBase<Employee> UpdateEmployee(string id, [FromBody] EmployeeUpdateDto input)
        {
            try
            {
                var employee = _db.Employees.Find(new Guid(id.Trim()));
                if (employee == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Employee>(404, "Employee not found", null);
                }
                var firstName = input.FirstName?.Trim();
                var lastName = input.LastName?.Trim();
                var phoneNumber = input.PhoneNumber?.Trim();
                var dob = input.DOB?.Trim();
                var departmentCode = input.DepartmentCode?.Trim().ToUpper();

                if (firstName != null && firstName != "")
                {
                    employee.FirstName = firstName;
                }
                if (lastName != null && lastName != "")
                {
                    employee.LastName = lastName;
                }

                if (departmentCode != null && departmentCode != "")
                {
                    // validate department code
                    var department = _db.Departments
                            .Where(x => x.Code == departmentCode)
                            .FirstOrDefault();
                    if (department == null)
                    {
                        Response.StatusCode = 422;
                        return new ResponseBase<Employee>(422, $"Validation Error: Department with code '{departmentCode}' was not found.", null);
                    }

                    employee.Department = department;
                }

                if (dob != null && dob != "")
                {
                    // validate dob
                    DateTime? _dob = null;
                    try
                    {
                        _dob = DateTime.ParseExact(dob, "dd/MM/yyyy", null);
                    }
                    catch { };
                    if (_dob == null)
                    {
                        Response.StatusCode = 422;
                        return new ResponseBase<Employee>(422, "Validation Error: Invalid dob provided", null);
                    }
                    if (_dob > DateTime.Now)
                    {
                        Response.StatusCode = 422;
                        return new ResponseBase<Employee>(422, "Validation Error: 'dob' cannot be in the future", null);
                    }

                    employee.DOB = (DateTime)_dob;
                }

                if (phoneNumber != null && phoneNumber != "")
                {
                    employee.PhoneNumber = phoneNumber;
                }

                _db.Employees.Update(employee);
                _db.SaveChanges();

                return new ResponseBase<Employee>(200, "Employee updated", employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Employee>(500, e.Message, null);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseBase<Employee> DeleteEmployee(string id)
        {
            try
            {
                var employee = _db.Employees.Find(new Guid(id.Trim()));
                if (employee == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Employee>(404, "Employee not found", null);
                }

                _db.Employees.Remove(employee);
                _db.SaveChanges();

                return new ResponseBase<Employee>(200, "Employee deleted", employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Employee>(500, e.Message, null);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseBase<Employee> GetEmployee(string id)
        {
            try
            {
                var employee = _db.Employees.Find(new Guid(id.Trim()));
                if (employee == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Employee>(404, "Employee not found", null);
                }

                // populate the many-to-one relationship
                _db.Entry(employee)
                    .Reference(x => x.Department)
                    .Load();

                return new ResponseBase<Employee>(200, "Employee found", employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Employee>(500, e.Message, null);
            }
        }

        [HttpGet]
        [Route("")]
        public ResponseBase<Employee[]> GetEmployees()
        {
            try
            {
                var employees = _db.Employees
                    //.Include()
                    .ToArray();

                return new ResponseBase<Employee[]>(200, "Employees fetched", employees);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Employee[]>(500, e.Message, null);
            }
        }
    }
}

