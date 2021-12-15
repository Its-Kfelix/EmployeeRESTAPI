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
       
        public HttpResponseMessage Get(string gender="All")
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, db.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(x => x.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(x => x.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value of gender must " +
                            "be either all, male or female." + gender + "is invalid");
                }
            }
            
        }
       
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
               var emply= db.Employees.FirstOrDefault(x => x.Employee_id == id);
                if (emply != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emply);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employ with Id: " + id.ToString() + " not found");
                }
            }

        }
        public HttpResponseMessage POST([FromBody] Employee employee)
        {
            try 
            { 
                using(EmployeeEntities db = new EmployeeEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Employee_id.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try 
            { 
                using(EmployeeEntities db= new EmployeeEntities())
                {
                    var emply = db.Employees.Remove(db.Employees.FirstOrDefault(x => x.Employee_id == id));
                    if (emply != null)

                    {
                        db.Employees.Remove(emply);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                      
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employ with Id: " + id.ToString() + " not found to delete.");
                    }
                
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage PUT(int id, [FromBody] Employee employee)
        {
            try 
            { 
                using(EmployeeEntities db= new EmployeeEntities())
                {
                    var emply = db.Employees.FirstOrDefault(x => x.Employee_id == id);
                    if (emply != null)
                    {
                        emply.Employee_id = employee.Employee_id;
                        emply.FirstName = employee.FirstName;
                        emply.LastName = employee.LastName;
                        emply.Gender = employee.Gender;
                        emply.Salary = employee.Salary;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, emply);
                    }
                    else
                    {
                       
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id: " + id.ToString() + " not found to update");
                    }
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
