using ClassLibrary1_Prueba;
using ControlUsuario_IPO2;
using LucarioGAC;
using Microsoft.Toolkit.Uwp.Notifications;
using Pokemon_Antonio_Campallo_Gomez;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CapturarPage : Page
    {
        private int contador = 0;
        private int max_intentos = 3;
        DispatcherTimer dtTimeB;
        bool direccionBarra = true;
        string ipokemon;

        public CapturarPage()
        {
            this.InitializeComponent();
            configurar_pokemons();
            pbMove();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null)
            {
                string str = e.Parameter as string;
                if (str == "Gengar")
                {
                    this.ipokemon = "Gengar";
                    vbGengar.Visibility = Visibility.Collapsed;
                    vbDracofire.Visibility = Visibility.Visible;
                }
                else if (str == "DracoFire")
                {
                    this.ipokemon = "DracoFire";
                    vbDracofire.Visibility = Visibility.Collapsed;
                    vbGengar.Visibility = Visibility.Visible;
                }
            }
        }

        private void configurar_pokemons()
        {
            DracofireGDLRS.verNombre(false);
            DracofireGDLRS.verPocionVida(false);
            DracofireGDLRS.verPocionEnergia(false);
            DracofireGDLRS.verFilaEnergia(false);
            DracofireGDLRS.verFilaVida(false);

            //ArticunoACG.verNombre(false);
            //ArticunoACG.verPocionVida(false);
            //ArticunoACG.verPocionEnergia(false);

            GengarJCC.verNombre(false);
            GengarJCC.verPocionVida(false);
            GengarJCC.verPocionEnergia(false);
            GengarJCC.verFilaEnergia(false);
            GengarJCC.verFilaVida(false);

            //MyUCLucario.verNombre(false);
            //MyUCLucario.verPocionVida(false);
            //MyUCLucario.verPocionEnergia(false);

            //ToxicroackJPG.verNombre(false);
            //ToxicroackJPG.verPocionVida(false);
            //ToxicroackJPG.verPocionEnergia(false);

            //DragoniteCSD.verNombre(false);
            //DragoniteCSD.verPocionVida(false);
            //DragoniteCSD.verPocionEnergia(false);
        }

        private void pbMove()
        {
            dtTimeB = new DispatcherTimer();
            dtTimeB.Interval = TimeSpan.FromMilliseconds(5);
            dtTimeB.Tick += move;
            dtTimeB.Start();
        }

        private void move(object sender, object e)
        {
            if (this.direccionBarra == true)
            {
                this.pbFuerza.Value += 0.5;
                if (pbFuerza.Value >= 100)
                {
                    this.direccionBarra = false;
                }
            }
            else if (this.direccionBarra == false)
            {
                this.pbFuerza.Value -= 0.5;
                if (pbFuerza.Value <= 0)
                {
                    this.direccionBarra = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.dtTimeB.Stop();

            if (40 <= this.pbFuerza.Value && this.pbFuerza.Value <= 60)
            {
                Storyboard sbaux = (Storyboard)this.Resources["LanzarBien"];
                sbaux.Begin();
                sbaux.Completed += (s, ev) => { sbaux.Stop();
                    ToastContentBuilder notificacion = new ToastContentBuilder();
                    notificacion.AddArgument("action", "Captura")
                    .AddText("¡Pokemon Capturado!")
                    .AddText("Has capturado un " + ipokemon)
                    .AddAppLogoOverride(new Uri("ms-appx:///Assets/Logo.png"), ToastGenericAppLogoCrop.Circle)
                    .SetToastDuration(0)
                    .Show();

                    Frame.Navigate(typeof(MisPokemonPage), ipokemon);
                };
            }
            else { 
                contador++;
                if (contador == 3)
                {
                    Storyboard sbaux = (Storyboard)this.Resources["LanzarMal"];
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => {
                        sbaux.Stop();
                        ToastContentBuilder notificacion = new ToastContentBuilder();
                        notificacion.AddArgument("action", "FalloCaptura")
                        .AddText("¡Captura Pokemon Fallida!")
                        .AddAppLogoOverride(new Uri("ms-appx:///Assets/imgCaptura/Error.png"), ToastGenericAppLogoCrop.Circle)
                        .SetToastDuration(0)
                        .Show();

                        Frame.GoBack();
                    };
                } else
                {
                    Storyboard sbaux = (Storyboard)this.Resources["LanzarMal"];
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { 
                        sbaux.Stop(); 
                        txt_intentos.Text = "Intentos restantes: " + (max_intentos - contador).ToString();                    
                    };
                    
                }

            }
        }
    }
}
