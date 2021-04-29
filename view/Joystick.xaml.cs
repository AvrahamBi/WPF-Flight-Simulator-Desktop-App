using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator2.viewModel;
using FlightSimulator2.model;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private JoystickVM joystick;
        /*public double Elevator
        {
            get
            {
                return Convert.ToDouble(GetValue(ElevatorProp));
            }

            set
            {
                SetValue(ElevatorProp, value);
            }
        }

        public double Aileron
        {
            get
            {
                return Convert.ToDouble(GetValue(AileronProp));
            }

            set
            {
                SetValue(AileronProp, value);
            }
        }

*/        /*public static readonly DependencyProperty ElevatorProp = DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        public static readonly DependencyProperty AileronProp = DependencyProperty.Register("Aileron", typeof(double), typeof(Joystick), null);*/

        /*private Point start;
        private double totalWidth, totalHeight;
        private readonly Storyboard cb;

        private bool pressed = false;*/

        public Joystick()
        {
            InitializeComponent();
            this.joystick = new JoystickVM(new JoystickM());
            DataContext = joystick;
            joystick.startflightSpecifiecJoystickMode();
        //    cb = Knob.Resources["CenterKnob"] as Storyboard;
        }

        
                private void KnobBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {

                }

                private void KnobBase_MouseMove(object sender, MouseEventArgs e)
                {

                }

                private void KnobBase_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
                {

                }

                private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
/*                    t.Text = "in MouseLeftButtonDown";
                    start = e.GetPosition(Base);
                    pressed = true;
                    Knob.CaptureMouse();
                    cb.Stop();*/
                }

                private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
                {
                 /*   Elevator = 0;
                    Aileron = 0;
                 //   t.Text = Elevator.ToString();
                    pressed = false;
                    Knob.ReleaseMouseCapture();
                    cb.Begin();*/
                }

                private void Knob_MouseMove(object sender, MouseEventArgs e)
                {
                /*    if (pressed)
                    {
                    //    t.Text = "in MouseMove";

                        Point newPosition = e.GetPosition(Base);
                        Point delta = new Point(start.X - newPosition.X, start.Y - newPosition.Y);
                        double dis = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));
                        totalWidth = (Base.ActualWidth - KnobBase.ActualWidth) / 2;
                        totalHeight = (Base.ActualHeight - KnobBase.ActualHeight) / 2;
                        double elev, aile;

                        if (dis >= totalWidth || dis >= totalHeight)
                        {
                            return;
                        }

                        knobPosition.X = -delta.X;
                        knobPosition.Y = -delta.Y;

                        elev = delta.Y / totalHeight;
                        aile = -delta.X / totalWidth;
                       // aile = getFl

                        if (elev > 1)
                        {
                            Elevator = 1;
                        }
                        else if(elev < 0)
                        {
                            Elevator = 0;
                        }
                        else
                        {
                            Elevator = elev;
                        }

                        if (aile > 1)
                        {
                            Aileron = 1;
                        }
                        else if (aile < 0)
                        {
                            Aileron = 0;
                        }
                        else
                        {
                            Aileron = aile;
                        }

                      //  Aileron = 
                    //    t.Text = Elevator.ToString();
                    }*/
                }

                private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
                {

                }

                private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
                {

                }

                private void centerKnob_Completed(object sender, EventArgs e) { }
            }
        
    }
