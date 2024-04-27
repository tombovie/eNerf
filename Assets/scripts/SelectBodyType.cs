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
    private bool nameFound;

    //loading screen
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public AudioSource shopAudio;



    // Start is called before the first frame update
    void Start()
    {
        //reset PlayerPrefs (deletes all keys and values)
        PlayerPrefs.DeleteAll();
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
            Debug.Log("checking character: " + character.name + " with name: " + name);
            if (character.name == name || character.name.ToLower() == name) //ignore case sensitive
            {
                PlayerPrefs.SetString("character", name);
                //change appearance of inputfield
                checkmark.SetActive(true);
                nameFound = true;
                break;
            }
            else 
            {
                checkmark.SetActive(false);
                nameFound = false;
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

    [Obsolete]
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
        if (!nameFound) { return; } //if name not found, do not proceed
        //activate black canvas 
        LoadingScreen.SetActive(true);
        shopAudio.Stop();
        //cullings mask of camera
        XR_Camera.GetComponent<Camera>().cullingMask = LayerMask.GetMask("LoadingScreen");
        XR_Camera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        //write current selected bodytype to local data
        PlayerPrefs.SetInt("bodytype", currentSelectedBodyType);
        PlayerPrefs.SetInt("bodycolor", currentSelectedBodyColor);

          
       //create list with all testscenes and select a random one
       List<int> scenesIndices = new List<int>(new int[] {1,2,3});
       int sceneId= scenesIndices[UnityEngine.Random.Range(0, scenesIndices.Count)]; //second argument is exlusive. first one is inclusive (see docs online)
       PlayerPrefs.SetInt("sceneCompleted" + sceneId, sceneId);
       LoadScene(sceneId);

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            nameInput.GetComponent<TMPro.TMP_InputField>().Select();
        }

        LoadingBarFill.fillAmount = Mathf.MoveTowards(LoadingBarFill.fillAmount, target_progress, 3 * Time.deltaTime);
    }

}
