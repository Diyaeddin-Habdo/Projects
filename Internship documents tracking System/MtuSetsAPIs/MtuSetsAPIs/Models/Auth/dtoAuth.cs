namespace MtuSetsAPIs.Models.Auth
{
    public class dtoAuth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }

        public dtoAuth(int id, string name, string email, string phone, string imagePath, string role)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.ImagePath = imagePath;
            this.Role = role;
        }

    }
}
