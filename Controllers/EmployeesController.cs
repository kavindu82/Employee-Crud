using ASP.NET.Data;
using ASP.NET.Models;
using ASP.NET.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCdBcontext mVCdBcontext;

        public EmployeesController(MVCdBcontext mVCdBcontext)
        {
            this.mVCdBcontext = mVCdBcontext;
        }
        [HttpGet]
        public async Task<IActionResult> index()
        {
            var employees = await mVCdBcontext.Employees.ToListAsync();
            return View(employees);
        }
        


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee
            {
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Description = addEmployeeRequest.Description,
            };
            
            await mVCdBcontext.Employees.AddAsync(employee);
            await mVCdBcontext.SaveChangesAsync();
            return Redirect("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id) 
        {
            var employee = await mVCdBcontext.Employees.FirstOrDefaultAsync(x => x.Id == Id);  

            if (employee != null)
            {
                var viewModel = new UpdateViewModel()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Description = employee.Description,
                };
                return await Task.Run(() => View("View" , viewModel));
            }

            return Redirect("Index");
        }
        [HttpPost]

        public async Task<IActionResult> View(UpdateViewModel updateViewModel)
        {
            var employee = await mVCdBcontext.Employees.FindAsync(updateViewModel.Id);
            
            if (employee != null) 
            {
                employee.Name = updateViewModel.Name;
                employee.Email = updateViewModel.Email;
                employee.Salary = updateViewModel.Salary;
                employee.DateOfBirth = updateViewModel.DateOfBirth;
                employee.Description = updateViewModel.Description; 

                await mVCdBcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateViewModel updateViewModel)
        {
            var employee = await mVCdBcontext.Employees.FindAsync(updateViewModel.Id);
            if (employee != null)
            {
                mVCdBcontext.Employees.Remove(employee);
                await mVCdBcontext.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
