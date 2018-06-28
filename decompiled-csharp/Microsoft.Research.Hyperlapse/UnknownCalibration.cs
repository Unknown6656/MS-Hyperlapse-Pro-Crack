using System;
using System.Collections.Generic;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000027 RID: 39
	public class UnknownCalibration : Calibration
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00008794 File Offset: 0x00006994
		public UnknownCalibration() : base(-1, "Unknown Camera", new List<string>
		{
			"N/A"
		}, "unknown", string.Empty)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000087C9 File Offset: 0x000069C9
		public override string ExtractToFolder(string folder)
		{
			return string.Empty;
		}
	}
}
