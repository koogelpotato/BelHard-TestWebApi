using System.ComponentModel.DataAnnotations;

namespace BelHard_TestWebApi.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string Position { get; set; }
    }
}
