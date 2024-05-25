using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace App_Pokemon
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ShowWelcomeNotification();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += opcionVolver;
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = "IPOkemon",
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },
                                new AdaptiveText()
                                {
                                    Text = "Un proyecto de IPO2",
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },
                            }
                        }
                    },
                    TileWide = new TileBinding()
                    {
                        Branding = TileBranding.NameAndLogo,
                        DisplayName = "Version 1.0",
                        Content = new TileBindingContentAdaptive()
                        {
                            Children = {
                                new AdaptiveText()
                                {
                                    Text = "IPOkemon",
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },
                                new AdaptiveText()
                                {
                                    Text = "Un Proyecto de IPO2",
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },
                                new AdaptiveText()
                                {
                                    Text = "Una aplicación sobre Pokemon hecha con tecnología UWP",
                                    HintWrap = true,
                                }
                            }
                        }
                    },
                    TileLarge = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children = {
                                new AdaptiveText()
                                {
                                    Text = "IPOkemon",
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },
                                new AdaptiveText()
                                {
                                    Text = "Un Proyecto de IPO2",
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },
                                new AdaptiveText()
                                {
                                    Text = "Una aplicación sobre Pokemon hecha con tecnología UWP",
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                }
                            }
                        }
                    },
                }
            };
            var notification = new TileNotification(content.GetXml());
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Update(notification);
        }

        public Frame MainFrame => fmMain;


        private void viPokedex_PointerReleased(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            var itemName = item.Content.ToString();

            switch (itemName)
            {
                case "Inicio":
                    this.Frame.Navigate(typeof(MainPage));
                    break;
                case "Mis Pokemon":
                    fmMain.Navigate(typeof(MisPokemonPage));
                    break;
                case "Pokedex":
                    fmMain.Navigate(typeof(PokedexPage));
                    break;
                case "Combate":
                    fmMain.Navigate(typeof(InicioCombate));
                    break;
                case "Capturar":
                    fmMain.Navigate(typeof(CapturarMap));
                    break;
                case "Acerca De":
                    fmMain.Navigate(typeof(AcercaDePage));
                    break;
            }
        }
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            navigationView.IsPaneOpen = false; // Ocultar el panel de navegación al iniciar
        }
        private void Dudas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fmMain.Navigate(typeof(AcercaDePage));
        }
        private void Pokemons_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fmMain.Navigate(typeof(MisPokemonPage));
        }
        private void Pelea_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fmMain.Navigate(typeof(InicioCombate));
        }
        private void Pokedex_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fmMain.Navigate(typeof(PokedexPage));
        }

        private void opcionVolver(object sender, BackRequestedEventArgs e)
        {
            if (MainFrame.BackStack.Any())
            {
                MainFrame.GoBack();
            }
        }
        private void ShowWelcomeNotification()
        {
            // Crea el contenido de la notificación
            var toastContent = new ToastContentBuilder()
                .AddArgument("action", "welcome")
                .AddText("¡Bienvenido a la Aplicación!")
                .AddText("Gracias por usar nuestra aplicación.")
                .AddAppLogoOverride(new Uri("ms-appx:///Assets/Logo.png"), ToastGenericAppLogoCrop.Circle)
                .GetToastContent();

            // Crea la notificación
            var toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTimeOffset.UtcNow.AddMinutes(1)
            };

            // Muestra la notificación
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}

