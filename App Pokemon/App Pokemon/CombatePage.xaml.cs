using System;
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
using ClassLibrary1_Prueba;
using Dracofire;
using Sesion4;
using LucarioGAC;
using ClassLibrary1_Prueba;
using static App_Pokemon.EligePokemonPage;
using ControlUsuario_IPO2;
using Pokemon_Antonio_Campallo_Gomez;
using ToxicroackJPG;
using piplupUserControl;


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
            System.Diagnostics.Debug.WriteLine($"Tipo de parámetro recibido: {e.Parameter.GetType()}");

            if (e.Parameter is PokemonSelectionParameters parameters)
            {
                MostrarPokemons(parameters.Jugador1, parameters.Jugador2);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("El parámetro no es del tipo esperado.");
                this.Frame.Navigate(typeof(InicioCombate)); // O manejar el error adecuadamente
            }

            // Verifica si el parámetro de navegación es una cadena
            /*
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
            }*/
        }

        private void MostrarPokemons(iPokemon pokemonJugador1, iPokemon pokemonJugador2)
        {
            // Asumiendo que tienes un método para crear un UserControl basado en iPokemon
            UserControl pokemonControl1 = CrearPokemonControl(pokemonJugador1);
            UserControl pokemonControl2 = CrearPokemonControl(pokemonJugador2);

            vb_Pokemon1.Child = pokemonControl1;
            vb_Pokemon2.Child = pokemonControl2;
        }


        private UserControl CrearPokemonControl(iPokemon pokemon)
        {
            UserControl control = null;
            // Aquí necesitas determinar qué UserControl utilizar basado en el tipo de Pokémon
            // Ejemplo simplificado:
            if (pokemon is DracofireGDLRS)
            {
                control = new DracofireGDLRS();
                

            }
            else if (pokemon is GengarJCC)
            {
                control = new GengarJCC();
            }

            else if (pokemon is MyUCLucario)
            {

                control = new MyUCLucario();
            }
            else if (pokemon is DragoniteCSD)
            {
                control = new DragoniteCSD();
            }
            else if (pokemon is ArticunoACG)
            {
                control = new ArticunoACG();
            }
            /*else if (pokemon is ToxicroackJPG)
            {
                return new ToxicroackJPG();
            }*/
            else { 
                throw new InvalidOperationException("Tipo de Pokémon no soportado");
            }

            if (control is ClassLibrary1_Prueba.iPokemon pokemonControl)
            {
                // Configura los ajustes visuales como deseas que aparezcan en la pantalla de combate
                pokemonControl.verFondo(false);
                pokemonControl.verPocionVida(false);
                pokemonControl.verPocionEnergia(false);
                pokemonControl.verNombre(false);  
            }
            else
            {
                throw new InvalidOperationException("El control creado no implementa iPokemon correctamente.");
            }
            return control;


            // Continúa con otros casos según tus necesidades
        }

        public class MyNavigationParameters
        {
            public ClassLibrary1_Prueba.iPokemon Jugador1 { get; set; }
            public ClassLibrary1_Prueba.iPokemon Jugador2 { get; set; }
        }

        
        



    }
}
