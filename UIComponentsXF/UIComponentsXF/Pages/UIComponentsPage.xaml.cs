using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIComponentsXF.DataStores;
using UIComponentsXF.Util;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponentsXF.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UIComponentsPage : ContentPage
    {
        public UIComponentsPageViewModel _viewModel { get; set; }
        public UIComponentsPage()
        {
            InitializeComponent();
            _viewModel = new UIComponentsPageViewModel();
            BindingContext = _viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnPropertyChanged("Translations");
            var count = _viewModel.Translations.Keys.Count();
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            LanguageDataStore.ChangeLanguage(Enumerators.Languages.English);
            _viewModel.SwitchTranslations();
          
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.ClearTranslations();
        }
    }
}