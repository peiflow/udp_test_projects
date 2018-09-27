using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AES_UI_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Updater updater;


        UDP_RX udp_rx;

        bool isENG2SwichtClick = false;
        bool isPS1_H_L_Click = false;
        bool isPS2_H_L_Click = false;
        bool isPS1_Mode_Click = false;
        bool isPS2_Mode_Click = false;
        bool isFuse_Fan1_Click = false;
        bool isFuse_Fan2_Click = false;
        bool isA60HQ_button_Click = false;
        bool isA61HQ_button_Click = false;
        bool isA62HQ_button_Click = false;
        bool isA63HQ_button_Click = false;

        #region Outputs
        //Outputs vector (RX_1020)
        byte[] sRX = new byte[18];

        bool Fan1_On = false;
        bool Fan2_On = false;
        bool FuseFan1 = true;
        bool FuseFan2 = true;
        bool Fan1Inop_Ind = false;
        bool Fan2Inop_Ind = false;
        bool RELAY_A66HQ = false;
        bool RELAY_ENG2 = false;
        bool RELAY_A52HQ = false;
        bool RELAY_A53HQ = false;
        bool RELAY_A54HQ = false;
        bool RELAY_A55HQ = false;
        bool RELAY_A50HQ = false;
        bool RELAY_A51HQ = false;
        bool RELAY_A64HQ = false;
        bool RELAY_A65HQ = false;
        bool RELAY_A56HQ = false;
        bool RELAY_A57HQ = false;
        #endregion

        #region Inputs
        byte[] sTX = new byte[12];
        //Inputs vector (TX_1030)
        bool A60HQ = false;
        bool A61HQ = false;
        bool A62HQ = false;
        bool A63HQ = false;
        bool A66HQ_On = false;
        bool ENG2_On = false;
        bool Fuse1 = true;
        bool Fuse2 = true;
        bool PS1_Mode = false;
        bool PS2_Mode = false;
        bool PS1_LH = false;
        bool PS2_LH = false;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < sTX.Length; i++)
            {
                sTX[i] = Convert.ToByte(false);
            }

            SendStates(sRX);
            updater = new Updater();


            UDP_RX _RX = new UDP_RX();

            this.SuscribeToEvent(_RX);

            Thread trh = new Thread(_RX.Listen);
            trh.IsBackground = true;
            trh.Start();



            //this.Dispatcher.Invoke(() => { this.SuscribeToEvent(_RX); });
            //Thread t = new Thread(_RX.Listen);
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();

            //UDP_RX.fan1 = fan1;
        }

        private void A60HQ_button_Click(object sender, RoutedEventArgs e)
        {
            if (isA60HQ_button_Click == true)
            {
                A60HQ_CB_2.Visibility = Visibility.Collapsed;
                A60HQ_CB.Visibility = Visibility.Visible;

                isA60HQ_button_Click = false;

                sTX[0] = 1;
            }
            else if (isA60HQ_button_Click == false)
            {
                A60HQ_CB_2.Visibility = Visibility.Visible;
                A60HQ_CB.Visibility = Visibility.Collapsed;

                isA60HQ_button_Click = true;

                sTX[0] = 0;
            }
            SendStates(sTX);

        }
        private void A61HQ_button_Click(object sender, RoutedEventArgs e)
        {

            if (isA61HQ_button_Click == true)
            {
                A61HQ_CB_2.Visibility = Visibility.Collapsed;
                A61HQ_CB.Visibility = Visibility.Visible;

                isA61HQ_button_Click = false;

                sTX[1] = 1;

            }
            else if (isA61HQ_button_Click == false)
            {
                A61HQ_CB_2.Visibility = Visibility.Visible;
                A61HQ_CB.Visibility = Visibility.Collapsed;

                isA61HQ_button_Click = true;

                sTX[1] = 0;

            }

            SendStates(sTX);


        }
        private void A62HQ_button_Click(object sender, RoutedEventArgs e)
        {

            if (isA62HQ_button_Click == true)
            {
                A62HQ_CB_2.Visibility = Visibility.Collapsed;
                A62HQ_CB.Visibility = Visibility.Visible;

                isA62HQ_button_Click = false;

                sTX[2] = 1;
            }
            else if (isA62HQ_button_Click == false)
            {
                A62HQ_CB_2.Visibility = Visibility.Visible;
                A62HQ_CB.Visibility = Visibility.Collapsed;

                isA62HQ_button_Click = true;

                sTX[2] = 0;

            }

            SendStates(sTX);
        }
        private void A63HQ_button_Click(object sender, RoutedEventArgs e)
        {

            if (isA63HQ_button_Click == true)
            {
                A63HQ_CB_2.Visibility = Visibility.Collapsed;
                A63HQ_CB.Visibility = Visibility.Visible;

                isA63HQ_button_Click = false;
                sTX[3] = 1;
            }
            else if (isA63HQ_button_Click == false)
            {
                A63HQ_CB_2.Visibility = Visibility.Visible;
                A63HQ_CB.Visibility = Visibility.Collapsed;

                isA63HQ_button_Click = true;
                sTX[3] = 0;
            }
            SendStates(sTX);

        }
        private void AES_Click(object sender, RoutedEventArgs e)
        {

            Fan1_On = Convert.ToBoolean(sTX[1]);

            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isENG2SwichtClick == true)
            {
                AES_Switch_1.Visibility = Visibility.Visible;
                AES_Switch_2.Visibility = Visibility.Collapsed;

                isENG2SwichtClick = false;
                controller.Pause();

                A66HQ.Background = Brushes.Red;

                sTX[4] = Convert.ToByte(true);
            }
            else if (isENG2SwichtClick == false)
            {
                AES_Switch_1.Visibility = Visibility.Collapsed;
                AES_Switch_2.Visibility = Visibility.Visible;

                isENG2SwichtClick = true;
                controller.Play();

                A66HQ.Background = Brushes.Green;

                sTX[4] = Convert.ToByte(false);
            }

            SendStates(sRX);
        }
        private void ENG2_Click(object sender, RoutedEventArgs e)
        {

            if (isENG2SwichtClick == true)
            {
                ENG2_1.Visibility = Visibility.Visible;
                ENG2_2.Visibility = Visibility.Collapsed;

                isENG2SwichtClick = false;

                ENG2.Background = Brushes.Red;

                sTX[5] = Convert.ToByte(false);
            }
            else if (isENG2SwichtClick == false)
            {
                ENG2_1.Visibility = Visibility.Collapsed;
                ENG2_2.Visibility = Visibility.Visible;

                isENG2SwichtClick = true;

                ENG2.Background = Brushes.Green;

                sTX[5] = Convert.ToByte(true);
            }

            SendStates(sTX);
        }
        private void Fuse_Fan1_Click(object sender, RoutedEventArgs e)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isFuse_Fan1_Click == true)
            {
                Fuse1_On.Visibility = Visibility.Visible;
                Fuse1_Off.Visibility = Visibility.Collapsed;

                isFuse_Fan1_Click = false;
                controller.Pause();

            }
            else if (isFuse_Fan1_Click == false)
            {
                Fuse1_On.Visibility = Visibility.Collapsed;
                Fuse1_Off.Visibility = Visibility.Visible;

                isFuse_Fan1_Click = true;
                controller.Play();

            }


        }
        private void Fuse_Fan2_Click(object sender, RoutedEventArgs e)
        {

            if (isFuse_Fan2_Click == true)
            {
                Fuse2_On.Visibility = Visibility.Visible;
                Fuse2_Off.Visibility = Visibility.Collapsed;

                isFuse_Fan2_Click = false;

                sRX[7] = Convert.ToByte(true);

            }
            else if (isFuse_Fan2_Click == false)
            {
                Fuse2_On.Visibility = Visibility.Collapsed;
                Fuse2_Off.Visibility = Visibility.Visible;

                isFuse_Fan2_Click = true;

                sRX[7] = Convert.ToByte(false);
            }

            SendStates(sRX);
        }
        private void PS1_Mode_Click(object sender, RoutedEventArgs e)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isPS1_Mode_Click == true)
            {
                PS1_Mode1.Visibility = Visibility.Visible;
                PS1_Mode2.Visibility = Visibility.Collapsed;

                isPS1_Mode_Click = false;
                controller.Pause();

            }
            else if (isPS1_Mode_Click == false)
            {
                PS1_Mode1.Visibility = Visibility.Collapsed;
                PS1_Mode2.Visibility = Visibility.Visible;

                isPS1_Mode_Click = true;
                controller.Play();

            }


        }
        private void PS2_Mode_Click(object sender, RoutedEventArgs e)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isPS2_Mode_Click == true)
            {
                PS2_Mode1.Visibility = Visibility.Visible;
                PS2_Mode2.Visibility = Visibility.Collapsed;

                isPS2_Mode_Click = false;
                controller.Pause();

            }
            else if (isPS2_Mode_Click == false)
            {
                PS2_Mode1.Visibility = Visibility.Collapsed;
                PS2_Mode2.Visibility = Visibility.Visible;

                isPS2_Mode_Click = true;
                controller.Play();

            }


        }
        private void PS1_H_L_Click(object sender, RoutedEventArgs e)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isPS1_H_L_Click == true)
            {
                PressLow1.Visibility = Visibility.Visible;
                PressHigh1.Visibility = Visibility.Collapsed;

                isPS1_H_L_Click = false;
                controller.Pause();

                sTX[10] = Convert.ToByte(true);

            }
            else if (isPS1_H_L_Click == false)
            {
                PressLow1.Visibility = Visibility.Collapsed;
                PressHigh1.Visibility = Visibility.Visible;

                isPS1_H_L_Click = true;
                controller.Play();
                sTX[10] = Convert.ToByte(false);

            }

            SendStates(sRX);
        }
        private void PS2_H_L_Click(object sender, RoutedEventArgs e)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

            if (isPS2_H_L_Click == true)
            {
                PressLow2.Visibility = Visibility.Visible;
                PressHigh2.Visibility = Visibility.Collapsed;

                isPS2_H_L_Click = false;
                controller.Pause();
                sTX[11] = Convert.ToByte(false);

            }
            else if (isPS2_H_L_Click == false)
            {
                PressLow2.Visibility = Visibility.Collapsed;
                PressHigh2.Visibility = Visibility.Visible;

                isPS2_H_L_Click = true;
                controller.Play();
                sTX[11] = Convert.ToByte(false);

            }
            SendStates(sRX);
        }

        #region Input_Signals
        private void F_A60HQ(object sender, RoutedEventArgs e)
        {
            if (A60HQ == true)
            {
                A60HQ_CB_2.Visibility = Visibility.Collapsed;
                A60HQ_CB.Visibility = Visibility.Visible;

                sTX[0] = 1;
            }
            else
            {
                A60HQ_CB_2.Visibility = Visibility.Visible;
                A60HQ_CB.Visibility = Visibility.Collapsed;
            }
        }
        private void F_A61HQ(object sender, RoutedEventArgs e)
        {
            if (A61HQ == true)
            {
                A61HQ_CB_2.Visibility = Visibility.Collapsed;
                A61HQ_CB.Visibility = Visibility.Visible;
            }
            else
            {
                A61HQ_CB_2.Visibility = Visibility.Visible;
                A61HQ_CB.Visibility = Visibility.Collapsed;
            }
        }
        private void F_A62HQ(object sender, RoutedEventArgs e)
        {
            if (A62HQ == true)
            {
                A62HQ_CB_2.Visibility = Visibility.Collapsed;
                A62HQ_CB.Visibility = Visibility.Visible;
            }
            else
            {
                A62HQ_CB_2.Visibility = Visibility.Visible;
                A62HQ_CB.Visibility = Visibility.Collapsed;
            }
        }
        private void F_A63HQ(object sender, RoutedEventArgs e)
        {
            if (A63HQ == true)
            {
                A63HQ_CB_2.Visibility = Visibility.Collapsed;
                A63HQ_CB.Visibility = Visibility.Visible;
            }
            else
            {
                A63HQ_CB_2.Visibility = Visibility.Visible;
                A63HQ_CB.Visibility = Visibility.Collapsed;
            }
        }
        private void F_A66HQ_On(object sender, RoutedEventArgs e)
        {
            if (A66HQ_On == true)
            {
                A66HQ.Background = Brushes.Green;
            }
            else
            {
                A66HQ.Background = Brushes.Red;
            }
        }
        private void F_ENG2_On(object sender, RoutedEventArgs e)
        {
            if (ENG2_On == true)
            {
                ENG2.Background = Brushes.Green;
            }
            else
            {
                ENG2.Background = Brushes.Red;
            }
        }

        #endregion

        private void SendStates(byte[] sTX)
        {
            UDP_TX tx = new UDP_TX();

            tx.SendData(sTX);
        }

        private void UpdateRX()
        {
            Dispatcher.Invoke(() =>
            {
                while (true)
                {
                    var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);

                    if (controller == null)
                        continue;

                    if (UDP_RX.fan1Res == false)
                    {
                        controller.Pause();
                    }
                    else
                    {
                        controller.Play();
                    }
                }
            });
        }

        public void SuscribeToEvent(UDP_RX reciever)
        {
            reciever.DataReceivedEvent += receiver_DataReceivedEvent;
        }

        #region Update UI
        void receiver_DataReceivedEvent(object sender, ReceivedDataArgs args)
        {
            sRX = args.ReceivedBytes;

            Console.WriteLine("Subscriptor");
            foreach (var item in sRX)
            {
                Console.Write(item);
            }
            Console.WriteLine(Environment.NewLine);

            Dispatcher.Invoke(() => { UpdateFan1(); });
            Dispatcher.Invoke(() => { UpdateFan2(); });
            Dispatcher.Invoke(() => { F_FuseFan1(); });
            
            //Repetir por cada elemento De la UI

        }

        private void UpdateFan1()
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan1);
            Fan1_On = Convert.ToBoolean(sRX[0]);

            if (Fan1_On == false)
            {
                controller.Pause();
            }
            else if (Fan1_On == true)
            {
                controller.Play();
            }
        }

        private void UpdateFan2()
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan2);
            Fan2_On = Convert.ToBoolean(sRX[1]);

            if (Fan2_On == false)
            {
                controller.Pause();
            }
            else if (Fan2_On == true)
            {
                controller.Play();
            }
        }

        private void F_FuseFan1()
        {
            FuseFan1 = Convert.ToBoolean(sRX[2]);

            if (FuseFan1 == false)
            {
                Fuse1_On.Visibility = Visibility.Collapsed;
                Fuse1_Off.Visibility = Visibility.Visible;
            }
            else if (FuseFan1 == true)
            {
                Fuse1_On.Visibility = Visibility.Visible;
                Fuse1_Off.Visibility = Visibility.Collapsed;
            }
        }

        private void F_FuseFan2()
        {
            FuseFan2 = Convert.ToBoolean(sRX[3]);

            if (FuseFan2 == false)
            {
                Fuse2_On.Visibility = Visibility.Collapsed;
                Fuse2_Off.Visibility = Visibility.Visible;
            }
            else if (FuseFan2 == true)
            {
                Fuse2_On.Visibility = Visibility.Visible;
                Fuse2_Off.Visibility = Visibility.Collapsed;
            }
        }

        private void F_Fan1Inop_Ind()
        {
            Fan1Inop_Ind = Convert.ToBoolean(sRX[4]);

            if (Fan1Inop_Ind == true)
                textBox1.Text = "FAN 1 INOP";
            else
                textBox1.Text = " ";
        }

        private void F_Fan2Inop_Ind()
        {
            Fan2Inop_Ind = Convert.ToBoolean(sRX[5]);

            if (Fan2Inop_Ind == true)
                textBox2.Text = "FAN 1 INOP";
            else
                textBox2.Text = " ";
        }
        private void F_RELAY_A66HQ()
        {
            RELAY_A66HQ = Convert.ToBoolean(sRX[6]);

            if (RELAY_A66HQ == true)
                A66HQ.Background = Brushes.Green;
            else
                A66HQ.Background = Brushes.Red;
        }
        private void F_RELAY_ENG2()
        {
            RELAY_ENG2 = Convert.ToBoolean(sRX[7]);

            if (RELAY_ENG2 == true)
                ENG2.Background = Brushes.Green;
            else
                ENG2.Background = Brushes.Red;
        }
        private void F_RELAY_A52HQ()
        {
            RELAY_A52HQ = Convert.ToBoolean(sRX[8]);

            if (RELAY_A52HQ == true)
                A52HQ.Background = Brushes.Green;
            else
                A52HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A53HQ()
        {
            RELAY_A53HQ = Convert.ToBoolean(sRX[9]);

            if (RELAY_A53HQ == true)
                A53HQ.Background = Brushes.Green;
            else
                A53HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A54HQ()
        {
            RELAY_A54HQ = Convert.ToBoolean(sRX[10]);

            if (RELAY_A54HQ == true)
                A54HQ.Background = Brushes.Green;
            else
                A54HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A55HQ()
        {
            RELAY_A55HQ = Convert.ToBoolean(sRX[11]);

            if (RELAY_A55HQ == true)
                A55HQ.Background = Brushes.Green;
            else
                A55HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A50HQ()
        {
            RELAY_A50HQ = Convert.ToBoolean(sRX[12]);

            if (RELAY_A50HQ == true)
                A50HQ.Background = Brushes.Green;
            else
                A50HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A51HQ()
        {
            RELAY_A51HQ = Convert.ToBoolean(sRX[13]);

            if (RELAY_A51HQ == true)
                A51HQ.Background = Brushes.Green;
            else
                A51HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A64HQ()
        {
            RELAY_A64HQ = Convert.ToBoolean(sRX[14]);

            if (RELAY_A64HQ == true)
                A64HQ.Background = Brushes.Green;
            else
                A64HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A65HQ()
        {
            RELAY_A65HQ = Convert.ToBoolean(sRX[15]);

            if (RELAY_A65HQ == true)
                A65HQ.Background = Brushes.Green;
            else
                A65HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A56HQ()
        {
            RELAY_A56HQ = Convert.ToBoolean(sRX[16]);

            if (RELAY_A56HQ == true)
                A56HQ.Background = Brushes.Green;
            else
                A56HQ.Background = Brushes.Red;
        }
        private void F_RELAY_A57HQ()
        {
            RELAY_A57HQ = Convert.ToBoolean(sRX[17]);

            if (RELAY_A57HQ == true)
                A57HQ.Background = Brushes.Green;
            else
                A57HQ.Background = Brushes.Red;
        }
        #endregion
    }
}
