using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200002C RID: 44
	public class ImportViewModel : ViewModel
	{
		// Token: 0x040000D8 RID: 216
		private Project project;

		// Token: 0x040000D9 RID: 217
		[CompilerGenerated]
		private NavigationViewModel <NavigationViewModel>k__BackingField;

		// Token: 0x040000DA RID: 218
		[CompilerGenerated]
		private WindowOperationsViewModel <WindowOperationsVM>k__BackingField;

		// Token: 0x040000DB RID: 219
		[CompilerGenerated]
		private Command <GoToSettingsCommand>k__BackingField;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private AsyncCommand <GoBackCommand>k__BackingField;

		// Token: 0x06000218 RID: 536 RVA: 0x0000A638 File Offset: 0x00008838
		public ImportViewModel(INavigation navigation, IUserInterface userInterface, WindowOperationsViewModel windowOperationsVM) : base(navigation, userInterface)
		{
			this.WindowOperationsVM = windowOperationsVM;
			this.NavigationViewModel = new NavigationViewModel(base.Navigation, "Import");
			this.GoToSettingsCommand = new Command(new Action(this.GoToSettings), () => this.VideoSource != null);
			this.GoBackCommand = new AsyncCommand(new Func<Task>(this.GoBack));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A6AC File Offset: 0x000088AC
		private void GoToSettings()
		{
			LoggerExtensionMethods.LogDiagnostic<ImportViewModel>(this, "Video Trimmed", new Dictionary<string, object>
			{
				{
					"StartTime",
					this.StartTime
				},
				{
					"EndTime",
					this.EndTime
				},
				{
					"VideoTotalLength",
					this.project.VideoInfo.Duration
				},
				{
					"TrimmedLength",
					this.project.InputLength
				},
				{
					"RotationAmount",
					this.RotationAmount
				}
			}, "GoToSettings", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ImportViewModel.cs", 39);
			base.Navigation.Navigate("Settings", new object[]
			{
				this.project
			});
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A775 File Offset: 0x00008975
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000A77D File Offset: 0x0000897D
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A786 File Offset: 0x00008986
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000A78E File Offset: 0x0000898E
		public WindowOperationsViewModel WindowOperationsVM
		{
			[CompilerGenerated]
			get
			{
				return this.<WindowOperationsVM>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WindowOperationsVM>k__BackingField = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A797 File Offset: 0x00008997
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A79F File Offset: 0x0000899F
		public Command GoToSettingsCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<GoToSettingsCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GoToSettingsCommand>k__BackingField = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A7A8 File Offset: 0x000089A8
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public AsyncCommand GoBackCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<GoBackCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GoBackCommand>k__BackingField = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000A7B9 File Offset: 0x000089B9
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public TimeSpan CurrentTime
		{
			get
			{
				return this.project.SelectedFrameTime;
			}
			set
			{
				if (value.Ticks != this.CurrentTime.Ticks)
				{
					this.project.SelectedFrameTime = value;
					this.NotifyPropertyChanged("CurrentTime");
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A803 File Offset: 0x00008A03
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000A810 File Offset: 0x00008A10
		public TimeSpan EndTime
		{
			get
			{
				return this.project.EndTime;
			}
			set
			{
				if (value.Ticks != this.EndTime.Ticks)
				{
					this.project.EndTime = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("EndTime");
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A857 File Offset: 0x00008A57
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000A864 File Offset: 0x00008A64
		public TimeSpan StartTime
		{
			get
			{
				return this.project.StartTime;
			}
			set
			{
				if (value.Ticks != this.StartTime.Ticks)
				{
					this.project.StartTime = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("StartTime");
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A8AB File Offset: 0x00008AAB
		public Rational FramesPerSecond
		{
			get
			{
				return this.project.VideoInfo.FramesPerSecond;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A8BD File Offset: 0x00008ABD
		public TimeSpan VideoDuration
		{
			get
			{
				return this.project.RoundedDuration;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A8CA File Offset: 0x00008ACA
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		public double RotationAmount
		{
			get
			{
				return this.project.VideoRotationAmount;
			}
			set
			{
				if (value != this.RotationAmount)
				{
					this.project.VideoRotationAmount = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("RotationAmount");
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A905 File Offset: 0x00008B05
		public Uri VideoSource
		{
			get
			{
				return this.project.VideoInfo.Filename;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A917 File Offset: 0x00008B17
		public override void OnNavigatedTo(object[] args)
		{
			base.OnNavigatedTo(args);
			this.project = (Project)args[0];
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AA04 File Offset: 0x00008C04
		private async Task GoBack()
		{
			await this.WindowOperationsVM.CloseProject();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A62A File Offset: 0x0000882A
		[CompilerGenerated]
		private bool <.ctor>b__0()
		{
			return this.VideoSource != null;
		}

		// Token: 0x0200004E RID: 78
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GoBack>d__3 : IAsyncStateMachine
		{
			// Token: 0x0400017C RID: 380
			public int <>1__state;

			// Token: 0x0400017D RID: 381
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400017E RID: 382
			public ImportViewModel <>4__this;

			// Token: 0x0400017F RID: 383
			private TaskAwaiter<bool> <>u__$awaiter4;

			// Token: 0x04000180 RID: 384
			private object <>t__stack;

			// Token: 0x0600028D RID: 653 RVA: 0x0000A930 File Offset: 0x00008B30
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = this.WindowOperationsVM.CloseProject().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, ImportViewModel.<GoBack>d__3>(ref taskAwaiter, ref this);
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
					taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
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

			// Token: 0x0600028E RID: 654 RVA: 0x0000A9F4 File Offset: 0x00008BF4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
