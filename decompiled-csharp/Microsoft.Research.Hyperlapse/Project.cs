using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000018 RID: 24
	public class Project
	{
		// Token: 0x0400006A RID: 106
		private string outputLocation = string.Empty;

		// Token: 0x0400006B RID: 107
		private Regex outputLocationRegex = new Regex("(.*)_hyperlapse_(\\d?\\d)x_(std|adv).mp4");

		// Token: 0x0400006C RID: 108
		private EventHandler ValueChanged;

		// Token: 0x0400006D RID: 109
		private TimeSpan startTime;

		// Token: 0x0400006E RID: 110
		private VideoFormatTester videoFormatTester;

		// Token: 0x0400006F RID: 111
		private TimeSpan endTime;

		// Token: 0x04000070 RID: 112
		private bool useAdvancedSmoothing;

		// Token: 0x04000071 RID: 113
		private bool isSaved;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private VideoInfo <VideoInfo>k__BackingField;

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private TimeSpan <SelectedFrameTime>k__BackingField;

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		private bool <CreditDisabled>k__BackingField;

		// Token: 0x04000075 RID: 117
		[CompilerGenerated]
		private int <SpeedupFactor>k__BackingField;

		// Token: 0x04000076 RID: 118
		[CompilerGenerated]
		private CalibrationInfo <CalibrationInfo>k__BackingField;

		// Token: 0x04000077 RID: 119
		[CompilerGenerated]
		private Size <OutputSize>k__BackingField;

		// Token: 0x04000078 RID: 120
		[CompilerGenerated]
		private Rational <OutputFramesPerSecond>k__BackingField;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		private double <VideoRotationAmount>k__BackingField;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		private string <ProjectFile>k__BackingField;

		// Token: 0x0400007B RID: 123
		[CompilerGenerated]
		private string <WorkingDirectory>k__BackingField;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600010D RID: 269 RVA: 0x00006460 File Offset: 0x00004660
		// (remove) Token: 0x0600010E RID: 270 RVA: 0x00006498 File Offset: 0x00004698
		public event EventHandler ValueChanged
		{
			add
			{
				EventHandler eventHandler = this.ValueChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ValueChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.ValueChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ValueChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000064CD File Offset: 0x000046CD
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000064D5 File Offset: 0x000046D5
		public VideoInfo VideoInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoInfo>k__BackingField = value;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000064DE File Offset: 0x000046DE
		public Project(VideoFormatTester videoFormatTester)
		{
			if (videoFormatTester == null)
			{
				throw new ArgumentNullException("videoFormatTester");
			}
			this.videoFormatTester = videoFormatTester;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006516 File Offset: 0x00004716
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000651E File Offset: 0x0000471E
		public TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = ((value > TimeSpan.FromSeconds(0.0)) ? value : TimeSpan.FromSeconds(0.0));
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000654D File Offset: 0x0000474D
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00006558 File Offset: 0x00004758
		public TimeSpan EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = ((value < this.RoundedDuration || this.RoundedDuration.TotalSeconds == 0.0) ? value : this.RoundedDuration);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000659C File Offset: 0x0000479C
		public TimeSpan RoundedDuration
		{
			get
			{
				return TimeSpan.FromSeconds(Math.Floor(this.VideoInfo.Duration.TotalSeconds));
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000065C6 File Offset: 0x000047C6
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000065CE File Offset: 0x000047CE
		public TimeSpan SelectedFrameTime
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedFrameTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedFrameTime>k__BackingField = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000065D8 File Offset: 0x000047D8
		public TimeSpan InputLength
		{
			get
			{
				return this.EndTime.Subtract(this.StartTime);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000065FC File Offset: 0x000047FC
		public TimeSpan OutputLength
		{
			get
			{
				return this.GetOutputHyperlapseLength().Add(this.CreditLength);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006620 File Offset: 0x00004820
		public TimeSpan CreditLength
		{
			get
			{
				if (this.CreditDisabled)
				{
					return TimeSpan.Zero;
				}
				TimeSpan outputHyperlapseLength = this.GetOutputHyperlapseLength();
				double value = Math.Ceiling(outputHyperlapseLength.TotalSeconds + 1.0) - outputHyperlapseLength.TotalSeconds;
				return TimeSpan.FromSeconds(value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006667 File Offset: 0x00004867
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000666F File Offset: 0x0000486F
		public bool CreditDisabled
		{
			[CompilerGenerated]
			get
			{
				return this.<CreditDisabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreditDisabled>k__BackingField = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006678 File Offset: 0x00004878
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006680 File Offset: 0x00004880
		public int SpeedupFactor
		{
			[CompilerGenerated]
			get
			{
				return this.<SpeedupFactor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SpeedupFactor>k__BackingField = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006689 File Offset: 0x00004889
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00006691 File Offset: 0x00004891
		public CalibrationInfo CalibrationInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CalibrationInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CalibrationInfo>k__BackingField = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000669A File Offset: 0x0000489A
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000066A2 File Offset: 0x000048A2
		public Size OutputSize
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputSize>k__BackingField = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000066AB File Offset: 0x000048AB
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000066B3 File Offset: 0x000048B3
		public Rational OutputFramesPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputFramesPerSecond>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputFramesPerSecond>k__BackingField = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000066BC File Offset: 0x000048BC
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000066CE File Offset: 0x000048CE
		public bool UseAdvancedSmoothing
		{
			get
			{
				return this.UseAdvancedSmoothingSettingEnabled && this.useAdvancedSmoothing;
			}
			set
			{
				if (this.UseAdvancedSmoothingSettingEnabled)
				{
					this.useAdvancedSmoothing = value;
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000066DF File Offset: 0x000048DF
		public bool UseAdvancedSmoothingSettingEnabled
		{
			get
			{
				return !(this.CalibrationInfo.Calibration is UnknownCalibration);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000066F8 File Offset: 0x000048F8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00006756 File Offset: 0x00004956
		public string OutputFile
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.outputLocation))
				{
					return this.GetDefaultOutputLocation(string.Empty);
				}
				Match match = this.outputLocationRegex.Match(this.outputLocation);
				if (match.Success)
				{
					return this.GetDefaultOutputLocation(match.Groups[1].Value);
				}
				return this.outputLocation;
			}
			set
			{
				if (value != this.OutputFile)
				{
					this.outputLocation = value;
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000676D File Offset: 0x0000496D
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006775 File Offset: 0x00004975
		public double VideoRotationAmount
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoRotationAmount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoRotationAmount>k__BackingField = value;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006780 File Offset: 0x00004980
		public int ConvertTimeSpanToFrameTime(TimeSpan timeSpan)
		{
			if (this.VideoInfo != null)
			{
				return (int)(timeSpan.TotalMilliseconds * this.VideoInfo.FramesPerSecond.AsDouble() / 1000.0);
			}
			LoggerExtensionMethods.LogWarning<Project>(this, "Attempting to get frame time, but no video loaded", null, "ConvertTimeSpanToFrameTime", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Projects\\Project.cs", 208);
			return 0;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000067D5 File Offset: 0x000049D5
		public string ProjectName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.ProjectFile))
				{
					return Path.GetFileNameWithoutExtension(this.ProjectFile);
				}
				return Path.GetFileNameWithoutExtension(this.VideoInfo.Filename.OriginalString);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006805 File Offset: 0x00004A05
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000680D File Offset: 0x00004A0D
		public string ProjectFile
		{
			[CompilerGenerated]
			get
			{
				return this.<ProjectFile>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ProjectFile>k__BackingField = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006816 File Offset: 0x00004A16
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000681E File Offset: 0x00004A1E
		public bool IsSaved
		{
			get
			{
				return this.isSaved;
			}
			set
			{
				this.isSaved = value;
				EventHandlerExtensions.RaiseIfNotNull(this.ValueChanged, this);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006833 File Offset: 0x00004A33
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000683B File Offset: 0x00004A3B
		public string WorkingDirectory
		{
			[CompilerGenerated]
			get
			{
				return this.<WorkingDirectory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WorkingDirectory>k__BackingField = value;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006844 File Offset: 0x00004A44
		public List<Size> GetAvailableOutputSizes()
		{
			return this.videoFormatTester.GetAvailableOutputSizes(this.WorkingDirectory, this.VideoInfo.Width, this.VideoInfo.Height, this.VideoInfo.BitsPerSecond, this.VideoInfo.FramesPerSecond, this.OutputFramesPerSecond);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006894 File Offset: 0x00004A94
		public List<Rational> GetAvailabledOutputFrameRates()
		{
			return this.videoFormatTester.GetAvailabledOutputFrameRates(this.WorkingDirectory, this.VideoInfo.Width, this.VideoInfo.Height, this.VideoInfo.BitsPerSecond, this.VideoInfo.FramesPerSecond, (int)this.OutputSize.Width, (int)this.OutputSize.Height);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000068F8 File Offset: 0x00004AF8
		private string GetDefaultOutputLocation(string prefix)
		{
			if (string.IsNullOrWhiteSpace(prefix))
			{
				string directoryName = Path.GetDirectoryName(this.VideoInfo.Filename.LocalPath);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.VideoInfo.Filename.LocalPath);
				prefix = Path.Combine(directoryName, fileNameWithoutExtension);
			}
			string arg = (this.UseAdvancedSmoothing && this.UseAdvancedSmoothingSettingEnabled) ? "adv" : "std";
			return string.Format("{0}_hyperlapse_{1}x_{2}.mp4", prefix, this.SpeedupFactor, arg);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006978 File Offset: 0x00004B78
		private TimeSpan GetOutputHyperlapseLength()
		{
			return TimeSpan.FromSeconds(this.InputLength.TotalSeconds / (double)this.SpeedupFactor);
		}
	}
}
