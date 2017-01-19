using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

namespace GX_Password
{
	//APP by Tiago Danin
	//License GPLv3
	//Based on Github:TiagoDanin/GenesiPassword
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
			{
				var statusBar = StatusBar.GetForCurrentView();
				var colorBar = Application.Current.Resources["SystemControlForegroundAccentBrush"] as SolidColorBrush;

				statusBar.BackgroundColor = colorBar.Color;
				statusBar.BackgroundOpacity = 1;
			}
		}

		private string gen(int lenPass, bool text, bool numb, bool sym)
		{
			string password = "";
			Random ran = new Random();

			for (int r = 0; r < lenPass;)
			{
				int random = ran.Next(33, 190);
				byte[] b = { Convert.ToByte(random) };
				string newChar = Encoding.UTF8.GetString(b);

				if (sym & random >= 33 & random <= 44)       { password += newChar; r++; }
				if (sym & random >= 58 & random <= 64)       { password += newChar; r++; }
				if (numb & random >= 49 & random <= 57)      { password += newChar; r++; }
				if (text & random >= 65 & random <= 90)      { password += newChar; r++; }
				if (text & random >= 97 & random <= 122)     { password += newChar; r++; }
			}

			return password;
		}

		private void btClipboard_click(object sender, RoutedEventArgs e)
		{
			var dataPackage = new DataPackage();
			dataPackage.SetText(P.Text);
			Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
		}

		private void btOK_click(object sender, RoutedEventArgs e)
		{
			if (tsLett.IsOn == false & tsNumb.IsOn == false & tsSymb.IsOn == false) {
				P.Text = "";
			} else {
				P.Text = gen(Convert.ToInt16(sLeng.Value), tsLett.IsOn, tsNumb.IsOn, tsSymb.IsOn);
			}
		}
	}
}
