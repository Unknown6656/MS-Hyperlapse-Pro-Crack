using System;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200002B RID: 43
	public interface IVideoTestWriter
	{
		// Token: 0x06000217 RID: 535
		bool TestWrite(string tempOutputDirectory, int width, int height, Rational frameRate, int bitRate, bool useHardwareMFTs);
	}
}
