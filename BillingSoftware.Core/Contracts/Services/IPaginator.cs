using System.Windows;
using System.Windows.Documents;

namespace BillingSoftware.Core.Contracts.Services
{
    public interface IPaginator
    {
        List<UIElement> PaginateAsync(Func<UIElement> pageFactory, Size pageSize, Thickness pageMargins, CancellationToken cancellationToken);

        FixedDocument GetFixedDocumentFromPages(List<UIElement> uiElements, Size pageSize);

    }
}