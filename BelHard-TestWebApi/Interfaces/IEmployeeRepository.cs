using BelHard_TestWebApi.Models;

namespace BelHard_TestWebApi.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);
        Task<bool> EmployeeExists(int id);
        Task<bool> Save();
    }
}
