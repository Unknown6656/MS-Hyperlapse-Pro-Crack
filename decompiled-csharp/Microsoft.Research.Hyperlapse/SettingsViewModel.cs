using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200002A RID: 42
	public class SettingsViewModel : ViewModel
	{
		// Token: 0x040000D1 RID: 209
		private Project project;

		// Token: 0x040000D2 RID: 210
		private CalibrationProvider calibrationProvider;

		// Token: 0x040000D3 RID: 211
		private ProcessingTimeEstimator processTimeEstimator = new ProcessingTimeEstimator();

		// Token: 0x040000D4 RID: 212
		private ActivationManager activationManager;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		private NavigationViewModel <NavigationViewModel>k__BackingField;

		// Token: 0x040000D6 RID: 214
		[CompilerGenerated]
		private AsyncCommand <NextCommand>k__BackingField;

		// Token: 0x040000D7 RID: 215
		[CompilerGenerated]
		private static Func<Calibration, string> CS$<>9__CachedAnonymousMethodDelegate2;

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009A1A File Offset: 0x00007C1A
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00009A22 File Offset: 0x00007C22
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009A2B File Offset: 0x00007C2B
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00009A33 File Offset: 0x00007C33
		public AsyncCommand NextCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<NextCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NextCommand>k__BackingField = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009A3C File Offset: 0x00007C3C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00009A4C File Offset: 0x00007C4C
		public int SpeedUpFactor
		{
			get
			{
				return this.project.SpeedupFactor;
			}
			set
			{
				if (value != this.SpeedUpFactor)
				{
					this.project.SpeedupFactor = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("SpeedUpFactor");
					base.NotifyPropertyChanged<TimeSpan>(() => this.OutputLength);
					base.NotifyPropertyChanged<string>(() => this.OutputLocation);
					if (this.project.SpeedupFactor == 1)
					{
						this.project.OutputFramesPerSecond = this.project.VideoInfo.FramesPerSecond;
					}
					base.NotifyPropertyChanged<Rational>(() => this.FrameRate);
					base.NotifyPropertyChanged<List<Rational>>(() => this.AvailableFrameRates);
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009B99 File Offset: 0x00007D99
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00009BA6 File Offset: 0x00007DA6
		public string OutputLocation
		{
			get
			{
				return this.project.OutputFile;
			}
			set
			{
				if (value != this.OutputLocation)
				{
					this.project.OutputFile = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("OutputLocation");
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009BD9 File Offset: 0x00007DD9
		public TimeSpan OutputLength
		{
			get
			{
				return this.project.OutputLength;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009BE6 File Offset: 0x00007DE6
		public TimeSpan OriginalInputLength
		{
			get
			{
				return this.project.VideoInfo.Duration;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public TimeSpan TrimmedInputLength
		{
			get
			{
				return this.project.InputLength;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009C05 File Offset: 0x00007E05
		public double RotationAmount
		{
			get
			{
				return this.project.VideoRotationAmount;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009C12 File Offset: 0x00007E12
		public Uri VideoFile
		{
			get
			{
				return this.project.VideoInfo.Filename;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009C24 File Offset: 0x00007E24
		public TimeSpan FrameTime
		{
			get
			{
				return this.project.SelectedFrameTime;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009C31 File Offset: 0x00007E31
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00009C40 File Offset: 0x00007E40
		public Rational FrameRate
		{
			get
			{
				return this.project.OutputFramesPerSecond;
			}
			set
			{
				if (value != this.FrameRate)
				{
					this.project.OutputFramesPerSecond = value;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("FrameRate");
					base.NotifyPropertyChanged<List<Size>>(() => this.AvailableOutputSizes);
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009CB3 File Offset: 0x00007EB3
		public TimeSpan EstimatedProcessingTime
		{
			get
			{
				return this.processTimeEstimator.EstimateTime(this.TrimmedInputLength, this.UseAdvancedSmoothing);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009CCC File Offset: 0x00007ECC
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public Size OutputSize
		{
			get
			{
				return this.SwapSizeIfRotated(this.project.OutputSize);
			}
			set
			{
				Size size = this.SwapSizeIfRotated(value);
				if (size.Width != this.OutputSize.Width && size.Height != this.OutputSize.Height)
				{
					this.project.OutputSize = size;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("OutputSize");
					base.NotifyPropertyChanged<List<Rational>>(() => this.AvailableFrameRates);
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00009D78 File Offset: 0x00007F78
		public List<Rational> AvailableFrameRates
		{
			get
			{
				if (this.SpeedUpFactor == 1)
				{
					return new List<Rational>
					{
						this.project.VideoInfo.FramesPerSecond
					};
				}
				return this.project.GetAvailabledOutputFrameRates();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009DB7 File Offset: 0x00007FB7
		public List<Size> AvailableOutputSizes
		{
			get
			{
				return this.project.GetAvailableOutputSizes().Select(new Func<Size, Size>(this.SwapSizeIfRotated)).ToList<Size>();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009DE2 File Offset: 0x00007FE2
		public List<string> CalibrationFiles
		{
			get
			{
				return (from c in this.calibrationProvider.GetCalibrations()
				select c.Description).ToList<string>();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009E16 File Offset: 0x00008016
		public List<string> VideoModes
		{
			get
			{
				return this.project.CalibrationInfo.Calibration.VideoModes;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009E2D File Offset: 0x0000802D
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00009E60 File Offset: 0x00008060
		public string CalibrationFile
		{
			get
			{
				return this.project.CalibrationInfo.Calibration.Description;
			}
			set
			{
				if (this.CalibrationFile != value)
				{
					this.project.CalibrationInfo.Calibration = this.calibrationProvider.GetCalibrations().Single((Calibration c) => c.Description == value);
					this.project.CalibrationInfo.WasAutoSelected = false;
					this.project.IsSaved = false;
					base.NotifyPropertyChanged<string>(() => this.VideoMode);
					base.NotifyPropertyChanged<List<string>>(() => this.VideoModes);
					this.NotifyPropertyChanged("CalibrationFile");
					base.NotifyPropertyChanged<bool>(() => this.UseAdvancedSmoothing);
					base.NotifyPropertyChanged<bool>(() => this.UseAdvancedSmoothingSettingEnabled);
					base.NotifyPropertyChanged<bool>(() => this.IsCalibrationFileManual);
					base.NotifyPropertyChanged<bool>(() => this.CalibrationSupportsHousing);
					base.NotifyPropertyChanged<bool>(() => this.CalibrationHousingOn);
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000A088 File Offset: 0x00008288
		public bool IsCalibrationFileManual
		{
			get
			{
				return !this.project.CalibrationInfo.WasAutoSelected;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A09D File Offset: 0x0000829D
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public string VideoMode
		{
			get
			{
				return this.project.CalibrationInfo.VideoMode;
			}
			set
			{
				if (string.Compare(this.VideoMode, value) != 0)
				{
					this.project.CalibrationInfo.VideoMode = value;
					this.project.CalibrationInfo.WasAutoSelected = false;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("VideoMode");
					base.NotifyPropertyChanged<bool>(() => this.IsCalibrationFileManual);
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A13E File Offset: 0x0000833E
		public bool CalibrationSupportsHousing
		{
			get
			{
				return this.project.CalibrationInfo.Calibration.SupportsHousing;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A155 File Offset: 0x00008355
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A16C File Offset: 0x0000836C
		public bool CalibrationHousingOn
		{
			get
			{
				return this.project.CalibrationInfo.Calibration.HousingOn;
			}
			set
			{
				this.project.CalibrationInfo.Calibration.HousingOn = value;
				this.NotifyPropertyChanged("CalibrationHousingOn");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A18F File Offset: 0x0000838F
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A19C File Offset: 0x0000839C
		public bool UseAdvancedSmoothing
		{
			get
			{
				return this.project.UseAdvancedSmoothing;
			}
			set
			{
				if (value != this.project.UseAdvancedSmoothing)
				{
					this.project.UseAdvancedSmoothing = value;
					this.project.IsSaved = false;
					base.NotifyPropertyChanged<string>(() => this.OutputLocation);
					base.NotifyPropertyChanged<bool>(() => this.UseAdvancedSmoothing);
					base.NotifyPropertyChanged<bool>(() => this.UseAdvancedSmoothingSettingEnabled);
					base.NotifyPropertyChanged<TimeSpan>(() => this.EstimatedProcessingTime);
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A2BA File Offset: 0x000084BA
		public bool UseAdvancedSmoothingSettingEnabled
		{
			get
			{
				return this.project.UseAdvancedSmoothingSettingEnabled;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A2C7 File Offset: 0x000084C7
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public bool IncludeEndCredit
		{
			get
			{
				return !this.IncludeEndCreditSettingIsEnabled || !this.project.CreditDisabled;
			}
			set
			{
				bool flag = !value;
				if (this.project.CreditDisabled != flag)
				{
					this.project.CreditDisabled = flag;
					this.project.IsSaved = false;
					this.NotifyPropertyChanged("IncludeEndCredit");
					base.NotifyPropertyChanged<TimeSpan>(() => this.OutputLength);
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A361 File Offset: 0x00008561
		public bool IncludeEndCreditSettingIsEnabled
		{
			get
			{
				return this.activationManager.GetActivationStatus();
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A37C File Offset: 0x0000857C
		public SettingsViewModel(INavigation navigation, IUserInterface userInterface, CalibrationProvider calibrationProvider, ActivationManager activationManager) : base(navigation, userInterface)
		{
			if (calibrationProvider == null)
			{
				throw new ArgumentNullException("calibrationProvider");
			}
			this.calibrationProvider = calibrationProvider;
			if (activationManager == null)
			{
				throw new ArgumentNullException("activationManager");
			}
			this.activationManager = activationManager;
			this.NavigationViewModel = new NavigationViewModel(navigation, "Settings");
			this.NextCommand = new AsyncCommand(new Func<Task>(this.GoToProcess), () => this.OutputLocation != null);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A594 File Offset: 0x00008794
		private async Task GoToProcess()
		{
			bool confirmOverwrite = true;
			if (File.Exists(this.project.OutputFile))
			{
				LoggerExtensionMethods.LogDiagnostic<SettingsViewModel>(this, "Output file exists", null, "GoToProcess", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\SettingsViewModel.cs", 378);
				confirmOverwrite = await base.UserInterface.ShowConfirmMessage("Overwrite file?", "The output file already exists, are you sure you want to overwrite it?");
			}
			if (confirmOverwrite)
			{
				LoggerExtensionMethods.LogDiagnostic<SettingsViewModel>(this, "Chose settings", null, "GoToProcess", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\SettingsViewModel.cs", 384);
				base.Navigation.Navigate("Process", new object[]
				{
					this.project
				});
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A5DA File Offset: 0x000087DA
		public override void OnNavigatedTo(object[] args)
		{
			this.project = (Project)args[0];
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A5EA File Offset: 0x000087EA
		private Size SwapSizeIfRotated(Size s)
		{
			if (this.RotationAmount % 360.0 != 270.0 && this.RotationAmount % 360.0 != 90.0)
			{
				return s;
			}
			return s.SwapDimensions();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009DDA File Offset: 0x00007FDA
		[CompilerGenerated]
		private static string <get_CalibrationFiles>b__1(Calibration c)
		{
			return c.Description;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A36E File Offset: 0x0000856E
		[CompilerGenerated]
		private bool <.ctor>b__7()
		{
			return this.OutputLocation != null;
		}

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5
		{
			// Token: 0x04000174 RID: 372
			public SettingsViewModel <>4__this;

			// Token: 0x04000175 RID: 373
			public string value;

			// Token: 0x06000289 RID: 649 RVA: 0x00009E44 File Offset: 0x00008044
			public <>c__DisplayClass5()
			{
			}

			// Token: 0x0600028A RID: 650 RVA: 0x00009E4C File Offset: 0x0000804C
			public bool <set_CalibrationFile>b__3(Calibration c)
			{
				return c.Description == this.value;
			}
		}

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GoToProcess>d__9 : IAsyncStateMachine
		{
			// Token: 0x04000176 RID: 374
			public int <>1__state;

			// Token: 0x04000177 RID: 375
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000178 RID: 376
			public SettingsViewModel <>4__this;

			// Token: 0x04000179 RID: 377
			public bool <confirmOverwrite>5__a;

			// Token: 0x0400017A RID: 378
			private TaskAwaiter<bool> <>u__$awaiterb;

			// Token: 0x0400017B RID: 379
			private object <>t__stack;

			// Token: 0x0600028B RID: 651 RVA: 0x0000A404 File Offset: 0x00008604
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						confirmOverwrite = true;
						if (!File.Exists(this.project.OutputFile))
						{
							goto IL_D4;
						}
						LoggerExtensionMethods.LogDiagnostic<SettingsViewModel>(this, "Output file exists", null, "GoToProcess", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\SettingsViewModel.cs", 378);
						taskAwaiter = base.UserInterface.ShowConfirmMessage("Overwrite file?", "The output file already exists, are you sure you want to overwrite it?").GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, SettingsViewModel.<GoToProcess>d__9>(ref taskAwaiter, ref this);
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
					bool flag = result;
					confirmOverwrite = flag;
					IL_D4:
					if (confirmOverwrite)
					{
						LoggerExtensionMethods.LogDiagnostic<SettingsViewModel>(this, "Chose settings", null, "GoToProcess", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\SettingsViewModel.cs", 384);
						base.Navigation.Navigate("Process", new object[]
						{
							this.project
						});
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

			// Token: 0x0600028C RID: 652 RVA: 0x0000A584 File Offset: 0x00008784
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
