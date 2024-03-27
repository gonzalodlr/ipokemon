using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace IPokemon
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dtTime;
        MediaPlayer mpSonidos;
        Storyboard sbIde;
        Storyboard moverAlas;
        Storyboard moverPatas;
        Storyboard moverCola;

        public MainPage()
        {
            this.InitializeComponent();
            // Iniciar la animación de movimiento
            StartAnimation();
            //sbIde = (Storyboard)this.Resources["start"];
            //sbIde.AutoReverse = true;
            //sbIde.RepeatBehavior = RepeatBehavior.Forever;
            //sbIde.Begin();

            //1. Hacer interactivo nuestro Pokemon para que atienda a eventos de teclado
            this.IsTabStop = true;

            //2. Asignar el manejador de eventos de teclado
            this.KeyDown += ControlTeclas;

        }
        //Controlar	evento de teclado y asignar cada animación a una tecla
        private async void ControlTeclas(object sender, KeyRoutedEventArgs e)
        {
            Storyboard sbaux;
            mpSonidos = new MediaPlayer();
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1:
                    sbaux = (Storyboard)this.Resources["ataque_lanzallamas"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/lanzallamas.mp3"));
                    
                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number2:
                    sbaux = (Storyboard)this.Resources["ataque_fuerte"];
                    // Configuro los audios
                    MediaPlayer mpSonidos1 = new MediaPlayer();
                    MediaPlayer mpSonidos2 = new MediaPlayer();
                    mpSonidos1.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/lanzallamas.mp3"));
                    mpSonidos2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/esquivar.mp3"));
                    // inicio la animacion y el audio 1
                    mpSonidos2.Play();
                    sbaux.SpeedRatio = 0.5;
                    sbaux.Begin();
                    // cuando pasan 4,5 segundos paro el audio 1, inicio el audio 2
                    await Task.Delay(4500);
                    
                    mpSonidos2.Pause();
                    mpSonidos1.Play();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos1.Pause(); mpSonidos2.Pause();};
                    break;
                case Windows.System.VirtualKey.Number3:
                    sbaux = (Storyboard)this.Resources["defensa_desaparecer"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/esquivar.mp3"));

                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number4:
                    sbaux = (Storyboard)this.Resources["defensa_escudo"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/escudo.mp3"));

                    sbaux.SpeedRatio = 0.5;
                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number5:
                    
                    sbaux = (Storyboard)this.Resources["estado_herido"];                    
                    sbaux.AutoReverse = true;
                    sbaux.RepeatBehavior = RepeatBehavior.Forever;
                    sbaux.Begin();

                    break;
                case Windows.System.VirtualKey.Number6:
                    StopAnimation();
                    sbaux = (Storyboard)this.Resources["estado_derrotado"];
                    
                    sbaux.RepeatBehavior = RepeatBehavior.Forever;
                    sbaux.Begin();

                    break;
            }
        }

        private void StartAnimation()
        {
            // Encuentra las animaciones individuales en los recursos de la página
            moverAlas = (Storyboard)this.Resources["mover_alas"];
            moverPatas = (Storyboard)this.Resources["mover_patas"];
            moverCola = (Storyboard)this.Resources["mover_cola"];
            
            // Comienza la animación
            moverAlas.Begin();
            moverPatas.Begin();
            moverCola.Begin();
        }

        private void StopAnimation()
        {
            // Detiene la animación
            moverAlas.Stop();
            moverPatas.Stop();
            moverCola.Stop();
        }

        private void usePotionRed(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseHealth;
            dtTime.Start();
            this.Pocion_Vida.Opacity = 0.5;
        }
        private void increaseHealth(object sender, object e)
        {
            this.ProgressBar_vida.Value += 0.2;
            if (ProgressBar_vida.Value >= 100)
            {
                this.dtTime.Stop();
                this.Pocion_Vida.Opacity = 1;
            }
        }

        private void usePotionYellow(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseShield;
            dtTime.Start();
            this.Pocion_Escudo.Opacity = 0.5;
        }
        private void increaseShield(object sender, object e)
        {
            this.ProgressBar_escudo.Value += 0.2;
            if (ProgressBar_escudo.Value >= 100)
            {
                this.dtTime.Stop();
                this.ProgressBar_escudo.Opacity = 1;
            }
        }
    }
}
