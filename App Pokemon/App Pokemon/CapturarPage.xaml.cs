using ClassLibrary1_Prueba;
using ControlUsuario_IPO2;
using LucarioGAC;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using Pokemon_Antonio_Campallo_Gomez;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static App_Pokemon.MisPokemonPage;

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
            pokemons_ocultar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pokemons_ocultar();
            if (e != null)
            {
                string str = e.Parameter as string;
                if (str == "Gengar")
                {
                    this.ipokemon = "Gengar";
                    vbGengar.Visibility = Visibility.Visible;
                }
                else if (str == "DracoFire")
                {
                    this.ipokemon = "Dracofire";
                    vbDracofire.Visibility = Visibility.Visible;
                }
                else if (str == "Articuno")
                {
                    this.ipokemon = "Articuno";
                    vbArticuno.Visibility = Visibility.Visible;
                }
                else if (str == "Toxicroac")
                {
                    this.ipokemon = "Toxicroack";
                    vbToxicroac.Visibility = Visibility.Visible;
                }
            }
        }

        private void pokemons_ocultar()
        {
            vbDracofire.Visibility = Visibility.Collapsed;
            vbGengar.Visibility = Visibility.Collapsed;
            vbArticuno.Visibility = Visibility.Collapsed;
            vbToxicroac.Visibility = Visibility.Collapsed;
        }

        private void configurar_pokemons()
        {
            DracofireGDLRS.verFondo(false);
            DracofireGDLRS.verNombre(false);
            DracofireGDLRS.verPocionVida(false);
            DracofireGDLRS.verPocionEnergia(false);
            DracofireGDLRS.verFilaEnergia(false);
            DracofireGDLRS.verFilaVida(false);

            ArticunoACG.verFondo(false);
            ArticunoACG.verNombre(false);
            ArticunoACG.verPocionVida(false);
            ArticunoACG.verPocionEnergia(false);
            ArticunoACG.verFilaEnergia(false);
            ArticunoACG.verFilaVida(false);
            
            GengarJCC.verFondo(false);
            GengarJCC.verNombre(false);
            GengarJCC.verPocionVida(false);
            GengarJCC.verPocionEnergia(false);
            GengarJCC.verFilaEnergia(false);
            GengarJCC.verFilaVida(false);

            ToxicroackJPG.verFondo(false);
            ToxicroackJPG.verNombre(false);
            ToxicroackJPG.verFilaEnergia(false);
            ToxicroackJPG.verFilaVida(false);
            ToxicroackJPG.verPocionVida(false);
            ToxicroackJPG.verPocionEnergia(false);

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
                sbaux.Completed += async (s, ev) => { sbaux.Stop();
                    ToastContentBuilder notificacion = new ToastContentBuilder();
                    notificacion.AddArgument("action", "Captura")
                    .AddText("¡Pokemon Capturado!")
                    .AddText("Has capturado un " + ipokemon+ "\n¡Lo tienes disponible en la página MisPokemon!")
                    .AddAppLogoOverride(new Uri("ms-appx:///Assets/Logo.png"), ToastGenericAppLogoCrop.Circle)
                    .SetToastDuration(0)
                    .Show();
                    await capturarPokemonAsync(ipokemon);
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
                        dtTimeB.Start();
                    };
                }

            }
        }

        private async Task capturarPokemonAsync(string pokemonCapturado)
        {
            // Obtener la referencia al archivo JSON en la carpeta de datos de la aplicación
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync("mispokemons.json", CreationCollisionOption.OpenIfExists);

            // Leer el contenido del archivo JSON
            string jsonString = await FileIO.ReadTextAsync(file);
            PokemonNames pokemonNames;

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                // Si el archivo no está vacío, deserializar su contenido
                pokemonNames = JsonConvert.DeserializeObject<PokemonNames>(jsonString);
            }
            else
            {
                // Si el archivo está vacío, crear una nueva instancia de PokemonNames
                pokemonNames = new PokemonNames { pokemonNames = new List<string>() };
            }

            // Añadir el nombre del Pokémon capturado a la lista si aún no está presente
            if (!pokemonNames.pokemonNames.Contains(pokemonCapturado))
            {
                pokemonNames.pokemonNames.Add(pokemonCapturado);

                // Serializar la lista actualizada a JSON
                string updatedJsonString = JsonConvert.SerializeObject(pokemonNames);

                // Escribir el JSON actualizado al archivo
                await FileIO.WriteTextAsync(file, updatedJsonString);
            }
        }

        private void btn_Huir_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
