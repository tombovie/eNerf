using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;


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
            //get the full model asynchronously (seperate thread from main game thread)
            Addressables.LoadAssetAsync<GameObject>("Assets/Shoes/mixamo_with_nike.glb").Completed +=
                (asyncOperationHandle) =>
                {
                    if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        //do something with result aka loaded model
                        GameObject product = asyncOperationHandle.Result;
                        Debug.Log("Got the result gameobject!");
                        Transform shoe_left = product.transform.Find("shoe_left");
                        Debug.Log("got the shoe_left gameobject underneath the full character component!");
                        Mesh shoe_left_mesh = shoe_left.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                        Material shoe_left_material = 
                        Debug.Log("got the mesh from the shoe_left component!");
                        skinnedMeshRenderer.sharedMesh = shoe_left_mesh;
                        skinnedMeshRenderer.sharedMaterial = 
                        Debug.Log("Placed the mesh onto the current model!!");
                        //Instantiate(product);
                    }
                    else
                    {
                        Debug.Log("Failed to load!");
                    }

                };

        }

    }
}
