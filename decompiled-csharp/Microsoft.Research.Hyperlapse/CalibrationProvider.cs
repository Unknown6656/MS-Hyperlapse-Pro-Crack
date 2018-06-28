using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000003 RID: 3
	public class CalibrationProvider
	{
		// Token: 0x04000004 RID: 4
		public readonly string CalibrationFileDirectory = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "calibrations");

		// Token: 0x04000005 RID: 5
		private static List<Calibration> calibrationFiles;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private static Func<Calibration, string> CS$<>9__CachedAnonymousMethodDelegate1;

		// Token: 0x06000008 RID: 8 RVA: 0x000020A5 File Offset: 0x000002A5
		public List<Calibration> GetCalibrations()
		{
			if (CalibrationProvider.calibrationFiles == null)
			{
				this.PopulateCalibrationFiles();
			}
			return CalibrationProvider.calibrationFiles;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020C4 File Offset: 0x000002C4
		private void PopulateCalibrationFiles()
		{
			CalibrationProvider.calibrationFiles = new List<Calibration>();
			this.GetCalibrationsFromResources();
			this.GetCalibrationsFromFiles();
			CalibrationProvider.calibrationFiles = (from c in CalibrationProvider.calibrationFiles
			orderby c.Description
			select c).ToList<Calibration>();
			CalibrationProvider.calibrationFiles.Add(new UnknownCalibration());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002128 File Offset: 0x00000328
		private void GetCalibrationsFromFiles()
		{
			if (!Directory.Exists(this.CalibrationFileDirectory))
			{
				return;
			}
			IEnumerable<string> enumerable = Directory.EnumerateFiles(this.CalibrationFileDirectory, "*.txt");
			foreach (string text in enumerable)
			{
				using (StreamReader streamReader = File.OpenText(text))
				{
					this.TryAddCalibrationFile<FileCalibration>(streamReader, text);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B0 File Offset: 0x000003B0
		private void GetCalibrationsFromResources()
		{
			string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			foreach (string text in manifestResourceNames)
			{
				if (text.Contains(Calibration.ResourcePrefix))
				{
					Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text);
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						this.TryAddCalibrationFile<ResourceCalibration>(streamReader, text);
					}
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		private void TryAddCalibrationFile<T>(StreamReader reader, string location) where T : Calibration
		{
			string text = "";
			int num = 0;
			List<string> list = new List<string>();
			string bareFile = "";
			bool flag = false;
			try
			{
				char[] separator = new char[]
				{
					' '
				};
				while (!reader.EndOfStream)
				{
					string text2 = reader.ReadLine();
					if (text2.ToLower().StartsWith("description "))
					{
						text = text2.Substring(12);
					}
					else if (text2.ToLower().StartsWith("cameraid "))
					{
						num = int.Parse(text2.Substring(9));
					}
					else if (text2.ToLower().StartsWith("res"))
					{
						string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length >= 8)
						{
							string item = array[7];
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
					else if (text2.ToLower().StartsWith("child"))
					{
						flag = true;
					}
					else if (text2.ToLower().StartsWith("barefile "))
					{
						bareFile = text2.Substring(9);
					}
				}
				if (!string.IsNullOrWhiteSpace(text) && num != 0 && list.Count > 0 && !flag)
				{
					if (typeof(T) == typeof(ResourceCalibration))
					{
						CalibrationProvider.calibrationFiles.Add(new ResourceCalibration(num, text, list, location, bareFile));
					}
					else if (typeof(T) == typeof(FileCalibration))
					{
						CalibrationProvider.calibrationFiles.Add(new FileCalibration(num, text, list, location, bareFile));
					}
				}
				else if (!flag)
				{
					LoggerExtensionMethods.LogDiagnostic<CalibrationProvider>(this, string.Format("File {0} is not a valid calibration file. VideoModeCount = {1}, Type = {2}", location, list.Count, typeof(T)), null, "TryAddCalibrationFile", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Calibrations\\CalibrationProvider.cs", 144);
				}
			}
			catch (Exception arg)
			{
				LoggerExtensionMethods.LogError<CalibrationProvider>(this, string.Format("Error while attempting to add calibration file. Location = {0}, Type = {1}, Exception = {2}", location, typeof(T), arg), null, "TryAddCalibrationFile", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Calibrations\\CalibrationProvider.cs", 149);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002434 File Offset: 0x00000634
		public CalibrationProvider()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020B9 File Offset: 0x000002B9
		[CompilerGenerated]
		private static string <PopulateCalibrationFiles>b__0(Calibration c)
		{
			return c.Description;
		}
	}
}
