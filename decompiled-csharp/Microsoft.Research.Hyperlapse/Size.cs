using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class Size
	{
		// Token: 0x040000B5 RID: 181
		[CompilerGenerated]
		private double <Width>k__BackingField;

		// Token: 0x040000B6 RID: 182
		[CompilerGenerated]
		private double <Height>k__BackingField;

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007CFA File Offset: 0x00005EFA
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007D02 File Offset: 0x00005F02
		public double Width
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007D0B File Offset: 0x00005F0B
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007D13 File Offset: 0x00005F13
		public double Height
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

		// Token: 0x060001B4 RID: 436 RVA: 0x00007D1C File Offset: 0x00005F1C
		public Size(double width, double height)
		{
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007D32 File Offset: 0x00005F32
		public Size()
		{
			this.Width = 0.0;
			this.Height = 0.0;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007D58 File Offset: 0x00005F58
		public Size SwapDimensions()
		{
			return new Size(this.Height, this.Width);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Size)
			{
				Size size = (Size)obj;
				return size.Width == this.Width && size.Height == this.Height;
			}
			return base.Equals(obj);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public override int GetHashCode()
		{
			return this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}
	}
}
