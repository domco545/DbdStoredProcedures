using System.Data;
using Dapper;
using DbdStoredProcedures.API.Models;

namespace DbdStoredProcedures.API.Repositories;

public class DepartmentRepository
{
    private readonly DbFactory _dbFactory;

    public DepartmentRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<int> CreateDepartment(Department department)
    {
        using var connection = _dbFactory.CreateConnection();

        var param = new DynamicParameters();
        param.Add("@DName", department.DName);
        param.Add("@MgrSSN", department.MgrSSN);
        // param.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
        param.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        var res = await connection.ExecuteAsync("usp_CreateDepartment", param, commandType: CommandType.StoredProcedure);
        
        var createdId = param.Get<int>("@c");
        return createdId;
    }

    public async Task UpdateDepartmentName(Department department)
    {
        using var connection = _dbFactory.CreateConnection();
        
        var id = await connection.ExecuteAsync(
            "usp_UpdateDepartmentName", 
            new
            {
                DNumber = department.DNumber, 
                DName = department.DName
            }, 
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateDepartmentManager(Department department)
    {
        using var connection = _dbFactory.CreateConnection();

        var id = await connection.ExecuteAsync(
            "usp_UpdateDepartmentManager", 
            new
            {
                DNumber = department.DNumber, 
                MgrSSN = department.MgrSSN
            }, 
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteDepartment(Department department)
    {
        using var connection = _dbFactory.CreateConnection();

        var id = await connection.ExecuteAsync(
            "usp_DeleteDepartment", 
            new
            {
                DNumber = department.DNumber
            }, 
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Department?> GetDepartment(int departmentId)
    {
        using var connection = _dbFactory.CreateConnection();

        var department = await connection.QueryFirstOrDefaultAsync<Department>(
            "usp_GetDepartment", 
            new
            {
                DNumber = departmentId
            }, 
            commandType: CommandType.StoredProcedure);

        return department;
    }

    public async Task<IEnumerable<Department>> GetAllDepartments()
    {
        using var connection = _dbFactory.CreateConnection();

        var departments = await connection.QueryAsync<Department>(
            "usp_GetAllDepartments",
            commandType: CommandType.StoredProcedure);

        return departments;
    }
}