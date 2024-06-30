using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using test2.Enums;

namespace test2.Models
{
    public class ApprovalRequest : EntityBase
    {
        
        [ForeignKey("employee")]
        public int? Approver {  get; set; }
        
        [ForeignKey("leaveRequest")]
        public int? LeaveRequest { get; set; }
        [Required]
        public ApprovalRequestStatus Status { get; set; }
        [MaxLength(100)]
        
        public string? Comment { get; set; }
        public Employee? employee { get; set; }
        public LeaveRequest? leaveRequest { get; set; }

    }
}
