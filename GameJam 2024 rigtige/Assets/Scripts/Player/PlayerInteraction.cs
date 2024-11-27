using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask npcLayer;

    private NPCBase currentNPC;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.EndDialogue();
            }
            else
            {
                InteractWithNPC();
            }
        }

        if (DialogueManager.Instance.IsDialogueActive && currentNPC != null)
        {
            float distance = Vector2.Distance(transform.position, currentNPC.transform.position);
            if (distance > interactionRange)
            {
                DialogueManager.Instance.EndDialogue();
                currentNPC = null; // Reset the current NPC reference
            }
        }
    }

    private void InteractWithNPC()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, npcLayer);

        foreach (Collider2D hit in hits)
        {
            NPCBase npc = hit.GetComponent<NPCBase>();
            if (npc != null)
            {
                currentNPC = npc;
                npc.Interact();
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
