using System;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000012 RID: 18
	public class VideoInfo
	{
		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private Rational <FramesPerSecond>k__BackingField;

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private Uri <Filename>k__BackingField;

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private TimeSpan <Duration>k__BackingField;

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private int <Width>k__BackingField;

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private int <Height>k__BackingField;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private int <CameraModel>k__BackingField;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private string <VideoMode>k__BackingField;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private int <Rotation>k__BackingField;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private double <BitsPerSecond>k__BackingField;

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private double <OriginalBitsPerSecond>k__BackingField;

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005EC8 File Offset: 0x000040C8
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005ED0 File Offset: 0x000040D0
		public Rational FramesPerSecond
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005ED9 File Offset: 0x000040D9
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005EE1 File Offset: 0x000040E1
		public Uri Filename
		{
			[CompilerGenerated]
			get
			{
				return this.<Filename>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Filename>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005EEA File Offset: 0x000040EA
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005EF2 File Offset: 0x000040F2
		public TimeSpan Duration
		{
			[CompilerGenerated]
			get
			{
				return this.<Duration>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Duration>k__BackingField = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005EFB File Offset: 0x000040FB
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005F03 File Offset: 0x00004103
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005F0C File Offset: 0x0000410C
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005F14 File Offset: 0x00004114
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005F1D File Offset: 0x0000411D
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005F25 File Offset: 0x00004125
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005F2E File Offset: 0x0000412E
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005F36 File Offset: 0x00004136
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005F3F File Offset: 0x0000413F
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005F47 File Offset: 0x00004147
		public int Rotation
		{
			[CompilerGenerated]
			get
			{
				return this.<Rotation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Rotation>k__BackingField = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005F50 File Offset: 0x00004150
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005F58 File Offset: 0x00004158
		public double BitsPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<BitsPerSecond>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BitsPerSecond>k__BackingField = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005F61 File Offset: 0x00004161
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005F69 File Offset: 0x00004169
		public double OriginalBitsPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<OriginalBitsPerSecond>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OriginalBitsPerSecond>k__BackingField = value;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005F72 File Offset: 0x00004172
		public VideoInfo()
		{
		}
	}
}
