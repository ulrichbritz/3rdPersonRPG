using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float raduis;
    public string interactableText;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raduis);
    }

    public virtual void Interact(PlayerManager playerManager)
    {
        print("Interacted with object");
    }
}
