using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectBodyType : MonoBehaviour
{
    public GameObject[] bodyTypes;
    public Material deselect_material;
    public Material select_material;
    private int currentSelectedBodyType;

    public Button confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all icons have a collider component for hover detection
        foreach (GameObject bodyType in bodyTypes)
        {
            if (!bodyType.GetComponent<Collider>())
            {
                bodyType.AddComponent<BoxCollider>(); // Example collider, adjust as needed
            }
        }

        //listen to button activity
        confirmButton.onClick.AddListener(Confirmed);
    }

    // Update is called once per frame
    void Update()
    {
        //save the selected bodytype (1-3 small to fat women, 4-6 small to fat men)
        currentSelectedBodyType = 0;
    }


    void Confirmed()
    {
        //TODOO
        //fetch selected bodytyp

        PlayerPrefs.SetInt("bodytype", currentSelectedBodyType); //fetch this data by: PlayerPrefs.GetInt("bodytype");

        //activate this body in the hierarchy
        //confirmed button clicked
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

}
