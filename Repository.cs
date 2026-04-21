
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Database;

namespace RepositoryPattern.Repository
{


    public class Repository : IRepoEmployee
    {
        private readonly AppDBContext _appDBContext;

        public Repository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;

        }
            public async Task<List<EmployeeDTO>> GetAll()
        {
            IQueryable<Employee> query = _appDBContext.employees.AsQueryable();

            return await query.Select(x => new EmployeeDTO
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.EmployeeName,
                EmployeeDescription = x.EmployeeDescription,


            }).ToListAsync();

        }

        public async Task<EmployeeDTO> GetByID(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
         var query = await _appDBContext.employees.Where(x =>x.EmployeeID == id)
                .Select(x => new EmployeeDTO
                {
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeDescription = x.EmployeeDescription,

                }).FirstOrDefaultAsync();

            return query;
        }

        private EmployeeDTO BadRequest()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee { 
            
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeDescription = employeeDTO.EmployeeDescription
            };

            await _appDBContext.AddAsync(employee);
            await _appDBContext.SaveChangesAsync();

            employeeDTO.EmployeeID = employee.EmployeeID;

            return employeeDTO;


        }

        public async Task<bool> DeleteEmployee(int id)
        {
           var Employee =  await _appDBContext.employees.Where(x => x.EmployeeID == id).FirstOrDefaultAsync();

            if (Employee == null) return false;

            else
            {
                _appDBContext.Remove(Employee);
                await _appDBContext.SaveChangesAsync();
                return true;
            }


        }



        public async Task<EmployeeDTO> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            var UpdateEmployee = await _appDBContext.employees.FirstOrDefaultAsync(x => x.EmployeeID == id);

            if (UpdateEmployee != null)
            {
                   
                UpdateEmployee.EmployeeName = employeeDTO.EmployeeName;
                UpdateEmployee.EmployeeDescription = employeeDTO.EmployeeDescription;
                UpdateEmployee.EmployeePhoneNumber = employeeDTO.EmployeePhoneNumber;

            }
            await _appDBContext.SaveChangesAsync();

            return new EmployeeDTO
            {

                EmployeeID = id,
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeDescription = employeeDTO.EmployeeDescription,
                EmployeePhoneNumber = employeeDTO.EmployeePhoneNumber
            };
        }

        public async Task<EmployeeDTO> Patch(int id, EmployeeDTO employeeDTO)
        {
            var employee = await _appDBContext.employees.Where (x=> x.EmployeeID == id).FirstOrDefaultAsync();

            if (employee == null) return null;

            if (employeeDTO.EmployeeName != null)
            {
                employee.EmployeeName = employeeDTO.EmployeeName;
            }
            if (employeeDTO.EmployeeDescription != null)
            {
                employee.EmployeeDescription = employeeDTO.EmployeeDescription;
            }
            if (employeeDTO.EmployeePhoneNumber != null)
            {
                employee.EmployeePhoneNumber = employeeDTO.EmployeePhoneNumber;
            }

            await _appDBContext.SaveChangesAsync();

            return new EmployeeDTO
            {
                EmployeeID = id,
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeDescription = employeeDTO.EmployeeDescription,
                EmployeePhoneNumber = employeeDTO.EmployeePhoneNumber
            };
        }
    }
}
