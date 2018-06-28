using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000005 RID: 5
	public class StartPageViewModel : ViewModel
	{
		// Token: 0x04000016 RID: 22
		private UpdateChecker updateChecker;

		// Token: 0x04000017 RID: 23
		private bool hasNavigated;

		// Token: 0x04000018 RID: 24
		private IAdvertisingModel advertisingModel;

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		private WindowOperationsViewModel <WindowOperationsVM>k__BackingField;

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private Command<AdvertisementViewModel> <AdClickedCommand>k__BackingField;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private Command <CheckForFileArgumentCommand>k__BackingField;

		// Token: 0x0400001C RID: 28
		[CompilerGenerated]
		private AsyncCommand <CheckForUpdateCommand>k__BackingField;

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002567 File Offset: 0x00000767
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000256F File Offset: 0x0000076F
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002578 File Offset: 0x00000778
		private IHyperlapseUserInterface UserInterface
		{
			get
			{
				return (IHyperlapseUserInterface)base.UserInterface;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002585 File Offset: 0x00000785
		public IAdvertisingModel Advertising
		{
			get
			{
				return this.advertisingModel;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000258D File Offset: 0x0000078D
		public StartPageViewModel(INavigation navigation, IUserInterface userInterface, WindowOperationsViewModel windowOperationsVM, UpdateChecker updateChecker, IAdvertisingModel advertisingModel) : base(navigation, userInterface)
		{
			if (updateChecker == null)
			{
				throw new ArgumentNullException("updateChecker");
			}
			this.updateChecker = updateChecker;
			if (advertisingModel == null)
			{
				throw new ArgumentNullException("advertisingModel");
			}
			this.advertisingModel = advertisingModel;
			this.WindowOperationsVM = windowOperationsVM;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000025D4 File Offset: 0x000007D4
		public Command<AdvertisementViewModel> AdClickedCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<AdClickedCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AdClickedCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025DD File Offset: 0x000007DD
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000025E5 File Offset: 0x000007E5
		public Command CheckForFileArgumentCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckForFileArgumentCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CheckForFileArgumentCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000025EE File Offset: 0x000007EE
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025F6 File Offset: 0x000007F6
		public AsyncCommand CheckForUpdateCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckForUpdateCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CheckForUpdateCommand>k__BackingField = value;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000273C File Offset: 0x0000093C
		public override void OnNavigatedTo(object[] args)
		{
			this.hasNavigated = false;
			this.AdClickedCommand = new Command<AdvertisementViewModel>(delegate(AdvertisementViewModel v)
			{
				this.AdClicked(v);
			});
			this.CheckForFileArgumentCommand = new Command(delegate()
			{
				this.HandleLaunchArgs(args);
			});
			this.CheckForUpdateCommand = new AsyncCommand(async delegate()
			{
				await this.CheckForUpdate(true);
			});
			this.CheckForUpdate(false);
			this.advertisingModel.DownloadLatestAds();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027C0 File Offset: 0x000009C0
		private void AdClicked(AdvertisementViewModel v)
		{
			if (!this.advertisingModel.AdClicked(v))
			{
				this.UserInterface.ShowMessage("Link Open Failed", "Sorry, but we couldn't launch your browser");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027F3 File Offset: 0x000009F3
		private void HandleLaunchArgs(object[] args)
		{
			if (args != null && args.Length > 0 && args[0] is string && args[0].ToString().Length > 0)
			{
				this.hasNavigated = true;
				this.WindowOperationsVM.HandleFileCommand.Execute(args[0]);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A4C File Offset: 0x00000C4C
		private async Task CheckForUpdate(bool overrideDisabledCheck = false)
		{
			string msiLink = await this.updateChecker.CheckForUpgradeAsync(overrideDisabledCheck);
			if (!string.IsNullOrWhiteSpace(msiLink) && !this.hasNavigated)
			{
				bool confirm = await this.UserInterface.ShowConfirmMessage("Update Available", "An newer version of Hyperlapse Pro is available\nWould you like to exit and install it now?");
				if (confirm)
				{
					try
					{
						Process.Start(msiLink);
						this.UserInterface.CloseApplication();
					}
					catch (Exception value)
					{
						LoggerExtensionMethods.LogWarning<StartPageViewModel>(this, "Couldn't run update MSI", new Dictionary<string, object>
						{
							{
								"Exception",
								value
							}
						}, "CheckForUpdate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\StartPageViewModel.cs", 121);
					}
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002607 File Offset: 0x00000807
		[CompilerGenerated]
		private void <OnNavigatedTo>b__0(AdvertisementViewModel v)
		{
			this.AdClicked(v);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026F4 File Offset: 0x000008F4
		[CompilerGenerated]
		private async Task <OnNavigatedTo>b__2()
		{
			await this.CheckForUpdate(true);
		}

		// Token: 0x02000035 RID: 53
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3
		{
			// Token: 0x040000F1 RID: 241
			public StartPageViewModel <>4__this;

			// Token: 0x040000F2 RID: 242
			public object[] args;

			// Token: 0x06000260 RID: 608 RVA: 0x000025FF File Offset: 0x000007FF
			public <>c__DisplayClass3()
			{
			}

			// Token: 0x06000261 RID: 609 RVA: 0x00002610 File Offset: 0x00000810
			public void <OnNavigatedTo>b__1()
			{
				this.<>4__this.HandleLaunchArgs(this.args);
			}
		}

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <<OnNavigatedTo>b__2>d__5 : IAsyncStateMachine
		{
			// Token: 0x040000F3 RID: 243
			public int <>1__state;

			// Token: 0x040000F4 RID: 244
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000F5 RID: 245
			public StartPageViewModel <>4__this;

			// Token: 0x040000F6 RID: 246
			private TaskAwaiter <>u__$awaiter6;

			// Token: 0x040000F7 RID: 247
			private object <>t__stack;

			// Token: 0x06000262 RID: 610 RVA: 0x00002624 File Offset: 0x00000824
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = this.CheckForUpdate(true).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, StartPageViewModel.<<OnNavigatedTo>b__2>d__5>(ref taskAwaiter, ref this);
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

			// Token: 0x06000263 RID: 611 RVA: 0x000026E4 File Offset: 0x000008E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x02000037 RID: 55
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CheckForUpdate>d__9 : IAsyncStateMachine
		{
			// Token: 0x040000F8 RID: 248
			public int <>1__state;

			// Token: 0x040000F9 RID: 249
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000FA RID: 250
			public StartPageViewModel <>4__this;

			// Token: 0x040000FB RID: 251
			public bool overrideDisabledCheck;

			// Token: 0x040000FC RID: 252
			public string <msiLink>5__a;

			// Token: 0x040000FD RID: 253
			public bool <confirm>5__b;

			// Token: 0x040000FE RID: 254
			private TaskAwaiter<string> <>u__$awaiterc;

			// Token: 0x040000FF RID: 255
			private object <>t__stack;

			// Token: 0x04000100 RID: 256
			private TaskAwaiter<bool> <>u__$awaiterd;

			// Token: 0x06000264 RID: 612 RVA: 0x00002834 File Offset: 0x00000A34
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				try
				{
					TaskAwaiter<string> taskAwaiter;
					TaskAwaiter<bool> taskAwaiter3;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<string> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<string>);
						num = -1;
						break;
					}
					case 1:
					{
						TaskAwaiter<bool> taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter<bool>);
						num = -1;
						goto IL_128;
					}
					default:
						taskAwaiter = this.updateChecker.CheckForUpgradeAsync(overrideDisabledCheck).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							TaskAwaiter<string> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, StartPageViewModel.<CheckForUpdate>d__9>(ref taskAwaiter, ref this);
							return;
						}
						break;
					}
					string result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<string>);
					string text = result;
					msiLink = text;
					if (string.IsNullOrWhiteSpace(msiLink) || this.hasNavigated)
					{
						goto IL_199;
					}
					taskAwaiter3 = this.UserInterface.ShowConfirmMessage("Update Available", "An newer version of Hyperlapse Pro is available\nWould you like to exit and install it now?").GetAwaiter();
					if (!taskAwaiter3.IsCompleted)
					{
						num = 1;
						TaskAwaiter<bool> taskAwaiter4 = taskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, StartPageViewModel.<CheckForUpdate>d__9>(ref taskAwaiter3, ref this);
						return;
					}
					IL_128:
					bool result2 = taskAwaiter3.GetResult();
					taskAwaiter3 = default(TaskAwaiter<bool>);
					bool flag = result2;
					confirm = flag;
					if (confirm)
					{
						try
						{
							Process.Start(msiLink);
							this.UserInterface.CloseApplication();
						}
						catch (Exception value)
						{
							LoggerExtensionMethods.LogWarning<StartPageViewModel>(this, "Couldn't run update MSI", new Dictionary<string, object>
							{
								{
									"Exception",
									value
								}
							}, "CheckForUpdate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\StartPageViewModel.cs", 121);
						}
					}
					IL_199:;
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

			// Token: 0x06000265 RID: 613 RVA: 0x00002A3C File Offset: 0x00000C3C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
