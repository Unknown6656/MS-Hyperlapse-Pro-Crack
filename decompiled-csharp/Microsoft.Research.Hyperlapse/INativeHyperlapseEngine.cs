using System;
using Microsoft.Research.VisionTools.Toolkit;

namespace Microsoft.Research.Hyperlapse
{
	// Token: 0x02000008 RID: 8
	public interface INativeHyperlapseEngine
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78
		float CurrentProgess { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004F RID: 79
		string CurrentStatus { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80
		bool ProductIsActivated { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000051 RID: 81
		string LastError { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000052 RID: 82
		Exception TrialException { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000053 RID: 83
		// (remove) Token: 0x06000054 RID: 84
		event EventHandler ProgressChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000055 RID: 85
		// (remove) Token: 0x06000056 RID: 86
		event EventHandler ActivationStatusChanged;

		// Token: 0x06000057 RID: 87
		void CancelProcessing();

		// Token: 0x06000058 RID: 88
		bool Process(int renderTarget, string localPath, string tempOutputDirectory, string videoOutputFilePath, string calibrationFile, int startFrame, int endFrame, int speedupFactor, Rational frameRate, int outputHeight, int outputWidth, int outputBitrate, string videoMode, int outputRotation, bool useGeometryShaders, bool forceSoftwareRendering, float creditLength, bool useHardwareVideoEncoder);

		// Token: 0x06000059 RID: 89
		void SetActivationInfo(string activationConfigFile, string activationRegistrationFile);
	}
}
