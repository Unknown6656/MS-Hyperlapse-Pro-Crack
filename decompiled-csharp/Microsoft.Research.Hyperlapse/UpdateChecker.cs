using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001C RID: 28
	public class UpdateChecker
	{
		// Token: 0x040000A0 RID: 160
		private readonly string VersionLink = "http://go.microsoft.com/fwlink/?LinkId=797864";

		// Token: 0x040000A1 RID: 161
		private readonly string IsUpdateCheckEnabledSettingsKey = "IsUpdateCheckEnabled";

		// Token: 0x040000A2 RID: 162
		private WebClient webClient;

		// Token: 0x040000A3 RID: 163
		private ISettingsStore settings;

		// Token: 0x040000A4 RID: 164
		private ProductInfo productInfo;

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007222 File Offset: 0x00005422
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007236 File Offset: 0x00005436
		public bool IsUpdateCheckEnabled
		{
			get
			{
				return this.settings.ReadSetting<bool>(this.IsUpdateCheckEnabledSettingsKey, true);
			}
			set
			{
				this.settings.WriteSetting<bool>(this.IsUpdateCheckEnabledSettingsKey, value);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000724A File Offset: 0x0000544A
		public UpdateChecker(ISettingsStore settings, ProductInfo productInfo)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;
			this.productInfo = productInfo;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000757C File Offset: 0x0000577C
		public async Task<string> CheckForUpgradeAsync(bool overrideDisabledCheck = false)
		{
			string result;
			if (!overrideDisabledCheck && !this.IsUpdateCheckEnabled)
			{
				result = null;
			}
			else
			{
				string installerLink = null;
				try
				{
					Version devVersion = Version.Parse("0.0.0.0");
					Version currentVersion = Assembly.GetEntryAssembly().GetName().Version;
					if (!(currentVersion == devVersion) && NetworkInterface.GetIsNetworkAvailable())
					{
						this.webClient = new WebClient();
						this.webClient.Headers[HttpRequestHeader.CacheControl] = "no-cache";
						string text = await this.webClient.DownloadStringTaskAsync(this.VersionLink);
						XmlDocument xmlDoc = this.LoadAsXml(text);
						XmlNode productNode = xmlDoc.SelectSingleNode(string.Format("/AppInfo/LatestVersion[@Edition='{0}' and @Platform = '{1}']", this.productInfo.Edition, this.productInfo.Platform));
						XmlNode versionNode = (productNode != null) ? productNode.SelectSingleNode("VersionNumber") : null;
						XmlNode linkNode = (productNode != null) ? productNode.SelectSingleNode("InstallerLink") : null;
						Version v;
						if (versionNode != null && linkNode != null && Version.TryParse(versionNode.InnerText.Trim(), out v) && currentVersion < v)
						{
							installerLink = linkNode.InnerText;
						}
					}
				}
				catch (Exception value)
				{
					LoggerExtensionMethods.LogWarning<UpdateChecker>(this, "Update check failed", new Dictionary<string, object>
					{
						{
							"Exception",
							value
						}
					}, "CheckForUpgradeAsync", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Update\\UpdateChecker.cs", 99);
				}
				this.webClient = null;
				result = installerLink;
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000075CA File Offset: 0x000057CA
		public void Cancel()
		{
			if (this.webClient != null)
			{
				this.webClient.CancelAsync();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000075E0 File Offset: 0x000057E0
		private XmlDocument LoadAsXml(string text)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				using (StringReader stringReader = new StringReader(text))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader))
					{
						xmlDocument.Load(xmlReader);
					}
				}
			}
			catch (XmlException value)
			{
				LoggerExtensionMethods.LogError<UpdateChecker>(this, "Error parsing response as XML", new Dictionary<string, object>
				{
					{
						"Exception",
						value
					}
				}, "LoadAsXml", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Update\\UpdateChecker.cs", 128);
			}
			return xmlDocument;
		}

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CheckForUpgradeAsync>d__1 : IAsyncStateMachine
		{
			// Token: 0x04000150 RID: 336
			public int <>1__state;

			// Token: 0x04000151 RID: 337
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000152 RID: 338
			public UpdateChecker <>4__this;

			// Token: 0x04000153 RID: 339
			public bool overrideDisabledCheck;

			// Token: 0x04000154 RID: 340
			public string <installerLink>5__2;

			// Token: 0x04000155 RID: 341
			public Version <devVersion>5__3;

			// Token: 0x04000156 RID: 342
			public Version <currentVersion>5__4;

			// Token: 0x04000157 RID: 343
			public bool <isDevVersion>5__5;

			// Token: 0x04000158 RID: 344
			public string <text>5__6;

			// Token: 0x04000159 RID: 345
			public XmlDocument <xmlDoc>5__7;

			// Token: 0x0400015A RID: 346
			public XmlNode <productNode>5__8;

			// Token: 0x0400015B RID: 347
			public XmlNode <versionNode>5__9;

			// Token: 0x0400015C RID: 348
			public XmlNode <linkNode>5__a;

			// Token: 0x0400015D RID: 349
			private TaskAwaiter<string> <>u__$awaiterb;

			// Token: 0x0400015E RID: 350
			private object <>t__stack;

			// Token: 0x0600027F RID: 639 RVA: 0x00007284 File Offset: 0x00005484
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				string result;
				try
				{
					int num = num2;
					if (num != 0)
					{
						if (!overrideDisabledCheck && !this.IsUpdateCheckEnabled)
						{
							result = null;
							goto IL_28F;
						}
						installerLink = null;
					}
					try
					{
						int num3 = num2;
						TaskAwaiter<string> taskAwaiter;
						if (num3 != 0)
						{
							devVersion = Version.Parse("0.0.0.0");
							currentVersion = Assembly.GetEntryAssembly().GetName().Version;
							bool isDevVersion = currentVersion == devVersion;
							if (isDevVersion || !NetworkInterface.GetIsNetworkAvailable())
							{
								goto IL_22C;
							}
							this.webClient = new WebClient();
							this.webClient.Headers[HttpRequestHeader.CacheControl] = "no-cache";
							taskAwaiter = this.webClient.DownloadStringTaskAsync(this.VersionLink).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num2 = 0;
								TaskAwaiter<string> taskAwaiter2 = taskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, UpdateChecker.<CheckForUpgradeAsync>d__1>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							TaskAwaiter<string> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<string>);
							num2 = -1;
						}
						string result2 = taskAwaiter.GetResult();
						taskAwaiter = default(TaskAwaiter<string>);
						string text2 = result2;
						text = text2;
						xmlDoc = this.LoadAsXml(text);
						productNode = xmlDoc.SelectSingleNode(string.Format("/AppInfo/LatestVersion[@Edition='{0}' and @Platform = '{1}']", this.productInfo.Edition, this.productInfo.Platform));
						versionNode = ((productNode != null) ? productNode.SelectSingleNode("VersionNumber") : null);
						linkNode = ((productNode != null) ? productNode.SelectSingleNode("InstallerLink") : null);
						Version v;
						if (versionNode != null && linkNode != null && Version.TryParse(versionNode.InnerText.Trim(), out v) && currentVersion < v)
						{
							installerLink = linkNode.InnerText;
						}
						IL_22C:;
					}
					catch (Exception value)
					{
						LoggerExtensionMethods.LogWarning<UpdateChecker>(this, "Update check failed", new Dictionary<string, object>
						{
							{
								"Exception",
								value
							}
						}, "CheckForUpgradeAsync", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Update\\UpdateChecker.cs", 99);
					}
					this.webClient = null;
					result = installerLink;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_28F:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000280 RID: 640 RVA: 0x0000756C File Offset: 0x0000576C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.<>t__builder.SetStateMachine(param0);
			}
		}
	}
}
