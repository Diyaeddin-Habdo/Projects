namespace MtuSetsAPIs.Models.Documents
{
    public class dtoAdd
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public IFormFile SGKStajFormu { get; set; }
        public IFormFile StajBasvuruFormu { get; set; }
        public IFormFile StajKabulFormu { get; set; }
        public IFormFile StajTaahhutnameFormu { get; set; }

    }
}
