using System;
using System.Threading.Tasks;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200000D RID: 13
	public interface IHyperlapseUserInterface : IUserInterface
	{
		// Token: 0x060000A9 RID: 169
		string OpenFile(string[] filterNames, string[][] filterExtensions);

		// Token: 0x060000AA RID: 170
		string SaveFile(string[] filterNames, string[][] filterExtensions, string fileName, string initialDirectory);

		// Token: 0x060000AB RID: 171
		string ChooseDirectory(string initialDirectory);

		// Token: 0x060000AC RID: 172
		Task ShowBusyMessage(string title, string message);

		// Token: 0x060000AD RID: 173
		void HideBusyMessage();

		// Token: 0x060000AE RID: 174
		Task<HyperlapseDialogResult> ShowConfirmMessageWithCancel(string title, string message);

		// Token: 0x060000AF RID: 175
		void CloseApplication();
	}
}
