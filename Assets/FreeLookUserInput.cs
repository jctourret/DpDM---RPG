using Cinemachine;
using UnityEngine;
public class FreeLookUserInput : MonoBehaviour
{
    FixedJoystick joystick;
    public CinemachineFreeLook freeLookCam;

    public float verticalSensitivity = 1;
    public float horizontalSensitivity = 1;

    // Use this for initialization
    void Start()
    {
        joystick = GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        freeLookCam.m_XAxis.m_InputAxisValue = joystick.Horizontal;
        freeLookCam.m_YAxis.m_InputAxisValue = joystick.Vertical;

    }
}