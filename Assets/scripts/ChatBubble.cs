using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    public static void Create(Transform parent, Vector3 localPosition, Transform personToLookAt, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;
        // Get the current Euler angles
        Vector3 currentRotation = personToLookAt.eulerAngles;
        // Add 180 degrees to the desired axis (e.g., Y-axis)
        //currentRotation.y += 180f;
        currentRotation.y += 270f;
        


        chatBubbleTransform.localRotation = Quaternion.Euler(currentRotation);
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);

        Destroy(chatBubbleTransform.gameObject, 5f);
    }



    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        Setup("hello");
    }

    private void Setup(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(6f, 1f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-6f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x /2f, 0f) + offset;

        //TextWriter.AddWriter_Static(textMeshPro, text, 0.05f, true, true);
    }
}
