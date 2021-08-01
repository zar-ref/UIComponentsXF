using System;
using System.Collections.Generic;
using System.Linq;
using UIComponentsXF.Controls;
using UIComponentsXF.DataStores;
using Xamarin.Forms;

namespace UIComponentsXF.ViewComponents
{
    public partial class DatePickerViewComponent : Grid
    {

        public DateTime ChosenDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime PreviousMonth { get; set; }
        public DateTime NextMonth { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public Dictionary<DayOfWeek, List<DateTime>> MonthDaysPerWeekDay { get; set; }
        public Dictionary<DayOfWeek, DateTime> PreviousMonthLastWeekDaysPerWeekDay { get; set; }
        public Dictionary<DayOfWeek, DateTime> NextMonthFirstWeekDaysPerWeekDay { get; set; }
        public static Dictionary<DayOfWeek, int> DayOfWeekIndexDictionary { get; set; } = new Dictionary<DayOfWeek, int>
        {
            { DayOfWeek.Sunday,     0 },
            { DayOfWeek.Monday,     1 },
            { DayOfWeek.Tuesday,    2 },
            { DayOfWeek.Wednesday,  3 },
            { DayOfWeek.Thursday,   4 },
            { DayOfWeek.Friday,     5 },
            { DayOfWeek.Saturday,   6 },

        };
        public DatePickerViewComponent(DateTime currentDate, EventHandler<DateTime> dateChoosenEvent, DateTime? minDate, DateTime? date)
        {
            InitializeComponent();
            CurrentDate = DateTime.Parse(currentDate.ToString("dd/MM/yyyy"), LanguageDataStore.CurrentAplicationCultureInfo);
            var firstDayCurrentMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            PreviousMonth = firstDayCurrentMonth.AddDays(-1); //last day of previous month

            NextMonth = CurrentDate.AddMonths(1).AddDays(-(CurrentDate.Day - 1)); //first day of next month

            SetMonthDaysPerWeekDay(CurrentDate);

            daysStack.Children.Add(ConstructDaysOfMonthStack());

            //yearDate.Text = currentDate.ToString("dd/MM/yyyy");
            ChosenDate = CurrentDate;
            MessagingCenter.Subscribe<DateButton, DateTime>(this, "DateChanged", (sender, arg) =>
            {
                ChosenDate = DateTime.Parse(arg.ToString("dd/MM/yyyy"), LanguageDataStore.CurrentAplicationCultureInfo);
                dateChoosenEvent?.Invoke(this, ChosenDate);


            });
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
            MonthDaysPerWeekDay = new Dictionary<DayOfWeek, List<DateTime>>();
            MonthDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).ToList());
            MonthDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).ToList());


            var lastMonth = date.AddMonths(-1);
            var nextMonth = date.AddMonths(1);

            SetPreviousMonthLastWeekDaysPerWeekDay(lastMonth);
            SetNextMonthFirstWeekDaysPerWeekDay(nextMonth);

        }

        private void SetPreviousMonthLastWeekDaysPerWeekDay(DateTime date)
        {
            var datesInMonth = LastWeekFromPreviousMonth(date.Year, date.Month);
            PreviousMonthLastWeekDaysPerWeekDay = new Dictionary<DayOfWeek, DateTime>();
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).FirstOrDefault());
            PreviousMonthLastWeekDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).FirstOrDefault());


        }

        private void SetNextMonthFirstWeekDaysPerWeekDay(DateTime date)
        {
            var datesInMonth = FirstWeekFromNextMonth(date.Year, date.Month);
            NextMonthFirstWeekDaysPerWeekDay = new Dictionary<DayOfWeek, DateTime>();
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Sunday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Sunday).FirstOrDefault()); ;
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Monday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Monday).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Tuesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Wednesday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Thursday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Friday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Friday).FirstOrDefault());
            NextMonthFirstWeekDaysPerWeekDay.Add(DayOfWeek.Saturday, datesInMonth.Where(d => d.DayOfWeek == DayOfWeek.Saturday).FirstOrDefault());


        }

        public StackLayout ConstructDaysOfMonthStack()
        {
            int gridCellDimensions = UtilViewBuilder.DeviceWidth / 14;


            StackLayout finalStack = new StackLayout() { Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

            var weekHeaders = MonthDaysPerWeekDay.Keys;
            StackLayout weekHeadersStack = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
            foreach (var weekHeader in weekHeaders)
            {
                var weekHeaderDay = LanguageDataStore.CurrentAplicationCultureInfo.DateTimeFormat.GetDayName(weekHeader);
                weekHeadersStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = weekHeaderDay.Substring(0, 1).ToUpper() }, gridCellDimensions));
                //weekHeadersStack.Children.Add(new Label() { Text = weekHeaderDay.Substring(0, 1).ToUpper(), HorizontalOptions = LayoutOptions.CenterAndExpand });
            }
            finalStack.Children.Add(weekHeadersStack);

            StackLayout firstWeekStack = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

            var previousMonthLastDayOfWeek = PreviousMonth.DayOfWeek;
            var previousMonthLastDayOfWeekIndex = DayOfWeekIndexDictionary[previousMonthLastDayOfWeek]; //3 -> quarta feira
            var dayOfWeek = PreviousMonthLastWeekDaysPerWeekDay.FirstOrDefault(d => d.Value.Day == previousMonthLastDayOfWeekIndex).Key; //quarta feira
            int lastWeekDayNumberOfPreviousMonth = PreviousMonthLastWeekDaysPerWeekDay[dayOfWeek].Day; //quarta feira dia 30 de junho
            int firstDayOfLastWeekDay = lastWeekDayNumberOfPreviousMonth - previousMonthLastDayOfWeekIndex; // 27 de junho
            int weekDay = 0;
            int firstWeekDayIndex = 0;
            bool isFirstWeekDayOfCurrentMonthASunday = MonthDaysPerWeekDay[DayOfWeek.Sunday].Any(d => d.Day == 1);
            if (!isFirstWeekDayOfCurrentMonthASunday)
            {
                for (weekDay = lastWeekDayNumberOfPreviousMonth; weekDay <= PreviousMonth.Day; weekDay++, firstWeekDayIndex++)
                {

                    // firstWeekStack.Children.Add(new Label() { Text = weekDay.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });

                    //  firstWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = weekDay.ToString() }, gridCellDimensions));
                    var date = PreviousMonthLastWeekDaysPerWeekDay.FirstOrDefault().Value;
                    DateTime buttonDate = new DateTime(date.Year, date.Month, weekDay);
                    firstWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, false), gridCellDimensions));
                }
            }





            var currentMonthFirstDayOfMonth = DayOfWeekIndexDictionary.FirstOrDefault(d => d.Value == firstWeekDayIndex).Key; // quinta feira
            int firstDayOfCurrentMonth = MonthDaysPerWeekDay[currentMonthFirstDayOfMonth].FirstOrDefault(d => d.Day == 1).Day;
            int currentMonthDayIndex = firstDayOfCurrentMonth;

            for (int j = 0; j < 7 - firstWeekDayIndex; j++, currentMonthDayIndex++)
            {
                //firstWeekStack.Children.Add(new Label() { Text = currentMonthDayIndex.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });

                var date = MonthDaysPerWeekDay.FirstOrDefault().Value.FirstOrDefault();
                DateTime buttonDate = new DateTime(date.Year, date.Month, currentMonthDayIndex);
                // firstWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = currentMonthDayIndex.ToString() }, gridCellDimensions));
                firstWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, true), gridCellDimensions));
            }

            finalStack.Children.Add(firstWeekStack);
            weekDay = 0;
            int lastDayOfCurrentMonth = NextMonth.AddDays(-1).Day;
            var nextMonthFirstDayOfWeek = NextMonth.DayOfWeek;
            var nextMonthFirstDayOfWeekIndex = DayOfWeekIndexDictionary[nextMonthFirstDayOfWeek];
            int i = 0;
            int nextMonthDay = 1;
            StackLayout finalWeekStack = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
            for (int j = 0; j < 5; j++)
            {
                StackLayout weekStack = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
                i = 0;
                for (i = 0; i < 7; i++, currentMonthDayIndex++)
                {
                    if (currentMonthDayIndex == lastDayOfCurrentMonth)
                        break;
                    //weekStack.Children.Add(new Label() { Text = currentMonthDayIndex.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });
                    //weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = currentMonthDayIndex.ToString() }, gridCellDimensions));
                    var date = MonthDaysPerWeekDay.FirstOrDefault().Value.FirstOrDefault();
                    DateTime buttonDate = new DateTime(date.Year, date.Month, currentMonthDayIndex);
                    weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, true), gridCellDimensions));

                }
                if (i % 7 == 0)
                {
                    finalStack.Children.Add(weekStack);
                    if (currentMonthDayIndex == lastDayOfCurrentMonth)
                        break;
                    continue;
                }
                if (weekStack.Children.Count() > 0)
                {

                    for (int k = weekStack.Children.Count(); k < 7; k++)
                    {
                        if (currentMonthDayIndex <= lastDayOfCurrentMonth)
                        {
                            // weekStack.Children.Add(new Label() { Text = currentMonthDayIndex.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });
                            // weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = currentMonthDayIndex.ToString() }, gridCellDimensions));
                            var date = MonthDaysPerWeekDay.FirstOrDefault().Value.FirstOrDefault();
                            DateTime buttonDate = new DateTime(date.Year, date.Month, currentMonthDayIndex);
                            weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, true), gridCellDimensions));
                            currentMonthDayIndex++;
                        }
                        else
                        {
                            //weekStack.Children.Add(new Label() { Text = nextMonthDay.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });
                            // weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = nextMonthDay.ToString() }, gridCellDimensions));

                            var date = NextMonthFirstWeekDaysPerWeekDay.FirstOrDefault().Value;
                            DateTime buttonDate = new DateTime(date.Year, date.Month, nextMonthDay);
                            weekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, false), gridCellDimensions));
                            nextMonthDay++;
                        }
                    }
                    finalStack.Children.Add(weekStack);
                    break;
                }

            }

            var firsDayOftWeekOfNextMonth = NextMonthFirstWeekDaysPerWeekDay.FirstOrDefault(d => d.Value.Day == nextMonthFirstDayOfWeekIndex).Key;
            int firstWeekDayNumberOfNextMonth = PreviousMonthLastWeekDaysPerWeekDay[firsDayOftWeekOfNextMonth].Day; // 1 de agosto

            for (int j = 0; j < 7; j++, nextMonthDay++)
            {
                // finalWeekStack.Children.Add(new Label() { Text = nextMonthDay.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand });
                //  finalWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new Label() { Text = nextMonthDay.ToString() }, gridCellDimensions));

                var date = NextMonthFirstWeekDaysPerWeekDay.FirstOrDefault().Value;
                DateTime buttonDate = new DateTime(date.Year, date.Month, nextMonthDay);

                finalWeekStack.Children.Add(UtilViewBuilder.CenteredGrid(new DateButton(buttonDate, false), gridCellDimensions));
            }

            finalStack.Children.Add(finalWeekStack);



            return finalStack;

        }


    }




    public class DateButton : Button
    {
        public DateTime Date { get; set; }
        public bool IsInCurrentMonth { get; set; }
        public DateButton(DateTime date, bool isInCurrentMonth) : base()
        {

            Date = date;
            IsInCurrentMonth = isInCurrentMonth;
            ConstructDateButton();
            Clicked += DateButton_Clicked;
            MessagingCenter.Subscribe<DateButton, DateTime>(this, "DateChanged", (sender, arg) =>
            {
                ConstructDateButton();
            });
        }

        private void DateButton_Clicked(object sender, EventArgs e)
        {


            MessagingCenter.Send<DateButton, DateTime>(this, "DateChanged", Date);
            BackgroundColor = Color.ForestGreen;
        }

        private void ConstructDateButton()
        {
            Text = Date.Day.ToString();
            TextColor = Color.Black;
            if (!IsInCurrentMonth)
                TextColor = Color.SlateGray;
            if (Date == DateTime.Today)
                TextColor = Color.Red;
            BackgroundColor = Color.Transparent;
        }
    }
}
