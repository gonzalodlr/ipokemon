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
        private int turnoActual = 1; // 1 para jugador 1, 2 para jugador 2
        //private iPokemon pokemonJugador1;
        //private iPokemon pokemonJugador2;
        private UserControl pokemonControlJugador1;
        private UserControl pokemonControlJugador2;
        //private TextBlock tbTurno; // Mostrará de quién es el turno
        private double ataque_Fuerte = 25;
        private double ataque_Debil = 15;
        // defensa recupera energia
        private double defensa = 15;
        // descanso recupera vida
        private double recuperacion = 25;


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
            pokemonControlJugador1 = CrearPokemonControl(pokemonJugador1);
            pokemonControlJugador2 = CrearPokemonControl(pokemonJugador2);

            vb_Pokemon1.Child = pokemonControlJugador1;
            vb_Pokemon2.Child = pokemonControlJugador2;
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
                _ => throw new InvalidOperationException("Tipo de Pokémon no soportado"),
            };

            if (control is iPokemon pokemonControl)
            {
                pokemonControl.verFondo(false);
                pokemonControl.verPocionVida(false);
                pokemonControl.verPocionEnergia(false);
                pokemonControl.verNombre(false);
                // inicialmente los pokemons tienen 100 de vida y 100 de energía
                pokemonControl.Vida = 100;
                pokemonControl.Energia = 100;
            }

            return control;


            // Continúa con otros casos según tus necesidades
        }

        public class MyNavigationParameters
        {
            public ClassLibrary1_Prueba.iPokemon Jugador1 { get; set; }
            public ClassLibrary1_Prueba.iPokemon Jugador2 { get; set; }
        }

        private void CambiarTurno()
        {
            turnoActual = turnoActual == 1 ? 2 : 1;
            tbTurno.Text = $"Turno del Jugador {turnoActual}";
        }

        private void MostrarMensajeFinal(string mensaje)
        {
            tbTurno.Text = mensaje;
            
        }

        private void CheckGameOver()
        {
            if ((pokemonControlJugador1 as iPokemon).Vida <= 0)
            {
                (pokemonControlJugador1 as iPokemon).animacionDerrota();
                MostrarMensajeFinal("Pokemon 2 ha ganado");
                // Detener el juego o reiniciar
            }
            else if ((pokemonControlJugador2 as iPokemon).Vida <= 0)
            {
                (pokemonControlJugador2 as iPokemon).animacionDerrota();
                MostrarMensajeFinal("Pokemon 1 ha ganado");
                // Detener el juego o reiniciar
            }
        }

        //private void EjecutarAccion(Action<iPokemon> accion)
        //{
        //    var controlActivo = turnoActual == 1 ? pokemonControlJugador1 : pokemonControlJugador2;
        //    if (controlActivo is iPokemon pokemonActivo)
        //    {
        //        accion(pokemonActivo);
        //        pokemonActivo.

        //    }
        //    CambiarTurno();
        //}

        //private void Btn_ataque_fuerte_Click(object sender, RoutedEventArgs e) => EjecutarAccion(pokemon => pokemon.animacionAtaqueFuerte());
        //private void Btn_ataque_flojo_Click(object sender, RoutedEventArgs e) => EjecutarAccion(pokemon => pokemon.animacionAtaqueFlojo());
        //private void Btn_descanso_Click(object sender, RoutedEventArgs e) => EjecutarAccion(pokemon => pokemon.animacionDescasar());
        //private void Btn_defensa_Click(object sender, RoutedEventArgs e) => EjecutarAccion(pokemon => pokemon.animacionDefensa());

        private void Btn_ataque_fuerte_Click(object sender, RoutedEventArgs e) {
            var controlActivo1 = pokemonControlJugador1;
            var controlActivo2 = pokemonControlJugador2;

            if (turnoActual == 1)
            {

                if (controlActivo1 is iPokemon pokemonActivo)
                {
                    pokemonActivo.animacionAtaqueFuerte();
                    if (controlActivo2 is iPokemon pokemonActivo2)
                    {
                        pokemonActivo2.Vida -= ataque_Fuerte;
                    }
                }
                CambiarTurno();
                CheckGameOver();
                return;
            }
            else
            {

                if (controlActivo2 is iPokemon pokemonActivo3)
                {
                    pokemonActivo3.animacionAtaqueFuerte();
                    if (controlActivo1 is iPokemon pokemonActivo4)
                    {
                        pokemonActivo4.Vida -= ataque_Fuerte;
                    }
                }
                CambiarTurno();
                CheckGameOver();
                return;
            }
        }
        private void Btn_ataque_flojo_Click(object sender, RoutedEventArgs e)
        {
            var controlActivo1 = pokemonControlJugador1;
            var controlActivo2 = pokemonControlJugador2;

            if (turnoActual == 1)
            {

                if (controlActivo1 is iPokemon pokemonActivo)
                {
                    pokemonActivo.animacionAtaqueFlojo();
                    if (controlActivo2 is iPokemon pokemonActivo2)
                    {
                        pokemonActivo2.Vida -= ataque_Debil;
                    }
                }
                CambiarTurno();
                CheckGameOver();
                return;
            }
            else
            {

                if (controlActivo2 is iPokemon pokemonActivo3)
                {
                    pokemonActivo3.animacionAtaqueFlojo();
                    if (controlActivo1 is iPokemon pokemonActivo4)
                    {
                        pokemonActivo4.Vida -= ataque_Debil;
                    }
                }
                CambiarTurno();
                CheckGameOver();    
                return;
            }
        }
        private void Btn_descanso_Click(object sender, RoutedEventArgs e)
        {
            if (turnoActual == 1)
            {
                var controlActivo = pokemonControlJugador1;
                if (controlActivo is iPokemon pokemonActivo)
                {
                    pokemonActivo.animacionDescasar();
                    pokemonActivo.Vida += recuperacion;
                }
                CambiarTurno();
                CheckGameOver();
            }
            else
            {
                var controlActivo = pokemonControlJugador2;
                if (controlActivo is iPokemon pokemonActivo)
                {
                    pokemonActivo.animacionDescasar();
                    pokemonActivo.Vida += recuperacion;
                }
                CambiarTurno();
                CheckGameOver();
            }
        }

        private void Btn_defensa_Click(object sender, RoutedEventArgs e)
        {
            var controlActivo = turnoActual == 1 ? pokemonControlJugador1 : pokemonControlJugador2;
            if (controlActivo is iPokemon pokemonActivo)
            {
                pokemonActivo.animacionDefensa();
                pokemonActivo.Energia += defensa;

            }
            CambiarTurno();
            CheckGameOver();
        }
    }
}

