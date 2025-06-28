namespace MtuSetsAPIs.Models.Documents
{
    public class dtoAdded
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string SGKStajFormu { get; set; }
        public string StajBasvuruFormu { get; set; }
        public string StajKabulFormu { get; set; }
        public string StajTaahhutnameFormu { get; set; }
        public string Status { get; set; }
        public DateTime UploadTime { get; set; }

        public dtoAdded(BusinessLayer.Documents d)
        {
            Id = d.Id;
            StudentId = d.StudentId;
            SGKStajFormu = d.SGKStajFormu;
            StajBasvuruFormu = d.StajBasvuruFormu;
            StajKabulFormu = d.StajKabulFormu;
            StajTaahhutnameFormu = d.StajTaahhutnameFormu;
            Status = d.Status;
            UploadTime = d.UploadTime;
        }
    }
}
