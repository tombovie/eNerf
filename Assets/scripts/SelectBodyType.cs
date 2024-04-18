using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectBodyType : MonoBehaviour
{
    private int currentSelectedBodyType, currentSelectedBodyColor;
    private Button selectedBodyType, selectedSkinColor;
    private Color selectedSkinButtonColor;
    public Button confirmButton;
    public List<Button> bodyTypeButtons, bodyColorButtons;

    // Start is called before the first frame update
    void Start()
    {

        //listen to button activity
        confirmButton.onClick.AddListener(Confirmed);


        foreach (Button button in bodyTypeButtons)
        {
            button.onClick.AddListener(() =>
            {
                ChangeBodyType(button);
            }); ;

        }

        foreach (Button button in bodyColorButtons)
        {
            button.onClick.AddListener(() =>
            {
                ChangeSkinColor(button);
            }); ;

        }


    }


    void ChangeBodyType(Button image) {

        if (selectedBodyType != null) {
            Color DeselectedColor = Color.white;
            DeselectedColor.a = 1;
            selectedBodyType.GetComponent<Image>().color = DeselectedColor;
        }

        //fetch index of bodytype
        currentSelectedBodyType = bodyTypeButtons.IndexOf(image);
        //change appearance of selected image
        Color imageSelectedColor = Color.red;
        imageSelectedColor.a = 0.3f;
        image.GetComponent<Image>().color = imageSelectedColor;
        
        //save selected (to deselect later if needed)
        selectedBodyType = image;
    }

    void ChangeSkinColor(Button image) 
    {
        

        //deselect previous if not first selection
        if (selectedSkinColor != null)
        {
            selectedSkinColor.GetComponent<Image>().color = selectedSkinButtonColor;
        }

        //fetch previous color
        selectedSkinButtonColor = image.GetComponent<Image>().color;

        //fetch bodycolor
        currentSelectedBodyColor = bodyColorButtons.IndexOf(image);
        //change appearance of selected image
        Color imageSelectedColor = Color.red;
        imageSelectedColor.a = 0.3f;
        image.GetComponent<Image>().color = imageSelectedColor;

        //save selected (to deselect later if needed)
        selectedSkinColor = image;
        
    }


    void Confirmed()
    {

        //write current selected bodytype to local data
        PlayerPrefs.SetInt("bodytype", currentSelectedBodyType);
        PlayerPrefs.SetInt("bodycolor", currentSelectedBodyColor);

        //confirmed button clicked
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

}
