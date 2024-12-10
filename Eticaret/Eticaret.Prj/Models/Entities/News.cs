using System.ComponentModel.DataAnnotations;

namespace Eticaret.Prj.Entities
{
    public class News : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }

    }
}
