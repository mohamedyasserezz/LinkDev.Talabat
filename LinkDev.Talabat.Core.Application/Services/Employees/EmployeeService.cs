using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications.Employees;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeesToReturnDto>> GetAllEmployees()
        {

            var spec = new EmployeeWithDepartmentSpecitication();
            var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
            var employeesDto = mapper.Map<IEnumerable<EmployeesToReturnDto>>(employees);
            return employeesDto;
        }

        public async Task<EmployeesToReturnDto> GetEmployee()
        {
            var spec = new EmployeeWithDepartmentSpecitication();
            var employees = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
            var employeesDto = mapper.Map<EmployeesToReturnDto>(employees);
            return employeesDto;
        }
    }
}
