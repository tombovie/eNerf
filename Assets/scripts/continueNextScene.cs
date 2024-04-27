using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class continueNextScene : MonoBehaviour
{

    public Button continueButton;
    private int previousSceneIndex, nextSceneIndex;
    private float target_progress;
    public GameObject waitCanvas, loadingCanvas;
    public Image LoadingBarFill;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        previousSceneIndex = PlayerPrefs.GetInt("previousSceneIndex");

        //nextSceneIndex = previousSceneIndex + 1;

        //take a random scene that is not taken yet
        List<Scene> scenes = new List<Scene>(SceneManager.GetAllScenes());
        scenes.RemoveAt(4); //delete transitionscene
        scenes.RemoveAt(0); //remove startscene
        //if already completed a scene, delete it from the list
        if (PlayerPrefs.HasKey("completedScene"))
        {
            scenes.RemoveAt(PlayerPrefs.GetInt("completedScene"));
        }

        //remove previous played scene
        int temp = 0;
        foreach(Scene scene in scenes)
        {
            if(scene.buildIndex == previousSceneIndex)
            {
                temp = scenes.IndexOf(scene);
            }
        }
        scenes.RemoveAt(temp);

        //fetch new random scene from list and return the buildindex
        nextSceneIndex = scenes[UnityEngine.Random.Range(0, scenes.Count - 1)].buildIndex;
        //set scene to be completed
        PlayerPrefs.SetInt("completedScene", previousSceneIndex);
    }

    private void OnContinueClicked()
    {
        StartCoroutine(LoadSceneAsync(nextSceneIndex));
    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        //operation.allowSceneActivation = false;
        //disable current canvas and enable loading canvas
        waitCanvas.SetActive(false);
        loadingCanvas.SetActive(true);

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

        LoadingBarFill.fillAmount = Mathf.MoveTowards(LoadingBarFill.fillAmount, target_progress, 3 * Time.deltaTime);
    }

}
