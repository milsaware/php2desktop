using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace php2desktop
{
    public partial class App : Form
    {
        private static WebView2 webView;
        private Process phpServer;

        readonly string settingsFilePath = Path.Combine(Application.StartupPath, "settings.json");
        readonly string json;
        readonly JArray settingsArray;

        public App()
        {
            InitializeComponent();

            if (File.Exists(settingsFilePath))
            {
                json = File.ReadAllText(settingsFilePath);
                settingsArray = JArray.Parse(json);
            }
            else
            {
                MessageBox.Show("Settings File is Missing!");
            }

            JObject settings = (JObject)settingsArray[2];

            JToken WindowSettings = settings["WindowSettings"];
            int WindowWidth = int.Parse(WindowSettings["Width"].ToString());
            int WindowHeight = int.Parse(WindowSettings["Height"].ToString());
            bool WindowIsMaximised = bool.Parse(WindowSettings["IsMaximised"].ToString());
            string WindowTitle = WindowSettings["Title"].ToString();
            string WindowIcon = WindowSettings["Icon"].ToString();

            if (WindowIsMaximised)
            {
                WindowState = FormWindowState.Maximized;
            }

            Text = WindowTitle;
            Icon = new Icon(WindowIcon);
            Size = new Size(WindowWidth, WindowHeight);
            content_panel.Dock = DockStyle.Fill;
            this.Controls.Add(content_panel);
            Closed += MainWindow_Closed;
            InitializeWebView2();
        }

        private async void InitializeWebView2()
        {
            JObject settings = (JObject)settingsArray[1];

            JToken webViewSettings = settings["WebViewSettings"];
            string webViewDataFolder = webViewSettings["DataFolder"].ToString();
            bool webViewDevTools = bool.Parse(webViewSettings["EnableDevTools"].ToString());
            bool webViewAcceleratorKeys = bool.Parse(webViewSettings["EnableAcceleratorKeys"].ToString());
            bool webViewDefaultContextMenu = bool.Parse(webViewSettings["EnableDefaultContextMenu"].ToString());
            bool webViewDefaultScriptDialogs = bool.Parse(webViewSettings["EnableDefaultScriptDialogs"].ToString());
            bool webViewHostObjectsAllowed = bool.Parse(webViewSettings["AllowHostObjects"].ToString());
            bool webViewBuiltInErrorPage = bool.Parse(webViewSettings["EnableBuiltInErrorPage"].ToString());
            bool webViewGeneralAutofill = bool.Parse(webViewSettings["EnableGeneralAutofill"].ToString());
            bool webViewPasswordAutosave = bool.Parse(webViewSettings["EnablePasswordAutosave"].ToString());
            bool webViewPinchZoom = bool.Parse(webViewSettings["EnablePinchZoom"].ToString());
            bool webViewReputationCheckingRequired = bool.Parse(webViewSettings["ReputationCheckingRequired"].ToString());
            bool webViewScript = bool.Parse(webViewSettings["EnableJavaScript"].ToString());
            bool webViewStatusBar = bool.Parse(webViewSettings["EnableStatusBar"].ToString());
            bool webViewSwipeNavigation = bool.Parse(webViewSettings["EnableSwipeNavigation"].ToString());
            bool webViewWebMessage = bool.Parse(webViewSettings["EnableWebMessage"].ToString());
            bool webViewZoomControl = bool.Parse(webViewSettings["EnableZoomControl"].ToString());
            string webViewUserAgent = webViewSettings["UserAgent"].ToString();

            CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(userDataFolder: webViewDataFolder);

            webView = new WebView2()
            {
                Dock = DockStyle.Fill
            };

            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;

            await webView.EnsureCoreWebView2Async(environment);
            webView.CoreWebView2.Settings.AreDevToolsEnabled = webViewDevTools;
            webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = webViewAcceleratorKeys;
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = webViewDefaultContextMenu;
            webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = webViewDefaultScriptDialogs;
            webView.CoreWebView2.Settings.AreHostObjectsAllowed = webViewHostObjectsAllowed;
            webView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = webViewBuiltInErrorPage;
            webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = webViewGeneralAutofill;
            webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = webViewPasswordAutosave;
            webView.CoreWebView2.Settings.IsPinchZoomEnabled = webViewPinchZoom;
            webView.CoreWebView2.Settings.IsReputationCheckingRequired = webViewReputationCheckingRequired;
            webView.CoreWebView2.Settings.IsScriptEnabled = webViewScript;
            webView.CoreWebView2.Settings.IsStatusBarEnabled = webViewStatusBar;
            webView.CoreWebView2.Settings.IsSwipeNavigationEnabled = webViewSwipeNavigation;
            webView.CoreWebView2.Settings.IsWebMessageEnabled = webViewWebMessage;
            webView.CoreWebView2.Settings.IsZoomControlEnabled = webViewZoomControl;
            webView.CoreWebView2.Settings.UserAgent = webViewUserAgent;

            content_panel.Controls.Add(webView);
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                StartPhpServer();
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed!");
            }
        }

        private void StartPhpServer()
        {
            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                JArray settingsArray = JArray.Parse(json);

                JObject settings = (JObject)settingsArray[0];

                JToken phpSettings = settings["PhpSettings"];
                string phpFileName = phpSettings["FileName"].ToString();
                string phpWebDir = phpSettings["WebDir"].ToString();
                bool phpCreateNoWindow = bool.Parse(phpSettings["CreateNoWindow"].ToString());
                bool phpUseShellExecute = bool.Parse(phpSettings["UseShellExecute"].ToString());

                Random random = new Random();
                int port = random.Next(49152, 65535);

                phpServer = new Process();
                phpServer.StartInfo.FileName = phpFileName;
                phpServer.StartInfo.Arguments = "-S 127.0.0.1:" + port + " -t " + phpWebDir;
                phpServer.StartInfo.CreateNoWindow = phpCreateNoWindow;
                phpServer.StartInfo.UseShellExecute = phpUseShellExecute;
                phpServer.StartInfo.RedirectStandardOutput = true;

                phpServer.Start();
                phpServer.BeginOutputReadLine();
                webView.CoreWebView2.Navigate("http://127.0.0.1:" + port);
            }
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            webView.Dispose();

            phpServer.CloseMainWindow();
            if (!phpServer.WaitForExit(1500))
            {
                phpServer.Kill();
            }
        }
    }
}