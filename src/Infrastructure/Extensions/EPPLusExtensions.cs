using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

/*
 * Extesion - EPPLusExtensions
 * Refer: https://stackoverflow.com/questions/33436525/how-to-parse-excel-rows-back-to-types-using-epplus
 * 
 */

namespace Infrastructure.Extensions
{
    public static class EPPLusExtensions
    {
        public async static Task<IEnumerable<T>> ConvertSheetToObjectsAsync<T>(this ExcelWorksheet worksheet) where T : new()
        {

            Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(Application.Attributes.ExcelColumn);

            var columns = typeof(T)
                    .GetProperties()
                    .Where(x => x.CustomAttributes.Any(columnOnly))
                    .Select(p => new
                    {
                        Property = p,
                        Column = p.GetCustomAttributes<Application.Attributes.ExcelColumn>().First().ColumnIndex //safe because if where above
                    }).ToList();


            var rows = worksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);


            //Create the collection container
            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var tnew = new T();
                    columns.ForEach(col =>
                    {
                    //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                    var val = worksheet.Cells[row, col.Column];
                    //If it is numeric it is a double since that is how excel stores all numbers
                    if (val.Value == null)
                        {
                            col.Property.SetValue(tnew, null);
                            return;
                        }
                        if (col.Property.PropertyType == typeof(int))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(double))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(DateTime))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime>());
                            return;
                        }
                    //Its a string
                    col.Property.SetValue(tnew, val.GetValue<string>());
                    });

                    return tnew;
                });

            return await Task.FromResult(collection); //Send it back            
        }
    }
}
