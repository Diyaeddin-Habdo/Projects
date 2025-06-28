namespace MtuSetsAPIs.Models.Student
{
    public class dtoAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string SNo { get; set; }
        public string DepartmentId { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
