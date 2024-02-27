﻿#if HAVE_GLTFAST || HAVE_UNITYGLTF
#define ANOTHER_IMPORTER_HAS_HIGHER_PRIORITY
#endif

#if !ANOTHER_IMPORTER_HAS_HIGHER_PRIORITY && !GLTFUTILITY_FORCE_DEFAULT_IMPORTER_OFF
#define ENABLE_DEFAULT_GLB_IMPORTER
#endif
#if GLTFUTILITY_FORCE_DEFAULT_IMPORTER_ON
#define ENABLE_DEFAULT_GLB_IMPORTER
#endif

#if !UNITY_2020_2_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#else
using UnityEditor.AssetImporters;
#endif
using UnityEngine;

namespace Siccity.GLTFUtility
{
#if ENABLE_DEFAULT_GLB_IMPORTER
	[ScriptedImporter(1, "gltf")]
#else
    [ScriptedImporter(1, null, overrideExts: new[] { "gltf" })]
#endif
	public class GLTFImporter : ScriptedImporter
	{

		public ImportSettings importSettings;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			// Load asset
			AnimationClip[] animations;
			if (importSettings == null) importSettings = new ImportSettings();
			GameObject root = Importer.LoadFromFile(ctx.assetPath, importSettings, out animations, Format.GLTF);

			// Save asset
			GLTFAssetUtility.SaveToAsset(root, animations, ctx, importSettings);
		}
	}
}