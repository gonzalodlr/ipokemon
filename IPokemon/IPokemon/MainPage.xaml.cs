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

        public MainPage()
        {
            this.InitializeComponent();
            StartAnimation();

        }
        private void StartAnimation()
        {
            // Encuentra el Storyboard en los recursos de la página
            Storyboard storyboard = (Storyboard)this.Resources["mover_alas"];
            Storyboard storyboard2 = (Storyboard)this.Resources["mover_pata"];
            Storyboard storyboard3 = (Storyboard)this.Resources["mover_cola"];
            //Storyboard storyboard4 = (Storyboard)this.Resources["defensa_esquivar"];

            // Comienza la animación
            storyboard.Begin();
            storyboard2.Begin();
            storyboard3.Begin();
            //storyboard4.Begin();
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
