using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CapturarMap : Page
    {
        public CapturarMap()
        {
            this.InitializeComponent();
            LoadMap();
        }

        private void LoadMap()
        {
            // Centro del mapa (puedes ajustar esto según sea necesario)
            myMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 40.416775,
                Longitude = -3.703790
            });
            myMap.ZoomLevel = 6;

            // Define las coordenadas de las ciudades
            AddMapIcon(new BasicGeoposition() { Latitude = 40.416775, Longitude = -3.703790 }, "Madrid");
            AddMapIcon(new BasicGeoposition() { Latitude = 39.862831, Longitude = -4.027323 }, "Toledo");
            AddMapIcon(new BasicGeoposition() { Latitude = 38.984783, Longitude = -3.927045 }, "Ciudad Real");
            AddMapIcon(new BasicGeoposition() { Latitude = 43.361914, Longitude = -5.849389 }, "Asturias");
        }

        private void AddMapIcon(BasicGeoposition position, string title)
        {
            var mapIcon = new MapIcon
            {
                Location = new Geopoint(position),
                NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),
                Title = title,
                ZIndex = 0
            };

            myMap.MapElements.Add(mapIcon);
        }
    }
}