using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PixelPerfectCameraSettings", menuName = "PixelPerfectCamera/Settings", order = 1)]
public class PixelPerfectCameraSettings : ScriptableObject {
    [SerializeField] private SettingsData data;

    public SettingsData Data {
        get => data;
        set => data = value;
    }

    [Serializable]
    public class SettingsData {
        public int assetPixelsPerUnit;
        public int referenceResolutionX;
        public int referenceResolutionY;
        public bool upscaleRenderTexture;
        public bool pixelSnapping;
        public bool cropFrameX;
        public bool cropFrameY;
        public bool stretchFill;
    }
}
