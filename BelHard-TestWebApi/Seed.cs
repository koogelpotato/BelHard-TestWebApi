using BelHard_TestWebApi.Data;
using BelHard_TestWebApi.Models;

namespace BelHard_TestWebApi
{
    public class Seed
    {
        private readonly DataContext _dataContext;

        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!_dataContext.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee { FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович", Position = "Менеджер" },
                    new Employee { FirstName = "Елена", LastName = "Смирнова", MiddleName = "Алексеевна", Position = "Супервайзер" },
                    new Employee { FirstName = "Михаил", LastName = "Петров", MiddleName = "Сергеевич", Position = "Ассистент" },
                    new Employee { FirstName = "Анна", LastName = "Сидорова", MiddleName = "Ивановна", Position = "Клерк" },
                    new Employee { FirstName = "Дмитрий", LastName = "Козлов", MiddleName = "Александрович", Position = "Техник" }
                };

                _dataContext.Employees.AddRange(employees);
                _dataContext.SaveChanges();
            }
        }
    }
}
