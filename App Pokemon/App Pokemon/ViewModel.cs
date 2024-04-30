using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App_Pokemon
{
    public class ViewModel
    {
        public ObservableCollection<UserControl> UserControls { get; set; }

        public ViewModel()
        {
            // Carga el ResourceDictionary desde el archivo XAML
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new System.Uri("ms-appx:///UserControlNamespaces.xaml");
            UserControls = new ObservableCollection<UserControl>();
            // Itera sobre los elementos del diccionario
            foreach (var item in resourceDictionary)
            {
                // Verifica si el valor es un control de usuario y agrégalo a la lista
                if (item.Value is UserControl userControl)
                {
                    UserControls.Add(userControl);
                }
            }

        }
    }
}
