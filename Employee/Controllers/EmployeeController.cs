using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAcceess;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : ApiController
    {       
       
        public IEnumerable<Employee> Get()
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                return db.Employees.ToList();
            }
            
        }
       
        public Employee GetEmployeeById(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                return db.Employees.FirstOrDefault(x => x.Employee_id == id);
            }

        }
    }
}
