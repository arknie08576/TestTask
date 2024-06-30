using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test2.Enums;

namespace test2.TableModels
{
    public class EmployeeView
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string FullName { get; set; }

        public Subdivision Subdivision { get; set; }

        public Position Position { get; set; }

        public EmployeeStatus Status { get; set; }


        public string? PeoplePartner { get; set; }

        public int Out_of_OfficeBalance { get; set; }

        public byte[]? Photo { get; set; }
        public int? AssignedProject { get; set; }
    }
}
