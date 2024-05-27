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
using System.Xml.Linq;

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

        private void inicio()
        {
            bool x = false;
            var itemsSource = FlipViewPokemon.ItemsSource;
            if (itemsSource is IList<UserControl> itemsList)
            { 
                x = itemsList.Any();
            }

            if (itemsSource == null || !x)
            {
                txt_pokemons.Visibility = Visibility.Visible;
                FlipViewPokemon.Visibility = Visibility.Collapsed;
            }
            else
            {
                txt_pokemons.Visibility = Visibility.Collapsed;
                FlipViewPokemon.Visibility = Visibility.Visible;
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is string capturedPokemon && !string.IsNullOrEmpty(capturedPokemon))
            {
                await ConfigurarPokemonsAsync();
                // Seleccionar el elemento con el nombre igual a e.Parameter
                var items= FlipViewPokemon.ItemsSource;
                
                if (items is IList<UserControl> itemsList)
                {
                    // si existe el elemento en array con nombre = a capturedPokemon
                    if (itemsList.Any(p => p is iPokemon && (p as iPokemon).Nombre == capturedPokemon))
                    {
                        FlipViewPokemon.SelectedItem = itemsList.FirstOrDefault(p => p is iPokemon && (p as iPokemon).Nombre == capturedPokemon);
                    }
                }
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
            inicio();
        }
        private async Task EliminarPokemonAsync(string pokemonAEliminar)
        {
            // Obtener la referencia al archivo JSON en la carpeta de datos de la aplicación
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.GetFileAsync("mispokemons.json");

            // Leer el contenido del archivo JSON
            string jsonString = await FileIO.ReadTextAsync(file);
            PokemonNames pokemonNames;

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                // Si el archivo no está vacío, deserializar su contenido
                pokemonNames = JsonConvert.DeserializeObject<PokemonNames>(jsonString);

                // Eliminar el nombre del Pokémon de la lista si está presente
                if (pokemonNames.pokemonNames.Contains(pokemonAEliminar))
                {
                    pokemonNames.pokemonNames.Remove(pokemonAEliminar);

                    // Serializar la lista actualizada a JSON
                    string updatedJsonString = JsonConvert.SerializeObject(pokemonNames);

                    // Escribir el JSON actualizado al archivo
                    await FileIO.WriteTextAsync(file, updatedJsonString);
                }
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
                var pokemon = e.AddedItems[e.AddedItems.Count - 1] as iPokemon;
                pokemon_seleccionado = pokemon;
            }
        }

        private async void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Confirmación de eliminación",
                Content = "¿Estás seguro de que deseas eliminar el Pokémon?",
                PrimaryButtonText = "Sí",
                CloseButtonText = "No"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (pokemon_seleccionado != null && FlipViewPokemon.ItemsSource != null)
                {
                    var itemsSource = FlipViewPokemon.ItemsSource;
                    if (itemsSource is IList<UserControl> itemsList)
                    {
                        if (itemsList.Contains((UserControl)pokemon_seleccionado))
                        {
                            _ = EliminarPokemonAsync(pokemon_seleccionado.Nombre);
                            itemsList.Remove((UserControl)pokemon_seleccionado);
                            pokemon_seleccionado = null;
                            FlipViewPokemon.ItemsSource = new ObservableCollection<UserControl>(itemsList);
                        }
                    }
                }
            }
        }
    }        
}
