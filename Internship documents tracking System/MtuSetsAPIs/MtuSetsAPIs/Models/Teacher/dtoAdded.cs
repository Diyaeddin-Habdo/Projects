using BusinessLayer;
namespace MtuSetsAPIs.Models.Teacher
{
    public class dtoAdded
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public string DepartmentId { get; set; }

        public dtoAdded(BusinessLayer.Teacher h)
        {
            Id = h.Id;
            Name = h.Name;
            Email = h.Email;
            Phone = h.Phone;
            Image = h.ImagePath;
            Role = h.Role;
            DepartmentId = h.DepartmentId;
        }
    }
}
