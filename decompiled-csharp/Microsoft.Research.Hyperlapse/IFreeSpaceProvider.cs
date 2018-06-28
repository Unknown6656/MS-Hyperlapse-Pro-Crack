using System;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000009 RID: 9
	public interface IFreeSpaceProvider
	{
		// Token: 0x0600005A RID: 90
		Tuple<ulong, ulong> GetFreeSpaceForPath(string path);
	}
}
