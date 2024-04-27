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

        //take a random scene that is not taken yet
        List<int> scenes = new List<int>(new int[] { 1, 2, 3 });
        List<int> toRemove = new();
        foreach (int sceneIndex in scenes)
        {
            if (PlayerPrefs.HasKey("sceneCompleted" + sceneIndex))
            {
                //Debug.Log("index to remove: " + sceneIndex);
                toRemove.Add(sceneIndex);
            }
        }
        foreach (int sceneIndex in toRemove)
        {
            scenes.Remove(sceneIndex);
        }

        //take random scene from remaining
        nextSceneIndex = scenes[UnityEngine.Random.Range(0, scenes.Count)]; //second argument is exlusive. first one is inclusive (see docs online)
        PlayerPrefs.SetInt("sceneCompleted"+nextSceneIndex, nextSceneIndex); //increase scene counter

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnContinueClicked();
        }
        LoadingBarFill.fillAmount = Mathf.MoveTowards(LoadingBarFill.fillAmount, target_progress, 3 * Time.deltaTime);
    }

}
