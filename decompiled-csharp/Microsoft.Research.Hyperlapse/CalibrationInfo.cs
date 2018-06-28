using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000033 RID: 51
	public class CalibrationInfo
	{
		// Token: 0x040000E9 RID: 233
		private Calibration calibration;

		// Token: 0x040000EA RID: 234
		[CompilerGenerated]
		private string <VideoMode>k__BackingField;

		// Token: 0x040000EB RID: 235
		[CompilerGenerated]
		private bool <WasAutoSelected>k__BackingField;

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000ACD2 File Offset: 0x00008ED2
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000ACDA File Offset: 0x00008EDA
		public Calibration Calibration
		{
			get
			{
				return this.calibration;
			}
			set
			{
				this.calibration = value;
				this.VideoMode = this.calibration.VideoModes.First<string>();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000AD01 File Offset: 0x00008F01
		public string VideoMode
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoMode>k__BackingField = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000AD0A File Offset: 0x00008F0A
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000AD12 File Offset: 0x00008F12
		public bool WasAutoSelected
		{
			[CompilerGenerated]
			get
			{
				return this.<WasAutoSelected>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WasAutoSelected>k__BackingField = value;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AD1B File Offset: 0x00008F1B
		public CalibrationInfo()
		{
		}
	}
}
