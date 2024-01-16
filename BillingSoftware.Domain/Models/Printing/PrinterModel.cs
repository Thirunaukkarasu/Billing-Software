using BillingSoftware.Domain.Enums;
using System.Collections.Generic;
using System.Printing;

namespace BillingSoftware.Domain.Models.Printing
{
    public class PrinterModel
    {
        public PrinterModel(string fullName, PrinterType printerType, IReadOnlyCollection<PageMediaSize> pageSizeCapabilities, IReadOnlyCollection<PageOrientation> pageOrientationCapabilities)
        {
            FullName = fullName;
            PrinterType = printerType;
            PageSizeCapabilities = pageSizeCapabilities;
            PageOrientationCapabilities = pageOrientationCapabilities;
        }

        public string FullName { get; }

        public PrinterType PrinterType { get; }

        public IReadOnlyCollection<PageMediaSize> PageSizeCapabilities { get; }

        public IReadOnlyCollection<PageOrientation> PageOrientationCapabilities { get; }
    }
}