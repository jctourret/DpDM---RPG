using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer instance;
    Transform player;
    public float xOffSet;
    public float yOffSet;
    public float zOffSet;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        player = PlayerController.instance.transform;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + xOffSet, player.transform.position.y + yOffSet, player.transform.position.z + zOffSet);
        transform.rotation = player.transform.rotation;
    }
}
