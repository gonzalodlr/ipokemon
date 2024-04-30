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
            //LoadUserControlInstances();
            //ContentGridView.ItemsSource = Items;

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


    }
}
