using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siccity.GLTFUtility;
using System.IO;
using UnityEditor;

public class productController : MonoBehaviour
{

    private SkinnedMeshRenderer skinnedMeshRenderer;
    // use this for build: public string bundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles/AssetBundles");
    private string filepath = "Assets/Shoes/mixamo_with_nike.glb";
    Mesh productMesh;
    AssetBundle assetBundle;
    //  [SerializeField] Material productMaterial;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();


        // Load AssetBundle at runtime
        assetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/AssetBundles");

        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle");
            return;
        }
/*
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);*/

    }

    // Update is called once per frame
    void Update()
    {
        //check if an item is grabbed
        //if yes check if button is clicked
        if (Input.GetKeyUp(KeyCode.F))
        {

            string[] names = assetBundle.GetAllAssetNames();
            foreach (string name in names) {
                Debug.Log("Assets of size "+ names.Length + ": " + name);
            }
            
            //load full character model from assetbundle
            GameObject loadedObject = assetBundle.LoadAsset<GameObject>("Assets/AssetBundles/characters.newbalance");

            //if yes, get its mesh from the meshfilter component
            if (loadedObject != null) {
                Debug.Log("loadedObject from assetbundle was not null!");
                Instantiate(loadedObject);
                //get the name of the mesh
                //skinnedMeshRenderer.sharedMesh = productMesh;
                //load the full character with the mesh on its body
                //extract only the shoes from the body (these should be the same as the single shoe
                //update the skinned mesh renderer of the current bodypart
                assetBundle.Unload(false);
            }
        }



        
    }
}
