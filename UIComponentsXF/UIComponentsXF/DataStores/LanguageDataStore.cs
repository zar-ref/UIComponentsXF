using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UIComponentsXF.Util;

namespace UIComponentsXF.DataStores
{
    public class LanguageDataStore
    {
        public static string Language { get; set; } = Enumerators.Languages.Portuguese.ToString();

        public static void ChangeLanguage(Enumerators.Languages language)
        {

            var ci = language.ToString().Equals(Language)
               ? new CultureInfo("pt")
               : new CultureInfo("en");
            UIComponentsXF.Resources.Resources.Culture = ci;
            if (ci.Name == "pt")
                Language = Enumerators.Languages.Portuguese.ToString();
            else
                Language = Enumerators.Languages.English.ToString();
        }

    }
}
