using System.ComponentModel.DataAnnotations;

namespace UserAuthAPI.API.Domain.Entities
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StoreStaff { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
