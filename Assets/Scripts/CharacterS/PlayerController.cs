using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Joystick joystick;
    public Joybutton joybutton;
    public Rigidbody rb;
    public Animator playerAnim;
    Interactable interObj;
    string currentScene;
    public bool interacting;
    public float runningSpeed = 5f;
    public float interactionRadius = 2f;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        findControllers();
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
    }
    public void findControllers()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        transform.position = spawnPoint.transform.position;
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }
    void Update()
    {
        rb.velocity = new Vector3(joystick.Horizontal * runningSpeed, rb.velocity.y, joystick.Vertical * runningSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (joystick.Vertical > 0)
            {
                playerAnim.SetBool("walkingForward", true);
            }
            else
            {
                playerAnim.SetBool("walkingBackward", true);
            }
        }
        else
        {
            playerAnim.SetBool("walkingForward", false);
            playerAnim.SetBool("walkingBackward", false);
        }
        if (joybutton.pressed)
        {
            if (!interacting)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, LayerMask.GetMask("Interactable"));
                foreach (Collider element in hits)
                {
                    if (interObj == null || Vector3.Distance(interObj.transform.position, transform.position) > Vector3.Distance(element.transform.position, transform.position))
                    {
                        interObj = element.GetComponent<Interactable>();
                    }
                }
                if (interObj != null)
                {
                    interObj.Interact();
                }
                interacting = true;
            }
        }
        else
        {
            interacting = false;
        }
    }
}
