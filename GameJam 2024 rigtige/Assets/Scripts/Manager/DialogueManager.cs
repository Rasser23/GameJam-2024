using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialogueUI; // UI Panel for dialogue
    [SerializeField] private Text npcNameText; // UI Text for NPC's name
    [SerializeField] private Text dialogueText; // UI Text for dialogue lines

    private int currentLineIndex;
    private string[] currentDialogueLines;
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool IsDialogueActive => isDialogueActive;

    public void StartDialogue(string npcName, string[] dialogueLines)
    {
        dialogueUI.SetActive(true);
        npcNameText.text = npcName;
        currentDialogueLines = dialogueLines;
        currentLineIndex = 0;
        isDialogueActive = true;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < currentDialogueLines.Length)
        {
            dialogueText.text = currentDialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
    }
}
