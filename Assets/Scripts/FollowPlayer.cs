using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    float currentZoom = 10f;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position - offset * currentZoom;
    }
}
