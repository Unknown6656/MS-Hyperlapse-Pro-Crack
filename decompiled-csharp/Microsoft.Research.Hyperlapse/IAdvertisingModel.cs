using System;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000015 RID: 21
	public interface IAdvertisingModel
	{
		// Token: 0x060000EA RID: 234
		Task DownloadLatestAds();

		// Token: 0x060000EB RID: 235
		bool AdClicked(AdvertisementViewModel adViewModel);

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EC RID: 236
		object AdvertisingViewModel { get; }
	}
}
