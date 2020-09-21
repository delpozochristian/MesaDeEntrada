using OfficeOpenXml;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mailroom.Helper
{
    public class ExcelHelper
    {

        public static ExcelPackage WithoutStyles<T>(List<T> ListaIngresos)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("INGRESOS");
            //var data = new[]{
            //    new{
            //        Filtro="Búsqueda realizada",
            //        NroSeguimiento= nroSeguimiento + "",
            //        Producto= tipoProducto+ "",
            //        Sector= sector+ "",
            //        Proveedor= proveedor+ "",
            //        Destinatario= destinatario+ "",
            //        Canalizacion= canalizacion+ "",
            //        Estado= estado+ "",
            //        FechaDesde= fechaDesde+ "",
            //        FechaHasta= fechaHasta+ "",
            //        Prefiltrado= prefiltrado+ "",
            //    },
            //};
            //workSheet.Cells[1,1].LoadFromCollection(data, true);
            workSheet.Cells[1, 1].LoadFromCollection<T>(ListaIngresos, true);
            return excel;
        }

        public static void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output, string title)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    Table table = new Table();
                    // TITLE
                    TableRow row = new TableRow();
                    TableHeaderCell hcell = new TableHeaderCell
                    {
                        Text = title,
                        //ForeColor = System.Drawing.Color.Blue
                    };
                    row.Cells.Add(hcell);
                    table.Rows.Add(row);

                    // TITLE
                    table.Rows.Add(new TableRow());

                    /// HEADERS
                    row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        hcell = new TableHeaderCell
                        {
                            Text = prop.Name
                        };
                        //hcell.ForeColor = System.Drawing.Color.White;
                        //hcell.BackColor = System.Drawing.Color.Blue;
                        row.Cells.Add(hcell);
                    }
                    table.Rows.Add(row);


                    

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell
                            {
                                Text = prop.Converter.ConvertToString(prop.GetValue(item))
                            };
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }

        }
    }
}