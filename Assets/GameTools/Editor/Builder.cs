using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class Builder
{

    private const string ANDROID_BUNDLE_IDENTIFIER = "com.StudioSunshine.ChainPuzzle2048";

    private const string PRODUCT_NAME = "ChainPuzzle2048";

    private const string AND_GA_GAME_KEY = "";
    private const string AND_GA_SECRET_KEY = "";

    private const string APK_LOCATION_PATH = "C:\\Users\\vantan\\Documents\\GooglePlayApp\\ChainPuzzle\\ABB\\studiosunshihe-chainpuzzle2048-1.0.2.apk";

    private const string ABB_LOCATION_PATH = "C:\\Users\\vantan\\Documents\\GooglePlayApp\\ChainPuzzle\\ABB\\studiosunshihe-chainpuzzle2048-1.0.2.aab";

    private const int BUNDLE_VERSION_CODE = 7;

    private const string BUNDLE_VERSION = "1.0.2";

    private const string KEYSTORE_NAME = "Key/chainpuzzle2048.keystore";
    private const string KEYSTORE_KEYALIAS_PASS = "pvfknbbs";
    private const string KEYALIAS_NAME = "chainpuzzle2048";

    [MenuItem("Tools/Build Project Android (.apk)")]
    public static void BuildProjectAndroidForAPK()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.buildAppBundle = false;

        PlayerSettings.Android.keystoreName = KEYSTORE_NAME;
        PlayerSettings.Android.keystorePass = KEYSTORE_KEYALIAS_PASS;
        PlayerSettings.Android.keyaliasName = KEYALIAS_NAME;
        PlayerSettings.Android.keyaliasPass = KEYSTORE_KEYALIAS_PASS;

        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.productName = PRODUCT_NAME;
        PlayerSettings.applicationIdentifier = ANDROID_BUNDLE_IDENTIFIER;
        PlayerSettings.bundleVersion = BUNDLE_VERSION;
        PlayerSettings.Android.bundleVersionCode = BUNDLE_VERSION_CODE;

        // GameAnalyticsê›íË(ì±ì¸éûÇ…åüèÿ)
        //var gaSetting = AssetDatabase.LoadAssetAtPath("Assets/Resources/GameAnalytics/Settings.asset", typeof(GameAnalyticsSDK.Setup.Settings)) as GameAnalyticsSDK.Setup.Settings;
        //gaSetting.UpdateGameKey(0, AND_GA_GAME_KEY);
        //gaSetting.UpdateSecretKey(0, AND_GA_SECRET_KEY);
        //EditorUtility.SetDirty(gaSetting);

        BuildOptions opt = BuildOptions.None;

        var report = BuildPipeline.BuildPlayer(GetAllScene(), APK_LOCATION_PATH, BuildTarget.Android, opt);
        var summary = report.summary;

        switch (summary.result)
        {
            case BuildResult.Succeeded:
                Debug.Log("Android Build succeeded");
                EditorUtility.RevealInFinder(APK_LOCATION_PATH);
                break;
            case BuildResult.Failed:
                Debug.LogError($"Build Faild\n{summary.totalErrors}");
                break;
            case BuildResult.Cancelled:
            case BuildResult.Unknown:
            default:
                Debug.LogWarning($"Build Error");
                break;
        }
    }

    [MenuItem("Tools/Build Project Android (.abb)")]
    public static void BuildProjectAndroidForABB()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.buildAppBundle = true;

        PlayerSettings.Android.keystoreName = KEYSTORE_NAME;
        PlayerSettings.Android.keystorePass = KEYSTORE_KEYALIAS_PASS;
        PlayerSettings.Android.keyaliasName = KEYALIAS_NAME;
        PlayerSettings.Android.keyaliasPass = KEYSTORE_KEYALIAS_PASS;

        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.productName = PRODUCT_NAME;
        PlayerSettings.applicationIdentifier = ANDROID_BUNDLE_IDENTIFIER;
        PlayerSettings.bundleVersion = BUNDLE_VERSION;
        PlayerSettings.Android.bundleVersionCode = BUNDLE_VERSION_CODE;

        // GameAnalyticsê›íË(ì±ì¸éûÇ…åüèÿ)
        //var gaSetting = AssetDatabase.LoadAssetAtPath("Assets/Resources/GameAnalytics/Settings.asset", typeof(GameAnalyticsSDK.Setup.Settings)) as GameAnalyticsSDK.Setup.Settings;
        //gaSetting.UpdateGameKey(0, AND_GA_GAME_KEY);
        //gaSetting.UpdateSecretKey(0, AND_GA_SECRET_KEY);
        //EditorUtility.SetDirty(gaSetting);

        BuildOptions opt = BuildOptions.None;

        var report = BuildPipeline.BuildPlayer(GetAllScene(), ABB_LOCATION_PATH, BuildTarget.Android, opt);
        var summary = report.summary;

        switch (summary.result)
        {
            case BuildResult.Succeeded:
                Debug.Log("Android Build succeeded");
                EditorUtility.RevealInFinder(ABB_LOCATION_PATH);
                break;
            case BuildResult.Failed:
                Debug.LogError($"Build Faild\n{summary.totalErrors}");
                break;
            case BuildResult.Cancelled:
            case BuildResult.Unknown:
            default:
                Debug.LogWarning($"Build Error");
                break;
        }
    }

    public static string[] GetAllScene()
    {
        List<string> allScene = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                allScene.Add(scene.path);
            }
        }

        return allScene.ToArray();
    }
}
