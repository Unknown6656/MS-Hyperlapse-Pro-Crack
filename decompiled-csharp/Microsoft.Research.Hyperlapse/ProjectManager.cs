using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000025 RID: 37
	public class ProjectManager
	{
		// Token: 0x040000B9 RID: 185
		private readonly int LatestProjectVersion = 1;

		// Token: 0x040000BA RID: 186
		private readonly string ProjectVersionFile = "HyperlapseVersion.dat";

		// Token: 0x040000BB RID: 187
		private readonly string ProjectInfoFile = "ProjectInfo.dat";

		// Token: 0x040000BC RID: 188
		private readonly string[] UnneededProjectFiles = new string[]
		{
			"cameras_input.dat",
			"points_input.dat",
			"cameras_optimized.dat",
			"point_optimized.dat"
		};

		// Token: 0x040000BD RID: 189
		private IVideoReader videoReader;

		// Token: 0x040000BE RID: 190
		private CalibrationMatcher calibrationMatcher;

		// Token: 0x040000BF RID: 191
		private ScratchManager scratchManager;

		// Token: 0x040000C0 RID: 192
		private VideoFormatTester videoFormatTester;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		private static Func<Size, bool> CS$<>9__CachedAnonymousMethodDelegate2;

		// Token: 0x060001C1 RID: 449 RVA: 0x00007E28 File Offset: 0x00006028
		public ProjectManager(IVideoReader videoReader, CalibrationMatcher calibrationMatcher, ScratchManager scratchManager, VideoFormatTester videoFormatTester)
		{
			if (videoReader == null)
			{
				throw new ArgumentNullException("videoReader");
			}
			this.videoReader = videoReader;
			if (calibrationMatcher == null)
			{
				throw new ArgumentNullException("calibrationMatcher");
			}
			this.calibrationMatcher = calibrationMatcher;
			if (scratchManager == null)
			{
				throw new ArgumentNullException("scratchManager");
			}
			this.scratchManager = scratchManager;
			if (videoFormatTester == null)
			{
				throw new ArgumentNullException("videoFormatTester");
			}
			this.videoFormatTester = videoFormatTester;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007EDC File Offset: 0x000060DC
		public List<Project> GetRecentProjects()
		{
			return new List<Project>();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007EF8 File Offset: 0x000060F8
		public Project NewProjectFromVideoFile(string filename)
		{
			LoggerExtensionMethods.LogDiagnostic<ProjectManager>(this, "New Project", new Dictionary<string, object>
			{
				{
					"FileExtension",
					Path.GetExtension(filename)
				}
			}, "NewProjectFromVideoFile", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Projects\\ProjectManager.cs", 70);
			VideoInfo videoInfo = this.videoReader.ReadInfoFromFile(filename);
			CalibrationInfo calibrationInfo = this.calibrationMatcher.FindCalibrationInfoForVideo(videoInfo);
			Project project = new Project(this.videoFormatTester);
			project.VideoInfo = videoInfo;
			project.CalibrationInfo = calibrationInfo;
			project.VideoRotationAmount = (double)project.VideoInfo.Rotation;
			project.EndTime = project.VideoInfo.Duration;
			project.SpeedupFactor = 8;
			project.WorkingDirectory = this.scratchManager.GetNewWorkingDirectory();
			project.OutputFramesPerSecond = new Rational(30000, 1001);
			List<Size> availableOutputSizes = project.GetAvailableOutputSizes();
			if (!availableOutputSizes.Any((Size s) => s.Width > 3000.0))
			{
				project.OutputFramesPerSecond = new Rational(60000, 1001);
				availableOutputSizes = project.GetAvailableOutputSizes();
			}
			project.OutputSize = this.videoFormatTester.GetDefaultOutputSize(availableOutputSizes, project.VideoInfo.Width, project.VideoInfo.Height);
			project.UseAdvancedSmoothing = true;
			project.IsSaved = false;
			return project;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000803C File Offset: 0x0000623C
		public Project OpenProject(string filename)
		{
			LoggerExtensionMethods.LogDiagnostic<ProjectManager>(this, "Open Project", null, "OpenProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Projects\\ProjectManager.cs", 103);
			if (!File.Exists(filename))
			{
				throw new Exception("Couldn't find project file");
			}
			string newWorkingDirectory = this.scratchManager.GetNewWorkingDirectory();
			Directory.CreateDirectory(newWorkingDirectory);
			try
			{
				ZipFile.ExtractToDirectory(filename, newWorkingDirectory);
			}
			catch (Exception innerException)
			{
				throw new Exception("Couldn't open project file", innerException);
			}
			string path = Path.Combine(newWorkingDirectory, this.ProjectVersionFile);
			if (!File.Exists(path))
			{
				throw new Exception("Couldn't read version");
			}
			File.ReadAllText(path);
			string path2 = Path.Combine(newWorkingDirectory, this.ProjectInfoFile);
			if (!File.Exists(path2))
			{
				throw new Exception("Couldn't find ProjectInfo.dat");
			}
			ProjectInfo projectInfo = null;
			using (FileStream fileStream = File.OpenRead(path2))
			{
				using (XmlReader xmlReader = XmlReader.Create(fileStream, new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Prohibit
				}))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectInfo));
					projectInfo = (ProjectInfo)xmlSerializer.Deserialize(xmlReader);
				}
			}
			if (projectInfo.OutputFramesPerSecondAsRational == null)
			{
				int num = (int)projectInfo.OutputFramesPerSecond;
				if (num == 24 || num == 30 || num == 60)
				{
					projectInfo.OutputFramesPerSecondAsRational = new Rational(1000 * num, 1001);
				}
				else
				{
					projectInfo.OutputFramesPerSecondAsRational = new Rational(num, 1);
				}
			}
			else
			{
				projectInfo.OutputFramesPerSecond = Math.Ceiling(projectInfo.OutputFramesPerSecondAsRational.AsDouble());
			}
			if (!File.Exists(projectInfo.InputFileName))
			{
				throw new Exception("Couldn't find input file " + projectInfo.InputFileName);
			}
			VideoInfo videoInfo = this.videoReader.ReadInfoFromFile(projectInfo.InputFileName);
			bool flag = false;
			if (videoInfo.VideoMode != projectInfo.VideoMode || videoInfo.CameraModel != projectInfo.CalibrationId)
			{
				videoInfo.VideoMode = projectInfo.VideoMode;
				videoInfo.CameraModel = projectInfo.CalibrationId;
				flag = true;
			}
			CalibrationInfo calibrationInfo = this.calibrationMatcher.FindCalibrationInfoForVideo(videoInfo);
			Project project = new Project(this.videoFormatTester);
			project.VideoInfo = videoInfo;
			project.CalibrationInfo = calibrationInfo;
			project.CalibrationInfo.Calibration.HousingOn = projectInfo.CalibrationHousingOn;
			project.CalibrationInfo.WasAutoSelected = (project.CalibrationInfo.WasAutoSelected && !flag);
			project.CreditDisabled = projectInfo.CreditDisabled;
			project.EndTime = TimeSpan.FromTicks(projectInfo.EndTimeTicks);
			project.OutputFile = projectInfo.OutputFileName;
			project.OutputFramesPerSecond = projectInfo.OutputFramesPerSecondAsRational;
			project.OutputSize = projectInfo.OutputSize;
			project.VideoRotationAmount = projectInfo.RotationAmount;
			project.SelectedFrameTime = TimeSpan.FromTicks(projectInfo.SelectedFrameTimeTicks);
			project.SpeedupFactor = projectInfo.SpeedupFactor;
			project.StartTime = TimeSpan.FromTicks(projectInfo.StartTimeTicks);
			project.UseAdvancedSmoothing = projectInfo.UseAdvancedSmoothing;
			project.WorkingDirectory = newWorkingDirectory;
			project.ProjectFile = filename;
			project.IsSaved = true;
			return project;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008374 File Offset: 0x00006574
		public void SaveProject(Project project, string filename)
		{
			LoggerExtensionMethods.LogDiagnostic<ProjectManager>(this, "Save Project", null, "SaveProject", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Projects\\ProjectManager.cs", 215);
			if (!Directory.Exists(project.WorkingDirectory))
			{
				Directory.CreateDirectory(project.WorkingDirectory);
			}
			using (StreamWriter streamWriter = File.CreateText(Path.Combine(project.WorkingDirectory, this.ProjectVersionFile)))
			{
				streamWriter.Write(this.LatestProjectVersion);
			}
			ProjectInfo projectInfo = new ProjectInfo();
			projectInfo.CalibrationId = project.CalibrationInfo.Calibration.ID;
			projectInfo.CalibrationHousingOn = project.CalibrationInfo.Calibration.HousingOn;
			projectInfo.CreditDisabled = project.CreditDisabled;
			projectInfo.EndTimeTicks = project.EndTime.Ticks;
			projectInfo.InputFileName = project.VideoInfo.Filename.LocalPath;
			projectInfo.OutputFileName = project.OutputFile;
			projectInfo.OutputFramesPerSecondAsRational = project.OutputFramesPerSecond;
			projectInfo.OutputFramesPerSecond = Math.Ceiling(project.OutputFramesPerSecond.AsDouble());
			projectInfo.OutputSize = project.OutputSize;
			projectInfo.RotationAmount = project.VideoRotationAmount;
			projectInfo.SelectedFrameTimeTicks = project.SelectedFrameTime.Ticks;
			projectInfo.SpeedupFactor = project.SpeedupFactor;
			projectInfo.StartTimeTicks = project.StartTime.Ticks;
			projectInfo.UseAdvancedSmoothing = project.UseAdvancedSmoothing;
			projectInfo.VideoMode = project.CalibrationInfo.VideoMode;
			using (StreamWriter streamWriter2 = File.CreateText(Path.Combine(project.WorkingDirectory, this.ProjectInfoFile)))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectInfo));
				xmlSerializer.Serialize(streamWriter2, projectInfo);
			}
			if (File.Exists(filename))
			{
				File.Delete(filename);
			}
			foreach (string path in this.UnneededProjectFiles)
			{
				File.Delete(Path.Combine(project.WorkingDirectory, path));
			}
			string[] files = Directory.GetFiles(project.WorkingDirectory, "*.txt");
			foreach (string path2 in files)
			{
				File.Delete(path2);
			}
			ZipFile.CreateFromDirectory(project.WorkingDirectory, filename);
			project.ProjectFile = filename;
			project.IsSaved = true;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007EE3 File Offset: 0x000060E3
		[CompilerGenerated]
		private static bool <NewProjectFromVideoFile>b__1(Size s)
		{
			return s.Width > 3000.0;
		}
	}
}
