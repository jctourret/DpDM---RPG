using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Joystick joystick;
    public Joybutton joybutton;
    public Rigidbody rb;
    public Animator playerAnim;
    Interactable interObj;
    public bool interacting;
    public float runningSpeed = 5f;
    public float interactionRadius = 2.2f;
    public float z;
    public float x;
#if UNITY_STANDALONE || UNITY_EDITOR
    public bool isInShop=false;
#endif
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
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
//#if UNITY_ANDROID
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
//#endif
    }
    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * z + transform.right * x;

        playerAnim.SetFloat("XSpeed", x);
        playerAnim.SetFloat("YSpeed", z);
        playerAnim.SetFloat("Magnitude", move.magnitude);


        transform.Translate(move* Time.deltaTime * runningSpeed);
        
        if (Input.GetMouseButtonDown(0)&&!isInShop)
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
#endif
#if UNITY_ANDROID

        Vector3 joystickMove = transform.forward * joystick.Vertical + transform.right * joystick.Horizontal;

        playerAnim.SetFloat("XSpeed", joystick.Horizontal);
        playerAnim.SetFloat("YSpeed", joystick.Vertical);

        playerAnim.SetFloat("Magnitude", joystickMove.magnitude);

        transform.Translate(joystickMove * Time.deltaTime * runningSpeed);

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
#endif
    }
}
