using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Joybutton joybutton;
    public Rigidbody rb;
    Interactable interObj;
    bool interacting;
    public float runningSpeed = 5f;
    public float interactionRadius = 2f;
    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = new Vector3(joystick.Horizontal*runningSpeed,rb.velocity.y,joystick.Vertical*runningSpeed);
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
