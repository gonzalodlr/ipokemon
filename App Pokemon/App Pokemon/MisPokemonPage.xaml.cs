using ClassLibrary1_Prueba;
using ControlUsuario_IPO2;
using Dracofire;
using LucarioGAC;
using OrtizCañameroRoberto_Snorlax;
using Pokemon_Antonio_Campallo_Gomez;
using PokemonIPO2;
using PokemonNoelia;
using Newtonsoft.Json;
using Scizor_AdrianPeinado;
using Sesion4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Storage;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MisPokemonPage : Page
    {
        private iPokemon pokemon_seleccionado;
        public class PokemonNames
        {
            public List<string> pokemonNames { get; set; }
        }
        private ObservableCollection<UserControl> pokemons;

        public MisPokemonPage()
        {
            this.InitializeComponent();
            // Establece el tamaño mínimo preferido de la ventana
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1500, 700));

            // Maneja el evento SizeChanged de la ventana
            Window.Current.SizeChanged += CurrentWindow_SizeChanged;

            _ = ConfigurarPokemonsAsync();
                        
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string capturedPokemon && !string.IsNullOrEmpty(capturedPokemon))
            {
                AddCapturedPokemon(capturedPokemon);
            }
        }

        private void AddCapturedPokemon(string pokemonName)
        {
            UserControl capturedPokemon = pokemonName switch
            {
                "DracoFire" => new DracofireGDLRS(),
                "Gengar" => new GengarJCC(),
                "Articuno" => new ArticunoACG(),
                "Toxicroac" => new ToxicroackJPG.ToxicroackJPG(),
                _ => null,
            };

            if (capturedPokemon != null && !pokemons.Contains(capturedPokemon))
            {
                pokemons.Add(capturedPokemon);
            }
        }

        private void CurrentWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Obtén el ancho y el alto actual de la ventana
            double width = Window.Current.Bounds.Width;
            double height = Window.Current.Bounds.Height;

            // Si el ancho es menor que 600, ajusta el ancho a 600
            if (width < 700)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size(700, height));
            }

            // Si la altura es menor que 600, ajusta la altura a 600
            if (height < 1500)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size(width, 1500));
            }
        }

        private async Task ConfigurarPokemonsAsync()
        {
            pokemons = new ObservableCollection<UserControl>
    {
        new DracofireGDLRS(),
        new GengarJCC(),
        new MyUCLucario(),
        new DragoniteCSD(),
        new ArticunoACG(),
        new ToxicroackJPG.ToxicroackJPG(),
        new ChandelureNDAA(),
        new SnorlaxROC(),
        new ScizorAPJ(),
        new MakuhitaAPQ2()
    };

            // Leer el archivo JSON
            string jsonString = await ReadJsonFileAsync("mispokemons.json");

            if (!string.IsNullOrEmpty(jsonString))
            {
                PokemonNames pokemonNames = JsonConvert.DeserializeObject<PokemonNames>(jsonString);

                // Filtrar los UserControls que coinciden con los nombres en el JSON
                var matchedPokemons = new List<UserControl>();

                foreach (UserControl control in pokemons)
                {
                    if (control is iPokemon pokemonControl && pokemonNames.pokemonNames.Contains(pokemonControl.Nombre))
                    {
                        pokemonControl.verFondo(false);
                        pokemonControl.verPocionVida(false);
                        pokemonControl.verPocionEnergia(false);
                        pokemonControl.verNombre(false);
                        matchedPokemons.Add(control);
                    }
                }

                FlipViewPokemon.ItemsSource = new ObservableCollection<UserControl>(matchedPokemons);
            }
        }
        public async Task<string> ReadJsonFileAsync(string fileName)
        {
            try
            {
                // Obtener la referencia a la carpeta de datos local de la aplicación
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                // Obtener el archivo "mispokemons.json" en la carpeta local de la aplicación
                StorageFile file = await localFolder.GetFileAsync(fileName);

                // Leer el contenido del archivo
                string jsonString = await FileIO.ReadTextAsync(file);

                return jsonString;
            }
            catch (FileNotFoundException)
            {
                // Manejar el caso en el que el archivo no existe
                return null;
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones que puedan ocurrir durante la lectura del archivo
                Console.WriteLine("Error al leer el archivo JSON: " + ex.Message);
                return null;
            }
        }


        private void btn_ataqueFuerte_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon_seleccionado != null)
            {
                pokemon_seleccionado.animacionAtaqueFuerte();
            }
        }
        private void btn_ataqueDebil_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon_seleccionado != null)
            {
                pokemon_seleccionado.animacionAtaqueFlojo();
            }
        }
        private void btn_defensa_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon_seleccionado != null)
            {
                pokemon_seleccionado.animacionDefensa();
            }
        }
        private void btn_recuperacion_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon_seleccionado != null)
            {
                pokemon_seleccionado.animacionDescasar();
            }  
        }
        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Obtener el elemento seleccionado actualmente en el FlipView
                var pokemon = e.AddedItems[0] as iPokemon;
                pokemon_seleccionado = pokemon;
            }
        }
    }        
}
