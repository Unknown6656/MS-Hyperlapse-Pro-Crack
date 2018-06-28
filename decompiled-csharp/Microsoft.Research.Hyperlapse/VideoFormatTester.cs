using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000011 RID: 17
	public class VideoFormatTester
	{
		// Token: 0x0400004B RID: 75
		private VideoBitrateEstimator videoBitrateEstimator;

		// Token: 0x0400004C RID: 76
		private Dictionary<Tuple<int, int, double, Rational, Rational, bool>, List<Size>> sizesCache = new Dictionary<Tuple<int, int, double, Rational, Rational, bool>, List<Size>>();

		// Token: 0x0400004D RID: 77
		private Dictionary<Tuple<int, int, double, Rational, int, int, bool>, List<Rational>> frameRatesCache = new Dictionary<Tuple<int, int, double, Rational, int, int, bool>, List<Rational>>();

		// Token: 0x0400004E RID: 78
		private AccelerationOptions accelerationOptions;

		// Token: 0x0400004F RID: 79
		private IVideoTestWriter videoTestWriter;

		// Token: 0x060000CD RID: 205 RVA: 0x000059D0 File Offset: 0x00003BD0
		public VideoFormatTester(VideoBitrateEstimator videoBitrateEstimator, AccelerationOptions accelerationOptions, IVideoTestWriter videoTestWriter)
		{
			if (videoBitrateEstimator == null)
			{
				throw new ArgumentNullException("videoBitrateEstimator");
			}
			this.videoBitrateEstimator = videoBitrateEstimator;
			if (accelerationOptions == null)
			{
				throw new ArgumentNullException("accelerationOptions");
			}
			this.accelerationOptions = accelerationOptions;
			if (videoTestWriter == null)
			{
				throw new ArgumentNullException("videoTestWriter");
			}
			this.videoTestWriter = videoTestWriter;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005A38 File Offset: 0x00003C38
		public bool ConformsToH264Level5_1(int width, int height, Rational framesPerSecond)
		{
			int num = (width + 15) / 16 * ((height + 15) / 16);
			return (double)num * framesPerSecond.AsDouble() <= 983040.0;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005AAC File Offset: 0x00003CAC
		public List<Size> GetAvailableOutputSizes(string workingDirectory, int originalWidth, int originalHeight, double originalBitsPerSecond, Rational originalFramesPerSecond, Rational desiredFramesPerSecond)
		{
			Tuple<int, int, double, Rational, Rational, bool> key = Tuple.Create<int, int, double, Rational, Rational, bool>(originalWidth, originalHeight, originalBitsPerSecond, originalFramesPerSecond, desiredFramesPerSecond, this.accelerationOptions.UseHardwareVideoEncoder);
			if (this.sizesCache.ContainsKey(key))
			{
				return this.sizesCache[key];
			}
			bool flag = originalWidth < originalHeight;
			int num = flag ? originalHeight : originalWidth;
			int num2 = flag ? originalWidth : originalHeight;
			bool flag2 = (double)num >= 1.5 * (double)num2;
			int num3 = flag2 ? 16 : 4;
			int num4 = flag2 ? 9 : 3;
			int[,] array = new int[,]
			{
				{
					1800,
					1440,
					1440
				},
				{
					1100,
					1080,
					1080
				},
				{
					900,
					720,
					720
				},
				{
					600,
					480,
					540
				},
				{
					400,
					360,
					360
				}
			};
			int length = array.GetLength(0);
			int num5 = flag2 ? 2 : 1;
			int num6 = num + num % 2;
			int num7 = num2 + num2 % 2;
			int num8 = length;
			for (int i = 0; i < length; i++)
			{
				if (num7 >= array[i, 0])
				{
					if (this.ConformsToH264Level5_1(num6, num7, desiredFramesPerSecond))
					{
						double num9 = this.videoBitrateEstimator.EstimateBitsPerSecond(originalBitsPerSecond, (double)originalWidth, (double)originalHeight, (double)num6, (double)num7, originalFramesPerSecond, desiredFramesPerSecond);
						int bitRate = (int)Math.Ceiling(num9 / 1000000.0);
						if (this.videoTestWriter.TestWrite(workingDirectory, num6, num7, desiredFramesPerSecond, bitRate, this.accelerationOptions.UseHardwareVideoEncoder))
						{
							num8 = i;
							break;
						}
					}
					num7 = array[i, num5];
					num6 = num7 * num3 / num4;
				}
			}
			int num10 = (num8 < length) ? array[num8, num5] : 0;
			int num11 = num10 * num3 / num4;
			int num12 = (num8 + 1 < length) ? array[num8 + 1, num5] : 0;
			int num13 = num12 * num3 / num4;
			Size item = new Size((double)(flag ? num7 : num6), (double)(flag ? num6 : num7));
			Size item2 = new Size((double)(flag ? num10 : num11), (double)(flag ? num11 : num10));
			Size item3 = new Size((double)(flag ? num12 : num13), (double)(flag ? num13 : num12));
			List<Size> list = (num12 > 0) ? new List<Size>
			{
				item,
				item2,
				item3
			} : ((num10 > 0) ? new List<Size>
			{
				item,
				item2
			} : new List<Size>
			{
				item
			});
			this.sizesCache[key] = list;
			return list;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005D10 File Offset: 0x00003F10
		public List<Rational> GetAvailabledOutputFrameRates(string workingDirectory, int originalWidth, int originalHeight, double originalBitsPerSecond, Rational originalFramesPerSecond, int desiredWidth, int desiredHeight)
		{
			Tuple<int, int, double, Rational, int, int, bool> key = Tuple.Create<int, int, double, Rational, int, int, bool>(originalWidth, originalHeight, originalBitsPerSecond, originalFramesPerSecond, desiredWidth, desiredHeight, this.accelerationOptions.UseHardwareVideoEncoder);
			if (this.frameRatesCache.ContainsKey(key))
			{
				return this.frameRatesCache[key];
			}
			Rational[] array = new SortedSet<Rational>(new FrameRateComparer())
			{
				originalFramesPerSecond,
				new Rational(60000, 1001),
				new Rational(50, 1),
				new Rational(30000, 1001),
				new Rational(25, 1),
				new Rational(24000, 1001)
			}.ToArray<Rational>();
			int length = array.GetLength(0);
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				if (this.ConformsToH264Level5_1(desiredWidth, desiredHeight, array[i]))
				{
					double num2 = this.videoBitrateEstimator.EstimateBitsPerSecond(originalBitsPerSecond, (double)originalWidth, (double)originalHeight, (double)desiredWidth, (double)desiredHeight, originalFramesPerSecond, array[i]);
					int bitRate = (int)Math.Ceiling(num2 / 1000000.0);
					if (this.videoTestWriter.TestWrite(workingDirectory, desiredWidth, desiredHeight, array[i], bitRate, this.accelerationOptions.UseHardwareVideoEncoder))
					{
						num = i;
						break;
					}
				}
			}
			List<Rational> list = (num == -1) ? new List<Rational>
			{
				array[4]
			} : array.Skip(num).ToList<Rational>();
			this.frameRatesCache[key] = list;
			return list;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005E8E File Offset: 0x0000408E
		public Size GetDefaultOutputSize(List<Size> outputSizes, int inputWidth, int inputHeight)
		{
			if (outputSizes.Count > 1 && outputSizes[0].Width == (double)inputWidth && outputSizes[0].Height == (double)inputHeight)
			{
				return outputSizes[1];
			}
			return outputSizes[0];
		}
	}
}
