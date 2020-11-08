using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 0.3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);  
    }
    public virtual void Interact()
    {
        Debug.Log("Interactng.");
    }
}
