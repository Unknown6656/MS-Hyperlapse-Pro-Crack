using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200002E RID: 46
	public class ProductInfo
	{
		// Token: 0x040000DF RID: 223
		private readonly string baseAppName = "Hyperlapse Pro";

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private ProductInfo.PlatformEnum <Platform>k__BackingField;

		// Token: 0x040000E1 RID: 225
		[CompilerGenerated]
		private ProductInfo.EditionEnum <Edition>k__BackingField;

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000AB5C File Offset: 0x00008D5C
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000AB64 File Offset: 0x00008D64
		public ProductInfo.PlatformEnum Platform
		{
			[CompilerGenerated]
			get
			{
				return this.<Platform>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Platform>k__BackingField = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000AB6D File Offset: 0x00008D6D
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000AB75 File Offset: 0x00008D75
		public ProductInfo.EditionEnum Edition
		{
			[CompilerGenerated]
			get
			{
				return this.<Edition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Edition>k__BackingField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000AB7E File Offset: 0x00008D7E
		public string ApplicationShortName
		{
			get
			{
				if (this.Edition == ProductInfo.EditionEnum.Pro)
				{
					return this.baseAppName;
				}
				return string.Format("{0} ({1} Edition)", this.baseAppName, this.Edition);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000ABAA File Offset: 0x00008DAA
		public string ApplicationName
		{
			get
			{
				return string.Format("Microsoft {0}", this.ApplicationShortName);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public string ActivationRegistrationFolder
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", this.ApplicationShortName);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		public string ActivationRegistrationFile
		{
			get
			{
				return Path.Combine(this.ActivationRegistrationFolder, "Registration.dat");
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public string ActivationConfigFile
		{
			get
			{
				string path = (this.Platform == ProductInfo.PlatformEnum.Windows) ? "Assets" : "../Resources";
				return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), path, "MicrosoftHyperlapsePKConfig_Signed.xmls");
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000AC24 File Offset: 0x00008E24
		public string BuyOnlineLink
		{
			get
			{
				if (this.Edition == ProductInfo.EditionEnum.Pro)
				{
					if (this.Platform == ProductInfo.PlatformEnum.Windows)
					{
						return "http://go.microsoft.com/fwlink/?LinkID=625071";
					}
					if (this.Platform == ProductInfo.PlatformEnum.Mac)
					{
						return "http://go.microsoft.com/fwlink/?LinkId=698735";
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AC50 File Offset: 0x00008E50
		public ProductInfo()
		{
			this.Platform = ((Environment.OSVersion.Platform == PlatformID.Win32NT) ? ProductInfo.PlatformEnum.Windows : ProductInfo.PlatformEnum.Mac);
			this.Edition = ProductInfo.EditionEnum.Pro;
		}

		// Token: 0x0200002F RID: 47
		public enum PlatformEnum
		{
			// Token: 0x040000E3 RID: 227
			Windows,
			// Token: 0x040000E4 RID: 228
			Mac
		}

		// Token: 0x02000030 RID: 48
		public enum EditionEnum
		{
			// Token: 0x040000E6 RID: 230
			Pro,
			// Token: 0x040000E7 RID: 231
			YI
		}
	}
}
