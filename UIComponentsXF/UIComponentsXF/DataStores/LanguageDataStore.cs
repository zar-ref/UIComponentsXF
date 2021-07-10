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
        public static Dictionary<string, CultureInfo> LanguageDictionary { get; set; } = new Dictionary<string, CultureInfo>
        {
            { Enumerators.Languages.Portuguese.ToString(), new CultureInfo("pt") },
            { Enumerators.Languages.English.ToString(), new CultureInfo("en") }

        };
        public static CultureInfo CurrentAplicationCultureInfo
        {
            get
            {
                return LanguageDictionary[Language];
            }
        }



        public static void ChangeLanguage(Enumerators.Languages newLanguage)
        {

            if (Language != newLanguage.ToString())
            {
                var newCultureInfo = LanguageDictionary[newLanguage.ToString()];
                UIComponentsXF.Resources.Resources.Culture = newCultureInfo;
                Language = newLanguage.ToString();
            }



        }

    }
}
