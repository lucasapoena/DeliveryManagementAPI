using MediatR;
using OfficeOpenXml;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommandHandler : IRequestHandler<InsertImportDeliveryCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(InsertImportDeliveryCommand command, CancellationToken cancellationToken)
        {
            var file = command.File;
            if (file == null || file.Length == 0)
                return await Result<Guid>.FailAsync("File Not Selected");

            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
                return await Result<Guid>.FailAsync("File Not Selected");            

            var rootFolder = @"C:\Files";
            var fileName = file.FileName;
            var filePath = Path.Combine(rootFolder, fileName);
            var fileLocation = new FileInfo(filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            if (file.Length <= 0)
                return await Result<Guid>.FailAsync("File Not Found");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileLocation))
            {
                var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                int totalRows = workSheet.Dimension.Rows;

                var DataList = new List<ExcelResourceDto>();

                for (int i = 2; i <= totalRows; i++)
                {
                    DataList.Add(new ExcelResourceDto
                    {
                        DataEntrega = workSheet.Cells[i, 1].Value.ToString(),
                        NomeProduto = workSheet.Cells[i, 2].Value.ToString(),
                        Quantidade = workSheet.Cells[i, 3].Value.ToString(),
                        ValorUnitario = workSheet.Cells[i, 4].Value.ToString()
                    });
                }
            }




            return await Result<Guid>.SuccessAsync("teste");
        }

        public class ExcelResourceDto
        {
            [Column("1")]
            [Required]
            public string DataEntrega { get; set; }

            [Column("2")]
            [Required]
            public string NomeProduto { get; set; }

            [Column("3")]
            [Required]
            public string Quantidade { get; set; }

            [Column("4")]
            [Required]
            public string ValorUnitario { get; set; }
        }
    }
}
