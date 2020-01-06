using IBMS.Web.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Utilities
{
    public static class Utilities
    {
        public static string IsActive(this HtmlHelper html, string mainMenuName, string subMenuName, string subSubMenuName)
        {
            var routeData = html.ViewContext.RouteData;

            var routeController = (string)routeData.Values["controller"];
            var routeAction = (string)routeData.Values["action"];

            var mainMenu = string.Empty;
            var subMenu = string.Empty;
            var subSubMenu = string.Empty;

            if (routeController == "Dashboard" && routeAction == "Index")
            {
                mainMenu = "Dashboard";
            }
            else if (routeController == "Agent" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Partner";
                subSubMenu = "Agent";
            }
            else if (routeController == "Introducer" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Partner";
                subSubMenu = "Introducer";
            }
            else if (routeController == "PartnerMapping" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Partner";
                subSubMenu = "PartnerMapping";
            }
            else if (routeController == "BusinessUnit" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "BusinessUnit";
            }
            else if (routeController == "CommissionHeader" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "CommissionStructure";
                subSubMenu = "CommissionStructureHeader";
            }
            else if (routeController == "CommisionStructure" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "CommissionStructure";
                subSubMenu = "CommissionStructureLine";
            }
            else if (routeController == "RateCategory" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "CommissionStructure";
                subSubMenu = "RateCategory";
            }
            else if (routeController == "InsCompany" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Insurance";
                subSubMenu = "InsuranceCompany";
            }
            else if (routeController == "InsClass" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Insurance";
                subSubMenu = "InsClass";
            }
            else if (routeController == "InsuranceSub" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Insurance";
                subSubMenu = "InsSubClass";
            }
            else if (routeController == "CommonInsurance" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Insurance";
                subSubMenu = "CommonInsuranceScope";
            }
            else if (routeController == "Employee" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Company";
                subSubMenu = "Employee";
            }
            else if (routeController == "Designation" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Company";
                subSubMenu = "Designation";
            }
            else if (routeController == "DocCategory" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Document";
                subSubMenu = "DocumentCategory";
            }
            else if (routeController == "Document" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Document";
                subSubMenu = "Document";
            }
            else if (routeController == "RequreDocument" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Document";
                subSubMenu = "RequiredDocument";
            }

            else if (routeController == "Policy" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Policy";
                subSubMenu = "Policy";
            }
            else if (routeController == "PolicyCategory" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Policy";
                subSubMenu = "PolicyCategory";
            }
            else if (routeController == "TransactionType" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Policy";
                subSubMenu = "TransactionType";
            }

            else if (routeController == "Setting" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Setting";
                subSubMenu = "Configuration";
            }
            else if (routeController == "ClaimPaymentType" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "ClaimPaymentType";
            }
            else if (routeController == "InternalPolicyNumberSetup" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "InternalPolicyNumberSetup";
            }
            else if (routeController == "ChargeType" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "ChargeType";
            }
            //else if (routeController == "ClaimPayment" && routeAction == "Index")
            //{
            //    mainMenu = "Master";
            //    subMenu = "ClaimPayment";
            //}
            else if (routeController == "ClaimRejectReason" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "ClaimRejectionReason";
            }
            else if (routeController == "ClaimReopenReason" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "ClaimReOpenReason";
            }
            else if (routeController == "CauseOfLoss" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "CauseOfLoss";
            }
            else if (routeController == "LoadingExcessType" && routeAction == "Index")
            {
                mainMenu = "Master";
                subMenu = "Claim";
                subSubMenu = "LoadingExcessType";
            }
            else if (routeController == "Transaction" && routeAction == "ManageClientRequest")
            {
                mainMenu = "Transaction";
                subMenu = "Customer";
                subSubMenu = "ClientRequest";
            }
            else if (routeController == "Transaction" && routeAction == "ManageQuotation")
            {
                mainMenu = "Transaction";
                subMenu = "Quotation";
                subSubMenu = "QuotationRequest";
            }
            else if (routeController == "Policy" && routeAction == "PolicyInfRecording")
            {
                mainMenu = "Transaction";
                subMenu = "Policy";
                subSubMenu = "PolicyInfRecording";
            }
            else if (routeController == "Policy" && routeAction == "PolicyRenewal")
            {
                mainMenu = "Transaction";
                subMenu = "Policy";
                subSubMenu = "PolicyRenewal";
            }
            else if (routeController == "ClaimPayment" && routeAction == "ClaimRecording")
            {
                mainMenu = "Transaction";
                subMenu = "Claim";
                subSubMenu = "ClaimRecording";
            }
            else if (routeController == "Payment" && routeAction == "PerformaInvoice")
            {
                mainMenu = "Transaction";
                subMenu = "Payment";
                subSubMenu = "PerformaInvoice";
            }
            else if (routeController == "Transaction" && routeAction == "ManagePayment")
            {
                mainMenu = "Transaction";
                subMenu = "Payment";
                subSubMenu = "ManagePayment";
            }
            else if (routeController == "Enquiries" && routeAction == "AgentList")
            {
                mainMenu = "Enquiries";
                subMenu = "AgentList";
            }
            else if (routeController == "Enquiries" && routeAction == "InsCompanyList")
            {
                mainMenu = "Enquiries";
                subMenu = "InsCompanyList";
            }
            else if (routeController == "Enquiries" && routeAction == "INSClasses")
            {
                mainMenu = "Enquiries";
                subMenu = "INSClasses";
            }
            else if (routeController == "Enquiries" && routeAction == "INSSubClasses")
            {
                mainMenu = "Enquiries";
                subMenu = "INSSubClasses";
            }
            else if (routeController == "Enquiries" && routeAction == "CommissionPercentages")
            {
                mainMenu = "Enquiries";
                subMenu = "CommissionPercentages";
            }
            else if (routeController == "Enquiries" && routeAction == "ClientList")
            {
                mainMenu = "Enquiries";
                subMenu = "ClientList";
            }
            else if (routeController == "Enquiries" && routeAction == "Employees")
            {
                mainMenu = "Enquiries";
                subMenu = "Employees";
            }
            else if (routeController == "User" && routeAction == "Profile")
            {
                mainMenu = "Administration";
                subMenu = "Profile";
            }
            else if (routeController == "RoleManagement" && routeAction == "RoleManagement")
            {
                mainMenu = "Administration";
                subMenu = "RoleFunction";
            }

            if (!string.IsNullOrEmpty(subMenuName) && !string.IsNullOrEmpty(subSubMenuName))
            {
                return (mainMenu == mainMenuName && subMenu == subMenuName && subSubMenu == subSubMenuName) ? "active" : "";
            }
            else if (!string.IsNullOrEmpty(subMenuName))
            {
                return (mainMenu == mainMenuName && subMenu == subMenuName) ? "active" : "";
            }
            else
            {
                return (mainMenu == mainMenuName) ? "active" : "";
            }
        }

        public static string IsMenuVisible(this HtmlHelper html, string menuType)
        {
            string visibleStatus = "hidden";

            if (menuType == "Master" && CurrentSession.LoggedUserPortal.MasterCount > 0 )
            {
                visibleStatus = string.Empty;
            }

            //if (menuType == "Master" && CurrentSession.LoggedUserPortal.MasterCount==null)
            //{
            //    visibleStatus = string.Empty;
            //}
            else if (menuType == "Transaction" && CurrentSession.LoggedUserPortal.TransactionCount > 0)
            {
                visibleStatus = string.Empty;
            }
            else if (menuType == "Enquiries" && CurrentSession.LoggedUserPortal.EnquiriesCount > 0)
            {
                visibleStatus = string.Empty;
            }
            else if (menuType == "Reports" && CurrentSession.LoggedUserPortal.ReportsCount > 0)
            {
                visibleStatus = string.Empty;
            }
            else if (menuType == "Administration" && CurrentSession.LoggedUserPortal.AdministrationCount > 0)
            {
                visibleStatus = string.Empty;
            }
            else
            {
                visibleStatus = "hidden";
            }

            return visibleStatus;
        }

        public static string IsSubMenuVisible(this HtmlHelper html, string subMenuItems)
        {
            string visibleStatus = "hidden";
            string[] menuItemArray = subMenuItems.Split(',');

            foreach (var menuItem in menuItemArray)
            {
                if (CurrentSession.LoggedUserPortal.AllowedFunctionList.Contains(Convert.ToInt32(menuItem)))
                {
                    visibleStatus = string.Empty;
                    break;
                }
            }

            return visibleStatus;
        }

        public static string IsSubSubMenuVisible(this HtmlHelper html, string subSubMenuNumber)
        {
            string visibleStatus = "hidden";

            if (CurrentSession.LoggedUserPortal.AllowedFunctionList.Contains(Convert.ToInt32(subSubMenuNumber)))
            {
                visibleStatus = string.Empty;
            }

            return visibleStatus;
        }
    }
}