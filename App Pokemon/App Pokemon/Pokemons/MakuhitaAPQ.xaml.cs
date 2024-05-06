using ClassLibrary1_Prueba;
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

namespace PokemonIPO2
{
    public sealed partial class MakuhitaAPQ2 : UserControl, iPokemon
    {
        public bool KeyPreview { get; }
        public double Vida
        {
            get { return this.pbVida.Value; }
            set { this.pbVida.Value = value; }
        }

        public double Energia
        {
            get { return this.pbEnergia.Value; }
            set { this.pbEnergia.Value = value; }
        }

        public string Nombre
        {
            get { return "Makuhita"; }
            set { throw new NotImplementedException(); }
        }

        public string Categoría
        {
            get { return "Pokemon Valiente"; }
            set { throw new NotImplementedException(); }
        }

        public string Tipo
        {
            get { return "Lucha"; }
            set { throw new NotImplementedException(); }
        }

        public double Altura
        {
            get { return 1.0; }
            set { throw new NotImplementedException(); }
        }

        public double Peso
        {
            get { return 86.4; }
            set { throw new NotImplementedException(); }
        }

        public string Evolucion
        {
            get { return "Hariyama"; }
            set { throw new NotImplementedException(); }
        }
        public string Descripcion
        {
            get { return this.Nombre + " tiene un espíritu inagotable, por muchas peleas que realice nunca se cansa ni se da por vencido. Por lo general, los Makuhita son muy activos ya que llevan una dieta sana y se mantienen en forma con distintos ejercicios que llevan a cabo. Cada vez que caen o son atacados se levantan con mas fuerza que la que tenían antes además que nunca se rinden hasta que ya están muy debilitados."; }
            set { throw new NotImplementedException(); }
        }
        Storyboard sbaux;

        DispatcherTimer dtTime;
        Storyboard sbMov;
        MediaPlayer mpSonidos = new MediaPlayer();
        bool herido = false;
        bool cansado = false;

        public MakuhitaAPQ2()
        {
            this.InitializeComponent();

            this.IsTabStop = true;

            this.KeyDown += ControlTeclas;

            sbMov = (Storyboard)this.Resources["MovimientoKey"];
            sbMov.RepeatBehavior = RepeatBehavior.Forever;

            sbMov.Begin();
        }

        private void UsarPocionVida(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += aumentarVida;
            dtTime.Start();
            imagenPocionVida.Visibility = Visibility.Collapsed;
        }

        private void aumentarVida(object sender, object e)
        {
            this.pbVida.Value += 0.2;
            if (pbVida.Value >= 100)
            {
                this.dtTime.Stop();
                this.imagenPocionVida.Opacity = 1;
            }
        }

        private void UsarPocionEnergia(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += aumentarEnergia;
            dtTime.Start();
            imagenPocionEnergia.Visibility = Visibility.Collapsed;
        }

        private void aumentarEnergia(object sender, object e)
        {
            this.pbEnergia.Value += 0.2;
            if (pbEnergia.Value >= 100)
            {
                this.dtTime.Stop();
                this.imagenPocionEnergia.Opacity = 1;
            }
        }

        private void ControlTeclas(object sender, KeyRoutedEventArgs e)
        {

            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1:
                    animacionAtaqueFlojo();
                    break;

                case Windows.System.VirtualKey.Number2:
                    animacionAtaqueFuerte();
                    break;

                case Windows.System.VirtualKey.Number3:
                    animacionDescasar();
                    break;

                case Windows.System.VirtualKey.Number4:
                    animacionDefensa();
                    break;

                case Windows.System.VirtualKey.Number5:
                    animacionCansado();

                    break;

                case Windows.System.VirtualKey.Number6:
                    animacionNoCansado();
                    break;

                case Windows.System.VirtualKey.Number7:
                    animacionHerido();

                    break;

                case Windows.System.VirtualKey.Number8:
                    animacionNoHerido();
                    break;

                case Windows.System.VirtualKey.Number9:
                    animacionEscudo();
                    break;

                case Windows.System.VirtualKey.Number0:
                    animacionDerrota();
                    break;
            }
        }

