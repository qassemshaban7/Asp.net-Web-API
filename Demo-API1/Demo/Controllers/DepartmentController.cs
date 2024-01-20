using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Demo.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Controllers
{
    [Route("api/[controller]")]//api/Department "UI  ==>request verb GET|POST|PUT|DELTE
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase //(response according status Code
    {
        private readonly ITIEntity context;

        public DepartmentController(ITIEntity context)
        {
            this.context = context;
        }
        //api/DEpartment
        [HttpGet]
        
        public IActionResult GetAllDEpartmet()
        {
            List<Department> deptList = context.Departments.ToList();
            return Ok(deptList);//response Body Json
        }



        [HttpGet("Emps/{id:int}")]//api/DEpartment/1
        public IActionResult GetByIDWithEmpNames(int id)
        {
            Department dept = context.Departments.Include(d=>d.Employee).FirstOrDefault(d => d.Id == id);
            DEpartmentDEtailsWithEmployeeNAme DepDto=new DEpartmentDEtailsWithEmployeeNAme();
            DepDto.ID = id;
            DepDto.DeptName = dept.Name;
            foreach (var item in dept.Employee)
            {
                DepDto.EmployeesName.Add(item.Name);
            }
            return Ok(DepDto);
        }
        
        
        [HttpGet("{id:int}",Name = "GetOneDeptRoute")]//api/DEpartment/1
        public IActionResult GetByID(int id)
        {
            Department dept=context.Departments.FirstOrDefault(d => d.Id == id);
            return Ok(dept);
        }

        [HttpGet("{Name:alpha}")]//api/DEpartment/ahmed
        public IActionResult GetByName(string Name)
        {
            Department dept = context.Departments.FirstOrDefault(d => d.Name == Name);
            return Ok(dept);
        }
        
        ////api/Department
        [HttpPost]//add Resourse "DEpartment
        public IActionResult PostAllDEpartmet(Department Dept)
        {
            if (ModelState.IsValid == true)
            {
                //201 Create ==>location
                context.Departments.Add(Dept);
                context.SaveChanges();
                //how to get current domain
                string url= Url.Link("GetOneDeptRoute", new {id= Dept.Id });
                return Created(url, Dept);
            }
            // return BadRequest("Department Not Valid");
            return BadRequest(ModelState);

        }
        //api/Department/7
       
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id ,[FromBody]Department dept)
        {
            if (ModelState.IsValid == true)
            {
                Department OldDept= context.Departments.FirstOrDefault(d => d.Id == id);
                if (OldDept != null)
                {
                    OldDept.Name = dept.Name;
                    OldDept.Manager = dept.Manager;
                    context.SaveChanges();
                    return StatusCode(204, OldDept);
                }
                return BadRequest("Id Not Valid");

            }
            return BadRequest(ModelState);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            Department OldDept = context.Departments.FirstOrDefault(d => d.Id == id);
            if (OldDept != null)
            {
                try
                {
                    context.Departments.Remove(OldDept);
                    context.SaveChanges();
                    return StatusCode(204, "Record Remove Success");
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Id Not Found");
        }
    }
}
