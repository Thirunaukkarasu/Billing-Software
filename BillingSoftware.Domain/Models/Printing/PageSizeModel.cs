using System.Printing;

namespace BillingSoftware.Domain.Models.Printing
{
    public class PageSizeModel
    {
        public PageSizeModel(PageMediaSize pageMediaSizeName)
        {
            PageMediaSize = pageMediaSizeName;
        }

        public PageMediaSize PageMediaSize { get; }

        public string PageSizeName => PageMediaSize.PageMediaSizeName.ToString();
    }
}