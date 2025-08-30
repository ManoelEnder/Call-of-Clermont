using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    [TextArea(3, 5)] public string text;
    public Sprite portrait;
}

public class DialogueController : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup box;               
    public TMP_Text speakerText;          
    public TMP_Text bodyText;             
    public Image portraitImage;           
    public GameObject continueIcon;       

    [Header("Config")]
    public List<DialogueLine> lines = new List<DialogueLine>();
    public float charsPerSecond = 40f;
    public float punctuationPause = 0.25f; 
    public KeyCode advanceKey = KeyCode.Mouse0; 

    int index = -1;
    bool typing = false;
    Coroutine typingRoutine;

    void Awake()
    {
        Show(false);
        if (continueIcon) continueIcon.SetActive(false);
    }

    public void StartDialogue()
    {
        index = -1;
        Show(true);
        Next();
    }

    public void StartDialogue(List<DialogueLine> newLines)
    {
        lines = newLines ?? new List<DialogueLine>();
        StartDialogue();
    }

    void Update()
    {
        if (box && box.alpha < 0.99f) return;

        if (Input.GetKeyDown(advanceKey) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (typing) FinishTyping();
            else Next();
        }
    }

    void Next()
    {
        index++;
        if (index >= lines.Count)
        {
            End();
            return;
        }

        var l = lines[index];

        if (speakerText) speakerText.text = l.speaker;
        if (portraitImage)
        {
            portraitImage.enabled = l.portrait != null;
            portraitImage.sprite = l.portrait;
        }

        if (typingRoutine != null) StopCoroutine(typingRoutine);
        typingRoutine = StartCoroutine(TypeRoutine(l.text));
    }

    IEnumerator TypeRoutine(string t)
    {
        typing = true;
        if (continueIcon) continueIcon.SetActive(false);
        if (bodyText) bodyText.text = "";

        foreach (char c in t)
        {
            bodyText.text += c;
            if (c == '.' || c == '!' || c == '?')
                yield return new WaitForSeconds(punctuationPause);
            else
                yield return new WaitForSeconds(1f / Mathf.Max(1f, charsPerSecond));
        }

        typing = false;
        if (continueIcon) continueIcon.SetActive(true);
    }

    void FinishTyping()
    {
        if (typingRoutine != null) StopCoroutine(typingRoutine);
        typing = false;
        if (continueIcon) continueIcon.SetActive(true);
        if (bodyText && index >= 0 && index < lines.Count)
            bodyText.text = lines[index].text;
    }

    void End()
    {
        Show(false);
    }

    void Show(bool v)
    {
        if (!box) return;
        box.alpha = v ? 1f : 0f;
        box.interactable = v;
        box.blocksRaycasts = v;
    }
}
