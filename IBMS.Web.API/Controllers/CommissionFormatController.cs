using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Shared.ViewModel;

namespace IBMS.Web.API.Controllers
{
    public class CommissionFormatController : Controller
    {
        // GET: CommissionFormat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadExcel()
        {
            List<CommissionFormatVM> lstStudent = new List<CommissionFormatVM>();
            //if (ModelState.IsValid)
            //{

            //    string filePath = string.Empty;
            //    if (Request != null)
            //    {
            //        HttpPostedFileBase file = Request.Files["file"];
            //        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            //        {

            //            string fileName = file.FileName;
            //            string fileContentType = file.ContentType;
            //            string path = Server.MapPath("~/Uploads/");
            //            if (!Directory.Exists(path))
            //            {
            //                Directory.CreateDirectory(path);
            //            }
            //            filePath = path + Path.GetFileName(file.FileName);
            //            string extension = Path.GetExtension(file.FileName);
            //            file.SaveAs(filePath);
            //            Stream stream = file.InputStream;
            //            // We return the interface, so that
            //            IExcelDataReader reader = null;
            //            if (file.FileName.EndsWith(".xls"))
            //            {
            //                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            //            }
            //            else if (file.FileName.EndsWith(".xlsx"))
            //            {
            //                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //            }
            //            else
            //            {
            //                ModelState.AddModelError("File", "This file format is not supported");
            //                return RedirectToAction("Index");
            //            }
            //            reader.IsFirstRowAsColumnNames = true;
            //            DataSet result = reader.AsDataSet();
            //            reader.Close();
            //            //delete the file from physical path after reading 
            //            string filedetails = path + fileName;
            //            FileInfo fileinfo = new FileInfo(filedetails);
            //            if (fileinfo.Exists)
            //            {
            //                fileinfo.Delete();
            //            }
            //            DataTable dt = result.Tables[0];
            //            lstStudent = ConvertDataTable<Student>(dt);
            //            TempData["Excelstudent"] = lstStudent;
            //        }
            //    }

            //}
            // var files = Request.Files;

            return new JsonResult { Data = lstStudent, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}