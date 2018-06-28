using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001D RID: 29
	public class UpgradeViewModel : ViewModel
	{
		// Token: 0x040000A5 RID: 165
		private string productKey = string.Empty;

		// Token: 0x040000A6 RID: 166
		private ActivationManager activationChecker;

		// Token: 0x040000A7 RID: 167
		private ProductInfo productInfo;

		// Token: 0x040000A8 RID: 168
		private bool didActivate;

		// Token: 0x040000A9 RID: 169
		[CompilerGenerated]
		private Command <ActivateProductCommand>k__BackingField;

		// Token: 0x040000AA RID: 170
		[CompilerGenerated]
		private Command <BuyProductCommand>k__BackingField;

		// Token: 0x040000AB RID: 171
		[CompilerGenerated]
		private Command <OpenWin7FixLinkCommand>k__BackingField;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		private int <ErrorCode>k__BackingField;

		// Token: 0x0600018A RID: 394 RVA: 0x00007688 File Offset: 0x00005888
		public UpgradeViewModel(INavigation navigation, IUserInterface userInterface, ActivationManager activationChecker, ProductInfo productInfo) : base(navigation, userInterface)
		{
			if (activationChecker == null)
			{
				throw new ArgumentNullException("activationChecker");
			}
			this.activationChecker = activationChecker;
			if (productInfo == null)
			{
				throw new ArgumentNullException("productInfo");
			}
			this.productInfo = productInfo;
			this.ActivateProductCommand = new Command(new Action(this.ActivateProduct), () => this.IsValidKey);
			this.BuyProductCommand = new Command(new Action(this.BuyProduct));
			this.OpenWin7FixLinkCommand = new Command(new Action(this.OpenWin7FixLink));
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000772C File Offset: 0x0000592C
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007734 File Offset: 0x00005934
		public Command ActivateProductCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ActivateProductCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActivateProductCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000773D File Offset: 0x0000593D
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007745 File Offset: 0x00005945
		public Command BuyProductCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<BuyProductCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BuyProductCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000774E File Offset: 0x0000594E
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007756 File Offset: 0x00005956
		public Command OpenWin7FixLinkCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<OpenWin7FixLinkCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OpenWin7FixLinkCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000775F File Offset: 0x0000595F
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007768 File Offset: 0x00005968
		public string ProductKey
		{
			get
			{
				return this.productKey;
			}
			set
			{
				this.productKey = value;
				this.NotifyPropertyChanged("ProductKey");
				base.NotifyPropertyChanged<bool>(() => this.IsValidKey);
				this.ActivateProductCommand.RaiseCanExecuteChanged();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000077CC File Offset: 0x000059CC
		public bool IsValidKey
		{
			get
			{
				return this.activationChecker.ValidateKey(this.productKey);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000077E0 File Offset: 0x000059E0
		public bool IsActivationError
		{
			get
			{
				int validationError = this.activationChecker.GetValidationError();
				if (validationError != 0)
				{
					this.ErrorCode = validationError;
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007806 File Offset: 0x00005A06
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000780E File Offset: 0x00005A0E
		public int ErrorCode
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorCode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ErrorCode>k__BackingField = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007817 File Offset: 0x00005A17
		public bool SupportsBuyOnline
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.productInfo.BuyOnlineLink);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000782C File Offset: 0x00005A2C
		public string ApplicationName
		{
			get
			{
				return this.productInfo.ApplicationName;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000783C File Offset: 0x00005A3C
		public override void OnNavigatedFrom()
		{
			base.OnNavigatedFrom();
			LoggerExtensionMethods.LogDiagnostic<UpgradeViewModel>(this, "Closed Upgrade Window", new Dictionary<string, object>
			{
				{
					"Cancelled",
					!this.didActivate
				}
			}, "OnNavigatedFrom", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\UpgradeViewModel.cs", 117);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007888 File Offset: 0x00005A88
		private void ActivateProduct()
		{
			LoggerExtensionMethods.LogCheckpoint<UpgradeViewModel>(this, "ActivateProduct", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\UpgradeViewModel.cs", 122);
			this.didActivate = this.activationChecker.Activate(this.productKey);
			if (!this.didActivate)
			{
				base.UserInterface.ShowMessage("Couldn't activate", "We're sorry but we couldn't activate your software.\nPlease visit our forums for further help");
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000078DC File Offset: 0x00005ADC
		private void BuyProduct()
		{
			LoggerExtensionMethods.LogDiagnostic<UpgradeViewModel>(this, "Clicked Buy Online Link", null, "BuyProduct", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\UpgradeViewModel.cs", 134);
			Process.Start(this.productInfo.BuyOnlineLink);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000790A File Offset: 0x00005B0A
		private void OpenWin7FixLink()
		{
			LoggerExtensionMethods.LogDiagnostic<UpgradeViewModel>(this, "Clicked open win 7 fix link", null, "OpenWin7FixLink", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\UpgradeViewModel.cs", 140);
			Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=26764");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007680 File Offset: 0x00005880
		[CompilerGenerated]
		private bool <.ctor>b__0()
		{
			return this.IsValidKey;
		}
	}
}
