using System;
using System.Collections.Generic;
using UIComponentsXF.Models;
using UIComponentsXF.Util;
using static UIComponentsXF.Util.Enumerators;

namespace UIComponentsXF.DataStores
{
    public class ColorsDataStore
    {
        public static string CurrentColorTheme { get; set; } = ColorThemes.DarkTheme.ToString();

        public static void ChangeCurrentColorTheme(ColorThemes colorTheme)
        {
            CurrentColorTheme = colorTheme.ToString();

        }

        /// <summary>
        /// Key: color Type
        /// Value: Current color theme's color
        /// </summary>
        public static Dictionary<string, ColorTheme> Colors { get; set; }

        public static void FillColorsDictionary()
        {
            Colors = new Dictionary<string, ColorTheme>();
            Colors.Add(ColorTypes.Background.ToString(), new ColorTheme()
            {
                DarkThemeColor = CustomStyles.GetColorFromName(ColorNames.Dark.ToString()),
                LightThemeColor = CustomStyles.GetColorFromName(ColorNames.Light.ToString())
            });

        }
    }
}
