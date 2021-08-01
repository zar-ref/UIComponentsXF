using System;
using System.Collections.Generic;
using System.Text;

namespace UIComponentsXF.ViewModels
{
    public class UIComponentsPageViewModel : BaseViewModel
    {
        private DateTime date1 { get; set; }
        public DateTime Date1
        {
            get

            {
                return date1;
            }
            set
            {
                date1 = value;
                OnPropertyChanged("Date1");
            }
        }
        private DateTime date2 { get; set; }
        public DateTime Date2
        {
            get

            {
                return date2;
            }
            set
            {
                date2 = value;
                OnPropertyChanged("Date2");
            }
        }

        public UIComponentsPageViewModel()
        {
            Translations = new Dictionary<string, string>();
            Date1 = DateTime.Today;
            Date2 = DateTime.Today;
        }

    }
}
