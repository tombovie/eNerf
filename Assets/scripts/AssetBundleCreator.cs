#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleCreator
{
    [MenuItem("Assets/Build Asset Bundle")]
    static void BuildBundles() {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget); //take the current user buildtarget
    }
}
#endif