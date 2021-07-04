using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using UIComponentsXF.DataStores;
using UIComponentsXF.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponentsXF.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {


        const string ResourcePath = "UIComponentsXF.Resources.Resources";
        public string Text { get; set; }

        internal static string GetLanguageResource(string item)
        {
            ResourceManager resourceManager = new ResourceManager(ResourcePath, typeof(TranslateExtension).GetTypeInfo().Assembly);

            var ci = Enumerators.Languages.Portuguese.ToString().Equals(LanguageDataStore.Language)
                ? new CultureInfo("pt")
                : new CultureInfo("en");

           
            return resourceManager.GetString(item, ci);
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourcePath, typeof(TranslateExtension).GetTypeInfo().Assembly);

            var ci = Enumerators.Languages.Portuguese.ToString().Equals(LanguageDataStore.Language)
                ? new CultureInfo("pt")
                : new CultureInfo("en");


            return resourceManager.GetString(Text, ci);
        }

       
    }

   
}
