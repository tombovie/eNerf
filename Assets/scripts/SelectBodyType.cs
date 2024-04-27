using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SelectBodyType : MonoBehaviour
{
    private int currentSelectedBodyType, currentSelectedBodyColor;
    private Button selectedBodyType, selectedSkinColor;
    public Button confirmButton;
    public List<Button> bodyTypeButtons, bodyColorButtons;
    public List<GameObject> characters;
    public GameObject nameInput;
    public GameObject checkmark;
    public GameObject keyboard;
    public GameObject XR_Camera;
    private float target_progress;

    //loading screen
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public AudioSource shopAudio;



    // Start is called before the first frame update
    void Start()
    {
        //listen to inputfield select
        nameInput.GetComponent<TMPro.TMP_InputField>().onSelect.AddListener(SelectInputField);
        //listen to input field changes
        nameInput.GetComponent<TMPro.TMP_InputField>().onValueChanged.AddListener(SelectCharacter);
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

    [Obsolete]
    private void DeSelectInputField()
    {
        if (keyboard.active)
        {
            keyboard.SetActive(false);
        }
    }

    [Obsolete]
    private void SelectInputField(string text)
    {
        if (!keyboard.active) 
        { 
            keyboard.SetActive(true);
        }
    }

    private void SelectCharacter(string name)
    {
        
        foreach (GameObject character in characters) 
        {
            if (character.name == name)
            {
                PlayerPrefs.SetString("character", name);
                //change appearance of inputfield
                checkmark.SetActive(true);
            }
            else 
            {
                checkmark.SetActive(false);
            }
        }
    }

    void ChangeBodyType(Button image) {

        //put away keyboard
        DeSelectInputField();

        if (selectedBodyType != null) {
            Color DeselectedColor = new Color(192f, 192f, 192f);
            DeselectedColor.a = 0.3f;
            selectedBodyType.GetComponent<Image>().color = DeselectedColor;
        }

        //fetch index of bodytype
        currentSelectedBodyType = bodyTypeButtons.IndexOf(image);
        //change appearance of selected image
        Color imageSelectedColor = Color.white;
        imageSelectedColor.a = 1;
        image.GetComponent<Image>().color = imageSelectedColor;
        
        //save selected (to deselect later if needed)
        selectedBodyType = image;
    }

    void ChangeSkinColor(Button image) 
    {
        //put away keyboard
        DeSelectInputField();

        //deselect previous if not first selection
        if (selectedSkinColor != null)
        {
            Outline selectedOutlineComponent = selectedSkinColor.GetComponent<Outline>();
            Color selectedOutlineColor = selectedOutlineComponent.effectColor;
            selectedOutlineColor.a = 0f;
            selectedOutlineComponent.effectColor = selectedOutlineColor;
        }

        //fetch bodycolor
        currentSelectedBodyColor = bodyColorButtons.IndexOf(image);

        Outline outlineComponent = image.GetComponent<Outline>();
        Color outlineColor = outlineComponent.effectColor;
        outlineColor.a = 1f;

        if (outlineComponent != null) 
        {
            Debug.Log("outlinecoponentfound!");
            outlineComponent.effectColor = outlineColor;
        }

        //save selected (to deselect later if needed)
        selectedSkinColor = image;

    }


    void Confirmed()
    {
        //activate black canvas 
        LoadingScreen.SetActive(true);
        shopAudio.Stop();
        //cullings mask of camera
        XR_Camera.GetComponent<Camera>().cullingMask = LayerMask.GetMask("LoadingScreen");
        XR_Camera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        //write current selected bodytype to local data
        PlayerPrefs.SetInt("bodytype", currentSelectedBodyType);
        PlayerPrefs.SetInt("bodycolor", currentSelectedBodyColor);

        //confirmed button clicked
        //LoadScene(1);
        //start random scene
        Scene[] scenes = SceneManager.GetAllScenes();
        LoadScene(UnityEngine.Random.Range(1, scenes.Length - 2)); // - 2 ==> exclude startscene and transitionscene
    }


    public void LoadScene(int sceneId) 
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        //operation.allowSceneActivation = false;
       

        while (!operation.isDone) 
        {
            target_progress = operation.progress;

            yield return null;
        }

        //operation.allowSceneActivation = true;
    }

    //for debugging purposes (delete later)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Confirmed();
        }

        LoadingBarFill.fillAmount = Mathf.MoveTowards(LoadingBarFill.fillAmount, target_progress, 3 * Time.deltaTime);
    }

}
