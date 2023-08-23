using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace xreactSoft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private string _comboPath, _proxyPath, _configPath, _type;
		public MainWindow()
        {
            InitializeComponent();
        }

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void Exit_App(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void Mini_App(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				base.DragMove();
			}
		}
		private void LoadCombo(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Load Combolist";
			open.Filter = "Text File|*.txt";
			if ((bool)open.ShowDialog())
			{
				_comboPath = open.FileName.Replace("\\", "/");
				lblComboName.Content = $"File Name : {Path.GetFileName(open.FileName)}";
				lblComboCount.Content = $"Combo Count : {File.ReadLines(open.FileName).Count()}";
			}
		}

		private void Web(object sender, MouseButtonEventArgs e)
		{
			Process.Start("https://www.xreactor.org/");
		}

        private void LoadProxy(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Load Proxylist";
			open.Filter = "Text File|*.txt";
			if ((bool)open.ShowDialog())
			{
				_proxyPath = open.FileName.Replace("\\", "/");
				lblProxyName.Content = $"File Name : {Path.GetFileName(open.FileName)}";
				lblProxyCount.Content = $"Proxy Count : {File.ReadLines(open.FileName).Count()}";
			}
		}

		private void LoadConfig(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Load Config";
			open.Filter = "$wrod File|*.sc";
			if ((bool)open.ShowDialog())
			{
				_configPath = open.FileName.Replace("\\", "/");
				lblConfigName.Content = $"Config Name : {Path.GetFileName(open.FileName)}";
			}
		}
		private void Start_App(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(_configPath) && !string.IsNullOrEmpty(_proxyPath) && !string.IsNullOrEmpty(_comboPath))
				{
					if (!string.IsNullOrEmpty(txtBot.Text) && !string.IsNullOrEmpty(txtTimeout.Text) && cboProxyType.SelectedIndex != -1)
					{
						switch (cboProxyType.SelectedIndex)
						{
							case 0:
								_type = "none";
								break;
							case 1:
								_type = "http";
								break;
							case 2:
								_type = "socks4";
								break;
							case 3:
								_type = "socks4a";
								break;
							case 4:
								_type = "socks5";
								break;
							default:
								_type = "none";
								break;
						}

						string arguments = $"--cfg '{_configPath}' --cl '{_comboPath}' --pl '{_proxyPath}' --pt {_type} -t {txtTimeout.Text} -b {txtBot.Text}";
						Process.Start("$word.exe", arguments);
					}
					else
					{
						MessageBox.Show("Please enter ( BotNumber Or TimeOut Or ProxyType )");
					}
				}
				else
				{
					MessageBox.Show("Please Load ( ComboList Or ProxyList Or Config )");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error : " + ex.Message);
			}
		}

	}
}
