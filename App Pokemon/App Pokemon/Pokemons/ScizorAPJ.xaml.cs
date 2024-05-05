using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration.Pnp;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

namespace Scizor_AdrianPeinado
{
    public sealed partial class ScizorAPJ : UserControl, iPokemon
    {
        DispatcherTimer dtTimeVida = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        DispatcherTimer dtTimeEnergia = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        public double Vida { get; set; }
        public double Energia { get; set; }
        public string Nombre { get; set; }
        public string Categoría { get; set; }
        public string Tipo { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public string Evolucion { get; set; }
        public string Descripcion { get; set; }

        public ScizorAPJ()
        {
            this.InitializeComponent();
            stckOpcionesDesplegables.Visibility = Visibility.Collapsed;
            tgFondo.IsOn = true;
            tgFilaVida.IsOn = true;
            tgFilaEnergia.IsOn = true;
            tgPocionVida.IsOn = true;
            tgPocionEnergia.IsOn = true;
            tgNombre.IsOn = true;
            activarAniIdle(true);
            Vida = 100;
            Energia = 100;
            Nombre = "Scizor";
            Categoría = "Tenaza";
            Tipo = "Bicho/Acero";
            Altura = 1.8;
            Peso = 118.0;
            Evolucion = "Ninguna";
            Descripcion = "Las pinzas que posee contienen acero y pueden hacer trizas cualquier objeto por duro que sea.";
        }

        private void UsarPocionSalud(object sender, PointerRoutedEventArgs e)
        {
            if (!dtTimeVida.IsEnabled && pbSalud.Value < 100)
            {
                dtTimeVida.Tick += AumentarSalud;
                dtTimeVida.Start();
                this.pocionSalud.Opacity = 0.5;
            }
        }
        private void AumentarSalud(object sender, object e)
        {
            this.pbSalud.Value += 2;
            if (pbSalud.Value >= 100)
            {
                this.dtTimeVida.Stop();
                dtTimeVida.Tick -= AumentarSalud;
                this.pocionSalud.Opacity = 1;
                verPocionVida(false);
            }
        }

        private void UsarPocionEnergia(object sender, PointerRoutedEventArgs e)
        {
            if (!dtTimeEnergia.IsEnabled && pbEnergia.Value < 100)
            {
                dtTimeEnergia.Tick += AumentarEnergia;
                dtTimeEnergia.Start();
                this.pocionEnergia.Opacity = 0.5;
            }
        }
        private void AumentarEnergia(object sender, object e)
        {
            this.pbEnergia.Value += 2;
            if (pbEnergia.Value >= 100)
            {
                this.dtTimeEnergia.Stop();
                dtTimeEnergia.Tick -= AumentarEnergia;
                this.pocionEnergia.Opacity = 1;
                verPocionEnergia(false);
            }
        }

        private void SetHand(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void SetArrow(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void BtnAtqDebil_click(object sender, RoutedEventArgs e)
        {
            animacionAtaqueFlojo();
        }

        private void BtnAtqFuerte_click(object sender, RoutedEventArgs e)
        {
            animacionAtaqueFuerte();
        }

        private void BtnEscudo_click(object sender, RoutedEventArgs e)
        {
            animacionDefensa();
        }

        private void BtnDescansar_click(object sender, RoutedEventArgs e)
        {
            animacionDescasar();
        }

        public void verFondo(bool ver)
        {
            if (ver) { this.imFondo.Opacity = 100; }
            else { this.imFondo.Opacity = 0; }
        }

        public void verFilaVida(bool ver)
        {
            if (ver) { 
                pbSalud.Visibility = Visibility.Visible;
                icoSalud.Visibility = Visibility.Visible;
            } 
            else { 
                pbSalud.Visibility = Visibility.Collapsed;
                icoSalud.Visibility = Visibility.Collapsed;
            }
        }

        public void verFilaEnergia(bool ver)
        {
            if (ver) { 
                pbEnergia.Visibility = Visibility.Visible;
                icoEnergia.Visibility = Visibility.Visible;
            } 
            else { 
                pbEnergia.Visibility = Visibility.Collapsed;
                icoEnergia.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            if (ver) { pocionSalud.Visibility = Visibility.Visible; }
            else { pocionSalud.Visibility = Visibility.Collapsed; }
        }

        public void verPocionEnergia(bool ver)
        {
            if (ver) { pocionEnergia.Visibility = Visibility.Visible; }
            else { pocionEnergia.Visibility = Visibility.Collapsed; }
        }

        public void verNombre(bool ver)
        {
            if (ver) { brdNombre.Visibility = Visibility.Visible; }
            else { brdNombre.Visibility = Visibility.Collapsed; }
        }

        public void activarAniIdle(bool activar)
        {
            if (activar) { MoverAlasIdle.Begin(); }
            else { MoverAlasIdle.Stop(); }
        }

        private void ReanudarAniIdle(object sender, object e)
        {
            activarAniIdle(true);
        }

        public void animacionAtaqueFlojo()
        {
            activarAniIdle(false);
            AtaqueDebil.Begin();
            MoverAlas.Begin();
            HaciaDelanteDebil.Begin();
            AtaqueDebil.Completed += ReanudarAniIdle;
        }

        public void animacionAtaqueFuerte()
        {
            activarAniIdle(false);
            AtaqueFuerte.Begin();
            MoverAlasFuerte.Begin();
            HaciaDelanteFuerte.Begin();
            AtaqueFuerte.Completed += ReanudarAniIdle;
        }

        public void animacionDefensa()
        {
            activarAniIdle(false);
            Escudo.Begin();
            MoverAlas.Begin();
            Escudo.Completed += ReanudarAniIdle;

        }

        public void animacionDescasar()
        {
            activarAniIdle(false);
            Descanso.Begin();
            Descanso.Completed += ReanudarAniIdle;
        }

        public void animacionCansado()
        {
            activarAniIdle(false);
            VisualStateManager.GoToState(this, "Cansado", false);
        }

        public void animacionNoCansado()
        {
            activarAniIdle(true);
        }

        public void animacionHerido()
        {
            activarAniIdle(false);
            VisualStateManager.GoToState(this, "Herido", true);
        }

        public void animacionNoHerido()
        {
            activarAniIdle(true);
        }

        public void animacionDerrota()
        {
            activarAniIdle(false);
            VisualStateManager.GoToState(this, "Derrotado", false);
        }

        private void TgMenu_Toggled(object sender, RoutedEventArgs e)
        {
            stckOpcionesDesplegables.Visibility = tgMenu.IsOn ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TgFondo_Toggled(object sender, RoutedEventArgs e)
        {
            verFondo(tgFondo.IsOn);
        }

        private void TgFilaVida_Toggled(object sender, RoutedEventArgs e)
        {
            verFilaVida(tgFilaVida.IsOn);
        }

        private void TgFilaEnergia_Toggled(object sender, RoutedEventArgs e)
        {
            verFilaEnergia(tgFilaEnergia.IsOn);
        }

        private void TgPocionVida_Toggled(object sender, RoutedEventArgs e)
        {
            verPocionVida(tgPocionVida.IsOn);
        }

        private void TgPocionEnergia_Toggled(object sender, RoutedEventArgs e)
        {
            verPocionEnergia(tgPocionEnergia.IsOn);
        }

        private void TgNombre_Toggled(object sender, RoutedEventArgs e)
        {
            verNombre(tgNombre.IsOn);
        }

        private void BtnDerrotado_click(object sender, RoutedEventArgs e)
        {
            animacionDerrota();
        }

        private void BtnHerido_click(object sender, RoutedEventArgs e)
        {
            animacionHerido();
        }

        private void BtnCansado_click(object sender, RoutedEventArgs e)
        {
            animacionCansado();
        }
    }
}
