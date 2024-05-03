using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ClassLibrary1_Prueba;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls.Maps;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace ToxicroackJPG
{
    public sealed partial class ToxicroackJPG : UserControl, iPokemon
    {
        private DispatcherTimer vidaTimer;
        private DispatcherTimer energiaTimer;
        private double vidaIncrement = 5; // Incremento de vida por intervalo de tiempo
        private double energiaIncrement = 5; // Incremento de energía por intervalo de tiempo

        public double Vida { get => this.BarraVida.Value;
                             set => this.BarraVida.Value = value; }

        public double Energia { get => this.BarraEnergia.Value;
                                set => this.BarraEnergia.Value = value; }
        public string Nombre { get => "Toxicroack"; set => throw new NotImplementedException(); }
        public string Categoría { get => "Veneno"; set => throw new NotImplementedException(); }
        public string Tipo { get => "Lucha"; set => throw new NotImplementedException(); }
        public double Altura { get => 1.30; set => throw new NotImplementedException(); }
        public double Peso { get => 44.4; set => throw new NotImplementedException(); }
        public string Evolucion { get => "No evoluciona"; set => throw new NotImplementedException(); }
        public string Descripcion { get => "Toxicroak es un Pokémon de tipo veneno/lucha introducido en la cuarta generación. Es la evolución de Croagunk."; set => throw new NotImplementedException(); }




        public ToxicroackJPG()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => Moverse.Begin();
            Vida=0;
            Energia=0;
            this.IsTabStop = true;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            // Inicializar los temporizadores
            vidaTimer = new DispatcherTimer();
            vidaTimer.Interval = TimeSpan.FromMilliseconds(100); // Intervalo de tiempo entre incrementos (en milisegundos)
            vidaTimer.Tick += VidaTimer_Tick;

            energiaTimer = new DispatcherTimer();
            energiaTimer.Interval = TimeSpan.FromMilliseconds(100); // Intervalo de tiempo entre incrementos (en milisegundos)
            energiaTimer.Tick += EnergiaTimer_Tick;

            Pocima_Vida.PointerReleased += Pocima_Vida_PointerReleased;
            Pocima_Energia.PointerReleased += Pocima_Energia_PointerReleased;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            // Dirigir el evento de teclado al control de usuario ToxicroackJPG
            ToxicroackJPG_KeyDown(args.VirtualKey);
        }

        private void ToxicroackJPG_KeyDown(Windows.System.VirtualKey key)
        {
            // Determinar qué tecla se presionó
            switch (key)
            {
                case Windows.System.VirtualKey.Number2:
                    // Llamar al método correspondiente al ataque débil
                    this.animacionAtaqueFlojo();
                    break;
                case Windows.System.VirtualKey.Number3:
                    // Llamar al método correspondiente al ataque fuerte
                    this.animacionAtaqueFuerte();
                    break;
                case Windows.System.VirtualKey.Number4:
                    // Llamar al método correspondiente a la defensa
                    this.animacionDefensa();
                    break;
                case Windows.System.VirtualKey.Number5:
                    this.animacionDescasar();
                    break;
                case Windows.System.VirtualKey.Number6:
                    this.animacionCansado();
                    break;
                case Windows.System.VirtualKey.Number7:
                    this.animacionHerido();
                    break;
                case Windows.System.VirtualKey.Number8:
                    this.animacionDerrota();
                    break;
                default:
                    // Si no se presionó una tecla específica que manejes, no hacer nada
                    break;
            }
        }


        private void Pocima_Vida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // Detener el temporizador de la vida si está en ejecución
            vidaTimer.Stop();
            // Iniciar el temporizador de la vida
            vidaTimer.Start();
        }

        private void Pocima_Energia_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // Detener el temporizador de la energía si está en ejecución
            energiaTimer.Stop();
            // Iniciar el temporizador de la energía
            energiaTimer.Start();
        }

        private void VidaTimer_Tick(object sender, object e)
        {
            // Incrementar la vida gradualmente
            Vida = Math.Min(Vida + vidaIncrement, 100);
            BarraVida.Value = Vida;

            // Verificar si se alcanzó el límite de vida
            if (Vida >= 100)
            {
                // Detener el temporizador de la vida
                vidaTimer.Stop();
            }
        }

        private void EnergiaTimer_Tick(object sender, object e)
        {
            // Incrementar la energía gradualmente
            Energia = Math.Min(Energia + energiaIncrement, 100);
            BarraEnergia.Value = Energia;

            // Verificar si se alcanzó el límite de energía
            if (Energia >= 100)
            {
                // Detener el temporizador de la energía
                energiaTimer.Stop();
            }
        }

        public void verFondo(bool ver)
        {
            if (ver)
            {
                // Mostrar la imagen de fondo
                FondoImagen.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la imagen de fondo
                FondoImagen.Visibility = Visibility.Collapsed;
            }
        }


        public void verFilaVida(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                BarraVida.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                BarraVida.Visibility = Visibility.Collapsed;
                this.img_Vida.Visibility = Visibility.Collapsed;
            }
        }


        public void verFilaEnergia(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                BarraEnergia.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                BarraEnergia.Visibility = Visibility.Collapsed;
                this.img_Energia.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                Pocima_Vida.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                Pocima_Vida.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                Pocima_Energia.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                Pocima_Energia.Visibility = Visibility.Collapsed;
            }
        }

        public void verNombre(bool ver)
        {
            if (ver)
            {
                // Mostrar el nombre del Pokémon
                NombrePokemon.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar el nombre del Pokémon
                this.NombrePokemon.Visibility = Visibility.Collapsed;
                //NombrePokemon.Visibility = Visibility.Collapsed;
            }
        }


        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                // Iniciar la animación "moverse"
                Moverse.Begin();
            }
        }

        public void animacionAtaqueFlojo()
        {
            // Obtener el storyboard de AtaqueDebil y suscribirse al evento Completed
            Storyboard storyboardAtaqueDebil = (Storyboard)this.Resources["AtaqueDebil"];
            MediaPlayer mpSonidos = new MediaPlayer();
            storyboardAtaqueDebil.Completed += (sender, e) =>
            {
                // Detener el sonido
                mpSonidos.Pause();
                // Una vez que la animación de AtaqueDebil se complete, activar la animación idle
                activarAniIdle(true);
            };

            // Iniciar la animación de AtaqueDebil
            storyboardAtaqueDebil.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/AtaqueDebil2.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();
        }

        public void animacionAtaqueFuerte()
        {
            // Obtener el storyboard de AtaqueFuerte y suscribirse al evento Completed
            Storyboard storyboardAtaqueFuerte = (Storyboard)this.Resources["AtaqueFuerte"];
            MediaPlayer mpSonidos = new MediaPlayer();
            storyboardAtaqueFuerte.Completed += (sender, e) =>
            {
                storyboardAtaqueFuerte.Stop();
                // Detener el sonido
                //mpSonidos.Pause();
                // Una vez que la animación de AtaqueFuerte se complete, activar la animación idle
                activarAniIdle(true);
            };

            // Iniciar la animación de AtaqueFuerte
            storyboardAtaqueFuerte.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/AtaqueFuerte.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();
        }

        public void animacionDefensa()
        {
            // Obtener el storyboard de Escudo1 y suscribirse al evento Completed
            Storyboard storyboardEscudo1 = (Storyboard)this.Resources["Escudo1"];
            storyboardEscudo1.Completed += (sender, e) =>
            {
                storyboardEscudo1.Stop();
                // Una vez que la animación de Escudo1 se complete, activar la animación idle
                activarAniIdle(true);
            };

            // Iniciar la animación de Escudo1
            storyboardEscudo1.Begin();
        }


        public void animacionDescasar()
        {
            // Iniciar la animación "Descanso"
            Storyboard storyboardDescanso = (Storyboard)this.Resources["Descanso"];
            storyboardDescanso.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/Ronquido.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Descanso"
            storyboardDescanso.Completed += (sender, e) =>
            {
                // Cuando la animación "Descanso" se complete, iniciar la animación "Mantenerse Dormido" y establecer un temporizador
                Storyboard storyboardMantenerseDormido = (Storyboard)this.Resources["MantenerseDormido"];
                storyboardMantenerseDormido.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "Mantenerse Dormido" después de 2 segundos
                    storyboardMantenerseDormido.Stop();
                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }


        public void animacionCansado()
        {
            // Iniciar la animación "Herido"
            Storyboard storyboardCansado = (Storyboard)this.Resources["Cansado"];
            storyboardCansado.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/Bostezo.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Herido"
            storyboardCansado.Completed += (sender, e) =>
            {
                // Cuando la animación "Herido" se complete, iniciar la animación "MantenerHerido" y establecer un temporizador
                Storyboard storyboardEstarCansado = (Storyboard)this.Resources["EstarCansado"];
                storyboardEstarCansado.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "MantenerHerido" después de 2 segundos
                    storyboardEstarCansado.Stop();
                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }

        public void animacionNoCansado()
        {
            Moverse.Begin();
        }

        public void animacionHerido()
        {
            // Iniciar la animación "Herido"
            Storyboard storyboardHerido = (Storyboard)this.Resources["Herido"];
            storyboardHerido.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/Herido.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Herido"
            storyboardHerido.Completed += (sender, e) =>
            {
                // Cuando la animación "Herido" se complete, iniciar la animación "MantenerHerido" y establecer un temporizador
                Storyboard storyboardMantenerHerido = (Storyboard)this.Resources["MantenerHerido"];
                storyboardMantenerHerido.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "MantenerHerido" después de 2 segundos
                    storyboardMantenerHerido.Stop();

                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }


        public void animacionNoHerido()
        {
            Moverse.Begin();
        }

        public void animacionDerrota()
        {
            // Obtener el storyboard de la animación "Muerte" y suscribirse al evento Completed
            Storyboard storyboardMuerte = (Storyboard)this.Resources["Muerte"];
            storyboardMuerte.Completed += (sender, e) =>
            {
                // Una vez que la animación de "Muerte" se complete, realizar las acciones necesarias
                // Por ejemplo, detener la animación, ocultar elementos, etc.
            };

            // Iniciar la animación de "Muerte"
            storyboardMuerte.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Pokemon+inicial/Sonidos/Muerte.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();
        }


    }
}
