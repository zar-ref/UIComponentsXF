using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIComponentsXF.DataStores;
using UIComponentsXF.Pages;
using UIComponentsXF.Resources;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;

namespace UIComponentsXF
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel _viewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            UIComponentsXF.Resources.Resources.Culture = new CultureInfo("pt");
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;


        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UIComponentsPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (LanguageDataStore.Language != _viewModel.CurrentLanguage)
                _viewModel.SwitchTranslations();
            else
                _viewModel.OnPropertyChanged("Translations");

            var count = _viewModel.Translations.Keys.Count();
        }
    }
}
