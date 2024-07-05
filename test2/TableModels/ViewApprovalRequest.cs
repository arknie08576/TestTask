using test2.Enums;

namespace test2.TableModels
{
    public class ViewApprovalRequest

    {
        public int Id { get; set; }
        public string? Approver { get; set; }
        public int? LeaveRequestt { get; set; }
        public ApprovalRequestStatus Status { get; set; }
        public string? Comment { get; set; }

    }
}
