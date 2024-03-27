using ClassLibrary1_Prueba;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dracofire
{
    public sealed partial class DracofireGDLRS : UserControl, iPokemon
    {
        DispatcherTimer dtTime;
        MediaPlayer mpSonidos;
        
        Storyboard sbaux;
        Storyboard moverAlas;
        Storyboard moverPatas;
        Storyboard moverCola;

        double iPokemon.Vida { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Energia { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nombre { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Categoría { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Tipo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Altura { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Peso { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Evolucion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Descripcion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DracofireGDLRS()
        {
            this.InitializeComponent();
            // Iniciar la animación de movimiento
            StartAnimation();

            //1. Hacer interactivo nuestro Pokemon para que atienda a eventos de teclado
            this.IsTabStop = true;

            //2. Asignar el manejador de eventos de teclado
            this.KeyDown += ControlTeclas;
        }

        //Controlar	evento de teclado y asignar cada animación a una tecla
        private async void ControlTeclas(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1:
                    sbaux = (Storyboard)this.Resources["ataque_lanzallamas"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/lanzallamas.mp3"));

                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number2:
                    sbaux = (Storyboard)this.Resources["ataque_fuerte"];
                    // Configuro los audios
                    MediaPlayer mpSonidos1 = new MediaPlayer();
                    MediaPlayer mpSonidos2 = new MediaPlayer();
                    mpSonidos1.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/lanzallamas.mp3"));
                    mpSonidos2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/esquivar.mp3"));
                    // inicio la animacion y el audio 1
                    mpSonidos2.Play();
                    sbaux.SpeedRatio = 0.5;
                    sbaux.Begin();
                    // cuando pasan 4,5 segundos paro el audio 1, inicio el audio 2
                    await Task.Delay(4500);

                    mpSonidos2.Pause();
                    mpSonidos1.Play();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos1.Pause(); mpSonidos2.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number3:
                    sbaux = (Storyboard)this.Resources["defensa_desaparecer"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/esquivar.mp3"));

                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number4:
                    sbaux = (Storyboard)this.Resources["defensa_escudo"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/escudo.mp3"));

                    sbaux.SpeedRatio = 0.5;
                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
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

        public void verFondo(bool ver)
        {
            if (!ver) { this.imFondo.Source = null; }
            else
            {
                this.imFondo.Source = new BitmapImage(new Uri("ms-appx:///AssetsDracofireGDLRS/escenario2.jpg"));
            }
        }

        public void verFilaVida(bool ver)
        {
            if (!ver) { this.ProgressBar_vida.Visibility = Visibility.Collapsed; }
            else { this.ProgressBar_vida.Visibility = Visibility.Visible; }
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver) { this.ProgressBar_escudo.Visibility = Visibility.Collapsed; }
            else { this.ProgressBar_escudo.Visibility = Visibility.Visible; }
        }

        public void verPocionVida(bool ver)
        {
            if (!ver) { this.Pocion_Vida.Visibility = Visibility.Collapsed; }
            else { this.Pocion_Vida.Visibility = Visibility.Visible; }
        }

        public void verPocionEnergia(bool ver)
        {
            if(!ver) { this.Pocion_Escudo.Visibility = Visibility.Collapsed; }
            else { this.Pocion_Escudo.Visibility = Visibility.Visible; }
        }

        public void verNombre(bool ver)
        {
            if (!ver) { this.Nombre_Pokemon.Visibility = Visibility.Collapsed; }
            else { this.Nombre_Pokemon.Visibility = Visibility.Visible; }
        }

        public void activarAniIdle(bool activar)
        {
            if (activar) { StartAnimation(); }
            else { StopAnimation(); }
        }

        public void animacionAtaqueFlojo()
        {
            sbaux = (Storyboard)this.Resources["ataque_lanzallamas"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/lanzallamas.mp3"));

            mpSonidos.Play();
            sbaux.Begin();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
        }

        public void animacionAtaqueFuerte()
        {
            Task task = animacionAtaqueFuerteAsync();
        }

        public async Task animacionAtaqueFuerteAsync()
        {
            sbaux = (Storyboard)this.Resources["ataque_fuerte"];
            // Configuro los audios
            MediaPlayer mpSonidos1 = new MediaPlayer();
            MediaPlayer mpSonidos2 = new MediaPlayer();
            mpSonidos1.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/lanzallamas.mp3"));
            mpSonidos2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/esquivar.mp3"));
            // inicio la animacion y el audio 1
            mpSonidos2.Play();
            sbaux.SpeedRatio = 0.5;
            sbaux.Begin();
            // cuando pasan 4,5 segundos paro el audio 1, inicio el audio 2
            await Task.Delay(4500);

            mpSonidos2.Pause();
            mpSonidos1.Play();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos1.Pause(); mpSonidos2.Pause(); };
        }

        public void animacionDefensa()
        {
            sbaux = (Storyboard)this.Resources["defensa_desaparecer"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/esquivar.mp3"));

            mpSonidos.Play();
            sbaux.Begin();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
        }

        public void animacionDescasar()
        {
            sbaux = (Storyboard)this.Resources["defensa_escudo"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/escudo.mp3"));

            sbaux.SpeedRatio = 0.5;
            mpSonidos.Play();
            sbaux.Begin();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
        }

        public void animacionCansado()
        {
            
        }

        public void animacionNoCansado()
        {
            StartAnimation();
        }

        public void animacionHerido()
        {
            StartAnimation();
            sbaux = (Storyboard)this.Resources["estado_herido"];
            sbaux.AutoReverse = true;
            sbaux.RepeatBehavior = RepeatBehavior.Forever;
            sbaux.Begin();
        }

        public void animacionNoHerido()
        {
            StartAnimation();
        }

        public void animacionDerrota()
        {
            StopAnimation();
            sbaux = (Storyboard)this.Resources["estado_derrotado"];

            sbaux.RepeatBehavior = RepeatBehavior.Forever;
            sbaux.Begin();
        }
    }
}
