using BackendApis.Domain.Entities;
using BackendApis.Repositories;

namespace BackendApis.Specification
{
    public class PersonnelDataSpecification : BaseSpecification<PersonnelData>
    {


        public PersonnelDataSpecification(string firstname,string lastname)
        {
            Criteria = i => i.FirstName.Trim()==firstname.Trim()&&i.LastName.Trim()==lastname.Trim();
        }

        public PersonnelDataSpecification(string firstname, string lastname,string date)
        {
            Criteria = i => i.FirstName.Trim() == firstname.Trim() && i.LastName.Trim() == lastname.Trim() && i.Date.Trim()==date.Trim();
        }
    }
}