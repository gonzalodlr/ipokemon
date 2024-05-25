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
using ToxicroackJPG;
using Windows.UI.Xaml.Documents;
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

        public PokedexPage()
        {
            this.InitializeComponent();
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
            DracofireGDLRS.verFondo(false);
            DracofireGDLRS.verNombre(false);
            DracofireGDLRS.verFilaVida(false);
            DracofireGDLRS.verFilaEnergia(false);
            DracofireGDLRS.verPocionVida(false);
            DracofireGDLRS.verPocionEnergia(false);

            ArticunoACG.verFondo(false);
            ArticunoACG.verNombre(false);
            ArticunoACG.verFilaVida(false);
            ArticunoACG.verFilaEnergia(false);
            ArticunoACG.verPocionVida(false);
            ArticunoACG.verPocionEnergia(false);

            GengarJCC.verFondo(false);
            GengarJCC.verNombre(false);
            GengarJCC.verFilaVida(false);
            GengarJCC.verFilaEnergia(false);
            GengarJCC.verPocionVida(false);
            GengarJCC.verPocionEnergia(false);

            MyUCLucario.verFondo(false);
            MyUCLucario.verNombre(false);
            MyUCLucario.verFilaVida(false);
            MyUCLucario.verFilaEnergia(false);
            MyUCLucario.verPocionVida(false);
            MyUCLucario.verPocionEnergia(false);


            ToxicroackJPG.verFondo(false);
            ToxicroackJPG.verNombre(false);
            ToxicroackJPG.verFilaVida(false);
            ToxicroackJPG.verFilaEnergia(false);
            ToxicroackJPG.verPocionVida(false);
            ToxicroackJPG.verPocionEnergia(false);

            DragoniteCSD.verFondo(false);
            DragoniteCSD.verNombre(false);
            DragoniteCSD.verFilaVida(false);
            DragoniteCSD.verFilaEnergia(false);
            DragoniteCSD.verPocionVida(false);
            DragoniteCSD.verPocionEnergia(false);


            /*Este Pokemon hace que pete la aplicación si se descomenta*/
            //ButterFreeACC.verFondo(false);
            //ButterFreeACC.verNombre(false);
            //ButterFreeACC.verFilaVida(false);
            //ButterFreeACC.verFilaEnergia(false);
            //ButterFreeACC.verPocionVida(false);
            //ButterFreeACC.verPocionEnergia(false);

            
            ChandelureNDAA.verFondo(false);
            ChandelureNDAA.verNombre(false);
            ChandelureNDAA.verFilaVida(false);
            ChandelureNDAA.verFilaEnergia(false);
            ChandelureNDAA.verPocionVida(false);
            ChandelureNDAA.verPocionEnergia(false);


            /*Este Pokemon es invisible xd*/
            //PiplupMLTN.verFondo(false);
            //PiplupMLTN.verNombre(false);
            //PiplupMLTN.verFilaVida(false);
            //PiplupMLTN.verFilaEnergia(false);
            //PiplupMLTN.verPocionVida(false);
            //PiplupMLTN.verPocionEnergia(false);

            Snorlax.verFondo(false);
            Snorlax.verNombre(false);
            Snorlax.verFilaVida(false);
            Snorlax.verFilaEnergia(false);
            Snorlax.verPocionVida(false);
            Snorlax.verPocionEnergia(false);

            ScizorAPJ.verFondo(false);
            ScizorAPJ.verNombre(false);
            ScizorAPJ.verFilaVida(false);
            ScizorAPJ.verFilaEnergia(false);
            ScizorAPJ.verPocionVida(false);
            ScizorAPJ.verPocionEnergia(false);

            MakuhitaAPQ.verFondo(false);
            MakuhitaAPQ.verNombre(false);
            MakuhitaAPQ.verFilaVida(false);
            MakuhitaAPQ.verFilaEnergia(false);
            MakuhitaAPQ.verPocionVida(false);
            MakuhitaAPQ.verPocionEnergia(false);

            //CharizardASM.verFondo(false);
            //CharizardASM.verNombre(false);
            //CharizardASM.verFilaVida(false);
            //CharizardASM.verFilaEnergia(false);
            //CharizardASM.verPocionVida(false);
            //CharizardASM.verPocionEnergia(false);
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

    }
}
