using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test2.Models;

namespace test2.ViewModels
{
    public class ViewLeaveRequest
    {
        public int Id { get; set; }
        public string Employee { get; set; }
      
        public AbsenceReason AbsenceReason { get; set; }
       
        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }
        
        public string? Comment { get; set; }
        
        public LeaveRequestStatus Status { get; set; }
    }
}
