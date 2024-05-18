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
using Xamarin.Forms.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CapturarMap : Page
    {
        private Flyout _currentFlyout;

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
            AddMapIcon(new BasicGeoposition() { Latitude = 40.416775, Longitude = -3.703790 }, "Madrid", "Gengar");
            AddMapIcon(new BasicGeoposition() { Latitude = 39.862831, Longitude = -4.027323 }, "Toledo", "DracoFire");
            AddMapIcon(new BasicGeoposition() { Latitude = 38.984783, Longitude = -3.927045 }, "Ciudad Real", "Toxicroac");
            AddMapIcon(new BasicGeoposition() { Latitude = 43.361914, Longitude = -5.849389 }, "Asturias", "Articuno");

            myMap.MapElementClick += MapControl_MapElementClick;
        }

        private void AddMapIcon(BasicGeoposition position, string title, string tag)
        {
            var mapIcon = new MapIcon
            {
                Location = new Geopoint(position),
                NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),
                Title = title,
                Tag = tag,
                ZIndex = 0
            };

            myMap.MapElements.Add(mapIcon);
        }

        private void MapControl_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {

            if (args.MapElements.FirstOrDefault() is MapIcon clickedIcon)
            {

                if (clickedIcon.Title.ToString() == "Madrid")
                {
                    Frame.Navigate(typeof(CapturarPage), "Gengar");
                }
                else if (clickedIcon.Title.ToString() == "Toledo")
                {
                    Frame.Navigate(typeof(CapturarPage), "DracoFire");
                }

            }
        }





        private void MapControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(myMap);
            var position = point.Position;

            // Convert screen coordinates to geographical coordinates
            Geopoint geoPoint;
            myMap.GetLocationFromOffset(position, out geoPoint);

            foreach (var mapElement in myMap.MapElements)
            {
                if (mapElement is MapIcon mapIcon)
                {
                    var iconPosition = mapIcon.Location.Position;

                    // Check if the cursor is close to the MapIcon (within a small threshold)
                    if (GetDistance(iconPosition, geoPoint.Position) < 0.01) // threshold in degrees
                    {
                        ShowFlyout(mapIcon, position);
                        return;
                    }
                }
            }

            // If no MapIcon is found under the cursor, close the current Flyout
            _currentFlyout?.Hide();
        }

        private double GetDistance(BasicGeoposition pos1, BasicGeoposition pos2)
        {
            double latDiff = pos1.Latitude - pos2.Latitude;
            double lonDiff = pos1.Longitude - pos2.Longitude;
            return Math.Sqrt(latDiff * latDiff + lonDiff * lonDiff);
        }

        private void ShowFlyout(MapIcon mapIcon, Windows.Foundation.Point position)
        {
            // Close the current Flyout if it exists
            _currentFlyout?.Hide();

            var flyout = new Flyout();
            var stackPanel = new StackPanel();

            var titleTextBlock = new TextBlock { Text = mapIcon.Title, FontWeight = Windows.UI.Text.FontWeights.Bold };
            var subtitleTextBlock = new TextBlock { Text = mapIcon.Tag as string };

            stackPanel.Children.Add(titleTextBlock);
            stackPanel.Children.Add(subtitleTextBlock);

            flyout.Content = stackPanel;
            _currentFlyout = flyout;

            // Create FlyoutShowOptions and set its position
            var showOptions = new FlyoutShowOptions
            {
                Position = position
            };
            flyout.ShowAt(myMap, showOptions);
        }
    }
}