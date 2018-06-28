using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000034 RID: 52
	public class ActivationManager : IDisposable
	{
		// Token: 0x040000EC RID: 236
		private const string DllName = "Hyperlapse.Native.dll";

		// Token: 0x040000ED RID: 237
		private int instanceId;

		// Token: 0x040000EE RID: 238
		private bool disposed;

		// Token: 0x040000EF RID: 239
		private FirstRunExperience firstRunExperience;

		// Token: 0x040000F0 RID: 240
		private ProductInfo productInfo;

		// Token: 0x06000251 RID: 593 RVA: 0x0000AD24 File Offset: 0x00008F24
		public ActivationManager(FirstRunExperience firstRunExperience, ProductInfo productInfo)
		{
			if (firstRunExperience == null)
			{
				throw new ArgumentNullException("firstRunExperience");
			}
			this.firstRunExperience = firstRunExperience;
			if (productInfo == null)
			{
				throw new ArgumentNullException("productInfo");
			}
			this.productInfo = productInfo;
			this.instanceId = ActivationManager.ProductKeyValidator_instance_new();
			ActivationManager.ProductKeyValidator_Initialize(this.instanceId, productInfo.ActivationConfigFile, productInfo.ActivationRegistrationFile);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AD84 File Offset: 0x00008F84
		public bool Activate(string key)
		{
			try
			{
				if (!Directory.Exists(this.productInfo.ActivationRegistrationFolder))
				{
					Directory.CreateDirectory(this.productInfo.ActivationRegistrationFolder);
				}
				if (Directory.Exists(this.productInfo.ActivationRegistrationFile))
				{
					Directory.Delete(this.productInfo.ActivationRegistrationFile);
					LoggerExtensionMethods.LogWarning<ActivationManager>(this, "Registration file was directory", null, "Activate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Activation\\ActivationManager.cs", 57);
				}
			}
			catch (Exception value)
			{
				LoggerExtensionMethods.LogError<ActivationManager>(this, "Couldn't create directory for registration file", new Dictionary<string, object>
				{
					{
						"Exception",
						value
					}
				}, "Activate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Activation\\ActivationManager.cs", 62);
				return false;
			}
			int num = 0;
			bool flag = ActivationManager.ProductKeyValidator_Activate(this.instanceId, key, out num);
			if (flag)
			{
				string value2;
				string value3;
				string value4;
				this.GetActivationInfo(out value2, out value3, out value4);
				LoggerExtensionMethods.LogEvent<ActivationManager>(this, "Activated Product", new Dictionary<string, object>
				{
					{
						"ProductKey",
						value2
					},
					{
						"Pid2",
						value4
					},
					{
						"MPC",
						value3
					},
					{
						"FirstRun",
						this.firstRunExperience.FirstRunTime.ToString()
					},
					{
						"DaysToActivation",
						DateTime.Now.Subtract(this.firstRunExperience.FirstRunTime).TotalDays
					},
					{
						"VideosProcessedToActivation",
						(double)this.firstRunExperience.VideosProcessed
					}
				}, "Activate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Activation\\ActivationManager.cs", 77);
			}
			else
			{
				LoggerExtensionMethods.LogEvent<ActivationManager>(this, "Couldn't activate product", new Dictionary<string, object>
				{
					{
						"Step",
						num
					}
				}, "Activate", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Activation\\ActivationManager.cs", 81);
			}
			return flag;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000AF50 File Offset: 0x00009150
		public bool GetActivationStatus()
		{
			string text;
			string text2;
			string text3;
			return this.GetActivationInfo(out text, out text2, out text3);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AF6C File Offset: 0x0000916C
		public string GetProductId()
		{
			string text;
			string text2;
			string result;
			this.GetActivationInfo(out text, out text2, out result);
			return result;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AF88 File Offset: 0x00009188
		public string GetProductKey()
		{
			string result;
			string text;
			string text2;
			this.GetActivationInfo(out result, out text, out text2);
			return result;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public bool ValidateKey(string key)
		{
			int num;
			return ActivationManager.ProductKeyValidator_ValidateKey(this.instanceId, key, out num);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public int GetValidationError()
		{
			int result;
			ActivationManager.ProductKeyValidator_ValidateKey(this.instanceId, string.Empty, out result);
			return result;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000AFE1 File Offset: 0x000091E1
		public void Dispose()
		{
			if (!this.disposed)
			{
				ActivationManager.ProductKeyValidator_instance_delete(this.instanceId);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000AFF8 File Offset: 0x000091F8
		private bool GetActivationInfo(out string key, out string mpc, out string pid)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty.PadLeft(32));
			StringBuilder stringBuilder2 = new StringBuilder(string.Empty.PadLeft(24));
			StringBuilder stringBuilder3 = new StringBuilder(string.Empty.PadLeft(16));
			bool result = ActivationManager.ProductKeyValidator_GetActivationStatus(this.instanceId, stringBuilder, stringBuilder2, stringBuilder3);
			key = stringBuilder.ToString();
			pid = stringBuilder2.ToString();
			mpc = stringBuilder3.ToString();
			return result;
		}

		// Token: 0x0600025A RID: 602
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern bool ProductKeyValidator_GetActivationStatus(int instance, StringBuilder outKey, StringBuilder outPid, StringBuilder outMPC);

		// Token: 0x0600025B RID: 603
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern bool ProductKeyValidator_Activate(int instance, string key, out int outStep);

		// Token: 0x0600025C RID: 604
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern bool ProductKeyValidator_ValidateKey(int instance, string key, out int error);

		// Token: 0x0600025D RID: 605
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern void ProductKeyValidator_Initialize(int instance, string configFile, string registrationFile);

		// Token: 0x0600025E RID: 606
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern int ProductKeyValidator_instance_new();

		// Token: 0x0600025F RID: 607
		[DllImport("Hyperlapse.Native.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void ProductKeyValidator_instance_delete(int instance);
	}
}
