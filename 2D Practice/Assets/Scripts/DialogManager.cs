using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] public int lettersPerASecond;


    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static DialogManager Instance { get; private set; }

    int currentLine = 0;
    Dialog dialog;
    bool isTyping;
    private bool dialogSkip;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnCloseDialog?.Invoke();
            }
        }
        //Allows the Player to Skip the text crawl
        else if (Input.GetKeyDown(KeyCode.F) && isTyping)
        {
            dialogSkip = true;
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            if (!dialogSkip) 
            {
                yield return new WaitForSeconds(1f / lettersPerASecond);
            }
        }
        if(dialogSkip)
        {
            dialogSkip = false;
        }
        isTyping = false;
    }
}
