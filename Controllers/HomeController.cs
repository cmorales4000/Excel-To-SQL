using System;
using Microsoft.AspNetCore.Mvc;
using ExcelToSQL.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ExcelToSQL.Controllers
{
    
    public class HomeController : Controller
    {
        private List<DataTable> _lstDt;

        private readonly IHostingEnvironment _hostingEnvironment;
        Validacion _encript = new Validacion();
        

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }
        public ActionResult File()
        {
            FileUploadViewModel model = new FileUploadViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult File(FileUploadViewModel model)
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + model.XlsFile.FileName;
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            using (var stream = new MemoryStream())
            {
                model.XlsFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    package.SaveAs(file);
                }
            }

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {

                    //return or alert message here
                }
                else
                {

                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        //consulta para llenar tprompecabezasconfig y local THIS IS EVERYTHING
                        _lstDt = Juegos.checkJuegos("M5",
                            (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                            (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(),
                            (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(),
                            (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim(),
                            (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim(),
                            (worksheet.Cells[row, 8].Value ?? string.Empty).ToString().Trim(),
                            "", "", "", "", "", "", (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim());


                        string key = _encript.EncryptarText("empId=" +(worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim() + "|camId=" + (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim() + "|locId=" + (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim());
                        
                        string data = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim() + "/check/" + key;

                        string ruta = "emp" + (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim() + "cam" + (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim();
                        
                        string rootFolder2 = _hostingEnvironment.WebRootPath + "\\" + ruta;
                        string fileName2 = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim()+".png";
                        FileInfo file2 = new FileInfo(Path.Combine(rootFolder2, fileName2));
                        string path = Path.Combine(this._hostingEnvironment.WebRootPath, ruta);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                            QRCode qRCode = new QRCode(qRCodeData);
                            using (Bitmap bitmap = qRCode.GetGraphic(20))
                            {
                                using (Graphics g = Graphics.FromImage(bitmap))
                                {
                                    Font drawFont = new Font("Arial", 50);
                                    StringFormat stringFormat = new StringFormat();
                                    stringFormat.Alignment = StringAlignment.Center;
                                    //stringFormat.LineAlignment = StringAlignment.Center;
                                    g.DrawString((worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(), drawFont, Brushes.Black, 550, 980, stringFormat);    // Characters look like blurred bold characters.
                                }
                                
                                bitmap.Save(memoryStream, ImageFormat.Png);
                                //ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                                bitmap.Save(file2.ToString(), ImageFormat.Png);
                            }
                        }

                       

                        //string rootFolder2 = _hostingEnvironment.WebRootPath;
                        //string fileName2 = (worksheet.Cells[row, 6].Value).ToString().Trim();
                        //FileInfo file2 = new FileInfo(Path.Combine(rootFolder2, fileName2));
                        //using (var stream2 = new MemoryStream())
                        //{
                        //    //ViewBag.QRCode.CopyToAsync(stream2);
                        //    using (var package2 = Image.FromStream(stream2))
                        //    {
                        //        package2.Save(file2.ToString(), ImageFormat.Png);
                        //    }
                        //}
                        //using (var stream3 = new MemoryStream())
                        //{
                        //    await model.ImageFile.CopyToAsync(stream3);
                        //    using (var img = Image.FromStream(stream3))
                        //    {
                        //        img.SaveAs(file);
                        //        // TODO: ResizeImage(img, 100, 100);
                        //    }
                        //}

                        //string wwwPath = this._hostingEnvironment.WebRootPath;
                        //string contentPath = this._hostingEnvironment.ContentRootPath;

                        //string path = Path.Combine(this._hostingEnvironment.WebRootPath, "Uploads");
                        //if (!Directory.Exists(path))
                        //{
                        //    Directory.CreateDirectory(path);
                        //}

                        //List<string> uploadedFiles = new List<string>();

                        //string fileName2 = Path.GetFileName((worksheet.Cells[row, 6].Value).ToString().Trim());
                        //using (FileStream stream = new FileStream(Path.Combine(path, fileName2), FileMode.Create))
                        //{
                        //    ViewBag.QRCode.CopyTo(stream);
                        //    uploadedFiles.Add(fileName2);
                        //    //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName2);
                        //}


                        model.StaffInfoViewModel.StaffList.Add(new StaffInfoViewModel
                        {
                            url = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim(),
                            empId = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                            camId = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim(),
                            locId = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(),
                            locNom = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(),
                        });
                    }
                    
                }
            }

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Index(string inputData)
        //{
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
        //        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(inputData, QRCodeGenerator.ECCLevel.Q);
        //        QRCode qRCode = new QRCode(qRCodeData);
        //        using (Bitmap bitmap = qRCode.GetGraphic(20))
        //        {
        //            bitmap.Save(memoryStream, ImageFormat.Png);
        //            ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
        //        }
        //    }

        //    return View();
        //}

    }
}
