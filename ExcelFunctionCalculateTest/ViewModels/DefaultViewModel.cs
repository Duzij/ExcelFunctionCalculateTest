using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ExcelFunctionCalculateTest.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public int Result { get; set; }
        public DefaultViewModel(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public void ReadAndReturnValueFromExcel()
        {
            var wb = new ClosedXML.Excel.XLWorkbook(Path.Combine(webHostEnvironment.WebRootPath, "Test.xlsx"));
            wb.TryGetWorksheet("List1", out var worksheet);

            var random = new Random();

            for (int i = 1; i < 101; i++)
            {
                Result += Convert.ToInt32(worksheet.Cell($"B{i}").Value);
                var randomValue = random.Next(100);
                Result += randomValue;
                worksheet.Cell($"D{i}").Value = randomValue;
            }

            var result = worksheet.Cell(1, 6);
            wb.CalculateMode = ClosedXML.Excel.XLCalculateMode.Auto;
            Result = Convert.ToInt32(result.Value);
        }
    }


}
