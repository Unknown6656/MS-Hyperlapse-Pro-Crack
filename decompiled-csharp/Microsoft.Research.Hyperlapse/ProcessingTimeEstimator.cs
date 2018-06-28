using System;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001E RID: 30
	public class ProcessingTimeEstimator
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00007932 File Offset: 0x00005B32
		public TimeSpan EstimateTime(TimeSpan inputLength, bool isAdvanced)
		{
			if (isAdvanced)
			{
				return TimeSpan.FromSeconds(inputLength.TotalSeconds * 4.0);
			}
			return TimeSpan.FromSeconds(inputLength.TotalSeconds / 7.0);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007964 File Offset: 0x00005B64
		public ProcessingTimeEstimator()
		{
		}
	}
}
