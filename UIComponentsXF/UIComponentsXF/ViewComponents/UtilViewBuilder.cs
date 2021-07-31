using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UIComponentsXF.ViewComponents
{
    public static class UtilViewBuilder
    {

        public static int DeviceWidthPx
        {
            get
            {
                var width = DeviceDisplay.MainDisplayInfo.Width;
                return (int)Math.Floor(width);
            }
        }

        public static int DeviceHeightPx
        {
            get
            {
                var height = DeviceDisplay.MainDisplayInfo.Height;
                return (int)Math.Floor(height);
            }
        }

        public static int DeviceWidth
        {
            get
            {
                var xamarinFormsWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density; ;
                return (int)Math.Floor(xamarinFormsWidth);
            }
        }

        public static int DeviceHeight
        {
            get
            {
                var xmarinFormsHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
                return (int)Math.Floor(xmarinFormsHeight);
            }
        }

        public static Grid CenteredGrid(View view, int gridDimensions)
        {


            view.HorizontalOptions = LayoutOptions.Center;
            view.VerticalOptions = LayoutOptions.Center;
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                 new RowDefinition { Height =  gridDimensions}
                },
                ColumnDefinitions =
                {
                new ColumnDefinition { Width = gridDimensions}
                }

            };
            grid.Children.Add(view, 0, 0);
            return grid;

        }

    }
}
