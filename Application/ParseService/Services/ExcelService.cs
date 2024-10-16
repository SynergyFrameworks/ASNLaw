using Microsoft.AspNetCore.Http;
using ParseService.Contracts;
using Domain.Parse.Model;
using Excel;
using Excel.EPPlus;
using Infrastructure.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParseService.Services
{
    public class ExcelService : IExcelService
    {
        private IExcelMongoService _mongoService;
        public ExcelService(IExcelMongoService mongoExcelService)
        {
            _mongoService = mongoExcelService;
        }

        public async Task<ExcelResult> UploadExcel(IFormFile file)
        {
            var parsedExcel = this.ParseExcel(file);
            await _mongoService.CreateExcelDocument(parsedExcel);
            return parsedExcel;
        }

        private ExcelResult ParseExcel(IFormFile file)
        {
            ExcelResult result = new ExcelResult();
            result.DateTimeUTC = DateTime.UtcNow;
            result.ExcelFileResult = new ExcelFileResult
            {
                FileName = file.FileName,
            };

            using (var fileStream = file.OpenReadStream())
            {
                var manager = CreateManager();
                var workbook = manager.OpenWorkbook(fileStream);
                var parsedWorkSheets = new List<ExcelWorkSheet>();
                var mappingProvider = new ExcelMappingProvider();
                var mapping = mappingProvider.GenerateMapping(workbook, file.FileName);
                var sheetNames = mapping.PropertyMappings.Select(pm => pm.SheetName).Distinct();

                Parallel.ForEach(sheetNames, sheetName =>
                {
                    var sheetMapping = mapping.PropertyMappings.Where(pm => pm.SheetName == $"'{sheetName}'" || pm.SheetName == sheetName)
                                                               .OrderBy(e => e.RowNumber).ThenBy(e => e.ColumnNumber);

                    parsedWorkSheets.Add(new ExcelWorkSheet
                    {
                        SheetName = sheetName,
                        Headers = sheetMapping.Where(pm => pm.RowNumber == 1).Select(pm => pm.PropertyName).ToList(),
                        Contents = sheetMapping.Where(pm => pm.RowNumber > 1)
                                               .Select(pm => new ExcelContent
                                               {
                                                   ColumnIndex = pm.ColumnNumber,
                                                   RowIndex = pm.RowNumber,
                                                   Label = pm.Column,
                                                   TypeOfValue = typeof(string).Name,
                                                   Value = pm.PropertyName
                                               }).ToList()
                    });
                });
                result.ExcelFileResult.WorkSheets = parsedWorkSheets;
            }

            return result;
        }

        private ExcelManager CreateManager()
        {
            ExcelManager manager = new ExcelManager();
            manager.ExcelAdapter = new EPPlusAdapter();
            return manager;
        }


    }
}
