using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractible : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private PlayerInteractUI playerInteractUI;
    [SerializeField] private AudioSource cashRegister;
    [SerializeField] private AudioSource buyAudio;
    [SerializeField] private AudioSource talkAudio;
    Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private string interactUIText;
    private string action;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Greet(Transform InteractingPerson, string text)
    {
        //playerInteractUI.HideWhileTalking();
        ChatBubble.CreateForSeconds(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, text, 3);
        //Debug.Log("Interact!");
        animator.SetTrigger("Greeting");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }
    public void Interact(Transform InteractingPerson, string text)
    {
        playerInteractUI.HideWhileTalking();
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, text);
        animator.SetTrigger("Talking");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }
    public void Buy(Transform InteractingPerson)
    {
        if (cashRegister != null) { cashRegister.Play();  }
        playerInteractUI.HideWhileTalking();
        if (buyAudio != null) { StartCoroutine(PlayBuyAudio()); }
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, "Thanks for shopping with us!");
        
        animator.SetTrigger("Talking");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);

        StartCoroutine(EndGame());
    }
    public void Talk(Transform InteractingPerson)
    {
        playerInteractUI.HideWhileTalking();
        if (talkAudio != null) { talkAudio.Play(); }
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, "You can bring me a \nshoe you want to buy!");

        animator.SetTrigger("Talking");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }

    public string GetInteractText()
    {
        return interactText;    
    }
    public void SetInteractText(string t)
    {
        interactText = t;
    }
    public void SetAction(string actionvar)
    {
        action = actionvar;
    }
    public string GetAction()
    {
        return action;
    }

    IEnumerator EndGame()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 4f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //save current scene in local data
        PlayerPrefs.SetInt("previousSceneIndex", currentSceneIndex);

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
        foreach (Scene scene in scenes)
        {
            if (scene.buildIndex == currentSceneIndex)
            {
                temp = scenes.IndexOf(scene);
            }
        }
        scenes.RemoveAt(temp);

        //go to next scene
        if(scenes.Count < 2) //meaning the last scene has been played --> go to start scene
        {
            SceneTransitionManager.singleton.GoToScene(0);
        }
        else
        {
            SceneTransitionManager.singleton.GoToScene(4);
        }
    }

    IEnumerator PlayBuyAudio()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        buyAudio.Play();
    }

}
