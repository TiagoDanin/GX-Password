using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GX_Password
{
	sealed partial class App : Application
	{
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif
			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame == null)
			{
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
				}

				Window.Current.Content = rootFrame;
			}

			if (e.PrelaunchActivated == false)
			{
				if (rootFrame.Content == null)
				{
					rootFrame.Navigate(typeof(MainPage), e.Arguments);
				}
				Window.Current.Activate();
			}
		}

		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		protected override void OnActivated(IActivatedEventArgs args)
		{
			base.OnActivated(args);

			if (args.Kind == ActivationKind.VoiceCommand)
			{
				var voiceCommandArgs = args as VoiceCommandActivatedEventArgs;

				if (voiceCommandArgs != null)
				{
					SpeechRecognitionResult result = voiceCommandArgs.Result;

					// ToDo: Do something with the result.
				}
			}
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			deferral.Complete();
		}
	}
}
