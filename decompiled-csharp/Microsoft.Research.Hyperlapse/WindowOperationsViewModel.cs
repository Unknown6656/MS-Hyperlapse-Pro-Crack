using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000A RID: 10
	public class WindowOperationsViewModel : ViewModel
	{
		// Token: 0x04000024 RID: 36
		private ProjectManager projectManager;

		// Token: 0x04000025 RID: 37
		private ActivationManager activationManager;

		// Token: 0x04000026 RID: 38
		private ProductInfo productInfo;

		// Token: 0x04000027 RID: 39
		private bool operationsDisabled;

		// Token: 0x04000028 RID: 40
		private Project currentProject;

		// Token: 0x04000029 RID: 41
		private readonly string[] supportedVideoTypes = new string[]
		{
			"mp4",
			"mov",
			"wmv"
		};

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		private Command <ShowHelpCommand>k__BackingField;

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		private AsyncCommand <ShowOptionsCommand>k__BackingField;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		private AsyncCommand<string> <NewProjectCommand>k__BackingField;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private AsyncCommand<string> <OpenProjectCommand>k__BackingField;

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		private AsyncCommand <SaveProjectCommand>k__BackingField;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		private AsyncCommand <SaveProjectAsCommand>k__BackingField;

		// Token: 0x04000030 RID: 48
		[CompilerGenerated]
		private AsyncCommand<string> <HandleFileCommand>k__BackingField;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		private Command <ActivateProductCommand>k__BackingField;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C98 File Offset: 0x00000E98
		public IHyperlapseUserInterface UserInterface
		{
			get
			{
				return (IHyperlapseUserInterface)base.UserInterface;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D24 File Offset: 0x00000F24
		public WindowOperationsViewModel(INavigation navigation, IHyperlapseUserInterface userInterface, ProjectManager projectManager, ActivationManager activationManager, ProductInfo productInfo) : base(navigation, userInterface)
		{
			if (projectManager == null)
			{
				throw new ArgumentNullException("projectManager");
			}
			this.projectManager = projectManager;
			if (activationManager == null)
			{
				throw new ArgumentNullException("activationManager");
			}
			this.activationManager = activationManager;
			if (productInfo == null)
			{
				throw new ArgumentNullException("productInfo");
			}
			this.productInfo = productInfo;
			this.ShowHelpCommand = new Command(new Action(this.ShowHelp));
			this.ShowOptionsCommand = new AsyncCommand(new Func<Task>(this.ShowOptions), () => !this.operationsDisabled);
			this.NewProjectCommand = new AsyncCommand<string>(new Func<string, Task>(this.NewProject), (string s) => !this.operationsDisabled);
			this.OpenProjectCommand = new AsyncCommand<string>(new Func<string, Task>(this.OpenProject), (string s) => !this.operationsDisabled);
			this.SaveProjectCommand = new AsyncCommand(() => this.SaveProject(false), () => !this.operationsDisabled && this.currentProject != null);
			this.SaveProjectAsCommand = new AsyncCommand(() => this.SaveProject(true), () => !this.operationsDisabled && this.currentProject != null);
			this.HandleFileCommand = new AsyncCommand<string>((string s) => this.HandleFile(s), (string s) => this.CanHandleFile(s));
			this.ActivateProductCommand = new Command(new Action(this.ShowUpgrade), () => this.IsInTrialMode);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F11 File Offset: 0x00001111
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002F19 File Offset: 0x00001119
		public Command ShowHelpCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowHelpCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShowHelpCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002F22 File Offset: 0x00001122
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002F2A File Offset: 0x0000112A
		public AsyncCommand ShowOptionsCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowOptionsCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShowOptionsCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F33 File Offset: 0x00001133
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002F3B File Offset: 0x0000113B
		public AsyncCommand<string> NewProjectCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<NewProjectCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NewProjectCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F44 File Offset: 0x00001144
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F4C File Offset: 0x0000114C
		public AsyncCommand<string> OpenProjectCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<OpenProjectCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OpenProjectCommand>k__BackingField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F55 File Offset: 0x00001155
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002F5D File Offset: 0x0000115D
		public AsyncCommand SaveProjectCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<SaveProjectCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SaveProjectCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F66 File Offset: 0x00001166
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002F6E File Offset: 0x0000116E
		public AsyncCommand SaveProjectAsCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<SaveProjectAsCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SaveProjectAsCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002F77 File Offset: 0x00001177
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F7F File Offset: 0x0000117F
		public AsyncCommand<string> HandleFileCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<HandleFileCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HandleFileCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F88 File Offset: 0x00001188
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F90 File Offset: 0x00001190
		public Command ActivateProductCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ActivateProductCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActivateProductCommand>k__BackingField = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F99 File Offset: 0x00001199
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002FA1 File Offset: 0x000011A1
		public bool OperationsDisabled
		{
			get
			{
				return this.operationsDisabled;
			}
			set
			{
				this.operationsDisabled = value;
				this.NewProjectCommand.RaiseCanExecuteChanged();
				this.OpenProjectCommand.RaiseCanExecuteChanged();
				this.SaveProjectCommand.RaiseCanExecuteChanged();
				this.SaveProjectAsCommand.RaiseCanExecuteChanged();
				this.ShowOptionsCommand.RaiseCanExecuteChanged();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002FE4 File Offset: 0x000011E4
		public string WindowTitle
		{
			get
			{
				string arg = (this.currentProject != null && !this.currentProject.IsSaved) ? "*" : "";
				string arg2 = (this.CurrentProject != null) ? string.Format("{0}{1} - ", this.CurrentProject.ProjectName, arg) : string.Empty;
				return string.Format("{0}{1}", arg2, this.productInfo.ApplicationName);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000304F File Offset: 0x0000124F
		public bool IsInTrialMode
		{
			get
			{
				return !this.activationManager.GetActivationStatus();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000305F File Offset: 0x0000125F
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003068 File Offset: 0x00001268
		public Project CurrentProject
		{
			get
			{
				return this.currentProject;
			}
			private set
			{
				if (this.currentProject != null)
				{
					this.currentProject.ValueChanged -= this.currentProject_ValueChanged;
				}
				if (value != null)
				{
					value.ValueChanged += this.currentProject_ValueChanged;
				}
				this.currentProject = value;
				this.NotifyPropertyChanged("CurrentProject");
				base.NotifyPropertyChanged<string>(() => this.WindowTitle);
				this.SaveProjectCommand.RaiseCanExecuteChanged();
				this.SaveProjectAsCommand.RaiseCanExecuteChanged();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000310B File Offset: 0x0000130B
		private void currentProject_ValueChanged(object sender, EventArgs e)
		{
			base.NotifyPropertyChanged<string>(() => this.WindowTitle);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003147 File Offset: 0x00001347
		private void ShowHelp()
		{
			LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Show Help", null, "ShowHelp", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 151);
			base.Navigation.Navigate("About", new object[0]);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032A4 File Offset: 0x000014A4
		private async Task ShowOptions()
		{
			LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Show Options", null, "ShowOptions", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 157);
			base.Navigation.Navigate("Options", new object[]
			{
				this.currentProject != null
			});
			await Task.FromResult<bool>(true);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032EC File Offset: 0x000014EC
		private void ShowUpgrade()
		{
			LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Show Upgrade", null, "ShowUpgrade", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 164);
			base.Navigation.Navigate("Upgrade", new object[0]);
			base.NotifyPropertyChanged<bool>(() => this.IsInTrialMode);
			this.ActivateProductCommand.RaiseCanExecuteChanged();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003528 File Offset: 0x00001728
		private async Task HandleFile(string filename)
		{
			string extension = Path.GetExtension(filename).TrimStart(new char[]
			{
				'.'
			}).ToLower();
			if (extension == "hyp")
			{
				await this.OpenProject(filename);
			}
			else if (this.supportedVideoTypes.Contains(extension))
			{
				await this.NewProject(filename);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003578 File Offset: 0x00001778
		private bool CanHandleFile(string filename)
		{
			string text = Path.GetExtension(filename).Trim(new char[]
			{
				'.'
			}).ToLower();
			return text == "hyp" || this.supportedVideoTypes.Contains(text);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003964 File Offset: 0x00001B64
		private async Task NewProject(string filename)
		{
			LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "New Project Requested", null, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 192);
			bool canceled = await this.CheckForUnsavedChanges();
			if (!canceled)
			{
				if (string.IsNullOrWhiteSpace(filename))
				{
					string text = this.UserInterface.OpenFile(new string[]
					{
						"Video Files"
					}, new string[][]
					{
						this.supportedVideoTypes
					});
					if (!string.IsNullOrWhiteSpace(text))
					{
						filename = text;
					}
					else
					{
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled file dialog", null, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 212);
					}
				}
				if (!string.IsNullOrWhiteSpace(filename))
				{
					Exception exc = null;
					try
					{
						await this.CloseProject(false);
						this.CurrentProject = this.projectManager.NewProjectFromVideoFile(filename);
						base.Navigation.Navigate("Import", new object[]
						{
							this.CurrentProject
						});
					}
					catch (Exception ex)
					{
						exc = ex;
					}
					if (exc != null)
					{
						await this.UserInterface.ShowMessage("Couldn't open video", "We're sorry but we couldn't open the video file.\nError was: " + exc.Message);
						LoggerExtensionMethods.LogError<WindowOperationsViewModel>(this, "Couldn't open video", new Dictionary<string, object>
						{
							{
								"Exception",
								exc
							}
						}, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 233);
					}
				}
				this.SaveProjectCommand.RaiseCanExecuteChanged();
				this.SaveProjectAsCommand.RaiseCanExecuteChanged();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003DDC File Offset: 0x00001FDC
		private async Task OpenProject(string filename)
		{
			LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Open Project Requested", null, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 244);
			bool canceled = await this.CheckForUnsavedChanges();
			if (!canceled)
			{
				if (string.IsNullOrWhiteSpace(filename))
				{
					string text = this.UserInterface.OpenFile(new string[]
					{
						"Hyperlapse Project Files"
					}, new string[][]
					{
						new string[]
						{
							"hyp"
						}
					});
					if (!string.IsNullOrWhiteSpace(text))
					{
						filename = text;
					}
					else
					{
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled file open dialog", null, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 263);
					}
				}
				if (!string.IsNullOrWhiteSpace(filename))
				{
					Exception exc = null;
					try
					{
						await this.UserInterface.ShowBusyMessage("Opening Project", "Please wait...");
						await this.CloseProject(false);
						this.CurrentProject = this.projectManager.OpenProject(filename);
						base.Navigation.Navigate("Import", new object[]
						{
							this.CurrentProject
						});
					}
					catch (Exception ex)
					{
						exc = ex;
					}
					if (exc != null)
					{
						await this.UserInterface.ShowMessage("Couldn't open project file", "We're sorry but we couldn't open the project file.\nError was: " + exc.Message);
						LoggerExtensionMethods.LogError<WindowOperationsViewModel>(this, "Couldn't open project", new Dictionary<string, object>
						{
							{
								"Exception",
								exc
							}
						}, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 285);
					}
				}
				this.UserInterface.HideBusyMessage();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004204 File Offset: 0x00002404
		private async Task<bool> SaveProject(bool saveAs)
		{
			LoggerExtensionMethods.LogEvent<WindowOperationsViewModel>(this, "Save project", new Dictionary<string, object>
			{
				{
					"SaveAs",
					saveAs
				}
			}, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 294);
			bool result;
			if (this.currentProject == null)
			{
				result = false;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(this.CurrentProject.ProjectFile) || saveAs)
				{
					string initialDirectory = saveAs ? Path.GetDirectoryName(this.CurrentProject.ProjectFile) : string.Empty;
					string text = this.UserInterface.SaveFile(new string[]
					{
						"Hyperlapse Project Files"
					}, new string[][]
					{
						new string[]
						{
							"hyp"
						}
					}, this.currentProject.ProjectName, initialDirectory);
					if (string.IsNullOrWhiteSpace(text))
					{
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User canceled save dialog", null, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 312);
						return true;
					}
					this.CurrentProject.ProjectFile = text;
				}
				Exception exc = null;
				try
				{
					await this.UserInterface.ShowBusyMessage("Saving Project", "Please wait...");
					this.projectManager.SaveProject(this.CurrentProject, this.CurrentProject.ProjectFile);
					base.NotifyPropertyChanged<string>(() => this.WindowTitle);
				}
				catch (Exception ex)
				{
					exc = ex;
				}
				if (exc != null)
				{
					await this.UserInterface.ShowMessage("Couldn't save project file", "We're sorry but we couldn't save the project file.\nError was: " + exc.Message);
					LoggerExtensionMethods.LogError<WindowOperationsViewModel>(this, "Couldn't save project", new Dictionary<string, object>
					{
						{
							"Exception",
							exc
						}
					}, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 332);
				}
				this.UserInterface.HideBusyMessage();
				result = false;
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004252 File Offset: 0x00002452
		public Task<bool> CloseProject()
		{
			return this.CloseProject(true);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000043B0 File Offset: 0x000025B0
		private async Task<bool> CloseProject(bool checkForUnsavedChanges)
		{
			bool result;
			if (!checkForUnsavedChanges || !(await this.CheckForUnsavedChanges()))
			{
				if (this.CurrentProject != null)
				{
					while (base.Navigation.CanGoBack())
					{
						base.Navigation.GoBack();
					}
					LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Closed current project", null, "CloseProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 356);
					this.CurrentProject = null;
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000045E8 File Offset: 0x000027E8
		private async Task<bool> CheckForUnsavedChanges()
		{
			bool cancelled = false;
			if (this.CurrentProject != null && !this.currentProject.IsSaved)
			{
				HyperlapseDialogResult confirm = await this.UserInterface.ShowConfirmMessageWithCancel("Unsaved Changes", "The current project hasn't been saved.\nDo you want to save it?");
				if (confirm == HyperlapseDialogResult.Canceled)
				{
					LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled close project", null, "CheckForUnsavedChanges", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 375);
					cancelled = true;
				}
				else if (confirm == HyperlapseDialogResult.Button1)
				{
					cancelled = await this.SaveProject(false);
				}
			}
			return cancelled;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002CA5 File Offset: 0x00000EA5
		[CompilerGenerated]
		private bool <.ctor>b__0()
		{
			return !this.operationsDisabled;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002CB0 File Offset: 0x00000EB0
		[CompilerGenerated]
		private bool <.ctor>b__1(string s)
		{
			return !this.operationsDisabled;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002CBB File Offset: 0x00000EBB
		[CompilerGenerated]
		private bool <.ctor>b__2(string s)
		{
			return !this.operationsDisabled;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002CC6 File Offset: 0x00000EC6
		[CompilerGenerated]
		private Task <.ctor>b__3()
		{
			return this.SaveProject(false);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002CCF File Offset: 0x00000ECF
		[CompilerGenerated]
		private bool <.ctor>b__4()
		{
			return !this.operationsDisabled && this.currentProject != null;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002CE7 File Offset: 0x00000EE7
		[CompilerGenerated]
		private Task <.ctor>b__5()
		{
			return this.SaveProject(true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002CF0 File Offset: 0x00000EF0
		[CompilerGenerated]
		private bool <.ctor>b__6()
		{
			return !this.operationsDisabled && this.currentProject != null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002D08 File Offset: 0x00000F08
		[CompilerGenerated]
		private Task <.ctor>b__7(string s)
		{
			return this.HandleFile(s);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002D11 File Offset: 0x00000F11
		[CompilerGenerated]
		private bool <.ctor>b__8(string s)
		{
			return this.CanHandleFile(s);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002D1A File Offset: 0x00000F1A
		[CompilerGenerated]
		private bool <.ctor>b__9()
		{
			return this.IsInTrialMode;
		}

		// Token: 0x02000038 RID: 56
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ShowOptions>d__14 : IAsyncStateMachine
		{
			// Token: 0x04000101 RID: 257
			public int <>1__state;

			// Token: 0x04000102 RID: 258
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000103 RID: 259
			public WindowOperationsViewModel <>4__this;

			// Token: 0x04000104 RID: 260
			private TaskAwaiter<bool> <>u__$awaiter15;

			// Token: 0x04000105 RID: 261
			private object <>t__stack;

			// Token: 0x06000266 RID: 614 RVA: 0x0000317C File Offset: 0x0000137C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				try
				{
					int num = num2;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Show Options", null, "ShowOptions", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 157);
						base.Navigation.Navigate("Options", new object[]
						{
							this.currentProject != null
						});
						taskAwaiter = Task.FromResult<bool>(true).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<ShowOptions>d__14>(ref taskAwaiter, ref this);
							return;
						}
					}
					else
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num2 = -1;
					}
					taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000267 RID: 615 RVA: 0x00003294 File Offset: 0x00001494
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x02000039 RID: 57
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleFile>d__17 : IAsyncStateMachine
		{
			// Token: 0x04000106 RID: 262
			public int <>1__state;

			// Token: 0x04000107 RID: 263
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000108 RID: 264
			public WindowOperationsViewModel <>4__this;

			// Token: 0x04000109 RID: 265
			public string filename;

			// Token: 0x0400010A RID: 266
			public string <extension>5__18;

			// Token: 0x0400010B RID: 267
			private TaskAwaiter <>u__$awaiter19;

			// Token: 0x0400010C RID: 268
			private object <>t__stack;

			// Token: 0x06000268 RID: 616 RVA: 0x00003370 File Offset: 0x00001570
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				try
				{
					TaskAwaiter taskAwaiter;
					TaskAwaiter taskAwaiter3;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = -1;
						break;
					}
					case 1:
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter3 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = -1;
						goto IL_142;
					}
					default:
						extension = Path.GetExtension(filename).TrimStart(new char[]
						{
							'.'
						}).ToLower();
						if (extension == "hyp")
						{
							taskAwaiter = this.OpenProject(filename).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = 0;
								TaskAwaiter taskAwaiter2 = taskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<HandleFile>d__17>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							if (!this.supportedVideoTypes.Contains(extension))
							{
								goto IL_151;
							}
							taskAwaiter3 = this.NewProject(filename).GetAwaiter();
							if (!taskAwaiter3.IsCompleted)
							{
								num = 1;
								TaskAwaiter taskAwaiter2 = taskAwaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<HandleFile>d__17>(ref taskAwaiter3, ref this);
								return;
							}
							goto IL_142;
						}
						break;
					}
					taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter);
					goto IL_151;
					IL_142:
					taskAwaiter3.GetResult();
					taskAwaiter3 = default(TaskAwaiter);
					IL_151:;
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000269 RID: 617 RVA: 0x00003518 File Offset: 0x00001718
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x0200003A RID: 58
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <NewProject>d__1c : IAsyncStateMachine
		{
			// Token: 0x0400010D RID: 269
			public int <>1__state;

			// Token: 0x0400010E RID: 270
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400010F RID: 271
			public WindowOperationsViewModel <>4__this;

			// Token: 0x04000110 RID: 272
			public string filename;

			// Token: 0x04000111 RID: 273
			public bool <canceled>5__1d;

			// Token: 0x04000112 RID: 274
			public Exception <exc>5__1e;

			// Token: 0x04000113 RID: 275
			public Dictionary<string, object> <>g__initLocal1b;

			// Token: 0x04000114 RID: 276
			private TaskAwaiter<bool> <>u__$awaiter1f;

			// Token: 0x04000115 RID: 277
			private object <>t__stack;

			// Token: 0x04000116 RID: 278
			private TaskAwaiter <>u__$awaiter20;

			// Token: 0x0600026A RID: 618 RVA: 0x000035C0 File Offset: 0x000017C0
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				try
				{
					TaskAwaiter<bool> taskAwaiter;
					TaskAwaiter taskAwaiter4;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num = -1;
						break;
					}
					case 1:
						Block_8:
						try
						{
							int num2 = num;
							TaskAwaiter<bool> taskAwaiter3;
							if (num2 != 1)
							{
								taskAwaiter3 = this.CloseProject(false).GetAwaiter();
								if (!taskAwaiter3.IsCompleted)
								{
									num = 1;
									TaskAwaiter<bool> taskAwaiter2 = taskAwaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<NewProject>d__1c>(ref taskAwaiter3, ref this);
									return;
								}
							}
							else
							{
								TaskAwaiter<bool> taskAwaiter2;
								taskAwaiter3 = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter<bool>);
								num = -1;
							}
							taskAwaiter3.GetResult();
							taskAwaiter3 = default(TaskAwaiter<bool>);
							this.CurrentProject = this.projectManager.NewProjectFromVideoFile(filename);
							base.Navigation.Navigate("Import", new object[]
							{
								this.CurrentProject
							});
						}
						catch (Exception ex)
						{
							exc = ex;
						}
						if (exc == null)
						{
							goto IL_307;
						}
						taskAwaiter4 = this.UserInterface.ShowMessage("Couldn't open video", "We're sorry but we couldn't open the video file.\nError was: " + exc.Message).GetAwaiter();
						if (!taskAwaiter4.IsCompleted)
						{
							num = 2;
							TaskAwaiter taskAwaiter5 = taskAwaiter4;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<NewProject>d__1c>(ref taskAwaiter4, ref this);
							return;
						}
						goto IL_2B2;
					case 2:
					{
						TaskAwaiter taskAwaiter5;
						taskAwaiter4 = taskAwaiter5;
						taskAwaiter5 = default(TaskAwaiter);
						num = -1;
						goto IL_2B2;
					}
					default:
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "New Project Requested", null, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 192);
						taskAwaiter = this.CheckForUnsavedChanges().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<NewProject>d__1c>(ref taskAwaiter, ref this);
							return;
						}
						break;
					}
					bool result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
					bool flag = result;
					canceled = flag;
					if (canceled)
					{
						goto IL_340;
					}
					if (string.IsNullOrWhiteSpace(filename))
					{
						string text = this.UserInterface.OpenFile(new string[]
						{
							"Video Files"
						}, new string[][]
						{
							this.supportedVideoTypes
						});
						if (!string.IsNullOrWhiteSpace(text))
						{
							filename = text;
						}
						else
						{
							LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled file dialog", null, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 212);
						}
					}
					if (!string.IsNullOrWhiteSpace(filename))
					{
						exc = null;
						goto Block_8;
					}
					goto IL_307;
					IL_2B2:
					taskAwaiter4.GetResult();
					taskAwaiter4 = default(TaskAwaiter);
					WindowOperationsViewModel windowOperationsViewModel = this;
					string text2 = "Couldn't open video";
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("Exception", exc);
					LoggerExtensionMethods.LogError<WindowOperationsViewModel>(windowOperationsViewModel, text2, dictionary, "NewProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 233);
					IL_307:
					this.SaveProjectCommand.RaiseCanExecuteChanged();
					this.SaveProjectAsCommand.RaiseCanExecuteChanged();
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_340:
				num = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00003954 File Offset: 0x00001B54
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x0200003B RID: 59
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OpenProject>d__23 : IAsyncStateMachine
		{
			// Token: 0x04000117 RID: 279
			public int <>1__state;

			// Token: 0x04000118 RID: 280
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000119 RID: 281
			public WindowOperationsViewModel <>4__this;

			// Token: 0x0400011A RID: 282
			public string filename;

			// Token: 0x0400011B RID: 283
			public bool <canceled>5__24;

			// Token: 0x0400011C RID: 284
			public Exception <exc>5__25;

			// Token: 0x0400011D RID: 285
			public Dictionary<string, object> <>g__initLocal22;

			// Token: 0x0400011E RID: 286
			private TaskAwaiter<bool> <>u__$awaiter26;

			// Token: 0x0400011F RID: 287
			private object <>t__stack;

			// Token: 0x04000120 RID: 288
			private TaskAwaiter <>u__$awaiter27;

			// Token: 0x0600026C RID: 620 RVA: 0x000039B4 File Offset: 0x00001BB4
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				try
				{
					TaskAwaiter<bool> taskAwaiter;
					TaskAwaiter taskAwaiter6;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num = -1;
						break;
					}
					case 1:
					case 2:
						Block_8:
						try
						{
							TaskAwaiter taskAwaiter3;
							TaskAwaiter<bool> taskAwaiter5;
							switch (num)
							{
							case 1:
							{
								TaskAwaiter taskAwaiter4;
								taskAwaiter3 = taskAwaiter4;
								taskAwaiter4 = default(TaskAwaiter);
								num = -1;
								break;
							}
							case 2:
							{
								TaskAwaiter<bool> taskAwaiter2;
								taskAwaiter5 = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter<bool>);
								num = -1;
								goto IL_253;
							}
							default:
								taskAwaiter3 = this.UserInterface.ShowBusyMessage("Opening Project", "Please wait...").GetAwaiter();
								if (!taskAwaiter3.IsCompleted)
								{
									num = 1;
									TaskAwaiter taskAwaiter4 = taskAwaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<OpenProject>d__23>(ref taskAwaiter3, ref this);
									return;
								}
								break;
							}
							taskAwaiter3.GetResult();
							taskAwaiter3 = default(TaskAwaiter);
							taskAwaiter5 = this.CloseProject(false).GetAwaiter();
							if (!taskAwaiter5.IsCompleted)
							{
								num = 2;
								TaskAwaiter<bool> taskAwaiter2 = taskAwaiter5;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<OpenProject>d__23>(ref taskAwaiter5, ref this);
								return;
							}
							IL_253:
							taskAwaiter5.GetResult();
							taskAwaiter5 = default(TaskAwaiter<bool>);
							this.CurrentProject = this.projectManager.OpenProject(filename);
							base.Navigation.Navigate("Import", new object[]
							{
								this.CurrentProject
							});
						}
						catch (Exception ex)
						{
							exc = ex;
						}
						if (exc == null)
						{
							goto IL_39B;
						}
						taskAwaiter6 = this.UserInterface.ShowMessage("Couldn't open project file", "We're sorry but we couldn't open the project file.\nError was: " + exc.Message).GetAwaiter();
						if (!taskAwaiter6.IsCompleted)
						{
							num = 3;
							TaskAwaiter taskAwaiter4 = taskAwaiter6;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<OpenProject>d__23>(ref taskAwaiter6, ref this);
							return;
						}
						goto IL_346;
					case 3:
					{
						TaskAwaiter taskAwaiter4;
						taskAwaiter6 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter);
						num = -1;
						goto IL_346;
					}
					default:
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Open Project Requested", null, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 244);
						taskAwaiter = this.CheckForUnsavedChanges().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<OpenProject>d__23>(ref taskAwaiter, ref this);
							return;
						}
						break;
					}
					bool result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
					bool flag = result;
					canceled = flag;
					if (canceled)
					{
						goto IL_3C4;
					}
					if (string.IsNullOrWhiteSpace(filename))
					{
						string text = this.UserInterface.OpenFile(new string[]
						{
							"Hyperlapse Project Files"
						}, new string[][]
						{
							new string[]
							{
								"hyp"
							}
						});
						if (!string.IsNullOrWhiteSpace(text))
						{
							filename = text;
						}
						else
						{
							LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled file open dialog", null, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 263);
						}
					}
					if (!string.IsNullOrWhiteSpace(filename))
					{
						exc = null;
						goto Block_8;
					}
					goto IL_39B;
					IL_346:
					taskAwaiter6.GetResult();
					taskAwaiter6 = default(TaskAwaiter);
					WindowOperationsViewModel windowOperationsViewModel = this;
					string text2 = "Couldn't open project";
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("Exception", exc);
					LoggerExtensionMethods.LogError<WindowOperationsViewModel>(windowOperationsViewModel, text2, dictionary, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 285);
					IL_39B:
					this.UserInterface.HideBusyMessage();
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3C4:
				num = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00003DCC File Offset: 0x00001FCC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x0200003C RID: 60
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SaveProject>d__2b : IAsyncStateMachine
		{
			// Token: 0x04000121 RID: 289
			public int <>1__state;

			// Token: 0x04000122 RID: 290
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000123 RID: 291
			public WindowOperationsViewModel <>4__this;

			// Token: 0x04000124 RID: 292
			public bool saveAs;

			// Token: 0x04000125 RID: 293
			public Exception <exc>5__2c;

			// Token: 0x04000126 RID: 294
			public Dictionary<string, object> <>g__initLocal2a;

			// Token: 0x04000127 RID: 295
			public Dictionary<string, object> <>g__initLocal29;

			// Token: 0x04000128 RID: 296
			private TaskAwaiter <>u__$awaiter2d;

			// Token: 0x04000129 RID: 297
			private object <>t__stack;

			// Token: 0x0600026E RID: 622 RVA: 0x00003E2C File Offset: 0x0000202C
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				bool result;
				try
				{
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter;
					switch (num)
					{
					case 0:
						break;
					case 1:
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = -1;
						goto IL_2EC;
					default:
					{
						WindowOperationsViewModel windowOperationsViewModel = this;
						string text = "Save project";
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						dictionary.Add("SaveAs", saveAs);
						LoggerExtensionMethods.LogEvent<WindowOperationsViewModel>(windowOperationsViewModel, text, dictionary, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 294);
						if (this.currentProject == null)
						{
							result = false;
							goto IL_36F;
						}
						if (string.IsNullOrWhiteSpace(this.CurrentProject.ProjectFile) || saveAs)
						{
							string initialDirectory = saveAs ? Path.GetDirectoryName(this.CurrentProject.ProjectFile) : string.Empty;
							string text2 = this.UserInterface.SaveFile(new string[]
							{
								"Hyperlapse Project Files"
							}, new string[][]
							{
								new string[]
								{
									"hyp"
								}
							}, this.currentProject.ProjectName, initialDirectory);
							if (string.IsNullOrWhiteSpace(text2))
							{
								LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User canceled save dialog", null, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 312);
								result = true;
								goto IL_36F;
							}
							this.CurrentProject.ProjectFile = text2;
						}
						exc = null;
						break;
					}
					}
					try
					{
						int num2 = num;
						TaskAwaiter taskAwaiter3;
						if (num2 != 0)
						{
							taskAwaiter3 = this.UserInterface.ShowBusyMessage("Saving Project", "Please wait...").GetAwaiter();
							if (!taskAwaiter3.IsCompleted)
							{
								num = 0;
								taskAwaiter2 = taskAwaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<SaveProject>d__2b>(ref taskAwaiter3, ref this);
								return;
							}
						}
						else
						{
							taskAwaiter3 = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							num = -1;
						}
						taskAwaiter3.GetResult();
						taskAwaiter3 = default(TaskAwaiter);
						this.projectManager.SaveProject(this.CurrentProject, this.CurrentProject.ProjectFile);
						base.NotifyPropertyChanged<string>(() => this.WindowTitle);
					}
					catch (Exception ex)
					{
						exc = ex;
					}
					if (exc == null)
					{
						goto IL_341;
					}
					taskAwaiter = this.UserInterface.ShowMessage("Couldn't save project file", "We're sorry but we couldn't save the project file.\nError was: " + exc.Message).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num = 1;
						taskAwaiter2 = taskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WindowOperationsViewModel.<SaveProject>d__2b>(ref taskAwaiter, ref this);
						return;
					}
					IL_2EC:
					taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter);
					WindowOperationsViewModel windowOperationsViewModel2 = this;
					string text3 = "Couldn't save project";
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2.Add("Exception", exc);
					LoggerExtensionMethods.LogError<WindowOperationsViewModel>(windowOperationsViewModel2, text3, dictionary2, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 332);
					IL_341:
					this.UserInterface.HideBusyMessage();
					result = false;
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_36F:
				num = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600026F RID: 623 RVA: 0x000041F4 File Offset: 0x000023F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x0200003D RID: 61
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CloseProject>d__2f : IAsyncStateMachine
		{
			// Token: 0x0400012A RID: 298
			public int <>1__state;

			// Token: 0x0400012B RID: 299
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400012C RID: 300
			public WindowOperationsViewModel <>4__this;

			// Token: 0x0400012D RID: 301
			public bool checkForUnsavedChanges;

			// Token: 0x0400012E RID: 302
			public bool <cancelled>5__30;

			// Token: 0x0400012F RID: 303
			private TaskAwaiter<bool> <>u__$awaiter31;

			// Token: 0x04000130 RID: 304
			private object <>t__stack;

			// Token: 0x06000270 RID: 624 RVA: 0x0000425C File Offset: 0x0000245C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				bool result;
				try
				{
					int num = num2;
					bool flag;
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						if (!checkForUnsavedChanges)
						{
							flag = false;
							goto IL_85;
						}
						taskAwaiter = this.CheckForUnsavedChanges().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<CloseProject>d__2f>(ref taskAwaiter, ref this);
							return;
						}
					}
					else
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num2 = -1;
					}
					flag = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<bool>);
					IL_85:
					bool flag2 = flag;
					bool cancelled = flag2;
					if (!cancelled)
					{
						if (this.CurrentProject != null)
						{
							while (base.Navigation.CanGoBack())
							{
								base.Navigation.GoBack();
							}
							LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "Closed current project", null, "CloseProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 356);
							this.CurrentProject = null;
						}
						result = false;
					}
					else
					{
						result = true;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000271 RID: 625 RVA: 0x000043A0 File Offset: 0x000025A0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}

		// Token: 0x0200003E RID: 62
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CheckForUnsavedChanges>d__33 : IAsyncStateMachine
		{
			// Token: 0x04000131 RID: 305
			public int <>1__state;

			// Token: 0x04000132 RID: 306
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000133 RID: 307
			public WindowOperationsViewModel <>4__this;

			// Token: 0x04000134 RID: 308
			public bool <cancelled>5__34;

			// Token: 0x04000135 RID: 309
			public HyperlapseDialogResult <confirm>5__35;

			// Token: 0x04000136 RID: 310
			private TaskAwaiter<HyperlapseDialogResult> <>u__$awaiter36;

			// Token: 0x04000137 RID: 311
			private object <>t__stack;

			// Token: 0x04000138 RID: 312
			private TaskAwaiter<bool> <>u__$awaiter37;

			// Token: 0x06000272 RID: 626 RVA: 0x00004400 File Offset: 0x00002600
			void IAsyncStateMachine.MoveNext()
			{
				int num;
				bool result3;
				try
				{
					TaskAwaiter<HyperlapseDialogResult> taskAwaiter;
					TaskAwaiter<bool> taskAwaiter3;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<HyperlapseDialogResult> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<HyperlapseDialogResult>);
						num = -1;
						break;
					}
					case 1:
					{
						TaskAwaiter<bool> taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter<bool>);
						num = -1;
						goto IL_15F;
					}
					default:
						cancelled = false;
						if (this.CurrentProject == null || this.currentProject.IsSaved)
						{
							goto IL_178;
						}
						taskAwaiter = this.UserInterface.ShowConfirmMessageWithCancel("Unsaved Changes", "The current project hasn't been saved.\nDo you want to save it?").GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = 0;
							TaskAwaiter<HyperlapseDialogResult> taskAwaiter2 = taskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<HyperlapseDialogResult>, WindowOperationsViewModel.<CheckForUnsavedChanges>d__33>(ref taskAwaiter, ref this);
							return;
						}
						break;
					}
					HyperlapseDialogResult result = taskAwaiter.GetResult();
					taskAwaiter = default(TaskAwaiter<HyperlapseDialogResult>);
					HyperlapseDialogResult hyperlapseDialogResult = result;
					confirm = hyperlapseDialogResult;
					if (confirm == HyperlapseDialogResult.Canceled)
					{
						LoggerExtensionMethods.LogDiagnostic<WindowOperationsViewModel>(this, "User cancelled close project", null, "CheckForUnsavedChanges", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\ViewModels\\WindowOperationsViewModel.cs", 375);
						cancelled = true;
						goto IL_178;
					}
					if (confirm != HyperlapseDialogResult.Button1)
					{
						goto IL_178;
					}
					taskAwaiter3 = this.SaveProject(false).GetAwaiter();
					if (!taskAwaiter3.IsCompleted)
					{
						num = 1;
						TaskAwaiter<bool> taskAwaiter4 = taskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, WindowOperationsViewModel.<CheckForUnsavedChanges>d__33>(ref taskAwaiter3, ref this);
						return;
					}
					IL_15F:
					bool result2 = taskAwaiter3.GetResult();
					taskAwaiter3 = default(TaskAwaiter<bool>);
					bool flag = result2;
					cancelled = flag;
					IL_178:
					result3 = cancelled;
				}
				catch (Exception exception)
				{
					num = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num = -2;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x06000273 RID: 627 RVA: 0x000045D8 File Offset: 0x000027D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
