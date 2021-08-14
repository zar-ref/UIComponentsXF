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


        private DateTime date3 { get; set; }
        public DateTime Date3
        {
            get

            {
                return date3;
            }
            set
            {
                date3 = value;
                OnPropertyChanged("Date3");
            }
        }

        public UIComponentsPageViewModel()
        {
            Translations = new Dictionary<string, string>();
            Date1 = new DateTime(2021, 10, 5);
            Date2 = new DateTime(2021, 9, 28);
            Date3 = new DateTime(2021, 10, 20);
        }

        public void ApplyBindingsAfterInit()
        {
      
        }

    }
}
