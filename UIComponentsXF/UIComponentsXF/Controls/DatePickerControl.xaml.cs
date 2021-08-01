using System;
using System.Collections.Generic;
using System.Linq;
using UIComponentsXF.Pages;
using UIComponentsXF.ViewComponents;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;

namespace UIComponentsXF.Controls
{
    public partial class DatePickerControl : Button //should be an image button maybe in the future...
    {


        public static readonly BindableProperty DateProperty = BindableProperty.Create(
           propertyName: "Date",
           returnType: typeof(DateTime),
           declaringType: typeof(DatePickerControl),
           defaultValue: DateTime.Today,
           propertyChanged: DatePropertyChanged);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DatePickerControl)bindable;
            control.Date = (DateTime)newValue;
        }


        public static readonly BindableProperty DisplayTextProperty = BindableProperty.Create(
            propertyName: "DisplayText",
            returnType: typeof(string),
            declaringType: typeof(DatePickerControl),
            defaultValue: string.Empty,
            propertyChanged: DisplayTextPropertyChanged);


        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        private static void DisplayTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DatePickerControl)bindable;
            control.Text = newValue.ToString();
        }

        public event EventHandler<DateTime> DateChoosenEvent;

        public DatePickerControl()
        {

            InitializeComponent();
            DateChoosenEvent += DatePickerControl_DateChoosenEvent;
            DisplayText = Date.ToString("dd/MM/yyyy");
        }

        private void DatePickerControl_DateChoosenEvent(object sender, DateTime e)
        {
            Date = e;
            DisplayText = Date.ToString("dd/MM/yyyy");
        }

        void OnDatePickerClicked(System.Object sender, System.EventArgs e)
        {
            BaseNavigationPage page = (BaseNavigationPage)Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            page.ToogleModalVisibility(true, new DatePickerViewComponent(Date, DateChoosenEvent, null, null));
        }
    }
}
