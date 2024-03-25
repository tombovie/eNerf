using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCopier : MonoBehaviour
{
    public GameObject[] sourceGameObjects; // Array of GameObjects containing materials to copy
    public GameObject[] targetGameObjects; // Array of GameObjects to copy materials to

    void Start()
    {
        CopyMaterials();
    }

    void CopyMaterials()
    {
        // Check if source and target arrays have the same length
        if (sourceGameObjects.Length != targetGameObjects.Length)
        {
            Debug.LogError("Source and target GameObject arrays must have the same length!");
            return;
        }

        // Loop through each pair of source and target objects
        for (int i = 0; i < sourceGameObjects.Length; i++)
        {
            GameObject sourceObject = sourceGameObjects[i];
            GameObject targetObject = targetGameObjects[i];

            // Get renderers from source and target
            Renderer[] sourceRenderers = sourceObject.GetComponentsInChildren<Renderer>();
            Renderer[] targetRenderers = targetObject.GetComponentsInChildren<Renderer>();

            // Ensure both objects have renderers
            if (sourceRenderers.Length == 0 || targetRenderers.Length == 0)
            {
                Debug.LogWarning("One or both GameObjects lack a Renderer component!");
                continue;
            }

            // Copy materials from each source renderer to the corresponding target renderer
            for (int j = 0; j < Mathf.Min(sourceRenderers.Length, targetRenderers.Length); j++)
            {
                Renderer sourceRenderer = sourceRenderers[j];
                Renderer targetRenderer = targetRenderers[j];

                if (sourceRenderer != null && targetRenderer != null)
                {
                    targetRenderer.materials = sourceRenderer.sharedMaterials;
                }
            }
        }
    }
}
