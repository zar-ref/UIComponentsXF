using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UIComponentsXF.Converters;
using UIComponentsXF.DataStores;
using UIComponentsXF.Resources;
using UIComponentsXF.Util;
using Xamarin.Forms;

namespace UIComponentsXF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string CurrentLanguage { get; set; } = Enumerators.Languages.Portuguese.ToString();

        //Key: resource
        //Value: translation
        private static Dictionary<string, string> translations { get; set; } = new Dictionary<string, string>();
        public  Dictionary<string, string> Translations
        {
            get
            {
                if (translations == null)
                    translations = new Dictionary<string, string>();
                return translations;
            }
            set
            {

                translations = value;
                OnPropertyChanged("Translations");
            }
        }

        public void ClearTranslations()
        {
            translations.Clear();
        }


        public void RegisterTranslation(string resource)
        {
            var translation = TranslateExtension.GetLanguageResource(resource);
            if (translation == null)
                translations.Add(resource, "Missing Translation");
            var alreadyHasKey = translations.Any(dic => dic.Key == resource);
            if (!alreadyHasKey)
            {
                translations.Add(resource, translation);
                OnPropertyChanged("Translations");
            }
          
        }

        public void SwitchTranslations()
        {
            var translationkeys = translations.Keys.ToList();
            ClearTranslations();
            CurrentLanguage = LanguageDataStore.Language;
            foreach (var resource in translationkeys)
            {
                RegisterTranslation(resource);
            }

        }

        public static Binding GetTranslationBindingFromResource( string resource)
        {
            var converter = new TranslationConverter();
            Binding binding = new Binding("Translations");
            binding.Converter = converter;
            binding.ConverterParameter = resource;
            return binding;
        }



        bool isLoading = false;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
