using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000028 RID: 40
	public class HyperlapseEngine
	{
		// Token: 0x040000C5 RID: 197
		private INativeHyperlapseEngine nativeEngine;

		// Token: 0x040000C6 RID: 198
		private Task hyperlapseTask;

		// Token: 0x040000C7 RID: 199
		private bool cancelRequested;

		// Token: 0x040000C8 RID: 200
		private EventHandler<ProcessingFinishedEventArgs> ProcessingFinished;

		// Token: 0x040000C9 RID: 201
		private EventHandler ProcessingCancelled;

		// Token: 0x040000CA RID: 202
		private EventHandler<ProcessingFailedEventArgs> ProcessingFailed;

		// Token: 0x040000CB RID: 203
		private EventHandler ProgressChanged;

		// Token: 0x040000CC RID: 204
		private EventHandler ActivationStatusChanged;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private bool <IsRunning>k__BackingField;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001D2 RID: 466 RVA: 0x000087D0 File Offset: 0x000069D0
		// (remove) Token: 0x060001D3 RID: 467 RVA: 0x00008808 File Offset: 0x00006A08
		public event EventHandler<ProcessingFinishedEventArgs> ProcessingFinished
		{
			add
			{
				EventHandler<ProcessingFinishedEventArgs> eventHandler = this.ProcessingFinished;
				EventHandler<ProcessingFinishedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ProcessingFinishedEventArgs> value2 = (EventHandler<ProcessingFinishedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ProcessingFinishedEventArgs>>(ref this.ProcessingFinished, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ProcessingFinishedEventArgs> eventHandler = this.ProcessingFinished;
				EventHandler<ProcessingFinishedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ProcessingFinishedEventArgs> value2 = (EventHandler<ProcessingFinishedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ProcessingFinishedEventArgs>>(ref this.ProcessingFinished, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001D4 RID: 468 RVA: 0x00008840 File Offset: 0x00006A40
		// (remove) Token: 0x060001D5 RID: 469 RVA: 0x00008878 File Offset: 0x00006A78
		public event EventHandler ProcessingCancelled
		{
			add
			{
				EventHandler eventHandler = this.ProcessingCancelled;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProcessingCancelled, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.ProcessingCancelled;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProcessingCancelled, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001D6 RID: 470 RVA: 0x000088B0 File Offset: 0x00006AB0
		// (remove) Token: 0x060001D7 RID: 471 RVA: 0x000088E8 File Offset: 0x00006AE8
		public event EventHandler<ProcessingFailedEventArgs> ProcessingFailed
		{
			add
			{
				EventHandler<ProcessingFailedEventArgs> eventHandler = this.ProcessingFailed;
				EventHandler<ProcessingFailedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ProcessingFailedEventArgs> value2 = (EventHandler<ProcessingFailedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ProcessingFailedEventArgs>>(ref this.ProcessingFailed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ProcessingFailedEventArgs> eventHandler = this.ProcessingFailed;
				EventHandler<ProcessingFailedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ProcessingFailedEventArgs> value2 = (EventHandler<ProcessingFailedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ProcessingFailedEventArgs>>(ref this.ProcessingFailed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001D8 RID: 472 RVA: 0x00008920 File Offset: 0x00006B20
		// (remove) Token: 0x060001D9 RID: 473 RVA: 0x00008958 File Offset: 0x00006B58
		public event EventHandler ProgressChanged
		{
			add
			{
				EventHandler eventHandler = this.ProgressChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProgressChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.ProgressChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProgressChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001DA RID: 474 RVA: 0x00008990 File Offset: 0x00006B90
		// (remove) Token: 0x060001DB RID: 475 RVA: 0x000089C8 File Offset: 0x00006BC8
		public event EventHandler ActivationStatusChanged
		{
			add
			{
				EventHandler eventHandler = this.ActivationStatusChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ActivationStatusChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.ActivationStatusChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ActivationStatusChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008A00 File Offset: 0x00006C00
		public HyperlapseEngine(INativeHyperlapseEngine nativeEngine, ProductInfo productInfo)
		{
			LoggerExtensionMethods.LogCheckpoint<HyperlapseEngine>(this, ".ctor", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 41);
			this.nativeEngine = nativeEngine;
			this.nativeEngine.SetActivationInfo(productInfo.ActivationConfigFile, productInfo.ActivationRegistrationFile);
			this.nativeEngine.ProgressChanged += this.engine_ProgressChanged;
			this.nativeEngine.ActivationStatusChanged += this.engine_ActivationStatusChanged;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008A71 File Offset: 0x00006C71
		public void Dispose()
		{
			this.nativeEngine.ProgressChanged -= this.engine_ProgressChanged;
			this.nativeEngine.ActivationStatusChanged -= this.engine_ActivationStatusChanged;
			this.nativeEngine = null;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public float TotalProgess
		{
			get
			{
				return this.nativeEngine.CurrentProgess;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008AB5 File Offset: 0x00006CB5
		public string Status
		{
			get
			{
				return this.nativeEngine.CurrentStatus;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008AC2 File Offset: 0x00006CC2
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00008ACA File Offset: 0x00006CCA
		public bool IsRunning
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRunning>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsRunning>k__BackingField = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008AD3 File Offset: 0x00006CD3
		public bool ProductIsActivated
		{
			get
			{
				return this.nativeEngine.ProductIsActivated;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public void Start(HyperlapseParameters param)
		{
			LoggerExtensionMethods.LogCheckpoint<HyperlapseEngine>(this, "Start", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 88);
			if (this.IsRunning)
			{
				LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, "Engine is already running!", null, "Start", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 91);
				return;
			}
			this.hyperlapseTask = this.RunHyperlapse(param);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008C54 File Offset: 0x00006E54
		public async Task Cancel()
		{
			LoggerExtensionMethods.LogCheckpoint<HyperlapseEngine>(this, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 100);
			this.cancelRequested = true;
			this.nativeEngine.CancelProcessing();
			await this.hyperlapseTask;
			EventHandlerExtensions.RaiseIfNotNull(this.ProcessingCancelled, this);
			this.cancelRequested = false;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000090E8 File Offset: 0x000072E8
		private async Task RunHyperlapse(HyperlapseParameters param)
		{
			DateTime startTime = DateTime.Now;
			LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Starting procesing: Video: {0}, Output: {1}, Calibration: {2}, Frames: {3}-{4}, Speedup: {5} ", new object[]
			{
				param.VideoUri.LocalPath,
				param.TempOutputDirectory,
				param.CalibrationFile.Location,
				param.StartFrame,
				param.EndFrame,
				param.SpeedupFactor
			}), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 113);
			this.IsRunning = true;
			bool success = false;
			await Task.Run(delegate()
			{
				try
				{
					string calibrationFile = param.UseAdvancedSmoothing ? param.CalibrationFile.ExtractToFolder(param.TempOutputDirectory) : string.Empty;
					success = this.nativeEngine.Process(param.RenderTarget, param.VideoUri.LocalPath, param.TempOutputDirectory, param.VideoOutputFilePath, calibrationFile, param.StartFrame, param.EndFrame, param.SpeedupFactor, param.FrameRate, param.OutputHeight, param.OutputWidth, param.OutputBitrate, param.VideoMode, param.OutputRotation, param.UseGeometryShaders, param.ForceSoftwareRendering, param.CreditLength, param.UseHardwareVideoEncoder);
				}
				catch (Exception arg)
				{
					LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Exception occured during processing: {0}", arg), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 127);
				}
			});
			this.IsRunning = false;
			if (success && !this.cancelRequested)
			{
				LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Processing succeeded! Took {0:c} to finish", DateTime.Now.Subtract(startTime)), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 135);
				EventHandlerExtensions.RaiseIfNotNull<ProcessingFinishedEventArgs>(this.ProcessingFinished, this, new ProcessingFinishedEventArgs(param));
			}
			else if (!this.cancelRequested)
			{
				LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Processing failed: {0}", this.nativeEngine.LastError), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 140);
				EventHandlerExtensions.RaiseIfNotNull<ProcessingFailedEventArgs>(this.ProcessingFailed, this, new ProcessingFailedEventArgs(this.nativeEngine.LastError));
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009136 File Offset: 0x00007336
		private void engine_ProgressChanged(object sender, EventArgs e)
		{
			EventHandlerExtensions.RaiseIfNotNull(this.ProgressChanged, this);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009144 File Offset: 0x00007344
		private void engine_ActivationStatusChanged(object sender, EventArgs e)
		{
			if (this.nativeEngine.TrialException != null)
			{
				LoggerExtensionMethods.LogError<HyperlapseEngine>(this, "Failed to get trial status", new Dictionary<string, object>
				{
					{
						"Exception",
						this.nativeEngine.TrialException
					},
					{
						"ErrorMessage",
						this.nativeEngine.TrialException.Message
					}
				}, "engine_ActivationStatusChanged", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 154);
			}
			EventHandlerExtensions.RaiseIfNotNull(this.ActivationStatusChanged, this);
		}

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Cancel>d__0 : IAsyncStateMachine
		{
			// Token: 0x04000164 RID: 356
			public int <>1__state;

			// Token: 0x04000165 RID: 357
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000166 RID: 358
			public HyperlapseEngine <>4__this;

			// Token: 0x04000167 RID: 359
			private TaskAwaiter <>u__$awaiter1;

			// Token: 0x04000168 RID: 360
			private object <>t__stack;

			// Token: 0x06000283 RID: 643 RVA: 0x00008B30 File Offset: 0x00006D30
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						LoggerExtensionMethods.LogCheckpoint<HyperlapseEngine>(this, "Cancel", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 100);
						this.cancelRequested = true;
						this.nativeEngine.CancelProcessing();
						taskAwaiter = this.hyperlapseTask.GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, HyperlapseEngine.<Cancel>d__0>(ref taskAwaiter, ref this);
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
					EventHandlerExtensions.RaiseIfNotNull(this.ProcessingCancelled, this);
					this.cancelRequested = false;
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

			// Token: 0x06000284 RID: 644 RVA: 0x00008C44 File Offset: 0x00006E44
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4
		{
			// Token: 0x04000169 RID: 361
			public bool success;

			// Token: 0x0400016A RID: 362
			public HyperlapseEngine <>4__this;

			// Token: 0x0400016B RID: 363
			public HyperlapseParameters param;

			// Token: 0x06000285 RID: 645 RVA: 0x00008C9A File Offset: 0x00006E9A
			public <>c__DisplayClass4()
			{
			}

			// Token: 0x06000286 RID: 646 RVA: 0x00008CA4 File Offset: 0x00006EA4
			public void <RunHyperlapse>b__3()
			{
				try
				{
					string calibrationFile = this.param.UseAdvancedSmoothing ? this.param.CalibrationFile.ExtractToFolder(this.param.TempOutputDirectory) : string.Empty;
					this.success = this.<>4__this.nativeEngine.Process(this.param.RenderTarget, this.param.VideoUri.LocalPath, this.param.TempOutputDirectory, this.param.VideoOutputFilePath, calibrationFile, this.param.StartFrame, this.param.EndFrame, this.param.SpeedupFactor, this.param.FrameRate, this.param.OutputHeight, this.param.OutputWidth, this.param.OutputBitrate, this.param.VideoMode, this.param.OutputRotation, this.param.UseGeometryShaders, this.param.ForceSoftwareRendering, this.param.CreditLength, this.param.UseHardwareVideoEncoder);
				}
				catch (Exception arg)
				{
					LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this.<>4__this, string.Format("Exception occured during processing: {0}", arg), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 127);
				}
			}
		}

		// Token: 0x0200004A RID: 74
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunHyperlapse>d__6 : IAsyncStateMachine
		{
			// Token: 0x0400016C RID: 364
			public int <>1__state;

			// Token: 0x0400016D RID: 365
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400016E RID: 366
			public HyperlapseEngine <>4__this;

			// Token: 0x0400016F RID: 367
			public HyperlapseParameters param;

			// Token: 0x04000170 RID: 368
			public DateTime <startTime>5__7;

			// Token: 0x04000171 RID: 369
			public HyperlapseEngine.<>c__DisplayClass4 CS$<>8__locals5;

			// Token: 0x04000172 RID: 370
			private TaskAwaiter <>u__$awaiter8;

			// Token: 0x04000173 RID: 371
			private object <>t__stack;

			// Token: 0x06000287 RID: 647 RVA: 0x00008DFC File Offset: 0x00006FFC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						CS$<>8__locals1 = new HyperlapseEngine.<>c__DisplayClass4();
						CS$<>8__locals1.param = param;
						CS$<>8__locals1.<>4__this = this;
						startTime = DateTime.Now;
						LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Starting procesing: Video: {0}, Output: {1}, Calibration: {2}, Frames: {3}-{4}, Speedup: {5} ", new object[]
						{
							CS$<>8__locals1.param.VideoUri.LocalPath,
							CS$<>8__locals1.param.TempOutputDirectory,
							CS$<>8__locals1.param.CalibrationFile.Location,
							CS$<>8__locals1.param.StartFrame,
							CS$<>8__locals1.param.EndFrame,
							CS$<>8__locals1.param.SpeedupFactor
						}), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 113);
						this.IsRunning = true;
						CS$<>8__locals1.success = false;
						taskAwaiter = Task.Run(new Action(CS$<>8__locals1.<RunHyperlapse>b__3)).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, HyperlapseEngine.<RunHyperlapse>d__6>(ref taskAwaiter, ref this);
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
					this.IsRunning = false;
					if (CS$<>8__locals1.success && !this.cancelRequested)
					{
						LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Processing succeeded! Took {0:c} to finish", DateTime.Now.Subtract(startTime)), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 135);
						EventHandlerExtensions.RaiseIfNotNull<ProcessingFinishedEventArgs>(this.ProcessingFinished, this, new ProcessingFinishedEventArgs(CS$<>8__locals1.param));
					}
					else if (!this.cancelRequested)
					{
						LoggerExtensionMethods.LogDiagnostic<HyperlapseEngine>(this, string.Format("Processing failed: {0}", this.nativeEngine.LastError), null, "RunHyperlapse", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\HyperlapseEngine.cs", 140);
						EventHandlerExtensions.RaiseIfNotNull<ProcessingFailedEventArgs>(this.ProcessingFailed, this, new ProcessingFailedEventArgs(this.nativeEngine.LastError));
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

			// Token: 0x06000288 RID: 648 RVA: 0x000090D8 File Offset: 0x000072D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
