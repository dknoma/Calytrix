using UnityEngine.U2D;

namespace Utility {
    public static class PixelPerfectCameraExtensions {
        public static void InitializeCamera(this PixelPerfectCamera cam, PixelPerfectCameraSettings settings) {
            PixelPerfectCameraSettings.SettingsData data = settings.Data;
            
            cam.assetsPPU = data.assetPixelsPerUnit;
            cam.refResolutionX = data.referenceResolutionX;
            cam.refResolutionY = data.referenceResolutionY;
            cam.upscaleRT = data.upscaleRenderTexture;
            cam.pixelSnapping = data.pixelSnapping;
            cam.cropFrameX = data.cropFrameX;
            cam.cropFrameY = data.cropFrameY;
            cam.stretchFill = data.stretchFill;
        }
    }
}