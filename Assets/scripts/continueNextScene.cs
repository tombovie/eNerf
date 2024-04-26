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

        nextSceneIndex = previousSceneIndex + 1;
       
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
