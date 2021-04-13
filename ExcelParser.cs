using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Windows;

namespace Lab2
{
    static class ExcelParser
    {
        public static List<Threat> ReadExcel(string fileName)
        {
            try
            {
                var file = new FileInfo(fileName);
                using (var package = new ExcelPackage(file))
                {
                    using (ExcelWorksheet workSheet = package.Workbook.Worksheets[1])
                    {
                        List<Threat> threats = new List<Threat>();
                        var list = workSheet.Cells[workSheet.Dimension.Start.Row + 2, 1, workSheet.Dimension.End.Row, 8].ToList();

                        for (int i = 0; i < list.Count; i += 8)
                        {
                            string str = list[i].Text;
                            Threat threat = new Threat()
                            { 
                                Id = Convert.ToInt32(list[i].Text),
                                Name = list[i + 1].Text,
                                Description = list[i + 2].Text,
                                Source = list[i + 3].Text,
                                Target = list[i + 4].Text,
                                BreachOfConf = (list[i + 5].Text == "0") ? false : true,
                                BreachOfIntegrity = (list[i + 6].Text == "0") ? false : true,
                                BreachOfAccess = (list[i + 7].Text == "0") ? false : true
                            };
                            threats.Add(threat);
                        }
                        return threats;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}

