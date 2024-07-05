using test2.Enums;

namespace test2.TableModels
{
    public class ViewProject
    {
        public int Id { get; set; }
        public ProjectType ProjectTypee { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? ProjectManager { get; set; }
        public string? Comment { get; set; }
        public ProjectStatus ProjectStatuss { get; set; }
    }
}
