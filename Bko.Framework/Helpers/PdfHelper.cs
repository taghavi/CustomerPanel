using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Framework.Helpers
{
    public class PdfHelper
    {
        private static string DefaultUploadPath = "~/App_Data/";

        public static string CreatePDFFile(string VirtualUploadPath, string pdfBody)
        {
            string strPathFileName = "";

            if (string.IsNullOrWhiteSpace( pdfBody))
            {
                throw new ArgumentNullException("pdf body to create is empty");
            }

            using (var pdfDoc = new Document(PageSize.A4))
            {
                if (!string.IsNullOrWhiteSpace(VirtualUploadPath))
                {
                    if (VirtualUploadPath.IndexOf("~/") == -1)
                        VirtualUploadPath = "~/" + VirtualUploadPath;

                    if (VirtualUploadPath.Substring(VirtualUploadPath.Length - 1, 1) != "/")
                    {
                        VirtualUploadPath = VirtualUploadPath + "/";
                    }

                    DefaultUploadPath = VirtualUploadPath;

                }
                try
                {
                    //Create Directory if Not Exists Need write Permission
                    if (!Directory.Exists(Utility.MapPath(DefaultUploadPath)))
                    {
                        Directory.CreateDirectory(Utility.MapPath(DefaultUploadPath));
                    }
                }
                catch (Exception ex)
                {
                    // To Do Log Error
                    throw new Exception("Access is denied.");
                }




                strPathFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                string strPhysicalPath = Utility.MapPath(DefaultUploadPath + strPathFileName);

                try
                {
                    PdfWriter.GetInstance(pdfDoc, new FileStream(strPhysicalPath, FileMode.Create));

                    pdfDoc.Open();

                }
                catch (Exception ex)
                {
                    // To Do Log Error
                    throw new Exception("Access iss denied.");
                }


                FontFactory.Register("c:\\windows\\fonts\\bnazanin.ttf");
                FontFactory.Register("c:\\windows\\fonts\\tahoma.ttf");
                StyleSheet styles = new StyleSheet();

                styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.FONTFAMILY, "bnazanin,tahoma");
                styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.ENCODING, "Identity-H");

                styles.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "bnazanin,tahoma");
                styles.LoadTagStyle(HtmlTags.DIV, HtmlTags.ENCODING, "Identity-H");

                styles.LoadTagStyle(HtmlTags.SPAN, HtmlTags.FONTFAMILY, "bnazanin,tahoma");
                styles.LoadTagStyle(HtmlTags.SPAN, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);

                styles.LoadTagStyle(HtmlTags.P, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);

                styles.LoadTagStyle(HtmlTags.UL, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);
                styles.LoadTagStyle(HtmlTags.LI, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);

                styles.LoadTagStyle(HtmlTags.TABLE, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);
                styles.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTFAMILY, "bnazanin,tahoma");
                styles.LoadTagStyle(HtmlTags.TD, HtmlTags.ALIGN, HtmlTags.ALIGN_LEFT);
                styles.LoadTagStyle(HtmlTags.TD, HtmlTags.FONTFAMILY, "bnazanin,tahoma");
                styles.LoadTagStyle(HtmlTags.TD, HtmlTags.ENCODING, "Identity-H");



                //styles.LoadTagStyle(HtmlTags.IMG, HtmlTags.ALIGN, HtmlTags.ALIGN_CENTER);
                var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(pdfBody), styles);
                PdfPCell pdfCell = new PdfPCell { Border = 0 };
                pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                foreach (var htmlElement in parsedHtmlElements)
                {

                    if (htmlElement is PdfPTable)
                    {

                        var table = (PdfPTable)htmlElement;
                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        foreach (var row in table.Rows)
                        {
                            foreach (var cell in row.GetCells())
                            {
                                cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                            }
                        }
                    }
                    pdfCell.AddElement(htmlElement);
                }
                var table1 = new PdfPTable(1);
                table1.WidthPercentage = 110;
                table1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table1.AddCell(pdfCell);
                pdfDoc.Add(table1);
                pdfDoc.Close();
            }

            return DefaultUploadPath + strPathFileName;

        }
    }
}
