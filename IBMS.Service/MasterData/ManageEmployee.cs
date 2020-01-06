using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageEmployee
    {
        private UnitOfWork unitOfWork;
        public ManageEmployee()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveEmployee(EmployeeVM employeeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblEmployee employee = new tblEmployee();
                    employee.EmpName = employeeVM.EmployeeName;
                    employee.Address1 = employeeVM.Address1;
                    employee.Address2 = employeeVM.Address2;
                    employee.Address3 = employeeVM.Address3;
                    employee.ContactNo = employeeVM.ContactNo;
                    employee.Email = employeeVM.Email;
                    employee.DesignationID = employeeVM.DesignationID;
                    employee.CreatedDate = DateTime.Now;
                    employee.CreatedBy = employeeVM.CreatedBy;
                    unitOfWork.TblEmployeeRepository.Insert(employee);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateEmployee(EmployeeVM employeeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblEmployee employee = unitOfWork.TblEmployeeRepository.GetByID(employeeVM.EmployeeID);
                    employee.EmpName = employeeVM.EmployeeName;
                    employee.Address1 = employeeVM.Address1;
                    employee.Address2 = employeeVM.Address2;
                    employee.Address3 = employeeVM.Address3;
                    employee.ContactNo = employeeVM.ContactNo;
                    employee.Email = employeeVM.Email;
                    employee.DesignationID = employeeVM.DesignationID;
                    employee.ModifiedDate = DateTime.Now;
                    employee.ModifiedBy = employeeVM.ModifiedBy;
                    unitOfWork.TblEmployeeRepository.Update(employee);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteEmployee(int employeeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblEmployee employee = unitOfWork.TblEmployeeRepository.GetByID(employeeID);
                    unitOfWork.TblEmployeeRepository.Delete(employee);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public List<EmployeeVM> GetAllEmployees()
        {
            try
            {
                var employeeData = unitOfWork.TblEmployeeRepository.Get().ToList();

                List<EmployeeVM> employeeList = new List<EmployeeVM>();

                foreach (var employee in employeeData)
                {
                    EmployeeVM employeeVM = new EmployeeVM();
                    employeeVM.EmployeeID = employee.EmpID;
                    employeeVM.EmployeeName = employee.EmpName;
                    employeeVM.Address1 = employee.Address1;
                    employeeVM.Address2 = employee.Address2;
                    employeeVM.Address3 = employee.Address3;
                    employeeVM.ContactNo = employee.ContactNo;
                    employeeVM.Email = employee.Email;
                    employeeVM.DesignationID = employee.DesignationID != null ? Convert.ToInt32(employee.DesignationID) : 0;

                    if (employeeVM.DesignationID > 0)
                    {
                        employeeVM.DesignationName = employee.tblDesignation.Designation;
                    }

                    employeeVM.CreatedDate = employee.CreatedDate != null ? employee.CreatedDate.ToString() : string.Empty;
                    employeeVM.ModifiedDate = employee.ModifiedDate != null ? employee.ModifiedDate.ToString() : string.Empty;
                    employeeVM.CreatedBy = employee.CreatedBy != null ? Convert.ToInt32(employee.CreatedBy) : 0;
                    employeeVM.ModifiedBy = employee.ModifiedBy != null ? Convert.ToInt32(employee.ModifiedBy) : 0;

                    employeeList.Add(employeeVM);
                }

                return employeeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EmployeeVM> GetAllEmployeesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var employeeData = unitOfWork.TblEmployeeRepository.Get(x => x.tblDesignation.BUID == businessUnitID).ToList();

                List<EmployeeVM> employeeList = new List<EmployeeVM>();

                foreach (var employee in employeeData)
                {
                    EmployeeVM employeeVM = new EmployeeVM();
                    employeeVM.EmployeeID = employee.EmpID;
                    employeeVM.EmployeeName = employee.EmpName;
                    employeeVM.Address1 = employee.Address1;
                    employeeVM.Address2 = employee.Address2;
                    employeeVM.Address3 = employee.Address3;
                    employeeVM.ContactNo = employee.ContactNo;
                    employeeVM.Email = employee.Email;
                    employeeVM.DesignationID = employee.DesignationID != null ? Convert.ToInt32(employee.DesignationID) : 0;

                    if (employeeVM.DesignationID > 0)
                    {
                        employeeVM.DesignationName = employee.tblDesignation.Designation;
                    }

                    employeeVM.CreatedDate = employee.CreatedDate != null ? employee.CreatedDate.ToString() : string.Empty;
                    employeeVM.ModifiedDate = employee.ModifiedDate != null ? employee.ModifiedDate.ToString() : string.Empty;
                    employeeVM.CreatedBy = employee.CreatedBy != null ? Convert.ToInt32(employee.CreatedBy) : 0;
                    employeeVM.ModifiedBy = employee.ModifiedBy != null ? Convert.ToInt32(employee.ModifiedBy) : 0;

                    employeeList.Add(employeeVM);
                }

                return employeeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EmployeeVM> GetEmployeesByDesignationID(int designationID)
        {
            try
            {
                var employeeData = unitOfWork.TblEmployeeRepository.Get(x => x.DesignationID == designationID).ToList();

                List<EmployeeVM> employeeList = new List<EmployeeVM>();

                foreach (var employee in employeeData)
                {
                    EmployeeVM employeeVM = new EmployeeVM();
                    employeeVM.EmployeeID = employee.EmpID;
                    employeeVM.EmployeeName = employee.EmpName;
                    employeeVM.Address1 = employee.Address1;
                    employeeVM.Address2 = employee.Address2;
                    employeeVM.Address3 = employee.Address3;
                    employeeVM.ContactNo = employee.ContactNo;
                    employeeVM.Email = employee.Email;
                    employeeVM.DesignationID = employee.DesignationID != null ? Convert.ToInt32(employee.DesignationID) : 0;

                    if (employeeVM.DesignationID > 0)
                    {
                        employeeVM.DesignationName = employee.tblDesignation.Designation;
                    }

                    employeeVM.CreatedDate = employee.CreatedDate != null ? employee.CreatedDate.ToString() : string.Empty;
                    employeeVM.ModifiedDate = employee.ModifiedDate != null ? employee.ModifiedDate.ToString() : string.Empty;
                    employeeVM.CreatedBy = employee.CreatedBy != null ? Convert.ToInt32(employee.CreatedBy) : 0;
                    employeeVM.ModifiedBy = employee.ModifiedBy != null ? Convert.ToInt32(employee.ModifiedBy) : 0;

                    employeeList.Add(employeeVM);
                }

                return employeeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public EmployeeVM GetEmployeeByID(int employeeID)
        {
            try
            {
                var employeeData = unitOfWork.TblEmployeeRepository.GetByID(employeeID);

                EmployeeVM employeeVM = new EmployeeVM();
                employeeVM.EmployeeID = employeeData.EmpID;
                employeeVM.EmployeeName = employeeData.EmpName;
                employeeVM.Address1 = employeeData.Address1;
                employeeVM.Address2 = employeeData.Address2;
                employeeVM.Address3 = employeeData.Address3;
                employeeVM.ContactNo = employeeData.ContactNo;
                employeeVM.Email = employeeData.Email;
                employeeVM.DesignationID = employeeData.DesignationID != null ? Convert.ToInt32(employeeData.DesignationID) : 0;

                if (employeeVM.DesignationID > 0)
                {
                    employeeVM.DesignationName = employeeData.tblDesignation.Designation;
                }

                employeeVM.CreatedDate = employeeData.CreatedDate != null ? employeeData.CreatedDate.ToString() : string.Empty;
                employeeVM.ModifiedDate = employeeData.ModifiedDate != null ? employeeData.ModifiedDate.ToString() : string.Empty;
                employeeVM.CreatedBy = employeeData.CreatedBy != null ? Convert.ToInt32(employeeData.CreatedBy) : 0;
                employeeVM.ModifiedBy = employeeData.ModifiedBy != null ? Convert.ToInt32(employeeData.ModifiedBy) : 0;

                return employeeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsEmployeeAvailable(int? empID, string empName)
        {
            try
            {
                if (empID != null && unitOfWork.TblEmployeeRepository.Get().Any(x => x.EmpName.ToLower() == empName.ToLower() && x.EmpID != empID))
                {
                    return true;
                }
                else if (empID == null && unitOfWork.TblEmployeeRepository.Get().Any(x => x.EmpName.ToLower() == empName.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
