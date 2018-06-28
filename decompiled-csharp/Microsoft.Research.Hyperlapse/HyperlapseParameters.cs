using System;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000019 RID: 25
	public class HyperlapseParameters
	{
		// Token: 0x0400007C RID: 124
		[CompilerGenerated]
		private Uri <VideoUri>k__BackingField;

		// Token: 0x0400007D RID: 125
		[CompilerGenerated]
		private string <VideoOutputFilePath>k__BackingField;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private int <SpeedupFactor>k__BackingField;

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private string <TempOutputDirectory>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private int <StartFrame>k__BackingField;

		// Token: 0x04000081 RID: 129
		[CompilerGenerated]
		private int <EndFrame>k__BackingField;

		// Token: 0x04000082 RID: 130
		[CompilerGenerated]
		private int <RenderTarget>k__BackingField;

		// Token: 0x04000083 RID: 131
		[CompilerGenerated]
		private double <FramesPerSecond>k__BackingField;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private TimeSpan <VideoLength>k__BackingField;

		// Token: 0x04000085 RID: 133
		[CompilerGenerated]
		private Calibration <CalibrationFile>k__BackingField;

		// Token: 0x04000086 RID: 134
		[CompilerGenerated]
		private Rational <FrameRate>k__BackingField;

		// Token: 0x04000087 RID: 135
		[CompilerGenerated]
		private int <InputWidth>k__BackingField;

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		private int <InputHeight>k__BackingField;

		// Token: 0x04000089 RID: 137
		[CompilerGenerated]
		private int <OutputWidth>k__BackingField;

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		private int <OutputHeight>k__BackingField;

		// Token: 0x0400008B RID: 139
		[CompilerGenerated]
		private int <OutputRotation>k__BackingField;

		// Token: 0x0400008C RID: 140
		[CompilerGenerated]
		private int <OutputBitrate>k__BackingField;

		// Token: 0x0400008D RID: 141
		[CompilerGenerated]
		private bool <UseAdvancedSmoothing>k__BackingField;

		// Token: 0x0400008E RID: 142
		[CompilerGenerated]
		private bool <UseGeometryShaders>k__BackingField;

		// Token: 0x0400008F RID: 143
		[CompilerGenerated]
		private bool <ForceSoftwareRendering>k__BackingField;

		// Token: 0x04000090 RID: 144
		[CompilerGenerated]
		private bool <UseHardwareVideoEncoder>k__BackingField;

		// Token: 0x04000091 RID: 145
		[CompilerGenerated]
		private int <CameraModel>k__BackingField;

		// Token: 0x04000092 RID: 146
		[CompilerGenerated]
		private string <VideoMode>k__BackingField;

		// Token: 0x04000093 RID: 147
		[CompilerGenerated]
		private float <CreditLength>k__BackingField;

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000069A0 File Offset: 0x00004BA0
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000069A8 File Offset: 0x00004BA8
		public Uri VideoUri
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoUri>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoUri>k__BackingField = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000069B1 File Offset: 0x00004BB1
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000069B9 File Offset: 0x00004BB9
		public string VideoOutputFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoOutputFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoOutputFilePath>k__BackingField = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000069C2 File Offset: 0x00004BC2
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000069CA File Offset: 0x00004BCA
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000069D3 File Offset: 0x00004BD3
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000069DB File Offset: 0x00004BDB
		public string TempOutputDirectory
		{
			[CompilerGenerated]
			get
			{
				return this.<TempOutputDirectory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TempOutputDirectory>k__BackingField = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000069E4 File Offset: 0x00004BE4
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000069EC File Offset: 0x00004BEC
		public int StartFrame
		{
			[CompilerGenerated]
			get
			{
				return this.<StartFrame>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StartFrame>k__BackingField = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000069F5 File Offset: 0x00004BF5
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000069FD File Offset: 0x00004BFD
		public int EndFrame
		{
			[CompilerGenerated]
			get
			{
				return this.<EndFrame>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EndFrame>k__BackingField = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006A06 File Offset: 0x00004C06
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006A0E File Offset: 0x00004C0E
		public int RenderTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<RenderTarget>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RenderTarget>k__BackingField = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006A17 File Offset: 0x00004C17
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006A1F File Offset: 0x00004C1F
		public double FramesPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<FramesPerSecond>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FramesPerSecond>k__BackingField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006A28 File Offset: 0x00004C28
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006A30 File Offset: 0x00004C30
		public TimeSpan VideoLength
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VideoLength>k__BackingField = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006A39 File Offset: 0x00004C39
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006A41 File Offset: 0x00004C41
		public Calibration CalibrationFile
		{
			[CompilerGenerated]
			get
			{
				return this.<CalibrationFile>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CalibrationFile>k__BackingField = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006A4A File Offset: 0x00004C4A
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006A52 File Offset: 0x00004C52
		public Rational FrameRate
		{
			[CompilerGenerated]
			get
			{
				return this.<FrameRate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FrameRate>k__BackingField = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006A5B File Offset: 0x00004C5B
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006A63 File Offset: 0x00004C63
		public int InputWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<InputWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InputWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006A6C File Offset: 0x00004C6C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006A74 File Offset: 0x00004C74
		public int InputHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<InputHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InputHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006A7D File Offset: 0x00004C7D
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006A85 File Offset: 0x00004C85
		public int OutputWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006A8E File Offset: 0x00004C8E
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006A96 File Offset: 0x00004C96
		public int OutputHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006A9F File Offset: 0x00004C9F
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006AA7 File Offset: 0x00004CA7
		public int OutputRotation
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputRotation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputRotation>k__BackingField = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006AB0 File Offset: 0x00004CB0
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public int OutputBitrate
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputBitrate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputBitrate>k__BackingField = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006AC1 File Offset: 0x00004CC1
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006AC9 File Offset: 0x00004CC9
		public bool UseAdvancedSmoothing
		{
			[CompilerGenerated]
			get
			{
				return this.<UseAdvancedSmoothing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseAdvancedSmoothing>k__BackingField = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006AD2 File Offset: 0x00004CD2
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00006ADA File Offset: 0x00004CDA
		public bool UseGeometryShaders
		{
			[CompilerGenerated]
			get
			{
				return this.<UseGeometryShaders>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseGeometryShaders>k__BackingField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006AE3 File Offset: 0x00004CE3
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00006AEB File Offset: 0x00004CEB
		public bool ForceSoftwareRendering
		{
			[CompilerGenerated]
			get
			{
				return this.<ForceSoftwareRendering>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ForceSoftwareRendering>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006AF4 File Offset: 0x00004CF4
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00006AFC File Offset: 0x00004CFC
		public bool UseHardwareVideoEncoder
		{
			[CompilerGenerated]
			get
			{
				return this.<UseHardwareVideoEncoder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseHardwareVideoEncoder>k__BackingField = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006B05 File Offset: 0x00004D05
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00006B0D File Offset: 0x00004D0D
		public int CameraModel
		{
			[CompilerGenerated]
			get
			{
				return this.<CameraModel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CameraModel>k__BackingField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006B16 File Offset: 0x00004D16
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006B1E File Offset: 0x00004D1E
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006B27 File Offset: 0x00004D27
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006B2F File Offset: 0x00004D2F
		public float CreditLength
		{
			[CompilerGenerated]
			get
			{
				return this.<CreditLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreditLength>k__BackingField = value;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006B38 File Offset: 0x00004D38
		public HyperlapseParameters()
		{
		}
	}
}
