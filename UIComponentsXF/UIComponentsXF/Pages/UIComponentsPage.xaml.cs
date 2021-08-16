using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIComponentsXF.Controls;
using UIComponentsXF.Converters;
using UIComponentsXF.DataStores;
using UIComponentsXF.Util;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponentsXF.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UIComponentsPage : BaseNavigationPage
    {

        

        public UIComponentsPageViewModel _viewModel { get; set; }
        public UIComponentsPage()
        {
            InitializeComponent();
            _viewModel = new UIComponentsPageViewModel();
            BindingContext = _viewModel;
            _viewModel.ApplyBindingsAfterInit();
            Label lblToAdd = new Label();
            //lblToAdd.BindingContext = _viewModel;
            lblToAdd.SetBinding(Label.TextProperty, BaseViewModel.GetTranslationBindingFromResource("label-go-to-next-page"));
            firstStack.Children.Add(lblToAdd);
      
            //DatePickerControl control = new DatePickerControl();
            // control.SetBinding(DatePickerControl.DateProperty)   ;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnPropertyChanged("Translations");
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            LanguageDataStore.ChangeLanguage(Enumerators.Languages.English);
            _viewModel.SwitchTranslations();
          
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {

            DisplayAlert("hello" , "hello from the other side" , "back");
        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            _viewModel.SwitchColorTheme();
        }
    }
}