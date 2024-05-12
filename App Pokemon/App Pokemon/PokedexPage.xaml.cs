using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Contacts;

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
        private iPokemon pokemon_seleccionado;


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

        //private void OnFilterChanged(object sender, TextChangedEventArgs args)
        //{
        //    // This is a Linq query that selects only items that return True after being passed through
        //    // the Filter function, and adds all of those selected items to filtered.
        //    var filtered = allContacts.Where(contact => Filter(contact));
        //    Remove_NonMatching(filtered);
        //    AddBack_Contacts(filtered);
        //}
        //private void Remove_NonMatching(IEnumerable<Contact> filteredData)
        //{
        //    for (int i = contacts3Filtered.Count - 1; i >= 0; i--)
        //    {
        //        var item = contacts3Filtered[i];
        //        // If contact is not in the filtered argument list, remove it from the ListView's source.
        //        if (!filteredData.Contains(item))
        //        {
        //            contacts3Filtered.Remove(item);
        //        }
        //    }
        //}
        //private bool Filter(Contact contact)
        //{
        //    return contact.FirstName.Contains(FilterByFirstName.Text, StringComparison.InvariantCultureIgnoreCase) &&
        //            contact.LastName.Contains(FilterByLastName.Text, StringComparison.InvariantCultureIgnoreCase) &&
        //            contact.Company.Contains(FilterByCompany.Text, StringComparison.InvariantCultureIgnoreCase);
        //}


        //private void AddBack_Contacts(IEnumerable<Contact> filteredData)
        //{
        //    foreach (var item in filteredData)
        //    {
        //        // If item in filtered list is not currently in ListView's source collection, add it back in
        //        if (!contacts3Filtered.Contains(item))
        //        {
        //            contacts3Filtered.Add(item);
        //        }
        //    }
        //}


        private void PokemonListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilteredListView.SelectedItem != null)
            {
                var selectedPokemon = FilteredListView.SelectedItem as iPokemon;
                pokemonDetailGrid.DataContext = selectedPokemon;
                pokemonDetailGrid.Visibility = Visibility.Visible;
                pokemon_seleccionado = selectedPokemon;
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

    }
}
