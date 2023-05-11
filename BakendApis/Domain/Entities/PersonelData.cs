namespace BackendApis.Domain.Entities
{
    public class PersonnelData: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public string Date { get; set; }
        public DateTime GDate { get; set; }
        public decimal Salary { get; set; }
        public decimal OverTime { get; set; }
    }
}
