namespace MtuSetsAPIs.Models.Teacher
{
    public class dtoAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public IFormFile Image { get; set; }

        public string Role { get; set; }
        public string DepartmentId { get; set; }
    }
}
