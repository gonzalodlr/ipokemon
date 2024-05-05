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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EligePokemonPage : Page
    {
        private UserControl pokemonCPU;

        public EligePokemonPage()
        {
            this.InitializeComponent();
            // Establece el tamaño mínimo preferido de la ventana
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1000, 1000));

            // Maneja el evento SizeChanged de la ventana
            Window.Current.SizeChanged += CurrentWindow_SizeChanged;
            configurar_pokedex();

        }

        private void CurrentWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Obtén el ancho y el alto actual de la ventana
            double width = Window.Current.Bounds.Width;
            double height = Window.Current.Bounds.Height;

            // Si el ancho es menor que 600, ajusta el ancho a 600
            if (width < 600)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size(600, height));
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

            DragoniteCSD.VerFondo(false);
            DragoniteCSD.VerNombre(false);
            DragoniteCSD.VerFilaVida(false);
            DragoniteCSD.VerFilaEnergia(false);
            DragoniteCSD.VerPocionVida(false);
            DragoniteCSD.VerPocionEnergia(false);


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
            // Aquí puedes manejar el evento de cambio de selección del GridView
            // Por ejemplo, puedes acceder al elemento seleccionado así:
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
            var clickedControl = e.ClickedItem as UserControl;
            // Haz lo que necesites con el control clickeado
            SeleccionarPokemonParaCPU();
            this.Frame.Navigate(typeof(CombatePage), new List<UserControl> { clickedControl, pokemonCPU });
        }

        private void SeleccionarPokemonParaCPU()
        {
            // Aquí seleccionas aleatoriamente un Pokémon para la CPU
            Random rnd = new Random();
            // Selecciona un índice aleatorio dentro del rango de la cantidad de controles de usuario de Pokémon
            int randomIndex = rnd.Next(0, ContentGridView.Items.Count);
            // Obtén el control de usuario de Pokémon en el índice aleatorio
            var pokemonCPU = ContentGridView.Items[randomIndex] as UserControl;
            // Haz lo que necesites con el control de usuario de Pokémon seleccionado para la CPU
            // Por ejemplo, podrías mostrar un mensaje con el nombre del Pokémon seleccionado
            //MessageBox.Show($"El Pokémon de la CPU es: {pokemonCPU.Name}");
            // O realizar alguna otra lógica de juego para la CPU
        }


    }
}
