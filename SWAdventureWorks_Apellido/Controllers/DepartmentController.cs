using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWAdventureWorks_Apellido.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWAdventureWorks_Apellido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AdventureWorks2019Context context;

        public DepartmentController(AdventureWorks2019Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return context.Department.ToList();
        }

        [HttpGet("{ID}")]
        public ActionResult<Department> GetById(short ID)
        {
            Department department = (from e in context.Department
                           where e.DepartmentId == ID
                           select e).SingleOrDefault();
            return department;
        }

        [HttpGet("name/{name}")]
        public ActionResult<Department> GetByName(string name)
        {

            Department department = (from e in context.Department
                           where e.Name == name
                           select e).SingleOrDefault();
            return department;
        }

        [HttpPost]
        public ActionResult<Department> Post(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Department.Add(department);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> Delete(short id)
        {
            var department = (from c in context.Department
                         where c.DepartmentId == id
                         select c).SingleOrDefault();

            if (department == null)
            {
                return NotFound();
            }
            context.Department.Remove(department);
            context.SaveChanges();
            return department;

        }
        [HttpPut("{id}")]
        public ActionResult<Department> Put(short id, [FromBody] Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            context.Entry(department).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
    }
    }
