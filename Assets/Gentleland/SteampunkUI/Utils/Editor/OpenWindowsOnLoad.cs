#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Gentleland.Utils.SteampunkUI
{
    [InitializeOnLoad]
    public static class OpenWindowsOnLoad
    {
        static OpenWindowsOnLoad()
        {
            PackageSettings settings = AssetDatabase.LoadAssetAtPath<PackageSettings>(PackageSettings.PackageSettingsPath);
            if (settings == null)
            {
                if (!AssetDatabase.IsValidFolder(PackageSettings.PackageSettingsFolderPath))
                {
                    AssetDatabase.CreateFolder("Assets", PackageSettings.PackageSettingsFolder);
                }
                settings = ScriptableObject.CreateInstance<PackageSettings>();
                AssetDatabase.CreateAsset(settings, PackageSettings.PackageSettingsPath);
            }
            if (settings.isFirstTimeUsingTheAsset)
            {
                EditorApplication.delayCall += WelcomeWindow.OpenWindow;
            }
        }
    }
}
#endif
