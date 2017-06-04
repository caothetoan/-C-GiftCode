using System.Data;

namespace Libs
{
    public class Paper
    {
        public static DataTable MakeDataPaper(int totalPages, int currPages, int recordPerPages)
        {
            var dtRet = new DataTable("DataPaper");
            dtRet.Columns.Add("ID", typeof (string));
            dtRet.Columns.Add("CssClass", typeof (string));
            dtRet.Columns.Add("Text", typeof (string));
            dtRet.Columns.Add("Page", typeof (int));
            dtRet.Columns.Add("Type", typeof (int));
            if (totalPages > 1)
            {
                int ranges = currPages/recordPerPages;
                if (currPages%recordPerPages > 0)
                    ranges += 1;
                int start = recordPerPages*(ranges - 1) + 1;
                int end = start + recordPerPages;
                if (end > totalPages)
                    end = totalPages + 1;
                DataRow dr = null;
                if (currPages > 1)
                {
                    //Trang dau tien
                    dr = dtRet.NewRow();
                    dr["ID"] = "first";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "First";
                    dr["Page"] = "1";
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                    //Quay lai
                    dr = dtRet.NewRow();
                    dr["ID"] = "back";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "Previous";
                    dr["Page"] = (currPages - 1).ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                }
                //
                for (int i = start; i < end; i++)
                {
                    if (i == currPages)
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager-current";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                }
                if (currPages < totalPages)
                {
                    //Trang tiep theo
                    dr = dtRet.NewRow();
                    dr["ID"] = "next";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "Next";
                    dr["Page"] = (currPages + 1).ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                    //Trang cuoi cung
                    dr = dtRet.NewRow();
                    dr["ID"] = "last";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "Last";
                    dr["Page"] = totalPages.ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                }
                dtRet.AcceptChanges();
            }
            return dtRet;
        }
    }
}