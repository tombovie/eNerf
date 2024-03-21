using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCopier : MonoBehaviour
{
    public GameObject sourceObject; // GameObject containing the materials to copy
    public GameObject destinationObject; // GameObject to copy the materials to

    void Start()
    {
        CopyMaterials();
    }

    void CopyMaterials()
    {
        // Get the renderers from both source and destination GameObjects
        Renderer[] sourceRenderers = sourceObject.GetComponentsInChildren<Renderer>();
        Renderer[] destinationRenderers = destinationObject.GetComponentsInChildren<Renderer>();

        // Iterate over each renderer
        for (int i = 0; i < sourceRenderers.Length; i++)
        {
            // Get the materials from the source renderer
            Material[] sourceMaterials = sourceRenderers[i].sharedMaterials;

            // Make sure the destination renderer exists and has the same number of materials
            if (i < destinationRenderers.Length && destinationRenderers[i] != null)
            {
                // Copy the materials to the destination renderer
                destinationRenderers[i].materials = sourceMaterials;
            }
        }
    }
}
