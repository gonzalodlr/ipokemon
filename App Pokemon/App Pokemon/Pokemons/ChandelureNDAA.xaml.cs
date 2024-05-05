using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace PokemonNoelia
{
    public sealed partial class ChandelureNDAA : UserControl, iPokemon
    {
        DispatcherTimer dtTime;
        DispatcherTimer dtTime2;
        MediaPlayer mpSonidos= new MediaPlayer();
        Storyboard sbaux;


        private void usePotionRed(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseHealth;
            dtTime.Start();
            imPocionMorada.Opacity = 0.5;
        }
        private void increaseHealth(object sender, object e)
        {
            this.barraVida.Value += 0.5;
            if (barraVida.Value >= 100)
            {
                this.dtTime.Stop();
                imPocionMorada.Opacity = 1;
                this.Focus(FocusState.Programmatic);

            }
        }

        private void usePotionYellow(object sender, PointerRoutedEventArgs e)
        {
            dtTime2 = new DispatcherTimer();
            dtTime2.Interval = TimeSpan.FromMilliseconds(100);
            dtTime2.Tick += increaseEnergia;
            dtTime2.Start();
            imPocionAmarilla.Opacity = 0.5;
        }
        private void increaseEnergia(object sender, object e)
        {
            barraEnergia.Value += 0.5;
            if (barraEnergia.Value >= 100)
            {
                dtTime2.Stop();
                barraEnergia.Opacity = 1;

            }
        }


        private void imRPotion_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            usePotionRed(sender, e);
        }

        private void imYPotion_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            usePotionYellow(sender, e);
        }



        private void prueba(object sender, KeyRoutedEventArgs e)
        {
            Boolean derrotado = false;
            if (!derrotado)
            {
                switch (e.Key)
                {
                    case Windows.System.VirtualKey.Number1:
                        sbaux = (Storyboard)this.Resources["Vuelo"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/Normal.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number2:
                        sbaux = (Storyboard)this.Resources["Herido"];
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/HeridoSonido.mp4"));
                        mpSonidos.Play();
                        sbaux.Begin();
                        break;
                    case Windows.System.VirtualKey.Number3:
                        sbaux = (Storyboard)this.Resources["HeridoFinal"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/HeridoSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number4:
                        sbaux = (Storyboard)this.Resources["Cansado"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/CansadoSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number5:
                        sbaux = (Storyboard)this.Resources["CansadoFinal"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/CansadoSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number6:
                        derrotado = true;
                        sbaux = (Storyboard)this.Resources["Muerte"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/MuerteSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number7:
                        sbaux = (Storyboard)this.Resources["Vida"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/VidaSonidoFeliz.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number8:
                        sbaux = (Storyboard)this.Resources["Defensa"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/VueloSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number9:
                        sbaux = (Storyboard)this.Resources["AtaqueFuerte"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/AtaqueFuerteSonido.mp4"));
                        mpSonidos.Play();
                        break;
                    case Windows.System.VirtualKey.Number0:
                        sbaux = (Storyboard)this.Resources["AtaqueDebil"];
                        sbaux.Begin();
                        mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/AtaqueDebilSonido.mp3"));
                        mpSonidos.Play();
                        break;
                }


            }
        }

        public double Vida { get => this.barraVida.Value; set => this.barraVida.Value = value; }
        public double Energia { get => this.barraEnergia.Value; set => this.barraEnergia.Value = value; }
        public string Nombre { get => "Chandelure"; set => throw new NotImplementedException(); }
        public string Categoría { get => "Gas"; set => throw new NotImplementedException(); }
        public string Tipo { get => "Fantasma/Fuego"; set => throw new NotImplementedException(); }
        public double Altura { get => 1.0; set => throw new NotImplementedException(); }
        public double Peso { get => 34.3; set => throw new NotImplementedException(); }
        public string Evolucion { get => "Lampent"; set => throw new NotImplementedException(); }
        public string Descripcion { get => "Chandelure es un Pokémon misterioso con un cuerpo compuesto por llamas moradas y negras. " +
                                           "Su aspecto evoca la imagen de un candelabro encantado, con ojos amarillos que brillan con un " +
                                           "resplandor inquietante. Capaz de absorber la energía vital de sus oponentes, Chandelure es un " +
                                           "adversario formidable en combate."; set => throw new NotImplementedException(); }

        public ChandelureNDAA()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
            //mpSonidos = new MediaPlayer();
            // Asignar los valores de energía y vida
            //this.Energia = 0.8;
            //this.Vida = 1;
        }


        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                sbaux = (Storyboard)this.Resources["Vuelo"];
                sbaux.Begin();
                mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/Normal.mp4"));
                mpSonidos.Play();
            }
            else sbaux.Stop();
        }

        public void animacionAtaqueFlojo()
        {
            sbaux = (Storyboard)this.Resources["AtaqueDebil"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/AtaqueDebilSonido.mp3"));
            mpSonidos.Play();
        }

        public void animacionAtaqueFuerte()
        {
            sbaux = (Storyboard)this.Resources["AtaqueFuerte"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/AtaqueFuerteSonido.mp4"));
            mpSonidos.Play();
        }

        public void animacionCansado()
        {
            sbaux = (Storyboard)this.Resources["Cansado"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/CansadoSonido.mp4"));
            mpSonidos.Play();
        }

        public void animacionDefensa()
        {
            sbaux = (Storyboard)this.Resources["Defensa"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/VueloSonido.mp4"));
            mpSonidos.Play();
        }

        public void animacionDerrota()
        {
            sbaux = (Storyboard)this.Resources["Muerte"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/MuerteSonido.mp4"));
            mpSonidos.Play();
        }

        public void animacionDescasar()
        {
            sbaux = (Storyboard)this.Resources["Vida"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/VidaSonidoFeliz.mp4"));
            mpSonidos.Play();
        }

        public void animacionHerido()
        {
            sbaux = (Storyboard)this.Resources["Herido"];
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/HeridoSonido.mp4"));
            mpSonidos.Play();
            sbaux.Begin();
        }

        public void animacionNoCansado()
        {
            sbaux = (Storyboard)this.Resources["CansadoFinal"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/CansadoSonido.mp4"));
            mpSonidos.Play();
        }

        public void animacionNoHerido()
        {
            sbaux = (Storyboard)this.Resources["HeridoFinal"];
            sbaux.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsChandelureNDAA/HeridoSonido.mp4"));
            mpSonidos.Play();
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver)
            {
                this.Fila2.Height = new GridLength(0);
            } else
            {
                this.Fila2.Height = new GridLength(10, GridUnitType.Star);
            }

        }

        public void verFilaVida(bool ver)
        {
            if (!ver)
            {
                this.Fila1.Height = new GridLength(0);
            }
            else
            {
                this.Fila1.Height = new GridLength(10, GridUnitType.Star);
            }
        }

        public void verFondo(bool ver)
        {
            if (!ver) 
            { 
                this.imFondo.ImageSource = null; 
            }
            else
            {
                this.imFondo.ImageSource = new BitmapImage(new Uri("/AssetsChandelureNDAA/fondo.png", UriKind.Relative));
            }

        }

        public void verNombre(bool ver)
        {
            if (!ver)
            {
                this.Fila4.Height = new GridLength(0);
            }
            else
            {
                this.Fila4.Height = new GridLength(10, GridUnitType.Star);
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (!ver)
            {
                this.imPocionAmarilla.Source = null;
            }
            else
            {
                this.imPocionAmarilla.Source = new BitmapImage(new Uri("/AssetsChandelureNDAA/pocionAmarilla.png", UriKind.Relative));
            }
        }

        public void verPocionVida(bool ver)
        {
            if (!ver)
            {
                this.imPocionMorada.Source = null;
            }
            else
            {
                this.imPocionMorada.Source = new BitmapImage(new Uri("/AssetsChandelureNDAA/pocionMorada.png", UriKind.Relative));
            }
        }
    }
    internal interface iPokemon
    {
        double Vida { get; set; }
        double Energia { get; set; }
        string Nombre { get; }
        string Categoría { get; }
        string Tipo { get; }
        double Altura { get; }
        double Peso { get; }
        string Evolucion { get; }
        string Descripcion { get; }
    }
}
