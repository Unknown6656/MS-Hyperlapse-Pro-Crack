using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000020 RID: 32
	public class MainWindowViewModel : ViewModel
	{
		// Token: 0x040000B1 RID: 177
		private ScratchManager scratchManager;

		// Token: 0x040000B2 RID: 178
		private FirstRunExperience firstRun;

		// Token: 0x040000B3 RID: 179
		private ActivationManager activationManager;

		// Token: 0x040000B4 RID: 180
		[CompilerGenerated]
		private WindowOperationsViewModel <WindowOperationsViewModel>k__BackingField;

		// Token: 0x060001A9 RID: 425 RVA: 0x00007A69 File Offset: 0x00005C69
		public MainWindowViewModel(INavigation navigation, IUserInterface userInterface, WindowOperationsViewModel windowVM, ScratchManager scratchManager, FirstRunExperience firstRun, ActivationManager activationManager) : base(navigation, userInterface)
		{
			this.WindowOperationsViewModel = windowVM;
			this.scratchManager = scratchManager;
			this.firstRun = firstRun;
			this.activationManager = activationManager;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007A92 File Offset: 0x00005C92
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007A9A File Offset: 0x00005C9A
		public WindowOperationsViewModel WindowOperationsViewModel
		{
			[CompilerGenerated]
			get
			{
				return this.<WindowOperationsViewModel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WindowOperationsViewModel>k__BackingField = value;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007AA3 File Offset: 0x00005CA3
		public override void OnNavigatedTo(object[] args)
		{
			LoggerExtensionMethods.LogEvent<MainWindowViewModel>(this, "App Started", this.GetStartupProperties(), "OnNavigatedTo", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\MainWindowViewModel.cs", 40);
			this.scratchManager.InitializeScratchSpace();
			base.Navigation.Navigate("Start", args);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public async Task<bool> CheckIfCanExit()
		{
			LoggerExtensionMethods.LogDiagnostic<MainWindowViewModel>(this, "Check if can exit", null, "CheckIfCanExit", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\MainWindowViewModel.cs", 47);
			return !(await this.WindowOperationsViewModel.CloseProject());
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007C1E File Offset: 0x00005E1E
		public override void OnNavigatedFrom()
		{
			this.scratchManager.TeardownScratchSpace();
			base.OnNavigatedFrom();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007C34 File Offset: 0x00005E34
		private Dictionary<string, object> GetStartupProperties()
		{
			return new Dictionary<string, object>
			{
				{
					"OS Version",
					Environment.OSVersion.VersionString
				},
				{
					".NET Version",
					Environment.Version.ToString()
				},
				{
					"App Version",
					Assembly.GetExecutingAssembly().GetName().Version.ToString()
				},
				{
					"Git Describe",
					Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product
				},
				{
					"First Run",
					this.firstRun.IsFirstRun.ToString()
				},
				{
					"Activated",
					this.activationManager.GetActivationStatus()
				},
				{
					"Product Key",
					this.activationManager.GetProductKey()
				}
			};
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CheckIfCanExit>d__0 : IAsyncStateMachine
		{
			// Token: 0x0400015F RID: 351
			public int <>1__state;

			// Token: 0x04000160 RID: 352
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000161 RID: 353
			public MainWindowViewModel <>4__this;

			// Token: 0x04000162 RID: 354
			private TaskAwaiter<bool> <>u__$awaiter1;

			// Token: 0x04000163 RID: 355
			private object <>t__stack;

			// Token: 0x06000281 RID: 641 RVA: 0x00007AE0 File Offset: 0x00005CE0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				bool result2;
				try
				{
					int num = num2;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						LoggerExtensionMethods.LogDiagnostic<MainWindowViewModel>(this, "Check if can exit", null, "CheckIfCanExit", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\MainWindowViewModel.cs", 47);
						taskAwaiter = this.WindowOperationsViewModel.CloseProject().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, MainWindowViewModel.<CheckIfCanExit>d__0>(ref taskAwaiter, ref this);
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
					int result = taskAwaiter.GetResult() ? 1 : 0;
					taskAwaiter = default(TaskAwaiter<bool>);
					result2 = (result == 0);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00007BC8 File Offset: 0x00005DC8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
