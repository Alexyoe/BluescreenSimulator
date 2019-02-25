﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace BluescreenSimulator
{
    /// <summary>
    /// Interaction logic for BluescreenWin7.xaml
    /// </summary>
    public partial class BluescreenWin7 : Window
    {
        //Variables
        private bool DoneDumping = false;
        Random r_dump = new Random();
        BluescreenWin7Data Win7Data = null;
        public BluescreenWin7(BluescreenWin7Data win7Data)
        {
            InitializeComponent();
            this.Win7Data = win7Data;
            SetupScreen();
        }

        private void SetupScreen()
        {
            DoneDump1.Visibility = Visibility.Hidden; DoneDump2.Visibility = Visibility.Hidden;
            ErrorCode.Text = Win7Data.ErrorCode;
            Step1.Text = Win7Data.Step1;
            Step2.Text = Win7Data.Step2;
            Tip.Text = Win7Data.Tip;
            StopCode.Text = Win7Data.StopCode;
            DoneDumping = SimulateDumping();
        }
        private void AfterDumping()
        {
            DoneDump1.Visibility = Visibility.Visible; DoneDump2.Visibility = Visibility.Visible;

        }
        void DumpLoop()
        {
            int MaxDump = 95;
            var CurrentDump = 0;
            while (CurrentDump < MaxDump)
            {
                Thread.Sleep(r_dump.Next(5000));
                CurrentDump += r_dump.Next(15, 25);
                if (CurrentDump > MaxDump)
                {
                    CurrentDump = MaxDump;
                    Application.Current.Dispatcher.InvokeAsync(() => AfterDumping());
                }

                Application.Current.Dispatcher.InvokeAsync(() => Dump.Text = "Dumping physical memory to disk: " + CurrentDump.ToString());
            }

        }
        private bool SimulateDumping()
        {
            Task.Factory.StartNew(this.DumpLoop);
            return true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F7)
            {
                this.Close();
            }
        }
    }
}
