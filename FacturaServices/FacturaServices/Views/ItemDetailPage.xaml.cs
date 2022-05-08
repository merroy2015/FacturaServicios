using FacturaServices.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FacturaServices.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}