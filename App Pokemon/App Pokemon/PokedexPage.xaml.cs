using System;
using System.Collections.Generic;
using System.Drawing;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PokedexPage : Page
    {
        public List<UserControl> UserControls { get; set; }
        public List<UserControl> Items { get; set;  } = new List<UserControl>();

        //private List<UserControl> UserControls = new List<UserControl>(); // Initialize the UserControls list
        public PokedexPage()
        {
            this.InitializeComponent();
            configurar_pokedex();
            //LoadUserControlInstances();
            //ContentGridView.ItemsSource = Items;

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

            //LucarioGAC.verFondo(false);
            //LucarioGAC.verNombre(false);
            //LucarioGAC.verFilaVida(false);
            //LucarioGAC.verFilaEnergia(false);
            //LucarioGAC.verPocionVida(false);
            //LucarioGAC.verPocionEnergia(false);

            //CharizardASM.verFondo(false);
            //CharizardASM.verNombre(false);
            //CharizardASM.verFilaVida(false);
            //CharizardASM.verFilaEnergia(false);
            //CharizardASM.verPocionVida(false);
            //CharizardASM.verPocionEnergia(false);
        }

        //private void LoadUserControlInstances()
        //{
        //    Items.Add(DracofireGDLRS);
        //    Items.Add(CharizardASM);
        //    Items.Add(MyUCLucario);
        //    Items.Add(ArticunoACG);
        //}
        //private void crearListaPokemons()
        //{
        //    ViewModel viewModel = new ViewModel();

        //    UserControls = new List<UserControl>(); // Initialize the UserControls list

        //    foreach (var userControl in viewModel.UserControls)
        //    {
        //        UserControls.Add(userControl);
        //    }
        //}
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
        }

    }
}
