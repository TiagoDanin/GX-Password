using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Core;
using Windows.System.Profile;
using Windows.Storage;
using System.Runtime;

namespace GX_Password
{
	//APP: GX Password
	//Develops Tiago Danin & BaezCrdrm
	//License GPLv3
	//Based on Github:TiagoDanin/GenesiPassword
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

			if (AppSettings.Values.ContainsKey("tsLett"))
			{
				tsLett.IsOn = (bool)AppSettings.Values["tsLett"];
			}
			if (AppSettings.Values.ContainsKey("tsNumb"))
			{
				tsNumb.IsOn = (bool)AppSettings.Values["tsNumb"];
			}
			if (AppSettings.Values.ContainsKey("tsSymb"))
			{
				tsSymb.IsOn = (bool)AppSettings.Values["tsSymb"];
			}
			if (AppSettings.Values.ContainsKey("tsSimi"))
			{
				tsSimi.IsOn = (bool)AppSettings.Values["tsSimi"];
			}
			if (AppSettings.Values.ContainsKey("tsAdv"))
			{
				tsSimi.IsOn = (bool)AppSettings.Values["tsAdv"];
			}
			if (AppSettings.Values.ContainsKey("tsQuotes"))
			{
				tsSimi.IsOn = (bool)AppSettings.Values["tsQuotes"];
			}
			if (AppSettings.Values.ContainsKey("tsLettUp"))
			{
				tsSimi.IsOn = (bool)AppSettings.Values["tsLettUp"];
			}
			if (AppSettings.Values.ContainsKey("tsLettDown"))
			{
				tsSimi.IsOn = (bool)AppSettings.Values["tsLettDown"];
			}
			if (AppSettings.Values.ContainsKey("sLeng"))
			{
				sLeng.Value = (double)AppSettings.Values["sLeng"];
			}

			string deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
			ulong osVersion = ulong.Parse(deviceFamilyVersion);
			int build = Convert.ToInt32((osVersion & 0x00000000FFFF0000L) >> 16);
			if (build >= 16299)
			{
				bgMain.Background = Resources["SystemControlAccentAcrylicWindowAccentMediumHighBrush"] as Brush;
			}

