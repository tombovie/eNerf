using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePlayer : MonoBehaviour
{

    private int bodyTypeIndex;
    private GameObject currentBodyType;
    private GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        bodyTypeIndex = PlayerPrefs.GetInt("bodytype");

        switch (bodyTypeIndex) {
            case 0:
                currentBodyType = (GameObject) Instantiate(Resources.Load("Basic_vrouw/vrouw_slank"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                break;
            default:
                //default character
                break;


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
