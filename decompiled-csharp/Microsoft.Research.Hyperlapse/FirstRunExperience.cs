using System;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000B RID: 11
	public class FirstRunExperience
	{
		// Token: 0x04000032 RID: 50
		private IUpgradeableSettingsStore settingsStore;

		// Token: 0x04000033 RID: 51
		[CompilerGenerated]
		private bool <IsFirstRun>k__BackingField;

		// Token: 0x06000089 RID: 137 RVA: 0x0000462E File Offset: 0x0000282E
		public FirstRunExperience(IUpgradeableSettingsStore settingsStore)
		{
			if (settingsStore == null)
			{
				throw new ArgumentNullException("settingsStore");
			}
			this.settingsStore = settingsStore;
			this.UpgradeIfNeeded();
			this.SetFirstRunTime();
			this.SetInstallationId();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000465D File Offset: 0x0000285D
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004674 File Offset: 0x00002874
		public DateTime FirstRunTime
		{
			get
			{
				return this.settingsStore.ReadSetting<DateTime>("FirstRunTime", DateTime.MinValue);
			}
			private set
			{
				this.settingsStore.WriteSetting<DateTime>("FirstRunTime", value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004687 File Offset: 0x00002887
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000468F File Offset: 0x0000288F
		public bool IsFirstRun
		{
			[CompilerGenerated]
			get
			{
				return this.<IsFirstRun>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsFirstRun>k__BackingField = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004698 File Offset: 0x00002898
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000046AB File Offset: 0x000028AB
		public int VideosProcessed
		{
			get
			{
				return this.settingsStore.ReadSetting<int>("VideosProcessed", 0);
			}
			set
			{
				this.settingsStore.WriteSetting<int>("VideosProcessed", value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000046BE File Offset: 0x000028BE
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000046D5 File Offset: 0x000028D5
		public string InstallationId
		{
			get
			{
				return this.settingsStore.ReadSetting<string>("InstallationId", "");
			}
			private set
			{
				this.settingsStore.WriteSetting<string>("InstallationId", value);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000046E8 File Offset: 0x000028E8
		private void SetFirstRunTime()
		{
			if (this.FirstRunTime == DateTime.MinValue)
			{
				this.FirstRunTime = DateTime.Now;
				this.IsFirstRun = true;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004710 File Offset: 0x00002910
		private void SetInstallationId()
		{
			if (string.IsNullOrWhiteSpace(this.InstallationId))
			{
				this.InstallationId = Guid.NewGuid().ToString();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004743 File Offset: 0x00002943
		private void UpgradeIfNeeded()
		{
			if (this.FirstRunTime == DateTime.MinValue)
			{
				this.settingsStore.UpgradeFromPreviousVersion();
			}
		}
	}
}
