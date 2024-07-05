using test2.Enums;

namespace test2.TableModels
{
    public class ViewLeaveRequest
    {
        public int Id { get; set; }
        public string Employee { get; set; }
        public AbsenceReason AbsenceReasonn { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
        public LeaveRequestStatus Status { get; set; }
    }
}
