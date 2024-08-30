using Billing.Domain.Models;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Domain.Extentions;
using BillingSoftware.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BillingSoftware.ViewModels;

public class PurchaseBoardViewModel : ObservableObject
{

    private readonly ICommonService _commonService;
    

    private ObservableCollection<InvoiceDto> purchaseInvoiceSource;
    public ObservableCollection<InvoiceDto> PurchaseInvoiceSource
    {
        get
        {
            return purchaseInvoiceSource;
        }
        set
        {
            if (value != null)
            {
                SetProperty(ref purchaseInvoiceSource, value);
            }
            OnPropertyChanged(nameof(purchaseInvoiceSource));
        }
    }

    public PurchaseBoardViewModel(ICommonService commonService)
    {
        _commonService = commonService;
        PurchaseInvoiceSource = _commonService.GetAllInvoicesDetails().ToObservableCollection();
    }


}
