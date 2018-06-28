using System;
using System.Collections.Generic;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x0200001A RID: 26
	public class AccelerationOptions
	{
		// Token: 0x04000094 RID: 148
		private ISettingsStore settings;

		// Token: 0x04000095 RID: 149
		private readonly string UseGeometryShadersKey = "UseGeometryShaders";

		// Token: 0x04000096 RID: 150
		private readonly string ForceSoftwareRenderingKey = "ForceSoftwareRendering";

		// Token: 0x04000097 RID: 151
		private readonly string UseHardwareVideoEncoderKey = "UseHardwareVideoEncoder";

		// Token: 0x0600016A RID: 362 RVA: 0x00006B40 File Offset: 0x00004D40
		public AccelerationOptions(ISettingsStore settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006B7E File Offset: 0x00004D7E
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006B94 File Offset: 0x00004D94
		public bool UseGeometryShaders
		{
			get
			{
				return this.settings.ReadSetting<bool>(this.UseGeometryShadersKey, true);
			}
			set
			{
				LoggerExtensionMethods.LogDiagnostic<AccelerationOptions>(this, "Toggled Use Geometry Shaders", new Dictionary<string, object>
				{
					{
						"Value",
						value
					}
				}, "UseGeometryShaders", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\AccelerationOptions.cs", 38);
				this.settings.WriteSetting<bool>(this.UseGeometryShadersKey, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006BE2 File Offset: 0x00004DE2
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public bool ForceSoftwareRendering
		{
			get
			{
				return this.settings.ReadSetting<bool>(this.ForceSoftwareRenderingKey, true);
			}
			set
			{
				LoggerExtensionMethods.LogDiagnostic<AccelerationOptions>(this, "Toggled Force Software Rendering", new Dictionary<string, object>
				{
					{
						"Value",
						value
					}
				}, "ForceSoftwareRendering", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\AccelerationOptions.cs", 51);
				this.settings.WriteSetting<bool>(this.ForceSoftwareRenderingKey, value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006C46 File Offset: 0x00004E46
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006C5C File Offset: 0x00004E5C
		public bool UseHardwareVideoEncoder
		{
			get
			{
				return this.settings.ReadSetting<bool>(this.UseHardwareVideoEncoderKey, false);
			}
			set
			{
				LoggerExtensionMethods.LogDiagnostic<AccelerationOptions>(this, "Toggled Use Hardware Video Encoder", new Dictionary<string, object>
				{
					{
						"Value",
						value
					}
				}, "UseHardwareVideoEncoder", "C:\\OxBuild\\30\\s\\hyperlapse_gui\\Hyperlapse\\Hyperlapse\\Model\\Engine\\AccelerationOptions.cs", 64);
				this.settings.WriteSetting<bool>(this.UseHardwareVideoEncoderKey, value);
			}
		}
	}
}
