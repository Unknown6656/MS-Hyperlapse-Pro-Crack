using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000017 RID: 23
	public class OptionsViewModel : ViewModel
	{
		// Token: 0x04000061 RID: 97
		private ScratchManager scratchManager;

		// Token: 0x04000062 RID: 98
		private UpdateChecker updateChecker;

		// Token: 0x04000063 RID: 99
		private AccelerationOptions accelerationOptions;

		// Token: 0x04000064 RID: 100
		[CompilerGenerated]
		private Command<string> <SetTempLocationCommand>k__BackingField;

		// Token: 0x04000065 RID: 101
		[CompilerGenerated]
		private Command <ResetTempLocationCommand>k__BackingField;

		// Token: 0x04000066 RID: 102
		[CompilerGenerated]
		private Command <SaveSettingsCommand>k__BackingField;

		// Token: 0x04000067 RID: 103
		[CompilerGenerated]
		private Command <CancelCommand>k__BackingField;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private List<string> <AccelerationOptionList>k__BackingField;

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private bool <CanChangeTempLocation>k__BackingField;

		// Token: 0x060000F1 RID: 241 RVA: 0x00005FC4 File Offset: 0x000041C4
		public OptionsViewModel(INavigation navigation, IUserInterface userInterface, ScratchManager scratchManager, UpdateChecker updateChecker, AccelerationOptions accelerationOptions) : base(navigation, userInterface)
		{
			if (scratchManager == null)
			{
				throw new ArgumentNullException("scratchManager");
			}
			this.scratchManager = scratchManager;
			if (updateChecker == null)
			{
				throw new ArgumentNullException("updateChecker");
			}
			this.updateChecker = updateChecker;
			if (accelerationOptions == null)
			{
				throw new ArgumentNullException("accelerationOptions");
			}
			this.accelerationOptions = accelerationOptions;
			this.AccelerationOptionList = new List<string>
			{
				"Full Hardware",
				"Partial Hardware",
				"Software"
			};
			this.SetTempLocationCommand = new Command<string>(delegate(string s)
			{
				this.UpdateTempLocation(s);
			});
			this.ResetTempLocationCommand = new Command(delegate()
			{
				this.ResetTempLocation();
			}, () => this.TempLocation != this.scratchManager.GetDefaultScratchRoot());
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006099 File Offset: 0x00004299
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000060A1 File Offset: 0x000042A1
		public Command<string> SetTempLocationCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<SetTempLocationCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SetTempLocationCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000060AA File Offset: 0x000042AA
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000060B2 File Offset: 0x000042B2
		public Command ResetTempLocationCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ResetTempLocationCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResetTempLocationCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000060BB File Offset: 0x000042BB
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000060C3 File Offset: 0x000042C3
		public Command SaveSettingsCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<SaveSettingsCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SaveSettingsCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000060CC File Offset: 0x000042CC
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000060D4 File Offset: 0x000042D4
		public Command CancelCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<CancelCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CancelCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000060DD File Offset: 0x000042DD
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000060E5 File Offset: 0x000042E5
		public List<string> AccelerationOptionList
		{
			[CompilerGenerated]
			get
			{
				return this.<AccelerationOptionList>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AccelerationOptionList>k__BackingField = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000060EE File Offset: 0x000042EE
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00006110 File Offset: 0x00004310
		public int AccelerationIndex
		{
			get
			{
				if (this.accelerationOptions.ForceSoftwareRendering)
				{
					return 2;
				}
				if (!this.accelerationOptions.UseGeometryShaders)
				{
					return 1;
				}
				return 0;
			}
			set
			{
				switch (value)
				{
				case 0:
					this.accelerationOptions.UseGeometryShaders = true;
					this.accelerationOptions.ForceSoftwareRendering = false;
					break;
				case 1:
					this.accelerationOptions.UseGeometryShaders = false;
					this.accelerationOptions.ForceSoftwareRendering = false;
					break;
				case 2:
					this.accelerationOptions.UseGeometryShaders = false;
					this.accelerationOptions.ForceSoftwareRendering = true;
					break;
				}
				LoggerExtensionMethods.LogDiagnostic<OptionsViewModel>(this, "Changed Hardware Acceleration Option", new Dictionary<string, object>
				{
					{
						"RenderingType",
						this.AccelerationOptionList[value]
					}
				}, "AccelerationIndex", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\OptionsViewModel.cs", 84);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000061B4 File Offset: 0x000043B4
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000061BC File Offset: 0x000043BC
		public bool CanChangeTempLocation
		{
			[CompilerGenerated]
			get
			{
				return this.<CanChangeTempLocation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanChangeTempLocation>k__BackingField = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000061C5 File Offset: 0x000043C5
		public string TempLocation
		{
			get
			{
				return this.scratchManager.ScratchRoot;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000061D2 File Offset: 0x000043D2
		public string ExpandedTempLocation
		{
			get
			{
				return this.scratchManager.ExpandedScratchRoot;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000061E0 File Offset: 0x000043E0
		public string TempLocationExtraInfo
		{
			get
			{
				string result;
				try
				{
					ulong freeScratchBytes = this.scratchManager.GetFreeScratchBytes();
					ulong totalScratchBytes = this.scratchManager.GetTotalScratchBytes();
					ByteSizeConverter byteSizeConverter = new ByteSizeConverter();
					result = string.Format("{0} has {1} free of {2} ", this.GetRootDrive(), byteSizeConverter.Convert(freeScratchBytes), byteSizeConverter.Convert(totalScratchBytes));
				}
				catch (Exception value)
				{
					LoggerExtensionMethods.LogError<OptionsViewModel>(this, "Couldn't read free disk space", new Dictionary<string, object>
					{
						{
							"Exception",
							value
						}
					}, "TempLocationExtraInfo", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\OptionsViewModel.cs", 120);
					result = "Error reading free disk space information";
				}
				return result;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000627C File Offset: 0x0000447C
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006289 File Offset: 0x00004489
		public bool CheckForUpdates
		{
			get
			{
				return this.updateChecker.IsUpdateCheckEnabled;
			}
			set
			{
				this.updateChecker.IsUpdateCheckEnabled = value;
				this.NotifyPropertyChanged("CheckForUpdates");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000062A2 File Offset: 0x000044A2
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000062AF File Offset: 0x000044AF
		public bool UseHardwareVideoEncoder
		{
			get
			{
				return this.accelerationOptions.UseHardwareVideoEncoder;
			}
			set
			{
				this.accelerationOptions.UseHardwareVideoEncoder = value;
				this.NotifyPropertyChanged("UseHardwareVideoEncoder");
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000062C8 File Offset: 0x000044C8
		private string GetRootDrive()
		{
			return Path.GetPathRoot(this.ExpandedTempLocation).TrimEnd(new char[]
			{
				'\\'
			});
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000062F4 File Offset: 0x000044F4
		private void UpdateTempLocation(string s)
		{
			try
			{
				LoggerExtensionMethods.LogCheckpoint<OptionsViewModel>(this, "UpdateTempLocation", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\OptionsViewModel.cs", 161);
				this.scratchManager.UpdateScratchRoot(s);
				base.NotifyPropertyChanged<string>(() => this.TempLocation);
				base.NotifyPropertyChanged<string>(() => this.ExpandedTempLocation);
				base.NotifyPropertyChanged<string>(() => this.TempLocationExtraInfo);
				this.ResetTempLocationCommand.RaiseCanExecuteChanged();
			}
			catch (Exception ex)
			{
				LoggerExtensionMethods.LogError<OptionsViewModel>(this, "Couldn't change scratch location", null, "UpdateTempLocation", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\OptionsViewModel.cs", 170);
				base.UserInterface.ShowMessage("Error", string.Format("Couldn't change temporary location:\n{0}", ex.Message));
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000642C File Offset: 0x0000462C
		private void ResetTempLocation()
		{
			LoggerExtensionMethods.LogCheckpoint<OptionsViewModel>(this, "ResetTempLocation", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\OptionsViewModel.cs", 177);
			this.UpdateTempLocation(this.scratchManager.GetDefaultScratchRoot());
			this.ResetTempLocationCommand.RaiseCanExecuteChanged();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005F9A File Offset: 0x0000419A
		[CompilerGenerated]
		private void <.ctor>b__1(string s)
		{
			this.UpdateTempLocation(s);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005FA3 File Offset: 0x000041A3
		[CompilerGenerated]
		private void <.ctor>b__2()
		{
			this.ResetTempLocation();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005FAB File Offset: 0x000041AB
		[CompilerGenerated]
		private bool <.ctor>b__3()
		{
			return this.TempLocation != this.scratchManager.GetDefaultScratchRoot();
		}
	}
}
