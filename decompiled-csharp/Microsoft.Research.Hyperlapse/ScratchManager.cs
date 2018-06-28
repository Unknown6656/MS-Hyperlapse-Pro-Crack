using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001B RID: 27
	public class ScratchManager
	{
		// Token: 0x04000098 RID: 152
		private readonly string CacheDirectory = "HyperlapseCache";

		// Token: 0x04000099 RID: 153
		private readonly string SessionIdFileName = "SessionId.txt";

		// Token: 0x0400009A RID: 154
		private readonly string ScrathRootSettingsKey = "ScratchRoot";

		// Token: 0x0400009B RID: 155
		private string processId;

		// Token: 0x0400009C RID: 156
		private ITelemetryClient telemetryClient;

		// Token: 0x0400009D RID: 157
		private ISettingsStore settingsStore;

		// Token: 0x0400009E RID: 158
		private IFreeSpaceProvider freeSpaceProvider;

		// Token: 0x0400009F RID: 159
		private ITempPathProvider tempPathProvider;

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006CAA File Offset: 0x00004EAA
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00006CC3 File Offset: 0x00004EC3
		public string ScratchRoot
		{
			get
			{
				return this.settingsStore.ReadSetting<string>(this.ScrathRootSettingsKey, this.GetDefaultScratchRoot());
			}
			set
			{
				this.settingsStore.WriteSetting<string>(this.ScrathRootSettingsKey, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006CD7 File Offset: 0x00004ED7
		public string ExpandedScratchRoot
		{
			get
			{
				return this.tempPathProvider.ExpandEnvironmentVariables(this.ScratchRoot);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006CEC File Offset: 0x00004EEC
		public ScratchManager(ITelemetryClient telemetryClient, ISettingsStore settingsStore, IFreeSpaceProvider freeSpaceProvider, ITempPathProvider tempPathProvider)
		{
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			this.telemetryClient = telemetryClient;
			if (settingsStore == null)
			{
				throw new ArgumentNullException("settingsStore");
			}
			this.settingsStore = settingsStore;
			if (freeSpaceProvider == null)
			{
				throw new ArgumentNullException("freeSpaceProvider");
			}
			this.freeSpaceProvider = freeSpaceProvider;
			if (tempPathProvider == null)
			{
				throw new ArgumentNullException("tempPathProvider");
			}
			this.tempPathProvider = tempPathProvider;
			this.processId = Process.GetCurrentProcess().Id.ToString();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006D90 File Offset: 0x00004F90
		public string GetNewWorkingDirectory()
		{
			string currentScratchDirectory = this.GetCurrentScratchDirectory();
			string text = Path.Combine(currentScratchDirectory, Path.GetRandomFileName());
			Directory.CreateDirectory(text);
			LoggerExtensionMethods.LogDiagnostic<ScratchManager>(this, "New Working Directory", null, "GetNewWorkingDirectory", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 95);
			return text;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public void InitializeScratchSpace()
		{
			string currentScratchDirectory = this.GetCurrentScratchDirectory();
			string path = Path.Combine(currentScratchDirectory, this.SessionIdFileName);
			this.DetectAndHandleUncleanShutdown();
			LoggerExtensionMethods.LogDiagnostic<ScratchManager>(this, "Initalising scratch", null, "InitializeScratchSpace", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 108);
			Directory.CreateDirectory(currentScratchDirectory);
			using (StreamWriter streamWriter = File.CreateText(path))
			{
				streamWriter.Write(this.telemetryClient.SessionID);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006E70 File Offset: 0x00005070
		private void DetectAndHandleUncleanShutdown()
		{
			string cacheDirectory = this.GetCacheDirectory();
			if (Directory.Exists(cacheDirectory))
			{
				string processName = Process.GetCurrentProcess().ProcessName;
				Process[] processesByName = Process.GetProcessesByName(processName);
				string[] directories = Directory.GetDirectories(cacheDirectory);
				string[] array = directories;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					int pid = -1;
					if (int.TryParse(fileNameWithoutExtension, out pid))
					{
						if (!processesByName.Any((Process p) => p.Id == pid && !p.HasExited))
						{
							string path = Path.Combine(text, this.SessionIdFileName);
							string value = "No Session File Found";
							if (File.Exists(path))
							{
								value = File.ReadAllText(path);
							}
							LoggerExtensionMethods.LogWarning<ScratchManager>(this, "Unclean shutdown detected", new Dictionary<string, object>
							{
								{
									"PreviousSessionId",
									value
								}
							}, "DetectAndHandleUncleanShutdown", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 149);
							this.TeardownScratchSpace(text);
						}
					}
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006F74 File Offset: 0x00005174
		public void TeardownScratchSpace()
		{
			string currentScratchDirectory = this.GetCurrentScratchDirectory();
			this.TeardownScratchSpace(currentScratchDirectory);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006F90 File Offset: 0x00005190
		private void TeardownScratchSpace(string scratchDir)
		{
			LoggerExtensionMethods.LogDiagnostic<ScratchManager>(this, "Tearing down scratch space", null, "TeardownScratchSpace", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 165);
			if (Directory.Exists(scratchDir))
			{
				try
				{
					Directory.Delete(scratchDir, true);
				}
				catch (Exception value)
				{
					LoggerExtensionMethods.LogError<ScratchManager>(this, "Couldn't tear down scratch space", new Dictionary<string, object>
					{
						{
							"Exception",
							value
						}
					}, "TeardownScratchSpace", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 175);
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000700C File Offset: 0x0000520C
		public void UpdateScratchRoot(string newRoot)
		{
			LoggerExtensionMethods.LogDiagnostic<ScratchManager>(this, "Attempting to change scratch root", new Dictionary<string, object>
			{
				{
					"NewRoot",
					newRoot
				}
			}, "UpdateScratchRoot", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 182);
			newRoot = this.tempPathProvider.RestoreEnvironmentVariables(newRoot);
			if (newRoot == this.ScratchRoot)
			{
				return;
			}
			string directory = this.tempPathProvider.ExpandEnvironmentVariables(newRoot);
			this.EnsureDirectoryExists(directory);
			this.CheckDirectoryIsWriteable(directory);
			this.TeardownScratchSpace();
			this.ScratchRoot = newRoot;
			this.InitializeScratchSpace();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007094 File Offset: 0x00005294
		private void CheckDirectoryIsWriteable(string directory)
		{
			try
			{
				string path = Path.Combine(directory, Path.GetRandomFileName());
				File.WriteAllText(path, string.Empty);
				File.Delete(path);
			}
			catch (Exception value)
			{
				LoggerExtensionMethods.LogWarning<ScratchManager>(this, "Coludn't write to scratch directory", new Dictionary<string, object>
				{
					{
						"Exception",
						value
					}
				}, "CheckDirectoryIsWriteable", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 214);
				throw new Exception("Directory is not writeable");
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000710C File Offset: 0x0000530C
		private void EnsureDirectoryExists(string directory)
		{
			if (!Directory.Exists(directory))
			{
				try
				{
					Directory.CreateDirectory(directory);
				}
				catch (Exception value)
				{
					LoggerExtensionMethods.LogWarning<ScratchManager>(this, "Coludn't make scratch directory", new Dictionary<string, object>
					{
						{
							"Exception",
							value
						}
					}, "EnsureDirectoryExists", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 229);
					throw new Exception("Couldn't create directory");
				}
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007174 File Offset: 0x00005374
		public void ResetScratchRoot()
		{
			LoggerExtensionMethods.LogDiagnostic<ScratchManager>(this, "Reset scratch root to default", null, "ResetScratchRoot", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Scratch\\ScratchManager.cs", 237);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007191 File Offset: 0x00005391
		public string GetDefaultScratchRoot()
		{
			return string.Format("{0}/Hyperlapse", this.tempPathProvider.TempPathEnvironmentVariable);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000071A8 File Offset: 0x000053A8
		public ulong GetFreeScratchBytes()
		{
			return this.GetScratchSizeInfo().Item1;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000071B5 File Offset: 0x000053B5
		public ulong GetTotalScratchBytes()
		{
			return this.GetScratchSizeInfo().Item2;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000071C4 File Offset: 0x000053C4
		private string GetCurrentScratchDirectory()
		{
			string cacheDirectory = this.GetCacheDirectory();
			return Path.Combine(cacheDirectory, this.processId);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000071E4 File Offset: 0x000053E4
		private string GetCacheDirectory()
		{
			string path = this.tempPathProvider.ExpandEnvironmentVariables(this.ScratchRoot);
			return Path.Combine(path, this.CacheDirectory);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000720F File Offset: 0x0000540F
		private Tuple<ulong, ulong> GetScratchSizeInfo()
		{
			return this.freeSpaceProvider.GetFreeSpaceForPath(this.GetCurrentScratchDirectory());
		}

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3
		{
			// Token: 0x0400014F RID: 335
			public int pid;

			// Token: 0x0600027D RID: 637 RVA: 0x00006E4C File Offset: 0x0000504C
			public <>c__DisplayClass3()
			{
			}

			// Token: 0x0600027E RID: 638 RVA: 0x00006E54 File Offset: 0x00005054
			public bool <DetectAndHandleUncleanShutdown>b__1(Process p)
			{
				return p.Id == this.pid && !p.HasExited;
			}
		}
	}
}
