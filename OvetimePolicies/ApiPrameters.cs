using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvetimePolicies
{
    public class Data
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }

    public class PersonnelDataDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public decimal BasicSalary { get; set; }
        [Required]
        public decimal Allowance { get; set; }
        [Required]
        public decimal Transportation { get; set; }
        [Required]
        public string Date { get; set; }
       
    }
    public class PersonnelDataForEditDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Transportation { get; set; }
        public string? Date { get; set; }

    }
    public class PesonelData
    {
        public Data? CustomerData { get; set; }
        public PersonnelDataDto? JsonData { get; set; }
        public string? CsvData { get; set; }
        public string OverTimeCalculator { get; set; }


    }
}
