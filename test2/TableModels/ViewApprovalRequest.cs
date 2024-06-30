using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test2.Models;

namespace test2.TableModels
{
    public class ViewApprovalRequest

    {
        public int Id { get; set; }
        public string Approver { get; set; }

        
        public int? LeaveRequestt { get; set; }
      
        public ApprovalRequestStatus Status { get; set; }
       

        public string? Comment { get; set; }

    }
}
