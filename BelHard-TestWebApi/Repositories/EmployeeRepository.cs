using BelHard_TestWebApi.Data;
using BelHard_TestWebApi.Interfaces;
using BelHard_TestWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelHard_TestWebApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            await _context.AddAsync(employee);
            return await Save();
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return await Save();
        }

        public async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.AnyAsync(x => x.Id == id);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Employee>> GetEmployees()
        {
            return await _context.Employees.OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return await Save();
        }
    }
}
