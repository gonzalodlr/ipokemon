﻿using System;
using System.Collections.Generic;
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
    public sealed partial class CombatePage : Page
    {
        public CombatePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Verifica si el parámetro de navegación es una cadena
            if (e.Parameter is string modo)
            {
                if (modo == "solo")
                {
                    // Realiza acciones específicas para el modo solo
                    // Por ejemplo:
                    // Hacer algo...
                }
                else if (modo == "multijugador")
                {
                    // Realiza acciones específicas para el modo multijugador
                    // Por ejemplo:
                    // Hacer algo diferente...
                }
                else
                {
                    this.Frame.Navigate(typeof(InicioCombate));
                }
            }
        }

    }
}