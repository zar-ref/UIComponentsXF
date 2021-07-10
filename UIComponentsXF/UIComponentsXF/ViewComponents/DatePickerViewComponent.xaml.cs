using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace UIComponentsXF.ViewComponents
{
    public partial class DatePickerViewComponent : Grid
    {

        public DateTime CurrentDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public Dictionary<DayOfWeek, List<int>> MonthDaysPerWeekDay { get; set; }
        public Dictionary<DayOfWeek, int> PreviousMonthLastWeekDaysPerWeekDay { get; set; }
        public Dictionary<DayOfWeek, int> NextMonthFirstWeekDaysPerWeekDay { get; set; }
        public DatePickerViewComponent(DateTime currentDate, DateTime? minDate, DateTime? date)
        {
            InitializeComponent();
            CurrentDate = currentDate;
            Year = currentDate.Year;
            Month = currentDate.Month;
            Day = currentDate.Day;
            SetMonthDaysPerWeekDay(CurrentDate);



            //yearDate.Text = currentDate.ToString("dd/MM/yyyy");
        }

        public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
        public static IEnumerable<DateTime> LastWeekFromPreviousMonth(int year, int previousMonth)
        {
            int days = DateTime.DaysInMonth(year, previousMonth);
            for (int day = days, previousMonthWeekDay = 0; previousMonthWeekDay < 6; day--, previousMonthWeekDay++)
            {
                yield return new DateTime(year, previousMonth, day);
            }
        }
        public static IEnumerable<DateTime> FirstWeekFromNextMonth(int year, int nextMonth)
        {
            for (int day = 1, nextMonthWeekDay = 0; nextMonthWeekDay < 6; day++, nextMonthWeekDay++)
            {
                yield return new DateTime(year, nextMonth, day);
            }
        }

        private void SetMonthDaysPerWeekDay(DateTime date)
        {
            var datesInMonth = AllDatesInMonth(date.Year, date.Month);
            MonthDaysPerWeekDay.Clear();
            MonthDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).Select(wd => wd.Day).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).Select(wd => wd.Day).ToList());
       

            var lastMonth = date.AddMonths(-1);
            var nextMonth = date.AddMonths(1);

            SetPreviousMonthLastWeekDaysPerWeekDay(lastMonth);
            SetNextMonthFirstWeekDaysPerWeekDay(nextMonth);

        }

        private void SetPreviousMonthLastWeekDaysPerWeekDay(DateTime date)
        {
            var datesInMonth = LastWeekFromPreviousMonth(date.Year, date.Month);
            PreviousMonthLastWeekDaysPerWeekDay.Clear();
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).Select(wd => wd.Day).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).Select(wd => wd.Day).FirstOrDefault());


        }

        private void SetNextMonthFirstWeekDaysPerWeekDay(DateTime date)
        {
            var datesInMonth = FirstWeekFromNextMonth(date.Year, date.Month);
            NextMonthFirstWeekDaysPerWeekDay.Clear();
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).Select(wd => wd.Day).FirstOrDefault()); ;
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).Select(wd => wd.Day).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).Select(wd => wd.Day).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).Select(wd => wd.Day).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).Select(wd => wd.Day).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).Select(wd => wd.Day).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).Select(wd => wd.Day).FirstOrDefault());


        }

        public StackLayout DaysOfMonthStack()
        {

            return new StackLayout();



        }
    }
}
