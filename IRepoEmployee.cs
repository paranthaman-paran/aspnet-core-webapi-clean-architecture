namespace RepositoryPattern.Repository
{
    public interface IRepoEmployee
    {
        public Task <List<EmployeeDTO>> GetAll();

        public Task<EmployeeDTO> GetByID(int id);

        public Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);

        public Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDTO);

        public Task<bool> DeleteEmployee(int id);

        public Task<EmployeeDTO> Patch(int  id, EmployeeDTO employeeDTO);


    }
}
