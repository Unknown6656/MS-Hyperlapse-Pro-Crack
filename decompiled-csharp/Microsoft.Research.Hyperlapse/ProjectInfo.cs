using System;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class ProjectInfo
	{
		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private string <InputFileName>k__BackingField;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		private long <StartTimeTicks>k__BackingField;

		// Token: 0x04000009 RID: 9
		[CompilerGenerated]
		private long <EndTimeTicks>k__BackingField;

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private long <SelectedFrameTimeTicks>k__BackingField;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private int <SpeedupFactor>k__BackingField;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private int <CalibrationId>k__BackingField;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private string <VideoMode>k__BackingField;

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		private Size <OutputSize>k__BackingField;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private double <OutputFramesPerSecond>k__BackingField;

		// Token: 0x04000010 RID: 16
		[CompilerGenerated]
		private Rational <OutputFramesPerSecondAsRational>k__BackingField;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private string <OutputFileName>k__BackingField;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private double <RotationAmount>k__BackingField;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private bool <UseAdvancedSmoothing>k__BackingField;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private bool <CalibrationHousingOn>k__BackingField;

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		private bool <CreditDisabled>k__BackingField;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002460 File Offset: 0x00000660
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002468 File Offset: 0x00000668
		public string InputFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<InputFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InputFileName>k__BackingField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002471 File Offset: 0x00000671
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002479 File Offset: 0x00000679
		public long StartTimeTicks
		{
			[CompilerGenerated]
			get
			{
				return this.<StartTimeTicks>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StartTimeTicks>k__BackingField = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002482 File Offset: 0x00000682
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000248A File Offset: 0x0000068A
		public long EndTimeTicks
		{
			[CompilerGenerated]
			get
			{
				return this.<EndTimeTicks>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EndTimeTicks>k__BackingField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002493 File Offset: 0x00000693
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000249B File Offset: 0x0000069B
		public long SelectedFrameTimeTicks
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedFrameTimeTicks>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedFrameTimeTicks>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024A4 File Offset: 0x000006A4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024AC File Offset: 0x000006AC
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024B5 File Offset: 0x000006B5
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024BD File Offset: 0x000006BD
		public int CalibrationId
		{
			[CompilerGenerated]
			get
			{
				return this.<CalibrationId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CalibrationId>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024C6 File Offset: 0x000006C6
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024CE File Offset: 0x000006CE
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024D7 File Offset: 0x000006D7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024DF File Offset: 0x000006DF
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024E8 File Offset: 0x000006E8
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024F0 File Offset: 0x000006F0
		public double OutputFramesPerSecond
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024F9 File Offset: 0x000006F9
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002501 File Offset: 0x00000701
		public Rational OutputFramesPerSecondAsRational
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputFramesPerSecondAsRational>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputFramesPerSecondAsRational>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000250A File Offset: 0x0000070A
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002512 File Offset: 0x00000712
		public string OutputFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputFileName>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000251B File Offset: 0x0000071B
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002523 File Offset: 0x00000723
		public double RotationAmount
		{
			[CompilerGenerated]
			get
			{
				return this.<RotationAmount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RotationAmount>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000252C File Offset: 0x0000072C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002534 File Offset: 0x00000734
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000253D File Offset: 0x0000073D
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002545 File Offset: 0x00000745
		public bool CalibrationHousingOn
		{
			[CompilerGenerated]
			get
			{
				return this.<CalibrationHousingOn>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CalibrationHousingOn>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000254E File Offset: 0x0000074E
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002556 File Offset: 0x00000756
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

		// Token: 0x0600002D RID: 45 RVA: 0x0000255F File Offset: 0x0000075F
		public ProjectInfo()
		{
		}
	}
}
