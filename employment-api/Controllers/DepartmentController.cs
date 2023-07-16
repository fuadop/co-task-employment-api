using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using employment_api.Models;
using employment_api.Dto;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace employment_api.Controllers
{
    [ApiController]
    [Route("departments")]
    public class DepartmentController : Controller
    {
        private DatabaseContext _db;

        public DepartmentController(DatabaseContext db)
        {
            // dependency injection
            _db = db;
        }

        [HttpPost]
        [Route("")]
        public ResponseBase<Department> CreateDepartment([FromBody] DepartmentDto input)
        {
            try
            {
                var code = input.Code.ToUpper().Trim();
                var name = input.Name.Trim();

                if (code == "" || name == "")
                {
                    Response.StatusCode = 422;
                    return new ResponseBase<Department>(422, "Validation Error: One or more required fields are empty (code, name)", null);
                }

                var existing = _db.Departments
                      .Where(x => x.Code == code)
                      .FirstOrDefault();
               
                if (existing != null)
                {
                    Response.StatusCode = 422;
                    return new ResponseBase<Department>(422, $"Validation Error: Department '{code}' already exists.", null);
                }

                var department = new Department
                {
                    Code = code,
                    Name = name, // remove all trailing whitespaces.
                };

                var entity = _db.Departments.Add(department);
                _db.SaveChanges();

                Response.StatusCode = 201;
                return new ResponseBase<Department>(201, "Deparment Created", entity.Entity);
            } catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Department>(500, e.Message, null);
            }
        }

        [HttpGet]
        [Route("")]
        public ResponseBase<Department[]> GetDepartments()
        {
            try
            {
                var departments = _db.Departments
                    .ToArray();

                Response.StatusCode = 200;
                return new ResponseBase<Department[]>(200, "Deparments fetched", departments);
            } catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Department[]>(500, e.Message, null);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseBase<Department> GetDepartment(string id)
        {
            try
            {
                string _id = id.Trim();
                Department? department = null;

                if (_id.Length == 36)
                {
                    // find by deparment id
                    department = _db.Departments.Find(new Guid(_id));
                } else
                {
                    // find by deparment code
                    department = _db.Departments
                       .Where(x => x.Code == _id.ToUpper())
                       .FirstOrDefault();
                }

                if (department == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Department>(404, $"Department '{id}' not found", null);
                }

                // populate the one-to-many relationship
                _db.Entry(department)
                    .Collection(x => x.Employees)
                    .Load();

                Response.StatusCode = 200;
                return new ResponseBase<Department>(200, "Deparment found", department);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Department>(500, e.Message, null);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public ResponseBase<Department> UpdateDepartment(string id, [FromBody]DepartmentUpdateDto input)
        {
            try
            {
                var department = _db.Departments.Find(new Guid(id.Trim()));
                if (department == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Department>(404, $"Department '{id}' not found", null);
                }

                var name = input.Name?.Trim();
                var code = input.Code?.Trim().ToUpper();

                if (name != null && name != "")
                {
                    department.Name = name;
                }
                if (code != null && code != "")
                {
                    department.Code = code;
                }

                _db.Departments.Update(department);
                _db.SaveChanges();

                return new ResponseBase<Department>(200, "Department Updated", department);
            } catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Department>(500, e.Message, null);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseBase<Department> DeleteDepartment(string id)
        {
            try
            {
                var department = _db.Departments.Find(new Guid(id.Trim()));

                if (department == null)
                {
                    Response.StatusCode = 404;
                    return new ResponseBase<Department>(404, "Department not found", null);
                }

                _db.Departments.Remove(department);
                _db.SaveChanges();

                return new ResponseBase<Department>(200, "Department deleted", department);
            } catch (Exception e)
            {
                Console.WriteLine(e);

                Response.StatusCode = 500;
                return new ResponseBase<Department>(500, e.Message, null);
            }
        }
    }
}

