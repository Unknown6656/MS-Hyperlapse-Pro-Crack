using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000007 RID: 7
	public class ResourceCalibration : Calibration
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C01 File Offset: 0x00000E01
		public ResourceCalibration(int id, string description, List<string> videoModes, string resourceName, string bareFile) : base(id, description, videoModes, resourceName, string.IsNullOrWhiteSpace(bareFile) ? string.Empty : (Calibration.ResourcePrefix + bareFile))
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C2C File Offset: 0x00000E2C
		public override string ExtractToFolder(string folder)
		{
			string text = base.ExtractToFolder(folder);
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(base.Location))
			{
				using (FileStream fileStream = File.Create(text))
				{
					manifestResourceStream.CopyTo(fileStream);
				}
			}
			return text;
		}
	}
}
