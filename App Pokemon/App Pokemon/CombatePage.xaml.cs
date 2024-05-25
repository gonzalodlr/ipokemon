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
using ClassLibrary1_Prueba;
using Dracofire;
using Sesion4;
using LucarioGAC;
using static App_Pokemon.EligePokemonPage;
using ControlUsuario_IPO2;
using Pokemon_Antonio_Campallo_Gomez;
using ToxicroackJPG;
using piplupUserControl;
using Windows.UI.Core;
using System.Threading.Tasks;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace App_Pokemon
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CombatePage : Page
    {
        private int turnoActual = 1; // 1 para jugador 1, 2 para jugador 2
        private string modoDeJuego;

        private UserControl pokemonControlJugador1;
        private UserControl pokemonControlJugador2;
  
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

            if (e.Parameter is PokemonSelectionParameters parameters)
            {
                MostrarPokemons(parameters.Jugador1, parameters.Jugador2);
                modoDeJuego = parameters.ModoDeJuego; // Inicializamos el modo de juego
            }
            else
            {
                this.Frame.Navigate(typeof(InicioCombate));
            }

        }

        private void MostrarPokemons(iPokemon pokemonJugador1, iPokemon pokemonJugador2)
        {
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
                ToxicroackJPG.ToxicroackJPG _ => new ToxicroackJPG.ToxicroackJPG(),
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

            if (modoDeJuego == "solo" && turnoActual == 2)
            {
                RealizarAccionAutomatica();
            }
        }

        private void InicializarJuego()
        {
            
            turnoActual = 1; 
            tbTurno.Text = "Turno del Jugador 1";
            (pokemonControlJugador1 as iPokemon).Vida = 100;
            (pokemonControlJugador2 as iPokemon).Vida = 100;
            MostrarPokemons((pokemonControlJugador1 as iPokemon), (pokemonControlJugador2 as iPokemon));

            // Rehabilitamos los botones
            btn_ataque_fuerte.IsEnabled = true;
            btn_ataque_flojo.IsEnabled = true;
            btn_descanso.IsEnabled = true;
            btn_defensa.IsEnabled = true;
        }

        private void MostrarMensajeFinal(string mensaje)
        {
            tbTurno.Text = mensaje;
            tbTurno.Visibility = Visibility.Collapsed;
            GameEndPanel.Visibility = Visibility.Visible;

        }

        private void MostrarMensajeenGrid(string mensaje)
        {
            tbGanador.Text = mensaje;
            //GameEndPanel.Visibility = Visibility.Visible;

        }

        private void DesactivarAcciones()
        {
            btn_ataque_fuerte.IsEnabled = false;
            btn_ataque_flojo.IsEnabled = false;
            btn_descanso.IsEnabled = false;
            btn_defensa.IsEnabled = false;
        }

        private void CheckGameOver()
        {
            if ((pokemonControlJugador1 as iPokemon).Vida <= 0)
            {
                (pokemonControlJugador1 as iPokemon).animacionDerrota();
                MostrarMensajeFinal("Pokemon 2 ha ganado");
                MostrarMensajeenGrid("¡Jugador 2 ha ganado!");
                DesactivarAcciones();
            }
            else if ((pokemonControlJugador2 as iPokemon).Vida <= 0)
            {
                (pokemonControlJugador2 as iPokemon).animacionDerrota();
                MostrarMensajeFinal("Pokemon 1 ha ganado");
                MostrarMensajeenGrid("¡Jugador 1 ha ganado!");
                DesactivarAcciones();
                
            }

            if ((pokemonControlJugador1 as iPokemon).Vida < 50)
            {
                (pokemonControlJugador1 as iPokemon).animacionHerido();
            }

            if ((pokemonControlJugador2 as iPokemon).Vida < 50)
            {
                (pokemonControlJugador2 as iPokemon).animacionHerido();
            }
        }


        private void Btn_ataque_fuerte_Click(object sender, RoutedEventArgs e)
        {
            if (modoDeJuego == "solo" && turnoActual == 2)
            {
                RealizarAtaqueFuerte(pokemonControlJugador2, pokemonControlJugador1);
            }
            else
            {
                var controlActivo1 = pokemonControlJugador1;
                var controlActivo2 = pokemonControlJugador2;

                if (turnoActual == 1)
                {
                    RealizarAtaqueFuerte(controlActivo1, controlActivo2);
                }
                else
                {
                    RealizarAtaqueFuerte(controlActivo2, controlActivo1);
                }
            }
        }

        private void RealizarAtaqueFuerte(UserControl atacante, UserControl defensor)
        {
            if (atacante is iPokemon pokemonAtacante)
            {
                pokemonAtacante.animacionAtaqueFuerte();
                if (defensor is iPokemon pokemonDefensor)
                {
                    pokemonDefensor.Vida -= ataque_Fuerte;
                }
            }
            CambiarTurno();
            CheckGameOver();
        }


        private void Btn_ataque_flojo_Click(object sender, RoutedEventArgs e)
        {
            if (modoDeJuego == "solo" && turnoActual == 2)
            {
                RealizarAtaqueFlojo(pokemonControlJugador2, pokemonControlJugador1);
            }
            else
            {
                var controlActivo1 = pokemonControlJugador1;
                var controlActivo2 = pokemonControlJugador2;

                if (turnoActual == 1)
                {
                    RealizarAtaqueFlojo(controlActivo1, controlActivo2);
                }
                else
                {
                    RealizarAtaqueFlojo(controlActivo2, controlActivo1);
                }
            }
        }

        private void RealizarAtaqueFlojo(UserControl atacante, UserControl defensor)
        {
            if (atacante is iPokemon pokemonAtacante)
            {
                pokemonAtacante.animacionAtaqueFlojo();
                if (defensor is iPokemon pokemonDefensor)
                {
                    pokemonDefensor.Vida -= ataque_Debil;
                }
            }
            CambiarTurno();
            CheckGameOver();
        }
        private void Btn_descanso_Click(object sender, RoutedEventArgs e)
        {
            if (modoDeJuego == "solo" && turnoActual == 2)
            {
                RealizarDescanso(pokemonControlJugador2);
            }
            else
            {
                if (turnoActual == 1)
                {
                    RealizarDescanso(pokemonControlJugador1);
                }
                else
                {
                    RealizarDescanso(pokemonControlJugador2);
                }
            }
        }

        private void RealizarDescanso(UserControl pokemon)
        {
            if (pokemon is iPokemon pokemonActivo)
            {
                pokemonActivo.animacionDescasar();
                pokemonActivo.Vida += recuperacion;
            }
            CambiarTurno();
            CheckGameOver();
        }

        private void Btn_defensa_Click(object sender, RoutedEventArgs e)
        {
            if (modoDeJuego == "solo" && turnoActual == 2)
            {
                RealizarDefensa(pokemonControlJugador2);
            }
            else
            {
                if (turnoActual == 1)
                {
                    RealizarDefensa(pokemonControlJugador1);
                }
                else
                {
                    RealizarDefensa(pokemonControlJugador2);
                }
            }
        }

        private void RealizarDefensa(UserControl pokemon)
        {
            if (pokemon is iPokemon pokemonActivo)
            {
                pokemonActivo.animacionDefensa();
                pokemonActivo.Energia += defensa;
            }
            CambiarTurno();
            CheckGameOver();
        }

        private async void RealizarAccionAutomatica()
        {
            //la máquina espera 2.5s para hacer su movimiento
            await Task.Delay(2500);

            Random random = new Random();
            int accion = random.Next(4);

            switch (accion)
            {
                case 0:
                    Btn_ataque_fuerte_Click(null, null);
                    break;
                case 1:
                    Btn_ataque_flojo_Click(null, null);
                    break;
                case 2:
                    Btn_descanso_Click(null, null);
                    break;
                case 3:
                    Btn_defensa_Click(null, null);
                    break;
            }
        }




        private void BtnRevancha_Click(object sender, RoutedEventArgs e)
        {
            GameEndPanel.Visibility = Visibility.Collapsed;
            InicializarJuego();
        }


        private void BtnIrInicio_Click(object sender, RoutedEventArgs e) //Hay que echarle un ojo a esto
        {
            // Navegar a la página MainPage
            this.Frame.Navigate(typeof(MainPage));

            // Obtener una referencia al NavigationView de MainPage
            MainPage mainPage = Window.Current.Content as MainPage;
            NavigationView navigationView = mainPage?.FindName("navigationView") as NavigationView;

            // Verificar si se obtuvo una referencia válida al NavigationView
            if (navigationView != null)
            {
                // Seleccionar el elemento "Inicio" en el NavigationView
                navigationView.SelectedItem = navigationView.MenuItems[0];
            }
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }
    }
}

