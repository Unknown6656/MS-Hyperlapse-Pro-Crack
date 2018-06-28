using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000022 RID: 34
	public class SourceFileViewModel
	{
		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private string <FileName>k__BackingField;

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private IImageHandle <Thumbnail>k__BackingField;

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007DDE File Offset: 0x00005FDE
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00007DE6 File Offset: 0x00005FE6
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FileName>k__BackingField = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007DEF File Offset: 0x00005FEF
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007DF7 File Offset: 0x00005FF7
		public IImageHandle Thumbnail
		{
			[CompilerGenerated]
			get
			{
				return this.<Thumbnail>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Thumbnail>k__BackingField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007E00 File Offset: 0x00006000
		public string Name
		{
			get
			{
				return Path.GetFileNameWithoutExtension(this.FileName);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007E0D File Offset: 0x0000600D
		public SourceFileViewModel(string name)
		{
			this.FileName = name;
		}
	}
}
