using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Paging
    {
        public int PageIndex { get; set; }

        DataTable PagedList = new DataTable();

        public DataTable Next(IList<Threat> ListToPage, int RecordsPerPage)
        {
            PageIndex++;
            if (PageIndex >= ListToPage.Count / RecordsPerPage)
            {
                PageIndex = ListToPage.Count / RecordsPerPage;
            }
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }

        public DataTable Previous(IList<Threat> ListToPage, int RecordsPerPage)
        {
            PageIndex--;
            if (PageIndex <= 0)
            {
                PageIndex = 0;
            }
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }

        public DataTable First(IList<Threat> ListToPage, int RecordsPerPage)
        {
            PageIndex = 0;
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }

		public DataTable SetPaging(IList<Threat> ListToPage, int RecordsPerPage)
        {
            int PageGroup = PageIndex * RecordsPerPage;

            IList<Threat> PagedList = new List<Threat>();

            PagedList = ListToPage.Skip(PageGroup).Take(RecordsPerPage).ToList();

            DataTable FinalPaging = PagedTable(PagedList);

            return FinalPaging;
        }

		private DataTable PagedTable<T>(IList<T> SourceList)
        {
            Type columnType = typeof(T);
            DataTable TableToReturn = new DataTable();

            foreach (var Column in columnType.GetProperties())
            {
                TableToReturn.Columns.Add(Column.Name, Column.PropertyType);
            }

            foreach (object item in SourceList)
            {
                DataRow ReturnTableRow = TableToReturn.NewRow();
                foreach (var Column in columnType.GetProperties())
                {
                    ReturnTableRow[Column.Name] = Column.GetValue(item);
                }
                TableToReturn.Rows.Add(ReturnTableRow);
            }
            return TableToReturn;
        }
    }
}
