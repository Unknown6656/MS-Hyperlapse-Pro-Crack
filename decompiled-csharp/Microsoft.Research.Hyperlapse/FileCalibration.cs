using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000032 RID: 50
	public class FileCalibration : Calibration
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		public FileCalibration(int id, string description, List<string> videoModes, string location, string bareFile) : base(id, description, videoModes, location, bareFile)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public override string ExtractToFolder(string folder)
		{
			string text = base.ExtractToFolder(folder);
			File.Copy(base.Location, text);
			return text;
		}
	}
}
