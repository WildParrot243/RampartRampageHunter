using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCtrl : MonoBehaviour
{
    [SerializeField] Behaviour[] DisableFeatures;
    [SerializeField] Rigidbody[] RagdollBodies;

    [field: SerializeField, HideInInspector] public bool isRagdolled { get; private set; }

    [ContextMenu("Begin Ragdoll")]

    public void BeginRagdoll()
    {
        if (isRagdolled)
            return;
        foreach (Rigidbody rb in RagdollBodies)
        {
            rb.isKinematic = false;
        }

        foreach(Behaviour mono in DisableFeatures)
        {
            mono.enabled = false;
        }
            isRagdolled = true;
    }

    [ContextMenu("Exit Ragdoll")]
    public void ExitRagdoll()
    {
        if (!isRagdolled)
            return;
        foreach (Rigidbody rb in RagdollBodies)
        {
            rb.isKinematic = true;
        }

        foreach(Behaviour mono in DisableFeatures)
        {
            mono.enabled = true;
        }
            isRagdolled = false;

    }

    public void FreezeRagdoll()
    {
        foreach (Rigidbody rb in RagdollBodies)
        {
            rb.isKinematic = true;
        }
    }

    public void ToggleRagdoll()
    {
        if (isRagdolled)
            ExitRagdoll();
        else
            BeginRagdoll();

    }

}
