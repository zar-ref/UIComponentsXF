using System;
using System.Collections.Generic;
using System.Linq;
using UIComponentsXF.Models.Controls;
using UIComponentsXF.Pages;
using UIComponentsXF.Util;
using UIComponentsXF.ViewComponents;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;

namespace UIComponentsXF.Controls
{
    public partial class DatePickerControl : Button, ICustomControl //should be an image button maybe in the future...
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

        public int ControlHashCode { get; set; }

        private static void DisplayTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DatePickerControl)bindable;
            control.Text = newValue.ToString();
        }

        public event EventHandler<DateTimeControlIdentifier> DateChoosenEvent;

        public DatePickerControl()
        {

            InitializeComponent();
            ControlHashCode = HashCodeGenerator.GenerateCustomControlHashCode();
            DateChoosenEvent += DatePickerControl_DateChoosenEvent;
            DisplayText = Date.ToString("dd/MM/yyyy");
        }

        private void DatePickerControl_DateChoosenEvent(object sender, DateTimeControlIdentifier e)
        {
            if (e.HashIdentifier != ControlHashCode)
                return;
            Date = e.Date;
            DisplayText = Date.ToString("dd/MM/yyyy");
        }

        void OnDatePickerClicked(System.Object sender, System.EventArgs e)
        {
            BaseNavigationPage page = (BaseNavigationPage)Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            page.ToogleModalVisibility(true, new DatePickerViewComponent(Date, DateChoosenEvent,  ControlHashCode, null, null));
        }
    }
}
