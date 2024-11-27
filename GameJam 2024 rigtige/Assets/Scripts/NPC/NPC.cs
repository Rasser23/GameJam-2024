using UnityEngine;

public class NPC : NPCBase
{
    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(npcName, dialogueLines);
    }
}
