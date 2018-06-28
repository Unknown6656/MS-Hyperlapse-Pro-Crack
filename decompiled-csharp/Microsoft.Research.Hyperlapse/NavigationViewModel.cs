using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001F RID: 31
	public class NavigationViewModel
	{
		// Token: 0x040000AD RID: 173
		private static readonly List<string> pages = new List<string>
		{
			"Import",
			"Settings",
			"Process",
			"Finish"
		};

		// Token: 0x040000AE RID: 174
		private string pageName;

		// Token: 0x040000AF RID: 175
		private INavigation platformNavigation;

		// Token: 0x040000B0 RID: 176
		[CompilerGenerated]
		private Command<string> <NavigateCommand>k__BackingField;

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000796C File Offset: 0x00005B6C
		public IEnumerable<string> NavigationItems
		{
			get
			{
				return NavigationViewModel.pages;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007974 File Offset: 0x00005B74
		public NavigationViewModel(INavigation platformNavigation, string pageName)
		{
			if (platformNavigation == null)
			{
				throw new ArgumentNullException("platformNavigation");
			}
			this.platformNavigation = platformNavigation;
			if (string.IsNullOrWhiteSpace(pageName))
			{
				throw new ArgumentNullException("pageName");
			}
			this.pageName = pageName;
			this.NavigateCommand = new Command<string>(new Action<string>(this.Navigate), new Func<string, bool>(this.CanNavigate));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000079D9 File Offset: 0x00005BD9
		private void Navigate(string page)
		{
			this.platformNavigation.Navigate(page, new object[0]);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000079ED File Offset: 0x00005BED
		private bool CanNavigate(string arg)
		{
			return false;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000079F0 File Offset: 0x00005BF0
		public Command GoBackCommand
		{
			get
			{
				return this.platformNavigation.GoBackCommand;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000079FD File Offset: 0x00005BFD
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007A05 File Offset: 0x00005C05
		public Command<string> NavigateCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<NavigateCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NavigateCommand>k__BackingField = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007A0E File Offset: 0x00005C0E
		public int PageIndex
		{
			get
			{
				return NavigationViewModel.pages.IndexOf(this.pageName) + 1;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007A24 File Offset: 0x00005C24
		// Note: this type is marked as 'beforefieldinit'.
		static NavigationViewModel()
		{
		}
	}
}
