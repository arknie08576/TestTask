using System.ComponentModel.DataAnnotations;

namespace test2.Models
{
    public abstract class EntityBase

    {
        [Key]
        public int Id { get; set; }
    }
}
