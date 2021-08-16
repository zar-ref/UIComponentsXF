using System;
using Xamarin.Forms;

namespace UIComponentsXF.Util
{
    public static class CustomStyles
    {
        public static Color GetColorFromName(string colorName)
        {
            Application.Current.Resources.TryGetValue(colorName, out var color);
            if (color != null)
                return (Color)color;
            else
                return Color.Transparent;
        }
    }
}
