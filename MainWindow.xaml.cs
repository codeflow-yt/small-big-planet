﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Small_Big_Planet
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public delegate void Notify();

	public partial class MainWindow : Window
	{
		SmallPlanet smallPlanet;
		BigPlanet bigPlanet;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			smallPlanet = new SmallPlanet(this.elp_SmallPlanet, this.cnv_Canvas);
			bigPlanet = new BigPlanet(this.elp_BigPlanet, this.cnv_Canvas);

			smallPlanet.SetTarget(this.elp_BigPlanet);

			smallPlanet.Move();
		}
	}
}
