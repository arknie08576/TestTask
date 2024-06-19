using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test2.Models;

namespace test2.ViewModels
{
    public class ViewProject
    {
        public int Id { get; set; }
        public ProjectType ProjectType { get; set; }
       
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        
        public string ProjectManager { get; set; }
        
        public string? Comment { get; set; }
        
        public ProjectStatus ProjectStatus { get; set; }
    }
}
