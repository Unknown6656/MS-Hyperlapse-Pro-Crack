using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000031 RID: 49
	public class ProcessingFinishedEventArgs
	{
		// Token: 0x040000E8 RID: 232
		[CompilerGenerated]
		private HyperlapseParameters <Parameters>k__BackingField;

		// Token: 0x06000245 RID: 581 RVA: 0x0000AC81 File Offset: 0x00008E81
		public ProcessingFinishedEventArgs(HyperlapseParameters parameters)
		{
			this.Parameters = parameters;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000AC90 File Offset: 0x00008E90
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000AC98 File Offset: 0x00008E98
		public HyperlapseParameters Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parameters>k__BackingField = value;
			}
		}
	}
}
