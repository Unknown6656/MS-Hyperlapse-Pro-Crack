using System;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000002 RID: 2
	public class RecentProjectViewModel : Notifier
	{
		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		private string <CreatedDate>k__BackingField;

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		private IImageHandle <Thumbnail>k__BackingField;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public string CreatedDate
		{
			[CompilerGenerated]
			get
			{
				return this.<CreatedDate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CreatedDate>k__BackingField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
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

		// Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		public RecentProjectViewModel(string name, DateTime dateTime)
		{
			this.Name = name;
			this.CreatedDate = dateTime.ToString();
		}
	}
}