        public void verFondo(bool ver)
        {
            if (!ver)
            {
                this.ImagenFondo.Opacity = 0;
                this.ImagenFondo2.Opacity = 0;
            }
           

        }

        public void verFilaVida(bool ver)
        {
            if (ver)
            {
                this.pbVida.Visibility = Visibility.Visible;
                
            }
            else
            {
                this.pbVida.Visibility = Visibility.Collapsed;
                this.imagenVida.Opacity = 0;
            }
        }

        public void verFilaEnergia(bool ver)
        {
            if (ver)
            {
                this.pbEnergia.Visibility = Visibility.Visible;
            }
            else
            {
                this.pbEnergia.Visibility = Visibility.Collapsed;
                this.imagenEnergia.Opacity = 0;
            }
        }

        public void verPocionVida(bool ver)
        {
            if (ver)
                this.imagenPocionVida.Visibility = Visibility.Visible;
            else
                this.imagenPocionVida.Opacity = 0.1;
        }

        public void verPocionEnergia(bool ver)
        {
            if (ver)
                this.imagenPocionEnergia.Visibility = Visibility.Visible;
            else
                this.imagenPocionEnergia.Opacity = 0.1;
        }

        public void verNombre(bool ver)
        {
            if (ver)
                this.vbNombre.Visibility = Visibility.Visible;
            else
                this.vbNombre.Visibility = Visibility.Collapsed;
        }

        public void activarAniIdle(bool activar)
        {
            throw new NotImplementedException();
        }

        public void animacionAtaqueFlojo()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(1300);
            dtTime.Start();
            sbaux = (Storyboard)this.Resources["AtaqueDebilKey"];
            sbaux.Begin();
            AtaqueDebil.Completed += (s, ev) =>
            {
                // Ejecuta el movimiento
                sbMov.Begin();
            };

        }

        public void animacionAtaqueFuerte()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(3000);
            dtTime.Start();
            sbaux = (Storyboard)this.Resources["AtaqueFuerteKey"];
            sbaux.Begin();
            AtaqueFuerte.Completed += (s, ev) =>
            {
                // Ejecuta el movimiento
                sbMov.Begin();
            };
        }

        public void animacionDefensa()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(2900);
            dtTime.Start();
            sbaux = (Storyboard)this.Resources["EsquivarKey"];
            sbaux.Begin();
            Esquivar.Completed += (s, ev) =>
            {
                // Ejecuta el movimiento
                sbMov.Begin();
            };
        }

        public void animacionDescasar()
        {

            //Storyboard sbaux;

            sbMov.Stop();
            sbaux = (Storyboard)this.Resources["DescansarKey"];
            sbaux.Begin();
            Descansar.Completed += (s, ev) =>
            {
                // Ejecuta el movimiento
                sbMov.Begin();
            };
        }

        public void animacionCansado()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            sbaux = (Storyboard)this.Resources["CansadoKey"];
            sbaux.Begin();
            cansado = true;
        }

        public void animacionNoCansado()
        {
            if (cansado)
            {
                sbaux.Stop();
                cansado = false;
            }
            sbMov.RepeatBehavior = RepeatBehavior.Forever;
            sbMov.Begin();
        }

        public void animacionHerido()
        {
            //Storyboard sbaux;

            sbaux = (Storyboard)this.Resources["HeridoKey"];
            sbaux.Begin();
            herido = true;
        }

        public void animacionNoHerido()
        {
            if (herido)
            {
                sbaux.Stop();
                herido = false;
            }
            sbMov.RepeatBehavior = RepeatBehavior.Forever;
            sbMov.Begin();
        }

        public void animacionDerrota()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SonidoDerrotado.mp3"));
            mpSonidos.Play();
            sbaux = (Storyboard)this.Resources["DerrotadoKey"];
            sbaux.Begin();
        }

        public void animacionEscudo()
        {
            //Storyboard sbaux;

            sbMov.Stop();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SonidoEscudo.mp3"));
            mpSonidos.Play();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(9000);
            dtTime.Start();
            sbaux = (Storyboard)this.Resources["ConEscudoKey"];

            sbaux.Begin();
            ConEscudo.Completed += (s, ev) =>
            {
                // Ejecuta el movimiento
                sbMov.Begin();
            };
        }
    }
}