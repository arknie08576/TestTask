using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using test2.Enums;

namespace test2.Models
{
    public class LeaveRequest : EntityBase
    {
        [Required]
        [ForeignKey("employee")]
        public int Employee {  get; set; }
        [Required] 
        public AbsenceReason AbsenceReason { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [MaxLength(100)]
        public string? Comment { get; set; }
        [Required]
        public LeaveRequestStatus Status { get; set; }
        public Employee employee { get; set; }







    }
}
