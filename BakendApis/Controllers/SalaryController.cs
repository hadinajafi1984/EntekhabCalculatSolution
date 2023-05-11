using BackendApis.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using OvetimePolicies;
using Newtonsoft.Json;
using BackendApis.Domain.Entities;
using BackendApis.Domain.Enums;
using BackendApis.Services;
using BackendApis.Helper;
using SuperConvert.Extensions;
using BackendApis.Specification;
using Dapper;
using OvetimePolicies.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendApis.Controllers
{
    [ServiceExceptionInterceptor]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private IServiceFactory ServiceFactory { get; set; }
        private ILogger<SalaryController> _logger;

        public SalaryController(IServiceFactory serviceFactory, ILogger<SalaryController> logger)
        {
            ServiceFactory = serviceFactory;
            _logger = logger;
        }

        /// <summary>
        /// Get Personnel Data with Name
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{firstname}/{lastname}")]
        public async Task<IActionResult> Get(string firstname, string lastname)
        {
            var data = await ServiceFactory.AppServices.GetByIdWitDapper<PersonnelData>(firstname,lastname);
            return Ok(data);
        }


        /// <summary>
        /// Get Personnel Date Between To Date
        /// </summary>
        /// <param name="startDate">String Of persian date</param>
        /// <param name="endDate">String Of persian date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{startDate}/{endDate}")]
        public async Task<IActionResult> GetRange(string startDate, string endDate)
        {
            var sDate = HelperClass.ConverDate(startDate);
            var eDate = HelperClass.ConverDate(endDate);
            var data = await ServiceFactory.AppServices.GetRangeWithDapper<PersonnelData>(sDate, eDate);
            return Ok(data);
        }

        /// <summary>
        /// Create New Personnel Data
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{datatype}/[controller]/[action]")]
        public async Task<IActionResult> Add(Domain.Enums.Datatype datatype, [FromBody] PesonelData properties)
        {

            if (datatype == Datatype.Custome)
            {
                if (properties.CustomerData == null)
                {
                    return BadRequest("Data is not correct");
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                var line1 = properties.CustomerData.Line1.Split('/');
                var line2 = properties.CustomerData.Line2.Split('/');
                PersonnelData personel = new();
                for (int i = 0; i < line1.Length; i++)
                {
                    switch (line1[i])
                    {
                        case nameof(personel.FirstName):
                            personel.FirstName = line2[i];
                            break;
                        case nameof(personel.LastName):
                            personel.LastName = line2[i];
                            break;
                        case nameof(personel.BasicSalary):
                            personel.BasicSalary = decimal.Parse(line2[i]);
                            break;
                        case nameof(personel.Allowance):
                            personel.Allowance = decimal.Parse(line2[i]);
                            break;
                        case nameof(personel.Transportation):
                            personel.Transportation = decimal.Parse(line2[i]);
                            break;
                        case nameof(personel.Date):
                            personel.Date = line2[i];
                            break;
                    }
                }

                OvetimeServices ovetimeServices = new OvetimeServices(personel.BasicSalary, personel.Allowance,
                    personel.Transportation, Convert.ToDecimal(9 / 100));
                personel.Salary = ovetimeServices.CalculatorA();
                personel.OverTime = ovetimeServices.CalculatorB();
                personel.GDate = HelperClass.ConverDate(personel.Date);
                personel.CreateDate = DateTime.Now;
                ;
                await ServiceFactory.AppServices.AddNew(personel);
                await ServiceFactory.SaveAsync();
                if (properties.OverTimeCalculator == "CalculatorA")
                {
                    return Ok(ovetimeServices.CalculatorA());
                }
                else if (properties.OverTimeCalculator == "CalculatorB")
                {
                    return Ok(ovetimeServices.CalculatorB());
                }
                else if (properties.OverTimeCalculator == "CalculatorC")
                {
                    return Ok(ovetimeServices.CalculatorC());
                }

            }
            else if (datatype == Datatype.Json)
            {
                if (properties.JsonData == null)
                {
                    return BadRequest("PersonnelDataDto is not correct");
                }

                var mapped = AutoMapperService.Map<PersonnelDataDto, PersonnelData>(properties.JsonData);
                OvetimeServices ovetimeServices = new OvetimeServices(mapped.BasicSalary, mapped.Allowance,
                    mapped.Transportation, Convert.ToDecimal(9 / 100));
                mapped.Salary = ovetimeServices.CalculatorA();
                mapped.OverTime = ovetimeServices.CalculatorB();
                mapped.GDate = HelperClass.ConverDate(mapped.Date);
                mapped.CreateDate = DateTime.Now;
                await ServiceFactory.AppServices.AddNew(mapped);
                await ServiceFactory.SaveAsync();
                if (properties.OverTimeCalculator == "CalculatorA")
                {
                    return Ok(ovetimeServices.CalculatorA());
                }
                else if (properties.OverTimeCalculator == "CalculatorB")
                {
                    return Ok(ovetimeServices.CalculatorB());
                }
                else if (properties.OverTimeCalculator == "CalculatorC")
                {
                    return Ok(ovetimeServices.CalculatorC());
                }
            }
            else if (datatype == Datatype.CSV)
            {
                if (properties.CsvData == null)
                {
                    return BadRequest("PersonnelDataDto is not correct");
                }

                var json = properties.CsvData.ToJson();

                var mapped = AutoMapperService.Map<PersonnelDataDto, PersonnelData>(properties.JsonData);
                OvetimeServices ovetimeServices = new OvetimeServices(mapped.BasicSalary, mapped.Allowance,
                    mapped.Transportation, Convert.ToDecimal(9 / 100));
                mapped.Salary = ovetimeServices.CalculatorA();
                mapped.OverTime = ovetimeServices.CalculatorB();
                mapped.GDate = HelperClass.ConverDate(mapped.Date);
                mapped.CreateDate = DateTime.Now;
                await ServiceFactory.AppServices.AddNew(mapped);
                await ServiceFactory.SaveAsync();
                if (properties.OverTimeCalculator == "CalculatorA")
                {
                    return Ok(ovetimeServices.CalculatorA());
                }
                else if (properties.OverTimeCalculator == "CalculatorB")
                {
                    return Ok(ovetimeServices.CalculatorB());
                }
                else if (properties.OverTimeCalculator == "CalculatorC")
                {
                    return Ok(ovetimeServices.CalculatorC());
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit Personnel Date
        /// </summary>
        /// <param name="properties">Name and Family are keys for update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Update([FromBody] PersonnelDataForEditDto properties)
        {
            var person =
                await ServiceFactory.AppServices.GetALL(new PersonnelDataSpecification(properties.FirstName,
                    properties.LastName, properties.Date));
            if (!person.Any())
            {
                return NotFound();
            }
            else
            {
                var updatedPerson = person.First();
                updatedPerson.Allowance = properties.Allowance ?? updatedPerson.Allowance;
                updatedPerson.BasicSalary = properties.BasicSalary ?? updatedPerson.BasicSalary;
                updatedPerson.Transportation = properties.Transportation ?? updatedPerson.Transportation;
                updatedPerson.Date = properties.Date ?? updatedPerson.Date;
                updatedPerson.Date = properties.Date ?? updatedPerson.Date;
                OvetimeServices ovetimeServices = new OvetimeServices(updatedPerson.BasicSalary, updatedPerson.Allowance,
                    updatedPerson.Transportation, Convert.ToDecimal(9 / 100));
                updatedPerson.Salary = ovetimeServices.CalculatorA();
                updatedPerson.OverTime = ovetimeServices.CalculatorB();
                updatedPerson.GDate = HelperClass.ConverDate(updatedPerson.Date);
                await ServiceFactory.AppServices.Update(updatedPerson);
                await ServiceFactory.SaveAsync();
                return Ok();
            }
           
        }

        /// <summary>
        /// Delete Personnel data 
        /// </summary>
        /// <param name="firstname">Key</param>
        /// <param name="lastname">Key</param>
        /// <returns></returns>

        [HttpDelete]
        [Route("[controller]/[action]/{firstname}/{lastname}/{date}")]
        public async Task<IActionResult> Delete(string firstname, string lastname,string date)
        {
            var person =
                await ServiceFactory.AppServices.GetALL(new PersonnelDataSpecification(firstname,
                    lastname, date));
            if (!person.Any())
            {
                return NotFound();
            }
            else
            {
                var updatedPerson = person.First();
                await ServiceFactory.AppServices.Remove(updatedPerson);
                await ServiceFactory.SaveAsync();
                return Ok();
            }

        }
    }
}
