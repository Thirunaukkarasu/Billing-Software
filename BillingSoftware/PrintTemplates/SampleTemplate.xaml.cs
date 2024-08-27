using Billing.Domain.Models;
using BillingSoftware.Models;
using BillingSoftware.ViewModels;
using System.Windows.Controls;

namespace BillingSoftware.PrintTemplates
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class SampleTemplate : UserControl
    { 
        public SampleTemplate(List<PurchasedProductsDto> products)
        {
            InitializeComponent();
             
            int i = 0;
            foreach (var product in products)
            {
                i++;
                ItemsControl2.Items.Add(new ReportDataModel(i, product.ProductName, product.Description, product.HSNCode, 1,2,3,4));
            }
        }
    }
}
