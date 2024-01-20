using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Demo.DTO;
namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIEntity context;

        public EmployeeController(ITIEntity _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetAllEmp()
        {
            List<Employee> EmpList = context.Employees.Include(s=>s.Department).ToList();

            return Ok(EmpList);
        }
        [HttpGet("{id:int}",Name ="OneEmployeeRoute")]
        public IActionResult GetEmp(int id)
        {
            Employee Emp = context.Employees.Include(s => s.Department)
                .FirstOrDefault(e=>e.Id==id);
            EmployeeDataWithDepartmentNameDTO EmpDto=new EmployeeDataWithDepartmentNameDTO();
            EmpDto.DepartmentName = Emp.Department.Name;
            EmpDto.StudentName = Emp.Name;
            EmpDto.ID = Emp.Id;
            EmpDto.Address = Emp.Address;
            return Ok(EmpDto);
        }
    }
}
