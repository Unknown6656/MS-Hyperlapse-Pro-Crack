using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000026 RID: 38
	public class AboutViewModel : ViewModel
	{
		// Token: 0x040000C2 RID: 194
		private ActivationManager activationManager;

		// Token: 0x040000C3 RID: 195
		private ProductInfo productInfo;

		// Token: 0x040000C4 RID: 196
		[CompilerGenerated]
		private Command<string> <GoToPageCommand>k__BackingField;

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000085D0 File Offset: 0x000067D0
		public string VersionString
		{
			get
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string version = ((AssemblyFileVersionAttribute)executingAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute)).FirstOrDefault<Attribute>()).Version;
				string product = ((AssemblyProductAttribute)executingAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute)).FirstOrDefault<Attribute>()).Product;
				return string.Format("Build number: {0}\nGit Describe: {1}", version, product);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008630 File Offset: 0x00006830
		public string ActivationStatusString
		{
			get
			{
				string arg = "Not Activated";
				if (this.activationManager.GetActivationStatus())
				{
					arg = this.activationManager.GetProductId();
				}
				return string.Format("Product ID: {0}", arg);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008667 File Offset: 0x00006867
		public string ApplicationName
		{
			get
			{
				return this.productInfo.ApplicationName;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008674 File Offset: 0x00006874
		public string Title
		{
			get
			{
				return string.Format("About {0}", this.ApplicationName);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008686 File Offset: 0x00006886
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000868E File Offset: 0x0000688E
		public Command<string> GoToPageCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<GoToPageCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GoToPageCommand>k__BackingField = value;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000086A0 File Offset: 0x000068A0
		public AboutViewModel(INavigation navigation, IUserInterface userInterface, ActivationManager activationManager, ProductInfo productInfo) : base(navigation, userInterface)
		{
			if (activationManager == null)
			{
				throw new ArgumentNullException("activationManager");
			}
			this.activationManager = activationManager;
			if (productInfo == null)
			{
				throw new ArgumentNullException("productInfo");
			}
			this.productInfo = productInfo;
			this.GoToPageCommand = new Command<string>(delegate(string s)
			{
				this.GoToPage(s);
			});
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008700 File Offset: 0x00006900
		private void GoToPage(string s)
		{
			LoggerExtensionMethods.LogDiagnostic<AboutViewModel>(this, "Link Clicked", new Dictionary<string, object>
			{
				{
					"Link",
					s
				}
			}, "GoToPage", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\AboutViewModel.cs", 82);
			try
			{
				Process.Start(s);
			}
			catch (Exception value)
			{
				LoggerExtensionMethods.LogError<AboutViewModel>(this, "Process start failed", new Dictionary<string, object>
				{
					{
						"Exception",
						value
					}
				}, "GoToPage", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\AboutViewModel.cs", 90);
				base.UserInterface.ShowMessage("Error", "We're sorry, but we couldn't open the link");
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008697 File Offset: 0x00006897
		[CompilerGenerated]
		private void <.ctor>b__0(string s)
		{
			this.GoToPage(s);
		}
	}
}
