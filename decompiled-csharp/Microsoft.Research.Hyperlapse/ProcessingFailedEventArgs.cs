using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000014 RID: 20
	public class ProcessingFailedEventArgs
	{
		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private string <ErrorMessage>k__BackingField;

		// Token: 0x060000E7 RID: 231 RVA: 0x00005F7A File Offset: 0x0000417A
		public ProcessingFailedEventArgs(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005F89 File Offset: 0x00004189
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005F91 File Offset: 0x00004191
		public string ErrorMessage
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorMessage>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ErrorMessage>k__BackingField = value;
			}
		}
	}
}
