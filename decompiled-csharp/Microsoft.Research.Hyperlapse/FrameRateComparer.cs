using System;
using System.Collections.Generic;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000010 RID: 16
	internal class FrameRateComparer : IComparer<Rational>
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00005990 File Offset: 0x00003B90
		public int Compare(Rational x, Rational y)
		{
			return y.AsDouble().ToString("0.###").CompareTo(x.AsDouble().ToString("0.###"));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000059C8 File Offset: 0x00003BC8
		public FrameRateComparer()
		{
		}
	}
}
