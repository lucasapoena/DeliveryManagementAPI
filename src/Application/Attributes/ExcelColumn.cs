using System;

namespace Application.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExcelColumn : System.Attribute
    {
        public int ColumnIndex { get; set; }


        public ExcelColumn(int column)
        {
            ColumnIndex = column;
        }
    }
}
