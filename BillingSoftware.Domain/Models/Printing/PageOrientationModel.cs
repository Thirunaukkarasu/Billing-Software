using System.Printing;

namespace BillingSoftware.Domain.Models.Printing
{
    public class PageOrientationModel
    {
        public PageOrientationModel(PageOrientation pageOrientation)
        {
            PageOrientation = pageOrientation;
        }

        public PageOrientation PageOrientation { get; }

        public string PageOrientationName => PageOrientation.ToString();
    }
}