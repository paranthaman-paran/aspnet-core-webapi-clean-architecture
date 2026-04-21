using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Repository;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IRepoEmployee _repoEmployee;

        public ValuesController(IRepoEmployee repoEmployee)
        {
            _repoEmployee = repoEmployee;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {

            var data = await _repoEmployee.GetAll();
            return Ok(data);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] EmployeeDTO employeeDTO)
        {
            await _repoEmployee.AddEmployee(employeeDTO);
            return Ok(employeeDTO);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetByID(int id)
        {
            var data = await _repoEmployee.GetByID(id);
            return Ok(data);
        }
        [HttpPut ("Update")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] EmployeeDTO employeeDTO)

        {
           var UpdateData = await _repoEmployee.UpdateEmployee(id, employeeDTO);
            return Ok(UpdateData);
        }

        [HttpPatch ("Patch")]
        public async Task<IActionResult> PatchEmployee(int id , [FromBody] EmployeeDTO employeeDTO)
        {
            var PatchEmployee = await _repoEmployee.Patch(id,employeeDTO);

            if (PatchEmployee == null) return NotFound();

            return Ok(PatchEmployee);
        }

        [HttpDelete("delete")] 
        public async Task<IActionResult> DeleteEmployee(int id)
        {
         var Deleteemployee =    await _repoEmployee.DeleteEmployee(id);
            if(Deleteemployee == null) return NotFound();
            return Ok(Deleteemployee);

        
        }
    }
}
