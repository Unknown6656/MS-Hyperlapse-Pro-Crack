using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000006 RID: 6
	public abstract class Calibration
	{
		// Token: 0x0400001D RID: 29
		public static readonly string ResourcePrefix = (Environment.OSVersion.Platform == PlatformID.Win32NT) ? "Microsoft.Research.Hyperlapse.Calibrations." : "Microsoft.Research.Hyperlapse.";

		// Token: 0x0400001E RID: 30
		private string bareLocation = "";

		// Token: 0x0400001F RID: 31
		private string location = "";

		// Token: 0x04000020 RID: 32
		[CompilerGenerated]
		private int <ID>k__BackingField;

		// Token: 0x04000021 RID: 33
		[CompilerGenerated]
		private string <Description>k__BackingField;

		// Token: 0x04000022 RID: 34
		[CompilerGenerated]
		private List<string> <VideoModes>k__BackingField;

		// Token: 0x04000023 RID: 35
		[CompilerGenerated]
		private bool <HousingOn>k__BackingField;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A9A File Offset: 0x00000C9A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ID>k__BackingField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AAB File Offset: 0x00000CAB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002AB3 File Offset: 0x00000CB3
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002ABC File Offset: 0x00000CBC
		public string Location
		{
			get
			{
				if (!this.SupportsHousing || this.HousingOn)
				{
					return this.location;
				}
				return this.bareLocation;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002ADB File Offset: 0x00000CDB
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public List<string> VideoModes
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoModes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VideoModes>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002AEC File Offset: 0x00000CEC
		public bool SupportsHousing
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.bareLocation);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AFC File Offset: 0x00000CFC
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002B04 File Offset: 0x00000D04
		public bool HousingOn
		{
			[CompilerGenerated]
			get
			{
				return this.<HousingOn>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HousingOn>k__BackingField = value;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B10 File Offset: 0x00000D10
		public Calibration(int id, string description, List<string> videoModes, string location, string bareFile)
		{
			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentNullException("description");
			}
			this.Description = description;
			if (string.IsNullOrWhiteSpace(location))
			{
				throw new ArgumentNullException("location");
			}
			this.location = location;
			if (videoModes == null)
			{
				throw new ArgumentNullException("videoModes");
			}
			if (videoModes.Count == 0)
			{
				throw new ArgumentException("videoModes must contain at least one video mode");
			}
			this.VideoModes = videoModes;
			this.ID = id;
			this.bareLocation = bareFile;
			this.HousingOn = true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public virtual string ExtractToFolder(string folder)
		{
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
			string fileName = Path.GetFileName(this.Location);
			return Path.Combine(folder, fileName);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BE1 File Offset: 0x00000DE1
		// Note: this type is marked as 'beforefieldinit'.
		static Calibration()
		{
		}
	}
}
