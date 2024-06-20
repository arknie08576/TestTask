using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test2.Models
{
    public class Employee : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(100)]
        public string Salt { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public Subdivision Subdivision { get; set; }
        [Required]
        public Position Position { get; set; }
        [Required]
        public EmployeeStatus Status { get; set; }
        
        [ForeignKey("employee")]
        public int? PeoplePartner {  get; set; }
        [Required]
        public int Out_of_OfficeBalance { get; set; }
        [MaxLength(100)]
        public string? Photo { get; set; }
        [ForeignKey("project")]
        public int AssignedProject {  get; set; }
        public Employee employee { get; set; }
        public Project project { get; set; }



    }
}
