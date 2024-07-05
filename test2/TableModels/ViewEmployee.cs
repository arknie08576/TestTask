using test2.Enums;

namespace test2.TableModels
{
    public class ViewEmployee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public Subdivision Subdivisionn { get; set; }
        public Position Positionn { get; set; }
        public EmployeeStatus Status { get; set; }
        public string PeoplePartner { get; set; }
        public int Out_of_OfficeBalance { get; set; }
        public int? AssignedProject { get; internal set; }
    }
}
