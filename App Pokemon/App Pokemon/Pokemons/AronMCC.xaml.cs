using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Threading.Tasks;
using Windows.Devices.Radios;
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

namespace PokemonAron
{
    public sealed partial class AronMCC : UserControl, iPokemon


    {
        private double INCREMENTOVIDA = 100;
        private double vidaASubir;
        DispatcherTimer miReloj;
        DispatcherTimer relojAnimaciones;
        Storyboard idle;
        private int danioGolpeCuerpo = 30;
        public event EventHandler AccionRealizada;
        bool estaCansado;
        bool energico;


        public double Vida
        {
            get { return this.pb_vida.Value; }
            set { this.pb_vida.Value = value; }
        }
        public double Energia
        {
            get { return this.pb_energia.Value; }
            set { this.pb_energia.Value = value; }
        }
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string categoría;
        public string Categoría
        {
            get { return categoría; }
            set { categoría = value; }
        }
        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private double altura;
        public double Altura
        {
            get { return altura; }
            set { altura = value; }
        }
        private double peso;
        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }
        private string evolucion;
        public string Evolucion
        {
            get { return evolucion; }
            set { evolucion = value; }
        }
        private string descripcion;
        public string Descripcion {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public AronMCC()
        { 
            this.InitializeComponent();
            miReloj = new DispatcherTimer();
            relojAnimaciones = new DispatcherTimer();
            this.KeyDown += UserControl_KeyDown;
            idle = (Storyboard)this.Resources["sbIdle"];
            estaCansado = false;
            energico = false;
            idle.AutoReverse = true;
            idle.RepeatBehavior = RepeatBehavior.Forever;
            idle.Begin();
            Vida = 100;
            Energia = 100;
            Nombre = "Aron";
            Categoría = "Coraza Férrea";
            Tipo = "Acero Roca";
            Altura = 0.4;
            Peso = 60;
            Evolucion = "Lairon";
            Descripcion = "Aron es un pequeño Pokémon blindado que tiene un cuerpo cubierto de una capa de placas de acero. Estas placas le brindan una excelente defensa contra los ataques físicos. Su cuerpo está diseñado para resistir el impacto de golpes y ataques, lo que lo hace extremadamente resistente. Vive en cuevas y minas, donde se alimenta de hierro y otros minerales. A pesar de su tamaño pequeño, Aron es valiente y defenderá su territorio con ferocidad. A menudo se le puede encontrar afilando sus colmillos en piedras para mantenerlos afilados y listos para el combate.";
        }
        private void OnAccionRealizada()
        {
            AccionRealizada?.Invoke(this, EventArgs.Empty);
        }
      
       
        public void verImagenVida(bool verImagenVida)
        {
            if (!verImagenVida)
                this.imgVida.Source = null;
            else
                this.imgVida.Source = new BitmapImage(new Uri("ms-appx:///AssetsAronMCC/Vida.png", UriKind.RelativeOrAbsolute));
        }

        public void verImagenEnergia(bool verImagenEnergia)
        {
            if (!verImagenEnergia)
                this.imgEnergia.Source = null;
            else
                this.imgEnergia.Source = new BitmapImage(new Uri("ms-appx:///AssetsAronMCC/Energia.png", UriKind.RelativeOrAbsolute));
        }
      
        public void verPocionVida(bool verPocionVida)
        {
            if (!verPocionVida)
                this.imgPocionVida.Source = null;
            else
                this.imgPocionVida.Source = new BitmapImage(new Uri("ms-appx:///AssetsAronMCC/PocionVida.png", UriKind.RelativeOrAbsolute));
        }

        public void verPocionEnergia(bool verPocionEnergia)
        {
            if (!verPocionEnergia)
                this.imgPocionEnergia.Source = null;
            else
                this.imgPocionEnergia.Source = new BitmapImage(new Uri("ms-appx:///AssetsAronMCC/PocionEnergia.png", UriKind.RelativeOrAbsolute));
        }

       

        private void subirVida(object sender, object e)
        {
            pb_vida.Value += 1;
            double vidaActual = pb_vida.Value;
            if (vidaActual == vidaASubir || (vidaActual == 100 && vidaASubir > 100))
            {
                OnAccionRealizada();
                miReloj.Tick -= subirVida;
                miReloj.Stop();
                if (vidaActual != 100)
                {
                    imgPocionVida.Opacity = 1;
                }
            }
        }

        private void subirEnergia(object sender, object e)
        {
            pb_energia.Value += 1;
            double energiaActual = pb_energia.Value;
            if (pb_energia.Value > 50 && estaCansado)
            {
                // Ejecutar la animación para recuperar energía
                Storyboard sbRecuperaEnergia = (Storyboard)this.Resources["sbRecuperaEnergia"];
                sbRecuperaEnergia.AutoReverse = false;
                sbRecuperaEnergia.Begin();
                estaCansado = false;
            }
            else if (pb_energia.Value >= 80 && !energico)
            {
                // Ejecutar la animación para energico
                Storyboard sbConEscudo = (Storyboard)this.Resources["sbConEscudo"];
                sbConEscudo.AutoReverse = false;
                sbConEscudo.Stop();
                energico = true;
            }
            else if (pb_energia.Value >= 100)
            {
                miReloj.Tick -= subirEnergia;
                miReloj.Stop();
                imgPocionEnergia.Opacity = 0.1;
                OnAccionRealizada();
            }
            else
            {
                imgPocionEnergia.Opacity = 0.1;
            }
        }

        private void imgPocionEnergia_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            miReloj.Interval = TimeSpan.FromMilliseconds(100);
            miReloj.Tick += subirEnergia;
            miReloj.Start();
            imgPocionEnergia.Opacity = 0.1;

        }

