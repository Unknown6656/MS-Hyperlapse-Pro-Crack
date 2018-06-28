using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000E RID: 14
	public class CalibrationMatcher
	{
		// Token: 0x0400003D RID: 61
		private CalibrationProvider calibrationProvider;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		private static Func<Calibration, bool> CS$<>9__CachedAnonymousMethodDelegate6;

		// Token: 0x060000B0 RID: 176 RVA: 0x00004B16 File Offset: 0x00002D16
		public CalibrationMatcher(CalibrationProvider calibrationProvider)
		{
			if (calibrationProvider == null)
			{
				throw new ArgumentNullException("calibrationProvider");
			}
			this.calibrationProvider = calibrationProvider;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004B70 File Offset: 0x00002D70
		public CalibrationInfo FindCalibrationInfoForVideo(VideoInfo videoInfo)
		{
			List<Calibration> calibrations = this.calibrationProvider.GetCalibrations();
			if (videoInfo.CameraModel != 0)
			{
				Calibration calibration = calibrations.SingleOrDefault((Calibration cf) => cf.ID == videoInfo.CameraModel);
				if (calibration != null)
				{
					string text = calibration.VideoModes.FirstOrDefault((string mode) => mode == videoInfo.VideoMode);
					if (!string.IsNullOrWhiteSpace(text))
					{
						return new CalibrationInfo
						{
							Calibration = calibration,
							VideoMode = text,
							WasAutoSelected = true
						};
					}
				}
			}
			Calibration calibration2 = calibrations.First((Calibration c) => c is UnknownCalibration);
			return new CalibrationInfo
			{
				Calibration = calibration2,
				VideoMode = "N/A",
				WasAutoSelected = false
			};
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004B63 File Offset: 0x00002D63
		[CompilerGenerated]
		private static bool <FindCalibrationInfoForVideo>b__3(Calibration c)
		{
			return c is UnknownCalibration;
		}

		// Token: 0x02000040 RID: 64
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7
		{
			// Token: 0x0400013E RID: 318
			public VideoInfo videoInfo;

			// Token: 0x06000276 RID: 630 RVA: 0x00004B33 File Offset: 0x00002D33
			public <>c__DisplayClass7()
			{
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00004B3B File Offset: 0x00002D3B
			public bool <FindCalibrationInfoForVideo>b__1(Calibration cf)
			{
				return cf.ID == this.videoInfo.CameraModel;
			}

			// Token: 0x06000278 RID: 632 RVA: 0x00004B50 File Offset: 0x00002D50
			public bool <FindCalibrationInfoForVideo>b__2(string mode)
			{
				return mode == this.videoInfo.VideoMode;
			}
		}
	}
}