			var colorBar = Application.Current.Resources["SystemControlForegroundAccentBrush"] as SolidColorBrush;
			if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
			{
				var statusBar = StatusBar.GetForCurrentView();
				if (statusBar != null)
				{
					statusBar.BackgroundColor = colorBar.Color;
					statusBar.BackgroundOpacity = 1;
				}
			}
			if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
			{
				ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

				if (titleBar != null && ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase") && build >= 16299)
				{
					CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
					titleBar.ButtonBackgroundColor = Colors.Transparent;
					titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
				}
				else if (titleBar != null && ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
				{
					titleBar.ButtonBackgroundColor = colorBar.Color;
					titleBar.BackgroundColor = colorBar.Color;
				}
			}
			Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().IsScreenCaptureEnabled = false;
		}

		private string gen(int lenPass, bool text, bool numb, bool sym, bool simi, bool noFixQuotes, bool lettUp, bool lettDown)
		{
			string password = "";
			int[] simiArray = new int[190];
			Random ran = new Random();

			for (int r = 0; r < lenPass;)
			{
				int random = ran.Next(33, 190);
				bool checkOk = true;
				// Avoid using single and dobule quotes
				// ASCII doble quote: 34
				// ASCII single quite: 39
				if (!noFixQuotes && (random == 34 || random == 39)) {
					checkOk = false;
				}

				if (simi)
				{
					// b 98 6 54
					if ((new List<int> { 98, 54 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 98, 54 }).Contains(element)))
						{
							simiArray[0] = random;
						}
						else
						{
							random = simiArray[0];
						}
					}
					// c 99 C 67 ( 40 { 123 [ 91
					if ((new List<int> { 99, 67, 40, 123, 91 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 99, 67, 40, 123, 91 }).Contains(element)))
						{
							simiArray[1] = random;
						}
						else
						{
							random = simiArray[1];
						}
					}
					// g 103 q 113 9 57
					if ((new List<int> { 103, 113, 57 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 103, 113, 57 }).Contains(element)))
						{
							simiArray[2] = random;
						}
						else
						{
							random = simiArray[2];
						}
					}
					// l 108 L 76 i 105 I 73 | 124
					if ((new List<int> { 108, 76, 105, 73, 124 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 108, 76, 105, 73, 124 }).Contains(element)))
						{
							simiArray[3] = random;
						}
						else
						{
							random = simiArray[3];
						}
					}
					// O 79 o 111 0 48
					if ((new List<int> { 79, 111, 48 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 79, 111, 48 }).Contains(element)))
						{
							simiArray[4] = random;
						}
						else
						{
							random = simiArray[4];
						}
					}
					// S 83 s 115 5 53
					if ((new List<int> { 83, 115, 53 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 83, 115, 53 }).Contains(element)))
						{
							simiArray[5] = random;
						}
						else
						{
							random = simiArray[5];
						}
					}
					// V 86 v 118 U 85 u 117
					if ((new List<int> { 86, 118, 85, 117 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 86, 118, 85, 117 }).Contains(element)))
						{
							simiArray[6] = random;
						}
						else
						{
							random = simiArray[6];
						}
					}
					// Z 90 z 122 2 50
					if ((new List<int> { 90, 122, 50 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 90, 122, 50 }).Contains(element)))
						{
							simiArray[7] = random;
						}
						else
						{
							random = simiArray[7];
						}
					}
					// W 87 w 119 V 86 v 118
					if ((new List<int> { 87, 119, 86, 118 }).Contains(random))
					{
						if (!Array.Exists(simiArray, element => (new List<int> { 87, 119, 86, 118 }).Contains(element)))
						{
							simiArray[8] = random;
						}
						else
						{
							random = simiArray[8];
						}
					}
				}

				byte[] b = { Convert.ToByte(random) };
				string newChar = Convert.ToString(Encoding.UTF8.GetString(b));

				if (checkOk && sym && random >= 33 && random <= 44)                { password += newChar; r++; }
				if (checkOk && sym && random >= 58 && random <= 64)                { password += newChar; r++; }
				if (checkOk && numb && random >= 49 && random <= 57)               { password += newChar; r++; }
				if (checkOk && lettUp && text && random >= 65 && random <= 90)     { password += newChar; r++; }
				if (checkOk && lettDown && text && random >= 97 && random <= 122)  { password += newChar; r++; }
			}

			return password;
		}

		private void btClipboard_click(object sender, RoutedEventArgs e)
		{
			var dataPackage = new DataPackage();
			dataPackage.SetText(P.Text);
			Clipboard.SetContent(dataPackage);
		}

		private void btOK_click(object sender, RoutedEventArgs e)
		{

			if (tsLett.IsOn == true && tsLettUp.IsOn == false && tsLettDown.IsOn == false) {
				tsLettUp.IsOn = true;
				tsLettDown.IsOn = true;
			} else if (tsLettUp.IsOn == false && tsLettDown.IsOn == false) {
				tsLett.IsOn = false;
			}

			if (tsLett.IsOn == false && tsNumb.IsOn == false && tsSymb.IsOn == false) {
				P.Text = "";
			} else {
				P.Text = gen(Convert.ToInt16(sLeng.Value), tsLett.IsOn, tsNumb.IsOn, tsSymb.IsOn, tsSimi.IsOn, tsQuotes.IsOn, tsLettUp.IsOn, tsLettDown.IsOn);
			}

			ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;
			AppSettings.Values["tsLett"] = tsLett.IsOn;
			AppSettings.Values["tsNumb"] = tsNumb.IsOn;
			AppSettings.Values["tsSymb"] = tsSymb.IsOn;
			AppSettings.Values["tsSimi"] = tsSimi.IsOn;
			AppSettings.Values["tsAdv"] = tsAdv.IsOn;
			AppSettings.Values["tsQuotes"] = tsQuotes.IsOn;
			AppSettings.Values["tsLettUp"] = tsLettUp.IsOn;
			AppSettings.Values["tsLettDown"] = tsLettDown.IsOn;
			AppSettings.Values["sLeng"] = sLeng.Value;
		}

		private void MainChanged(object sender, SizeChangedEventArgs e)
		{

		}

		private void tsAdvCheck(object sender, RoutedEventArgs e)
		{
			if (tsAdv.IsOn)
			{
				spAdv.Visibility = Visibility.Visible;
 			} else {
				spAdv.Visibility = Visibility.Collapsed;
			}
		}
	}
}