        private void imgPocionVida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            vidaASubir = pb_vida.Value + INCREMENTOVIDA;
            miReloj.Interval = TimeSpan.FromMilliseconds(100);
            miReloj.Tick += subirVida;
            miReloj.Start();
            imgPocionVida.Opacity = 0.1;
        }




        private void btnGolpeCuerpo_Click(object sender, RoutedEventArgs e)
        {
            int danioAtaque = danioGolpeCuerpo;
            animacionAtaqueFuerte(danioAtaque);
        }
   
        public void verFondo(bool verFondo)
        {
            if (!verFondo)
                this.imgFondo.Source = null;
            else
                this.imgFondo.Source = new BitmapImage(new Uri("ms-appx:///AssestsAronMCC/Imagen fondo.png", UriKind.RelativeOrAbsolute));
        }

        public void verFilaVida(bool verFilaVida)
        {
            if (!verFilaVida)
            {       this.pb_vida.Visibility = Visibility.Collapsed;
                    this.imgVida.Visibility = Visibility.Collapsed;
            }
            else
                this.pb_vida.Visibility = Visibility.Visible;
        }

        public void verFilaEnergia(bool verFilaEnergia)
        {
            if (!verFilaEnergia) { 
                this.pb_energia.Visibility = Visibility.Collapsed;
            this.imgEnergia.Visibility = Visibility.Collapsed; }
            else
                this.pb_energia.Visibility = Visibility.Visible;
        }

        public void verNombre(bool ver)
        {
            if (!ver)
                this.txtBNombre.Visibility = Visibility.Collapsed;
            else
                this.txtBNombre.Visibility = Visibility.Visible;
            
        }

        public void activarAniIdle(bool activar)
        {
            throw new NotImplementedException();
        }

        public void animacionAtaqueFlojo(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["sbAtaqueCabeza"];
            sb.AutoReverse = false;
            sb.Begin();
        }

        public void animacionAtaqueFuerte(int danioAtaque)
        {
            idle.Pause();

            Storyboard sb = (Storyboard)this.Resources["sbAtaqueCuerpo"];
            sb.AutoReverse = true;
            sb.Begin();

           
            System.Timers.Timer timer = new System.Timers.Timer(3400);
            timer.Elapsed += (sender, e) =>
            {
               
                timer.Stop(); 
                timer.Dispose(); 
            };
            timer.Start();
            idle.Begin();
        }

        public void animacionDefensa(object sender, RoutedEventArgs e)
        {
            imgEscudo.Visibility = Visibility.Visible;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += (s, args) =>
            {
                imgEscudo.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }
        private void UserControl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Verifica si la tecla presionada es un número del 1 al 9
            if (e.Key >= Windows.System.VirtualKey.Number1 && e.Key <= Windows.System.VirtualKey.Number9)
            {
                // Obtiene el número presionado
                int numero = (int)e.Key - (int)Windows.System.VirtualKey.Number0;

                // Verifica si el número corresponde a una acción válida
                if (numero == 1)
                {
                    // Ejecuta el método animacionDefensa
                    animacionDefensa(null, null);
                }
                else if (numero == 2)
                {
                    // Ejecuta el método animacionAtaqueFlojo
                    animacionAtaqueFlojo(null, null);
                }
                else if (numero == 3)
                {
                    // Ejecuta el método animacionAtaqueFuerte
                    animacionAtaqueFuerte(0);
                }
                else if (numero == 4)
                {
                    // Ejecuta el método animacionDescansar
                    animacionDescasar(null, null);
                }
                else if (numero == 5)
                {
                    // Ejecuta el método animacionCansado
                    animacionCansado(null, null);
                }
                else if (numero == 6)
                {
                    // Ejecuta el método animacionHerido
                    animacionHerido(null, null);
                }
                else if (numero == 7)
                {
                    // Ejecuta el método animacionNoHerido
                    animacionNoHerido(null, null);
                }
                else if (numero == 8)
                {
                    // Ejecuta el método animacionDerrota
                    animacionDerrota(null, null);
                }
              

                e.Handled = true;
            }
        }

        public void animacionDescasar(object sender, RoutedEventArgs e)
        {
            idle.Pause();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, args) =>
            {
                idle.Begin();
                timer.Stop();
            };
            timer.Start();
        }

        public void animacionCansado(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["sbCansado"];
            sb.Begin();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4); 
            timer.Tick += (s, args) =>
            {
                sb.Stop(); 
                timer.Stop(); 
            };
            timer.Start();
        }

        public void animacionNoCansado()
        {
            throw new NotImplementedException();
        }

        public void animacionHerido(object sender, RoutedEventArgs e)
        {
            Storyboard sbHerido = (Storyboard)this.Resources["sbHerido"];
            sbHerido.Begin();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4); 
            timer.Tick += (s, args) =>
            {
                sbHerido.Stop(); 
                timer.Stop(); 
            };
            timer.Start();
        }

        public void animacionNoHerido(object sender, RoutedEventArgs e)
        {
            Storyboard sbConEscudo = (Storyboard)this.Resources["sbConEscudo"];
            sbConEscudo.Begin();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4); 
            timer.Tick += (s, args) =>
            {
                sbConEscudo.Stop(); 
                timer.Stop(); 
            };
            timer.Start();
        }

        public void animacionDerrota(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["sbDerrotado"];
            sb.Begin();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4); 
            timer.Tick += (s, args) =>
            {
                sb.Stop();
                timer.Stop(); 
            };
            timer.Start();
        }

        public void animacionAtaqueFlojo()
        {
            throw new NotImplementedException();
        }

        public void animacionAtaqueFuerte()
        {
            throw new NotImplementedException();
        }

        public void animacionDefensa()
        {
            throw new NotImplementedException();
        }

        public void animacionDescasar()
        {
            throw new NotImplementedException();
        }

        public void animacionCansado()
        {
            throw new NotImplementedException();
        }

        public void animacionHerido()
        {
            throw new NotImplementedException();
        }

        public void animacionNoHerido()
        {
            throw new NotImplementedException();
        }

        public void animacionDerrota()
        {
            throw new NotImplementedException();
        }
    }
}
