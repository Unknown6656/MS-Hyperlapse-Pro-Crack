using System;
using System.Collections.Generic;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200002D RID: 45
	public class HyperlapseTelemetryClient : ITelemetryClient
	{
		// Token: 0x040000DD RID: 221
		private ProductInfo productInfo;

		// Token: 0x040000DE RID: 222
		private ITelemetryClient client;

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000AA4A File Offset: 0x00008C4A
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000AA57 File Offset: 0x00008C57
		public bool IsAutomaticCrashLoggingEnabled
		{
			get
			{
				return this.client.IsAutomaticCrashLoggingEnabled;
			}
			set
			{
				this.client.IsAutomaticCrashLoggingEnabled = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000AA65 File Offset: 0x00008C65
		public string SessionID
		{
			get
			{
				return this.client.SessionID;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AA72 File Offset: 0x00008C72
		public HyperlapseTelemetryClient(ITelemetryClient client, ProductInfo productInfo)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			this.client = client;
			if (productInfo == null)
			{
				throw new ArgumentNullException("firstRunExperience");
			}
			this.productInfo = productInfo;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public void Event(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			this.client.Event(eventName, this.AppendClientDetails(properties), metrics);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AABA File Offset: 0x00008CBA
		public void Exception(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			this.client.Exception(exception, this.AppendClientDetails(properties), metrics);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public void Metric(string name, double value)
		{
			this.client.Metric(name, value);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AADF File Offset: 0x00008CDF
		public void PageView(string name)
		{
			this.client.PageView(name);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AAED File Offset: 0x00008CED
		public void UnhandledException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			this.client.UnhandledException(exception, this.AppendClientDetails(properties), metrics);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AB04 File Offset: 0x00008D04
		private IDictionary<string, string> AppendClientDetails(IDictionary<string, string> properties)
		{
			properties.Add(new KeyValuePair<string, string>("Platform", this.productInfo.Platform.ToString()));
			properties.Add(new KeyValuePair<string, string>("Edition", this.productInfo.Edition.ToString()));
			return properties;
		}
	}
}
