using ClassLibrary1_Prueba;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ControlUsuario_IPO2;
using Dracofire;
using LucarioGAC;
using Pokemon_Antonio_Campallo_Gomez;
using Sesion4;
using PokemonIPO2;
using Scizor_AdrianPeinado;
using PokemonNoelia;
using OrtizCañameroRoberto_Snorlax;
using ButterFreeACC;
using ToxicroackJPG;
using CharizardASM;
using Windows.UI.Xaml.Documents;
using Windows.UI.ViewManagement;
using System.Collections.ObjectModel;
using Windows.Foundation;
// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PokedexPage : Page
    {
        private iPokemon pokemon_seleccionado { get; set; }
        private ObservableCollection<UserControl> pokemons;

        public PokedexPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1800, 1000));

            configurar_pokedex();
            richTextBlock.SizeChanged += RichTextBlock_SizeChanged;

        }
        
        private void RichTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Implement logic to adjust font size based on RichTextBlock's size
            double newFontSize = CalculateOptimalFontSize();
            foreach (var block in richTextBlock.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (var inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            run.FontSize = newFontSize;
                        }
                    }
                }
            }
        }

        private double CalculateOptimalFontSize()
        {
            double minFontSize = 10;
            double maxFontSize = 100;
            double fontSize = maxFontSize;

            while (minFontSize < maxFontSize)
            {
                fontSize = (minFontSize + maxFontSize) / 2;

                if (DoesTextFit(fontSize))
                {
                    minFontSize = fontSize + 0.1;
                }
                else
                {
                    maxFontSize = fontSize - 0.1;
                }
            }

            return fontSize;
        }

        private bool DoesTextFit(double fontSize)
        {
            var textBlock = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                TextTrimming = TextTrimming.None,
                FontSize = fontSize
            };

            foreach (var block in richTextBlock.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (var inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            textBlock.Text += run.Text;
                        }
                    }
                }
            }

            textBlock.Measure(new Size(richTextBlock.ActualWidth, double.PositiveInfinity));

            return textBlock.DesiredSize.Height <= richTextBlock.ActualHeight;
        }

        private void configurar_pokedex()
        {
            pokemons = new ObservableCollection<UserControl>
            {
                new DracofireGDLRS(),
                new GengarJCC(),
                //new CharizardASM.CharizardASM(), a pesar de arreglarlo sigue petando el programa pero no da error parece magia
                //new ButterFreeACC.ButterFreeACC(), igual: no funciona a pesar de no dar error
                new MyUCLucario(),
                new DragoniteCSD(),
                new ArticunoACG(),
                new ToxicroackJPG.ToxicroackJPG(),
                new ChandelureNDAA(),
                new SnorlaxROC(),
                new ScizorAPJ(),
                new piplupUserControl.PiplupMLTN(),
                new MakuhitaAPQ2()

            };

            foreach (UserControl control in pokemons)
            {
                if (control is iPokemon pokemonControl)
                {
                    pokemonControl.verFondo(false);
                    pokemonControl.verFilaVida(false);
                    pokemonControl.verFilaEnergia(false);
                    pokemonControl.verPocionVida(false);
                    pokemonControl.verPocionEnergia(false);
                    pokemonControl.verNombre(false);
                }
            }

            FilteredListView.ItemsSource = pokemons;

        }
               
        private void PokemonListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilteredListView.SelectedItem != null)
            {
                var selectedPokemon = FilteredListView.SelectedItem as iPokemon;
                pokemon_seleccionado = selectedPokemon;
                DataContext = pokemon_seleccionado;
                
                UserControl pokemonControlJugador1 = CrearPokemonControl(pokemon_seleccionado);
                Pokemon_Content.Child = pokemonControlJugador1;
                pokemonDetailGrid.Visibility = Visibility.Visible;
            }
        }

        private UserControl CrearPokemonControl(iPokemon pokemon)
        {
            UserControl control = pokemon switch
            {
                DracofireGDLRS _ => new DracofireGDLRS(),
                GengarJCC _ => new GengarJCC(),
                MyUCLucario _ => new MyUCLucario(),
                DragoniteCSD _ => new DragoniteCSD(),
                ArticunoACG _ => new ArticunoACG(),
                ToxicroackJPG.ToxicroackJPG _ => new ToxicroackJPG.ToxicroackJPG(),
                ChandelureNDAA _ => new ChandelureNDAA(),
                SnorlaxROC _ => new SnorlaxROC(),
                ScizorAPJ _ => new ScizorAPJ(),
                MakuhitaAPQ2 _ => new MakuhitaAPQ2(),
                piplupUserControl.PiplupMLTN _ => new piplupUserControl.PiplupMLTN(),
                _ => throw new InvalidOperationException("Tipo de Pokémon no soportado"),
            };

            if (control is iPokemon pokemonControl)
            {
                pokemonControl.verFilaVida(false);
                pokemonControl.verFilaEnergia(false);
                pokemonControl.verPocionVida(false);
                pokemonControl.verPocionEnergia(false);
                pokemonControl.verNombre(false);
            }

            return control;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Obtiene el ancho y el alto actual de la ventana
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;

            // Si el ancho es menor que 1800, ajusta el ancho a 1800
            if (width < 1800 || height < 1000)
            {
                double newWidth = width < 1800 ? 1800 : width;
                double newHeight = height < 1000 ? 1000 : height;

                // Redimensiona la ventana a las dimensiones mínimas permitidas
                ApplicationView.GetForCurrentView().TryResizeView(new Size(newWidth, newHeight));
            }
        }
    }
}
