using System;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000029 RID: 41
	public class VideoBitrateEstimator
	{
		// Token: 0x040000CE RID: 206
		private static double kbps = 1000.0;

		// Token: 0x040000CF RID: 207
		private static double Mbps = 1000000.0;

		// Token: 0x040000D0 RID: 208
		private double[,] h264LevelLimits;

		// Token: 0x060001E8 RID: 488 RVA: 0x000091BC File Offset: 0x000073BC
		private double ComputeMaxBitsPerSecond(double width, double height, Rational framesPerSecond)
		{
			int num = (int)Math.Ceiling(width / 16.0);
			int num2 = (int)Math.Ceiling(height / 16.0);
			int num3 = num * num2;
			double num4 = (double)num3 * framesPerSecond.AsDouble();
			double result = 40.0 * VideoBitrateEstimator.Mbps;
			int num5 = 3;
			for (int i = 0; i < this.h264LevelLimits.Length; i++)
			{
				if (num4 <= this.h264LevelLimits[i, 0] && (double)num3 <= this.h264LevelLimits[i, 1])
				{
					result = this.h264LevelLimits[i, num5];
					break;
				}
			}
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009264 File Offset: 0x00007464
		public double EstimateBitsPerSecond(double inputBitsPerSecond, double inputWidth, double inputHeight, double outputWidth, double outputHeight, Rational inputFramesPerSecond, Rational outputFramesPerSecond)
		{
			double num = 2.5;
			double val = num * inputBitsPerSecond * outputWidth * outputHeight / (inputWidth * inputHeight);
			double val2 = this.ComputeMaxBitsPerSecond(outputWidth, outputHeight, outputFramesPerSecond);
			return Math.Min(val, val2);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009530 File Offset: 0x00007730
		public VideoBitrateEstimator()
		{
			double[,] array = new double[,]
			{
				{
					1485.0,
					99.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					3000.0,
					396.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					6000.0,
					396.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					11880.0,
					396.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					19800.0,
					792.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					20250.0,
					1620.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					40500.0,
					1620.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					108000.0,
					3600.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					216000.0,
					5120.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					245760.0,
					8192.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					522240.0,
					8704.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					589824.0,
					22080.0,
					0.0,
					0.0,
					0.0,
					0.0
				},
				{
					983040.0,
					36864.0,
					0.0,
					0.0,
					0.0,
					0.0
				}
			};
			array[0, 2] = 128.0 * VideoBitrateEstimator.kbps;
			array[0, 3] = 160.0 * VideoBitrateEstimator.kbps;
			array[0, 4] = 384.0 * VideoBitrateEstimator.kbps;
			array[0, 5] = 512.0 * VideoBitrateEstimator.kbps;
			array[1, 2] = 192.0 * VideoBitrateEstimator.kbps;
			array[1, 3] = 240.0 * VideoBitrateEstimator.kbps;
			array[1, 4] = 576.0 * VideoBitrateEstimator.kbps;
			array[1, 5] = 768.0 * VideoBitrateEstimator.kbps;
			array[2, 2] = 384.0 * VideoBitrateEstimator.kbps;
			array[2, 3] = 480.0 * VideoBitrateEstimator.kbps;
			array[2, 4] = 1152.0 * VideoBitrateEstimator.kbps;
			array[2, 5] = 1536.0 * VideoBitrateEstimator.kbps;
			array[3, 2] = 2.0 * VideoBitrateEstimator.Mbps;
			array[3, 3] = 2.5 * VideoBitrateEstimator.Mbps;
			array[3, 4] = 6.0 * VideoBitrateEstimator.Mbps;
			array[3, 5] = 8.0 * VideoBitrateEstimator.Mbps;
			array[4, 2] = 4.0 * VideoBitrateEstimator.Mbps;
			array[4, 3] = 5.0 * VideoBitrateEstimator.Mbps;
			array[4, 4] = 12.0 * VideoBitrateEstimator.Mbps;
			array[4, 5] = 16.0 * VideoBitrateEstimator.Mbps;
			array[5, 2] = 4.0 * VideoBitrateEstimator.Mbps;
			array[5, 3] = 5.0 * VideoBitrateEstimator.Mbps;
			array[5, 4] = 12.0 * VideoBitrateEstimator.Mbps;
			array[5, 5] = 16.0 * VideoBitrateEstimator.Mbps;
			array[6, 2] = 10.0 * VideoBitrateEstimator.Mbps;
			array[6, 3] = 12.5 * VideoBitrateEstimator.Mbps;
			array[6, 4] = 30.0 * VideoBitrateEstimator.Mbps;
			array[6, 5] = 40.0 * VideoBitrateEstimator.Mbps;
			array[7, 2] = 14.0 * VideoBitrateEstimator.Mbps;
			array[7, 3] = 17.5 * VideoBitrateEstimator.Mbps;
			array[7, 4] = 42.0 * VideoBitrateEstimator.Mbps;
			array[7, 5] = 56.0 * VideoBitrateEstimator.Mbps;
			array[8, 2] = 20.0 * VideoBitrateEstimator.Mbps;
			array[8, 3] = 25.0 * VideoBitrateEstimator.Mbps;
			array[8, 4] = 60.0 * VideoBitrateEstimator.Mbps;
			array[8, 5] = 80.0 * VideoBitrateEstimator.Mbps;
			array[9, 2] = 50.0 * VideoBitrateEstimator.Mbps;
			array[9, 3] = 50.0 * VideoBitrateEstimator.Mbps;
			array[9, 4] = 150.0 * VideoBitrateEstimator.Mbps;
			array[9, 5] = 200.0 * VideoBitrateEstimator.Mbps;
			array[10, 2] = 50.0 * VideoBitrateEstimator.Mbps;
			array[10, 3] = 50.0 * VideoBitrateEstimator.Mbps;
			array[10, 4] = 150.0 * VideoBitrateEstimator.Mbps;
			array[10, 5] = 200.0 * VideoBitrateEstimator.Mbps;
			array[11, 2] = 135.0 * VideoBitrateEstimator.Mbps;
			array[11, 3] = 168.75 * VideoBitrateEstimator.Mbps;
			array[11, 4] = 405.0 * VideoBitrateEstimator.Mbps;
			array[11, 5] = 540.0 * VideoBitrateEstimator.Mbps;
			array[12, 2] = 240.0 * VideoBitrateEstimator.Mbps;
			array[12, 3] = 300.0 * VideoBitrateEstimator.Mbps;
			array[12, 4] = 720.0 * VideoBitrateEstimator.Mbps;
			array[12, 5] = 960.0 * VideoBitrateEstimator.Mbps;
			this.h264LevelLimits = array;
			base..ctor();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000929F File Offset: 0x0000749F
		// Note: this type is marked as 'beforefieldinit'.
		static VideoBitrateEstimator()
		{
		}
	}
}
