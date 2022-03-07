using DbdStoredProcedures.API.Models;
using DbdStoredProcedures.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DbdStoredProcedures.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController: ControllerBase
{
    private readonly DepartmentRepository _departmentRepository;

    public DepartmentController(DepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] string departmentName, int managerSsn)
    {
        var result = await _departmentRepository.CreateDepartment(new Department(departmentName, managerSsn));
        return Ok(result);
    }

    [HttpPut("{dNumber:int}/Name")]
    public async Task<IActionResult> UpdateDepartmentName(int dNumber, [FromBody] string departmentName)
    {
        var department = await _departmentRepository.GetDepartment(dNumber);
        if (department is null) 
            return BadRequest($"Department with number {dNumber} does not exist.");

        department.DName = departmentName;
        await _departmentRepository.UpdateDepartmentName(department);
        return Ok();
    }
    
    [HttpPut("{dNumber:int}/Manager")]
    public async Task<IActionResult> UpdateDepartmentManager(int dNumber, [FromBody] int managerSsn)
    {
        var department = await _departmentRepository.GetDepartment(dNumber);
        if (department is null) 
            return BadRequest($"Department with number {dNumber} does not exist.");

        department.MgrSSN = managerSsn;
        await _departmentRepository.UpdateDepartmentName(department);
        return Ok();
    }

    [HttpDelete("{dNumber:int}")]
    public async Task<IActionResult> DeleteDepartment(int dNumber)
    {
        var department = await _departmentRepository.GetDepartment(dNumber);
        if (department is null) 
            return BadRequest($"Department with number {dNumber} does not exist.");
        
        await _departmentRepository.DeleteDepartment(department);
        return Ok();
    }

    [HttpGet("{dNumber:int}")]
    public async Task<IActionResult> GetDepartment(int dNumber)
    {
        var department = await _departmentRepository.GetDepartment(dNumber);

        return Ok(department);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDepartments()
    {
        var departments = await _departmentRepository.GetAllDepartments();

        return Ok(departments);
    }
}