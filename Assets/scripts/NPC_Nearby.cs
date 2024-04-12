using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Nearby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float range = 2f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out NPCInteractible interactible))
                {
                    interactible.Interact(transform);
                }
            }
        }
    }
}
