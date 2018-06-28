using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000C RID: 12
	public class FinishViewModel : ViewModel
	{
		// Token: 0x04000034 RID: 52
		private Project project;

		// Token: 0x04000035 RID: 53
		private WindowOperationsViewModel operationsViewModel;

		// Token: 0x04000036 RID: 54
		private IVideoReader videoReader;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		private TimeSpan <VideoLength>k__BackingField;

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		private Command <OpenVideoLocationCommand>k__BackingField;

		// Token: 0x04000039 RID: 57
		[CompilerGenerated]
		private Command <GoBackToSettingsCommand>k__BackingField;

		// Token: 0x0400003A RID: 58
		[CompilerGenerated]
		private Command <GoBackToHomeCommand>k__BackingField;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		private NavigationViewModel <NavigationViewModel>k__BackingField;

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private static Func<bool> CS$<>9__CachedAnonymousMethodDelegate1;

		// Token: 0x06000095 RID: 149 RVA: 0x00004762 File Offset: 0x00002962
		public FinishViewModel(INavigation navigation, IUserInterface userInterface, WindowOperationsViewModel operationsViewModel, IVideoReader videoReader) : base(navigation, userInterface)
		{
			this.NavigationViewModel = new NavigationViewModel(navigation, "Finish");
			if (operationsViewModel == null)
			{
				throw new ArgumentNullException("operationsViewModel");
			}
			this.operationsViewModel = operationsViewModel;
			this.videoReader = videoReader;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000479A File Offset: 0x0000299A
		public Uri OutputDirectory
		{
			get
			{
				return new Uri(Path.GetDirectoryName(this.project.OutputFile));
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000047B1 File Offset: 0x000029B1
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000047B9 File Offset: 0x000029B9
		public TimeSpan VideoLength
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VideoLength>k__BackingField = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000047C2 File Offset: 0x000029C2
		public Rational OutputFramesPerSecond
		{
			get
			{
				return this.project.OutputFramesPerSecond;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000047CF File Offset: 0x000029CF
		public Uri VideoSource
		{
			get
			{
				return new Uri(this.project.OutputFile);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000047E1 File Offset: 0x000029E1
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000047E9 File Offset: 0x000029E9
		public Command OpenVideoLocationCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<OpenVideoLocationCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OpenVideoLocationCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000047F2 File Offset: 0x000029F2
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000047FA File Offset: 0x000029FA
		public Command GoBackToSettingsCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<GoBackToSettingsCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GoBackToSettingsCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004803 File Offset: 0x00002A03
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000480B File Offset: 0x00002A0B
		public Command GoBackToHomeCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<GoBackToHomeCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GoBackToHomeCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004814 File Offset: 0x00002A14
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000481C File Offset: 0x00002A1C
		public NavigationViewModel NavigationViewModel
		{
			[CompilerGenerated]
			get
			{
				return this.<NavigationViewModel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NavigationViewModel>k__BackingField = value;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004828 File Offset: 0x00002A28
		public override void OnNavigatedTo(object[] args)
		{
			this.project = (Project)args[0];
			this.OpenVideoLocationCommand = new Command(new Action(this.OpenExplorerToVideoFile), () => true);
			this.GoBackToSettingsCommand = new Command(new Action(this.GoBackToSettings));
			this.GoBackToHomeCommand = new Command(new Action(this.GoBackToHome));
			this.VideoLength = this.GetVideoLength();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000048B4 File Offset: 0x00002AB4
		private TimeSpan GetVideoLength()
		{
			TimeSpan timeSpan = this.project.OutputLength;
			try
			{
				VideoInfo videoInfo = this.videoReader.ReadInfoFromFile(this.project.OutputFile);
				timeSpan = videoInfo.Duration;
			}
			catch (Exception ex)
			{
				LoggerExtensionMethods.LogWarning<FinishViewModel>(this, "Couldn't get real duration. Falling back to estimated", new Dictionary<string, object>
				{
					{
						"Exception",
						ex
					},
					{
						"HResult",
						ex.HResult
					}
				}, "GetVideoLength", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\FinishViewModel.cs", 121);
			}
			return TimeSpan.FromSeconds(Math.Floor(timeSpan.TotalSeconds));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004954 File Offset: 0x00002B54
		private void GoBackToSettings()
		{
			LoggerExtensionMethods.LogDiagnostic<FinishViewModel>(this, "Go Back to settings", null, "GoBackToSettings", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\FinishViewModel.cs", 131);
			base.Navigation.GoBack(2);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A74 File Offset: 0x00002C74
		private async void GoBackToHome()
		{
			if (!(await this.operationsViewModel.CloseProject()))
			{
				LoggerExtensionMethods.LogDiagnostic<FinishViewModel>(this, "Go Back home", null, "GoBackToHome", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\FinishViewModel.cs", 139);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004AB0 File Offset: 0x00002CB0
		private void OpenExplorerToVideoFile()
		{
			LoggerExtensionMethods.LogDiagnostic<FinishViewModel>(this, "Open Video in explorer", null, "OpenExplorerToVideoFile", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\FinishViewModel.cs", 145);
			Process.Start(new ProcessStartInfo
			{
				FileName = "explorer.exe",
				Arguments = string.Format(string.Format("/select,\"{0}\"", this.project.OutputFile), new object[0])
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004825 File Offset: 0x00002A25
		[CompilerGenerated]
		private static bool <OnNavigatedTo>b__0()
		{
			return true;
		}

		// Token: 0x0200003F RID: 63
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GoBackToHome>d__3 : IAsyncStateMachine
		{
			// Token: 0x04000139 RID: 313
			public int <>1__state;

			// Token: 0x0400013A RID: 314
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x0400013B RID: 315
			public FinishViewModel <>4__this;

			// Token: 0x0400013C RID: 316
			private TaskAwaiter<bool> <>u__$awaiter4;

			// Token: 0x0400013D RID: 317
			private object <>t__stack;

			// Token: 0x06000274 RID: 628 RVA: 0x00004980 File Offset: 0x00002B80
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = this.operationsViewModel.CloseProject().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, FinishViewModel.<GoBackToHome>d__3>(ref taskAwaiter, ref this);
							return;
						}
					}
					else
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num2 = -1;
					}
					bool result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
					if (!result)
					{
						LoggerExtensionMethods.LogDiagnostic<FinishViewModel>(this, "Go Back home", null, "GoBackToHome", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\FinishViewModel.cs", 139);
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000275 RID: 629 RVA: 0x00004A64 File Offset: 0x00002C64
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
