using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public Transform player;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }
}
