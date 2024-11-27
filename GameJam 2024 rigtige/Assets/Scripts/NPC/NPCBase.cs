using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    public string npcName; // NPC's name
    public string[] dialogueLines; // Array of dialogue lines for the NPC

    public abstract void Interact(); // Abstract method for interaction logic
}
