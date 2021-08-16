using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UIComponentsXF.DataStores;
using UIComponentsXF.Models;
using UIComponentsXF.ViewModels;
using Xamarin.Forms;
using static UIComponentsXF.Util.Enumerators;

namespace UIComponentsXF.Converters
{
    public class ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorDictionary = (Dictionary<string, ColorTheme>)value;
            if (value == null)
                return string.Empty;

            var key = (string)parameter;
            if (key == null)
                return Color.Transparent;

            if (ColorsDataStore.CurrentColorTheme == ColorThemes.DarkTheme.ToString())
                return colorDictionary[key].DarkThemeColor;
            else if (ColorsDataStore.CurrentColorTheme == ColorThemes.LightTheme.ToString())
                return colorDictionary[key].LightThemeColor;
            else
                return Color.Transparent;





        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();


        }


    }
}
