using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using WYJ.Core.Interfaces;

namespace WYJ.Core.Themes
{
    public class ThemeManager
    {
        private static Lazy<ThemeManager> _instance = new Lazy<ThemeManager>(() => new ThemeManager());
        /// <summary>
        /// 实例化
        /// </summary>
        public static ThemeManager Instance { get { return _instance.Value; } }
        private ThemeManager()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("Themes"));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        [ImportMany(typeof(ITheme))]
        public List<ITheme> Themes { get; set; }
        public static string GetThemeName(DependencyObject obj)
        {
            return (string)obj.GetValue(ThemeNameProperty);
        }

        public static void SetThemeName(DependencyObject obj, string value)
        {
            obj.SetValue(ThemeNameProperty, value);
        }
        public static readonly DependencyProperty ThemeNameProperty =
            DependencyProperty.RegisterAttached("ThemeName", typeof(string), typeof(ThemeManager), new PropertyMetadata(string.Empty, OnThemeNameChangedCallback));

        private static void OnThemeNameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ThemeManager.Instance.Themes == null || ThemeManager.Instance.Themes.Count <= 0)
                return;
            var oldTheme = ThemeManager.Instance.Themes.FirstOrDefault(l => l.ThemeName == e.OldValue.ToString());
            var newTheme = ThemeManager.Instance.Themes.FirstOrDefault(l => l.ThemeName == e.NewValue.ToString());
            if (oldTheme != null)
            {
                var resourceDictionaryToRemove = Application.Current.Resources.MergedDictionaries.FirstOrDefault(r => r.Source == oldTheme.GetResourceUri());
                if (resourceDictionaryToRemove != null)
                    Application.Current.Resources.MergedDictionaries.Remove(resourceDictionaryToRemove);
            }
            if (newTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = newTheme.GetResourceUri() });
            }
        }
    }
}
