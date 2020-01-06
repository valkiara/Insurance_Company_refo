using IBMS.Service.MasterData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class EmployeeController : ApiController
    {
        ManageEmployee manageEmployee = new ManageEmployee();

        [HttpPost()]
        [ActionName("SaveEmployee")]
        public IHttpActionResult SaveEmployee([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                string empName = !string.IsNullOrEmpty(data.SelectToken("EmpName").Value<string>()) ? data.SelectToken("EmpName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("Address1").Value<string>()) ? data.SelectToken("Address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("Address2").Value<string>()) ? data.SelectToken("Address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("Address3").Value<string>()) ? data.SelectToken("Address3").Value<string>() : string.Empty;
                string contactNo = !string.IsNullOrEmpty(data.SelectToken("ContactNo").Value<string>()) ? data.SelectToken("ContactNo").Value<string>() : string.Empty;
                string email = !string.IsNullOrEmpty(data.SelectToken("Email").Value<string>()) ? data.SelectToken("Email").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageEmployee.IsEmployeeAvailable(null, empName))
                {
                    EmployeeVM employeeVM = new EmployeeVM();
                    employeeVM.EmployeeName = empName;
                    employeeVM.Address1 = address1;
                    employeeVM.Address2 = address2;
                    employeeVM.Address3 = address3;
                    employeeVM.ContactNo = contactNo;
                    employeeVM.Email = email;
                    employeeVM.DesignationID = designationID;
                    employeeVM.CreatedBy = userID;

                    bool status = manageEmployee.SaveEmployee(employeeVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Saved" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Employee Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateEmployee")]
        public IHttpActionResult UpdateEmployee([FromBody]JObject data)
        {
            try
            {
                int empID = !string.IsNullOrEmpty(data.SelectToken("EmpID").Value<string>()) ? Convert.ToInt32(data.SelectToken("EmpID").Value<string>()) : 0;
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                string empName = !string.IsNullOrEmpty(data.SelectToken("EmpName").Value<string>()) ? data.SelectToken("EmpName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("Address1").Value<string>()) ? data.SelectToken("Address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("Address2").Value<string>()) ? data.SelectToken("Address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("Address3").Value<string>()) ? data.SelectToken("Address3").Value<string>() : string.Empty;
                string contactNo = !string.IsNullOrEmpty(data.SelectToken("ContactNo").Value<string>()) ? data.SelectToken("ContactNo").Value<string>() : string.Empty;
                string email = !string.IsNullOrEmpty(data.SelectToken("Email").Value<string>()) ? data.SelectToken("Email").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageEmployee.IsEmployeeAvailable(empID, empName))
                {
                    EmployeeVM employeeVM = new EmployeeVM();
                    employeeVM.EmployeeID = empID;
                    employeeVM.EmployeeName = empName;
                    employeeVM.Address1 = address1;
                    employeeVM.Address2 = address2;
                    employeeVM.Address3 = address3;
                    employeeVM.ContactNo = contactNo;
                    employeeVM.Email = email;
                    employeeVM.DesignationID = designationID;
                    employeeVM.ModifiedBy = userID;

                    bool status = manageEmployee.UpdateEmployee(employeeVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully updated" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Update Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Employee Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteEmployee")]
        public IHttpActionResult DeleteEmployee([FromBody]JObject data)
        {
            try
            {
                int empID = !string.IsNullOrEmpty(data.SelectToken("EmpID").Value<string>()) ? Convert.ToInt32(data.SelectToken("EmpID").Value<string>()) : 0;
                bool status = manageEmployee.DeleteEmployee(empID);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Deleted" });
                }
                else
                {
                    return Json(new { status = false, message = "Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            try
            {
                var employeeList = manageEmployee.GetAllEmployees();
                return Json(new
                {
                    status = true,
                    data = employeeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllEmployeesByBUID")]
        public IHttpActionResult GetAllEmployeesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var employeeList = manageEmployee.GetAllEmployeesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = employeeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetEmployeesByDesignationID")]
        public IHttpActionResult GetEmployeesByDesignationID([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                var employeeList = manageEmployee.GetEmployeesByDesignationID(designationID);
                return Json(new
                {
                    status = true,
                    data = employeeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetEmployeeByID")]
        public IHttpActionResult GetEmployeeByID([FromBody]JObject data)
        {
            try
            {
                int empID = !string.IsNullOrEmpty(data.SelectToken("EmpID").Value<string>()) ? Convert.ToInt32(data.SelectToken("EmpID").Value<string>()) : 0;
                var employee = manageEmployee.GetEmployeeByID(empID);
                return Json(new
                {
                    status = true,
                    data = employee
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
