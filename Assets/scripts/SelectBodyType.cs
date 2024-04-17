using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectBodyType : MonoBehaviour
{
    public GameObject[] bodyTypes_images;
    public Material deselect_material;
    public Material select_material;
    private int currentSelectedBodyType;
    private GameObject currentImage;
    private GameObject selectedImage;

    public Button confirmButton;
    public List<Button> imageButtons;

    // Start is called before the first frame update
    void Start() { 

        //listen to button activity
        confirmButton.onClick.AddListener(Confirmed);

        foreach (Button image in imageButtons)
        {
            image.onClick.AddListener(() => { 
                ChangeImageAppearance(image);
            }); ;

        }


    }

    

    // Update is called once per frame
    void Update()
    {



        
    }


    void ChangeImageAppearance(Button image) {

        if (selectedImage != null) {
            selectedImage.GetComponent<Image>().color = Color.white;
        }

        currentSelectedBodyType = imageButtons.IndexOf(image);
        //write current selected bodytype to local data
        PlayerPrefs.SetInt("bodytype", currentSelectedBodyType);
        //fetch the image behind transparant button
        currentImage = bodyTypes_images[currentSelectedBodyType];

        currentImage.GetComponent<Image>().color = Color.red;
        selectedImage = currentImage;
    }


    void Confirmed()
    {
        //confirmed button clicked
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

}
