using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    CharacterController controller;
    public Joystick joystick;
    public Joybutton joybutton;
    public Animator playerAnim;
    Interactable interObj;
    public bool interacting;
    public float interactionRadius = 2.2f;

    public float runningSpeed = 5f;
    float z;
    float x;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool noFallFirstFrame;
    private float gravityValue = -9.81f;

    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

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
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void SetToSpawnPoint()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        noFallFirstFrame = true;
    }
    public void findControllers()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }
    void Update()
    {
        float angle;
        float targetAngle;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

#if UNITY_STANDALONE || UNITY_EDITOR
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(x,0,z).normalized;

        playerAnim.SetFloat("XSpeed", move.x);
        playerAnim.SetFloat("YSpeed", move.z);
        playerAnim.SetFloat("Magnitude", move.magnitude);


        if(move.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0.0f,targetAngle,0.0f)*Vector3.forward;


            controller.Move(moveDir.normalized * Time.deltaTime * runningSpeed);
        }

        if (!noFallFirstFrame)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            noFallFirstFrame = false;
        }

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

        //playerAnim.SetFloat("XSpeed", joystick.Horizontal);
        //playerAnim.SetFloat("YSpeed", joystick.Vertical);
        //
        //playerAnim.SetFloat("Magnitude", joystickMove.magnitude);

        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

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

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,transform.forward * 3,Color.red);
        Debug.DrawRay(transform.position, transform.right * 3,Color.yellow);
    }
}
