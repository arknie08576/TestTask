using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test2.Models
{
    public class Project : EntityBase
    {
        [Required]
        public ProjectType ProjectType { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        
        [ForeignKey("employee")]
        public int? ProjectManager {  get; set; }
        [MaxLength(100)]
        public string? Comment { get; set; }
        [Required]
        public ProjectStatus ProjectStatus { get; set; }
        public Employee employee { get; set; }
    }
}
