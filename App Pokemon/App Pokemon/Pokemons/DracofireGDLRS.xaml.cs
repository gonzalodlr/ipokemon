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

        public double Vida
        { 
            get { return ProgressBar_vida.Value; }
            set { ProgressBar_vida.Value = value; }
        }
        public double Energia { 
            get { return ProgressBar_escudo.Value; }
            set { ProgressBar_escudo.Value = value; }
        }
        public string Nombre { 
            get { return Nombre_Pokemon_Texto.Text; }
            set { Nombre_Pokemon_Texto.Text = value; }
        }
        private string categoria = "Dragón";
        public string Categoría { 
            get { return categoria; }
            set { categoria = value; }
        }
        private string tipo = "Fuego";
        public string Tipo { 
            get { return tipo; }
            set { tipo = value; }
        }
        private double altura = 4.3;
        public double Altura { 
            get { return altura; }
            set { altura = value; }
        }
        private double peso = 580.75;
        public double Peso { 
            get { return peso; }
            set { peso = value; }
        }
        private string evolucion = "Octopusfire";
        public string Evolucion { 
            get { return evolucion; }
            set { evolucion = value; }
        }
        private string descripcion = "Dracofire es un dragón de fuego que habita en las montañas de la región de España. Es un Pokémon muy poderoso y temido por su aliento de fuego.";
        public string Descripcion { 
            get { return descripcion; }
            set { descripcion = value; }
        }

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
            Storyboard sbaux;
            mpSonidos = new MediaPlayer();
            StartAnimation();
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1:
                    Reiniciar();
                    sbaux = (Storyboard)this.Resources["ataque_lanzallamas"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/lanzallamas.mp3"));

                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number2:
                    Reiniciar();
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
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos1.Pause(); mpSonidos2.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number3:
                    Reiniciar();
                    sbaux = (Storyboard)this.Resources["defensa_desaparecer"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/esquivar.mp3"));

                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number4:
                    Reiniciar();
                    sbaux = (Storyboard)this.Resources["defensa_escudo"];
                    mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/escudo.mp3"));

                    sbaux.SpeedRatio = 0.5;
                    mpSonidos.Play();
                    sbaux.Begin();
                    sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
                    break;
                case Windows.System.VirtualKey.Number5:
                    Reiniciar();
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
                case Windows.System.VirtualKey.Number7:
                    animacionCansado();
                    break;
            }
        }

        private void Reiniciar()
        {
            // Reiniciar la animación
            StopAnimation();
            StartAnimation();
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
            // Detengo todas las animaciones
            foreach (var animacion in this.Resources.Values)
            {
                if (animacion is Storyboard)
                {
                    Storyboard sb = (Storyboard)animacion;
                    sb.Stop();
                }
            }
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
            if (!ver) { this.Vida_Foto.Visibility = Visibility.Collapsed; }
            else { this.Vida_Foto.Visibility = Visibility.Visible; }
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver) { this.ProgressBar_escudo.Visibility = Visibility.Collapsed; }
            else { this.ProgressBar_escudo.Visibility = Visibility.Visible; }
            if (!ver) { this.Escudo.Visibility = Visibility.Collapsed; }
            else { this.Escudo.Visibility = Visibility.Visible; }
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
            if (activar) { Reiniciar(); }
            else { StopAnimation(); }
        }

        public void animacionAtaqueFlojo()
        {
            Reiniciar();
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
            Reiniciar();
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
            Reiniciar();
            sbaux = (Storyboard)this.Resources["defensa_desaparecer"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/esquivar.mp3"));

            mpSonidos.Play();
            sbaux.Begin();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
        }

        public void animacionDescasar()
        {
            Reiniciar();
            sbaux = (Storyboard)this.Resources["defensa_escudo"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDracofireGDLRS/escudo.mp3"));

            sbaux.SpeedRatio = 0.5;
            mpSonidos.Play();
            sbaux.Begin();
            sbaux.Completed += (s, ev) => { sbaux.Stop(); mpSonidos.Pause(); };
        }

        public void animacionCansado()
        {
            Reiniciar();

            sbaux = (Storyboard)this.Resources["estado_cansado"];

            sbaux.RepeatBehavior = RepeatBehavior.Forever;
            sbaux.Begin();
        }

        public void animacionNoCansado()
        {
            Reiniciar();
        }

        public void animacionHerido()
        {
            Reiniciar();
            sbaux = (Storyboard)this.Resources["estado_herido"];
            sbaux.AutoReverse = true;
            sbaux.RepeatBehavior = RepeatBehavior.Forever;
            sbaux.Begin();
        }

        public void animacionNoHerido()
        {
            Reiniciar();
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
