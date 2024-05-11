using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ClassLibrary1_Prueba;
using System.Diagnostics;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EligePokemonPage : Page
    {
        private List<iPokemon> pokemonsSeleccionados = new List<iPokemon>();
        private int jugadorActual = 1;
        private string modoDeJuego;
        private iPokemon seleccionJugador1;
        private iPokemon seleccionJugador2;
        private iPokemon pokemonTempSeleccionado;

        public EligePokemonPage()
        {
            this.InitializeComponent();
            // Establece el tamaño mínimo preferido de la ventana
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1000, 1000));

            // Maneja el evento SizeChanged de la ventana
            Window.Current.SizeChanged += CurrentWindow_SizeChanged;
            configurar_pokedex();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            modoDeJuego = e.Parameter as string;
            if (modoDeJuego == "multijugador")
            {
                MostrarMensaje("Jugador 1, escoge un Pokémon");
            }

        }

        private void CurrentWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Obtén el ancho y el alto actual de la ventana
            double width = Window.Current.Bounds.Width;
            double height = Window.Current.Bounds.Height;

            // Si el ancho es menor que 600, ajusta el ancho a 600
            if (width < 600)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size(700, height));
            }

            // Si la altura es menor que 600, ajusta la altura a 600
            if (height < 600)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size(width, 600));
            }
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


            //CharizardASM.verFondo(false);
            //CharizardASM.verNombre(false);
            //CharizardASM.verFilaVida(false);
            //CharizardASM.verFilaEnergia(false);
            //CharizardASM.verPocionVida(false);
            //CharizardASM.verPocionEnergia(false);
        }

        private void PokemonPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        private void PokemonPointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }

        private void ContentGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentGridView.SelectedItem != null)
            {
                var selectedControl = ContentGridView.SelectedItem as UserControl;

                // Haz lo que necesites con el control seleccionado
            }
           
        }

        private void ContentGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Aquí puedes manejar el evento de clic en un elemento del GridView
            // Por ejemplo, puedes acceder al elemento haciendo casting del argumento e:
            //var clickedControl = e.ClickedItem as UserControl;
            // Haz lo que necesites con el control clickeado
            var clickedItem = e.ClickedItem as iPokemon;
            if (clickedItem != null)
            {
                pokemonTempSeleccionado = clickedItem;
                MostrarMensaje($"Jugador {jugadorActual}, ha seleccionado {clickedItem.Nombre}. Confirme o seleccione otro.");

            }
            else
            {
                Debug.WriteLine("Clicked item is not a Pokemon.");
                Debug.WriteLine($"Clicked item: {clickedItem}");
            }


        }

        private void ConfirmarSeleccion_Click(object sender, RoutedEventArgs e)
        {
            if (pokemonTempSeleccionado != null)
            {
                if (jugadorActual == 1)
                {
                    seleccionJugador1 = pokemonTempSeleccionado;
                    jugadorActual = 2;
                    pokemonTempSeleccionado = null; // Resetea la selección temporal
                    MostrarMensaje("Jugador 2, escoge un Pokémon");
                }
                else if (jugadorActual == 2)
                {
                    seleccionJugador2 = pokemonTempSeleccionado;
                    NavegarAPantallaDeCombate();
                }
            }
            else
            {
                MostrarMensaje("Seleccione un Pokémon antes de confirmar.");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            MensajeTextBlock.Text = mensaje;

        }

        public class PokemonSelectionParameters
        {
            public iPokemon Jugador1 { get; set; }
            public iPokemon Jugador2 { get; set; }
        }

        private void NavegarAPantallaDeCombate()
        {
            var parameters = new PokemonSelectionParameters
            {
                Jugador1 = seleccionJugador1,
                Jugador2 = seleccionJugador2
            };

            //Frame.Navigate(typeof(CombatePage), new { Jugador1 = seleccionJugador1, Jugador2 = seleccionJugador2 });
            Frame.Navigate(typeof(CombatePage), parameters);
        }




    }
}
