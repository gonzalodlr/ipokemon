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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MisPokemonPage : Page
    {
        public MisPokemonPage()
        {
            this.InitializeComponent();
            //...
            ApplicationView.GetForCurrentView().VisibleBoundsChanged
            += UcRatingText_VisibleBoundsChanged;
            miGolbat.verFondo(false);
            miGolbat.verNombre(false);
            miGolbat.verFilaVida(false);
            miGolbat.verFilaEnergia(false);
            miGolbat.verPocionVida(false);
            miGolbat.verPocionEnergia(false);
            
            tbNombre.Text = "Nombre: " + miGolbat.Nombre;
            tbCategoria.Text = "Categoría: " + miGolbat.Categoría;
            tbDesc.Text = "Descripción: " + miGolbat.Descripcion;
        }
        private void UcRatingText_VisibleBoundsChanged(ApplicationView sender, object args)
        {
            var Width = ApplicationView.GetForCurrentView().VisibleBounds.Width;

            if (Width >= 900)
            {
                RelativePanel.SetBelow(tbNombre, null);
                RelativePanel.SetRightOf(tbNombre, miGolbat);
                RelativePanel.SetRightOf(tbCategoria, miGolbat);
                RelativePanel.SetRightOf(tbDesc, miGolbat);

                RelativePanel.SetBelow(tbCategoria, tbNombre);
                RelativePanel.SetBelow(tbDesc, tbCategoria);
            }
            else
            {
                RelativePanel.SetRightOf(tbNombre, null);
                RelativePanel.SetRightOf(tbCategoria, null);
                RelativePanel.SetRightOf(tbDesc, null);
                RelativePanel.SetBelow(tbNombre, miGolbat);
                RelativePanel.SetBelow(tbCategoria, tbNombre);
                RelativePanel.SetBelow(tbDesc, tbCategoria);
            }
        }
    }
    }
