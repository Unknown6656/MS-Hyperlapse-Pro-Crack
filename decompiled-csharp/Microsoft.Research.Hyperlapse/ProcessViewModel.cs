using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000F RID: 15
	public class ProcessViewModel : ViewModel
	{
		// Token: 0x0400003F RID: 63
		private Project project;

		// Token: 0x04000040 RID: 64
		private HyperlapseEngine engine;

		// Token: 0x04000041 RID: 65
		private DateTime processingStart;

		// Token: 0x04000042 RID: 66
		private DateTime lastUiUpdate;

		// Token: 0x04000043 RID: 67
		private bool isFailed;

		// Token: 0x04000044 RID: 68
		private WindowOperationsViewModel windowOperationsVM;

		// Token: 0x04000045 RID: 69
		private AccelerationOptions accelerationOptions;

		// Token: 0x04000046 RID: 70
		private VideoBitrateEstimator videoBitrateEstimator;

		// Token: 0x04000047 RID: 71
		private FirstRunExperience firstRunExperience;

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private NavigationViewModel <NavigationViewModel>k__BackingField;

		// Token: 0x04000049 RID: 73
		[CompilerGenerated]
		private AsyncCommand <CancelCommand>k__BackingField;

		// Token: 0x0400004A RID: 74
		[CompilerGenerated]
		private Command<int> <StartProcessingCommand>k__BackingField;

		// Token: 0x060000B3 RID: 179 RVA: 0x00004C5C File Offset: 0x00002E5C
		public ProcessViewModel(INavigation navigation, IUserInterface userInterface, HyperlapseEngine engine, WindowOperationsViewModel windowOperationsVM, AccelerationOptions accelerationOptions, VideoBitrateEstimator videoBitrateEstimator, FirstRunExperience firstRunExperience) : base(navigation, userInterface)
		{
			this.NavigationViewModel = new NavigationViewModel(navigation, "Process");
			this.CancelCommand = new AsyncCommand(new Func<Task>(this.Cancel));
			this.StartProcessingCommand = new Command<int>(new Action<int>(this.StartProcessing));
			this.windowOperationsVM = windowOperationsVM;
			this.accelerationOptions = accelerationOptions;
			this.videoBitrateEstimator = videoBitrateEstimator;
			this.firstRunExperience = firstRunExperience;
			this.engine = engine;
			this.engine.ProgressChanged += this.engine_ProgressChanged;
			this.engine.ProcessingFinished += this.engine_ProcessingFinished;
			this.engine.ProcessingFailed += this.engine_ProcessingFailed;
			this.engine.ProcessingCancelled += this.engine_ProcessingCancelled;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004D34 File Offset: 0x00002F34
		private void StartProcessing(int renderTarget)
		{
			this.windowOperationsVM.OperationsDisabled = true;
			LoggerExtensionMethods.LogEvent<ProcessViewModel>(this, "Processing Started", this.GetStartProcessingProperties(), "StartProcessing", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 68);
			HyperlapseParameters hyperlapseParameters = new HyperlapseParameters();
			hyperlapseParameters.CalibrationFile = this.project.CalibrationInfo.Calibration;
			hyperlapseParameters.EndFrame = this.project.ConvertTimeSpanToFrameTime(this.project.EndTime);
			hyperlapseParameters.FrameRate = this.project.OutputFramesPerSecond;
			hyperlapseParameters.OutputHeight = (int)this.project.OutputSize.Height;
			hyperlapseParameters.OutputRotation = (int)this.project.VideoRotationAmount;
			hyperlapseParameters.OutputWidth = (int)this.project.OutputSize.Width;
			hyperlapseParameters.OutputBitrate = (int)this.videoBitrateEstimator.EstimateBitsPerSecond(this.project.VideoInfo.BitsPerSecond, (double)this.project.VideoInfo.Width, (double)this.project.VideoInfo.Height, this.project.OutputSize.Width, this.project.OutputSize.Height, this.project.VideoInfo.FramesPerSecond, this.project.OutputFramesPerSecond);
			hyperlapseParameters.RenderTarget = renderTarget;
			hyperlapseParameters.SpeedupFactor = this.project.SpeedupFactor;
			hyperlapseParameters.VideoMode = this.project.CalibrationInfo.VideoMode;
			hyperlapseParameters.VideoUri = this.project.VideoInfo.Filename;
			hyperlapseParameters.VideoOutputFilePath = this.project.OutputFile;
			hyperlapseParameters.TempOutputDirectory = this.project.WorkingDirectory;
			hyperlapseParameters.StartFrame = this.project.ConvertTimeSpanToFrameTime(this.project.StartTime);
			hyperlapseParameters.UseAdvancedSmoothing = this.project.UseAdvancedSmoothing;
			hyperlapseParameters.CreditLength = (float)this.project.CreditLength.TotalMilliseconds / 1000f;
			hyperlapseParameters.ForceSoftwareRendering = this.accelerationOptions.ForceSoftwareRendering;
			hyperlapseParameters.UseGeometryShaders = this.accelerationOptions.UseGeometryShaders;
			hyperlapseParameters.UseHardwareVideoEncoder = this.accelerationOptions.UseHardwareVideoEncoder;
			this.processingStart = DateTime.Now;
			this.lastUiUpdate = DateTime.Now;
			this.engine.Start(hyperlapseParameters);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004F7C File Offset: 0x0000317C
		private void engine_ProcessingCancelled(object sender, EventArgs e)
		{
			LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Processing Cancelled", new Dictionary<string, object>
			{
				{
					"TimeToCancellationInSeconds",
					DateTime.Now.Subtract(this.processingStart).TotalSeconds
				}
			}, "engine_ProcessingCancelled", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 101);
			this.DeleteEngine();
			base.Navigation.GoBack();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000051A8 File Offset: 0x000033A8
		private async void engine_ProcessingFailed(object sender, ProcessingFailedEventArgs e)
		{
			LoggerExtensionMethods.LogEvent<ProcessViewModel>(this, "Processing failed", new Dictionary<string, object>
			{
				{
					"Failure",
					e.ErrorMessage
				},
				{
					"Progress",
					this.TotalProgress
				},
				{
					"TimeToFailureInSeconds",
					DateTime.Now.Subtract(this.processingStart).TotalSeconds
				}
			}, "engine_ProcessingFailed", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 108);
			this.IsFailed = true;
			await base.UserInterface.ShowMessage("Processing failed", "Sorry, but the processing failed\n\nError was: " + e.ErrorMessage);
			this.DeleteEngine();
			base.Navigation.GoBack();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000051EC File Offset: 0x000033EC
		private void DeleteEngine()
		{
			this.windowOperationsVM.OperationsDisabled = false;
			this.engine.ProgressChanged -= this.engine_ProgressChanged;
			this.engine.ProcessingFinished -= this.engine_ProcessingFinished;
			this.engine.ProcessingFailed -= this.engine_ProcessingFailed;
			this.engine.ProcessingCancelled -= this.engine_ProcessingCancelled;
			this.engine.Dispose();
			this.engine = null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005274 File Offset: 0x00003474
		private void engine_ProgressChanged(object sender, EventArgs e)
		{
			base.NotifyPropertyChanged<double>(() => this.TotalProgress);
			base.NotifyPropertyChanged<string>(() => this.Status);
			if (DateTime.Now.Subtract(this.lastUiUpdate).TotalSeconds >= 1.0)
			{
				this.lastUiUpdate = DateTime.Now;
				base.NotifyPropertyChanged<TimeSpan>(() => this.FrameTimeToDisplay);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005360 File Offset: 0x00003560
		private void engine_ProcessingFinished(object sender, ProcessingFinishedEventArgs e)
		{
			this.firstRunExperience.VideosProcessed++;
			LoggerExtensionMethods.LogEvent<ProcessViewModel>(this, "Processing finished", new Dictionary<string, object>
			{
				{
					"ProcessingTimeInSeconds",
					DateTime.Now.Subtract(this.processingStart).TotalSeconds
				},
				{
					"TotalVideosProcessed",
					this.firstRunExperience.VideosProcessed
				}
			}, "engine_ProcessingFinished", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 142);
			this.DeleteEngine();
			base.Navigation.Navigate("Finish", new object[]
			{
				this.project
			});
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000540E File Offset: 0x0000360E
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005416 File Offset: 0x00003616
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000541F File Offset: 0x0000361F
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005427 File Offset: 0x00003627
		public AsyncCommand CancelCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<CancelCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CancelCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005430 File Offset: 0x00003630
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005438 File Offset: 0x00003638
		public Command<int> StartProcessingCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<StartProcessingCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StartProcessingCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005441 File Offset: 0x00003641
		public HyperlapseEngine Engine
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005449 File Offset: 0x00003649
		public double TotalProgress
		{
			get
			{
				return (double)this.engine.TotalProgess;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005457 File Offset: 0x00003657
		public string Status
		{
			get
			{
				return this.engine.Status;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005464 File Offset: 0x00003664
		public Uri SourceVideo
		{
			get
			{
				return this.project.VideoInfo.Filename;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005476 File Offset: 0x00003676
		public double RotationAmount
		{
			get
			{
				return this.project.VideoRotationAmount;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005484 File Offset: 0x00003684
		public TimeSpan FrameTimeToDisplay
		{
			get
			{
				double totalMilliseconds = this.project.InputLength.TotalMilliseconds;
				double value = totalMilliseconds * this.TotalProgress;
				return TimeSpan.FromMilliseconds(value).Add(this.project.StartTime);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000054CA File Offset: 0x000036CA
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000054D2 File Offset: 0x000036D2
		public bool IsFailed
		{
			get
			{
				return this.isFailed;
			}
			set
			{
				this.isFailed = value;
				this.NotifyPropertyChanged("IsFailed");
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000056CC File Offset: 0x000038CC
		private async Task Cancel()
		{
			LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel requested", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 232);
			bool confirm = await base.UserInterface.ShowConfirmMessage("Cancel Processing?", "All unsaved processing will be lost.\nAre you sure you want to cancel?");
			if (confirm)
			{
				DateTime now = DateTime.Now;
				LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel initiated", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 237);
				await this.engine.Cancel();
			}
			else
			{
				LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel aborted", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 242);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005712 File Offset: 0x00003912
		public override void OnNavigatedTo(object[] args)
		{
			LoggerExtensionMethods.LogCheckpoint<ProcessViewModel>(this, "OnNavigatedTo", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 248);
			this.project = (Project)args[0];
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005738 File Offset: 0x00003938
		private Dictionary<string, object> GetStartProcessingProperties()
		{
			return new Dictionary<string, object>
			{
				{
					"InputVideoLengthInSeconds",
					this.project.VideoInfo.Duration.TotalSeconds
				},
				{
					"InputVideoWidth",
					this.project.VideoInfo.Width
				},
				{
					"InputVideoHeight",
					this.project.VideoInfo.Height
				},
				{
					"InputFramesPerSecond",
					this.project.VideoInfo.FramesPerSecond.AsFloat()
				},
				{
					"OriginalBitsPerSecond",
					this.project.VideoInfo.OriginalBitsPerSecond
				},
				{
					"CorrectedBitsPerSecond",
					this.project.VideoInfo.BitsPerSecond
				},
				{
					"AutoDectedCameraModel",
					this.project.VideoInfo.CameraModel
				},
				{
					"SpeedupFactor",
					this.project.SpeedupFactor
				},
				{
					"OutputLength",
					this.project.OutputLength
				},
				{
					"FrameRate",
					this.project.OutputFramesPerSecond.AsFloat()
				},
				{
					"OutputWidth",
					this.project.OutputSize.Width
				},
				{
					"OutputHeight",
					this.project.OutputSize.Height
				},
				{
					"Calibration",
					this.project.CalibrationInfo.Calibration.Description
				},
				{
					"VideoMode",
					this.project.CalibrationInfo.VideoMode
				},
				{
					"UseAdvancedSmoothing",
					this.project.UseAdvancedSmoothing
				},
				{
					"CalibrationAutoDetected",
					this.project.CalibrationInfo.WasAutoSelected
				},
				{
					"HousingOn",
					this.project.CalibrationInfo.Calibration.HousingOn
				},
				{
					"IncludeEndCredit",
					this.project.CreditLength != TimeSpan.Zero
				}
			};
		}

		// Token: 0x02000041 RID: 65
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <engine_ProcessingFailed>d__2 : IAsyncStateMachine
		{
			// Token: 0x0400013F RID: 319
			public int <>1__state;

			// Token: 0x04000140 RID: 320
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x04000141 RID: 321
			public ProcessViewModel <>4__this;

			// Token: 0x04000142 RID: 322
			public ProcessingFailedEventArgs e;

			// Token: 0x04000143 RID: 323
			public Dictionary<string, object> <>g__initLocal1;

			// Token: 0x04000144 RID: 324
			private TaskAwaiter <>u__$awaiter3;

			// Token: 0x04000145 RID: 325
			private object <>t__stack;

			// Token: 0x06000279 RID: 633 RVA: 0x00004FE4 File Offset: 0x000031E4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						ProcessViewModel processViewModel = this;
						string text = "Processing failed";
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						dictionary.Add("Failure", e.ErrorMessage);
						dictionary.Add("Progress", this.TotalProgress);
						dictionary.Add("TimeToFailureInSeconds", DateTime.Now.Subtract(this.processingStart).TotalSeconds);
						LoggerExtensionMethods.LogEvent<ProcessViewModel>(processViewModel, text, dictionary, "engine_ProcessingFailed", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 108);
						this.IsFailed = true;
						taskAwaiter = base.UserInterface.ShowMessage("Processing failed", "Sorry, but the processing failed\n\nError was: " + e.ErrorMessage).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ProcessViewModel.<engine_ProcessingFailed>d__2>(ref taskAwaiter, ref this);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter);
					this.DeleteEngine();
					base.Navigation.GoBack();
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

			// Token: 0x0600027A RID: 634 RVA: 0x00005198 File Offset: 0x00003398
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x02000042 RID: 66
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Cancel>d__6 : IAsyncStateMachine
		{
			// Token: 0x04000146 RID: 326
			public int <>1__state;

			// Token: 0x04000147 RID: 327
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000148 RID: 328
			public ProcessViewModel <>4__this;

			// Token: 0x04000149 RID: 329
			public bool <confirm>5__7;

			// Token: 0x0400014A RID: 330
			private TaskAwaiter<bool> <>u__$awaiter8;

			// Token: 0x0400014B RID: 331
			private object <>t__stack;

			// Token: 0x0400014C RID: 332
			private TaskAwaiter <>u__$awaiter9;

			// Token: 0x0600027B RID: 635 RVA: 0x000054E8 File Offset: 0x000036E8
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				try
				{
					TaskAwaiter<bool> taskAwaiter;
					TaskAwaiter taskAwaiter3;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num = -1;
						break;
					}
					case 1:
					{
						TaskAwaiter taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter);
						num = -1;
						goto IL_14B;
					}
					default:
						LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel requested", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 232);
						taskAwaiter = base.UserInterface.ShowConfirmMessage("Cancel Processing?", "All unsaved processing will be lost.\nAre you sure you want to cancel?").GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, ProcessViewModel.<Cancel>d__6>(ref taskAwaiter, ref this);
							return;
						}
						break;
					}
					bool result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
					bool flag = result;
					confirm = flag;
					if (!confirm)
					{
						LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel aborted", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 242);
						goto IL_17C;
					}
					DateTime now = DateTime.Now;
					LoggerExtensionMethods.LogDiagnostic<ProcessViewModel>(this, "Cancel initiated", null, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\ProcessViewModel.cs", 237);
					taskAwaiter3 = this.engine.Cancel().GetAwaiter();
					if (!taskAwaiter3.IsCompleted)
					{
						num = 1;
						TaskAwaiter taskAwaiter4 = taskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ProcessViewModel.<Cancel>d__6>(ref taskAwaiter3, ref this);
						return;
					}
					IL_14B:
					taskAwaiter3.GetResult();
					taskAwaiter3 = default(TaskAwaiter);
					IL_17C:;
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600027C RID: 636 RVA: 0x000056BC File Offset: 0x000038BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
