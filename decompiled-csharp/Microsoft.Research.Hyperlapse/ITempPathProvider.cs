using System;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000016 RID: 22
	public interface ITempPathProvider
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000ED RID: 237
		string TempPath { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EE RID: 238
		string TempPathEnvironmentVariable { get; }

		// Token: 0x060000EF RID: 239
		string RestoreEnvironmentVariables(string path);

		// Token: 0x060000F0 RID: 240
		string ExpandEnvironmentVariables(string path);
	}
}
