using System.Windows.Controls;

namespace BillingSoftware.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
