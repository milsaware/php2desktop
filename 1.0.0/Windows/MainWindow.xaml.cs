using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;

namespace PHP_DT
{
	public partial class MainWindow : Window
	{
		private readonly ChromiumWebBrowser browser;
		private Process phpServer;
		private readonly int port;

		public MainWindow()
		{
			InitializeComponent();

			CefSettings settings = new();
			settings.CefCommandLineArgs.Add("enable-javascript", "1");
			Cef.Initialize(settings);

			port = GenerateRandomPort();
			browser = new ChromiumWebBrowser("http://127.0.0.1:" + port);
			Content = browser;

			Loaded += MainWindow_Loaded;
			Closed += MainWindow_Closed;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			phpServer = new Process();
			phpServer.StartInfo.FileName = "php/php.exe";
			phpServer.StartInfo.CreateNoWindow = true;
			phpServer.StartInfo.UseShellExecute = false;
			phpServer.StartInfo.RedirectStandardOutput = true;
			phpServer.StartInfo.Arguments = "-S 127.0.0.1:" + port + " -t www";
			phpServer.Start();
		}

		private void MainWindow_Closed(object sender, System.EventArgs e)
		{
			browser.Dispose();
			Cef.Shutdown();
			
			phpServer.CloseMainWindow();
			if (!phpServer.WaitForExit(1500))
			{
				phpServer.Kill();
			}
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F5)
			{
				browser.Reload();
			}
		}

		private static int GenerateRandomPort()
		{
			Random random = new();
			return random.Next(25217, 65535);
		}
	}
}
