using UnityEngine;
using UnityEngine.InputSystem;

public class Mouselook : MonoBehaviour
{

    [Header("Look Settings")]
    public float mouseSensitivity = 1f;
    public Transform playerbody;

    [Header("Look Constraints")]
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private PlayerControls controls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }
    
    void OnEnable()
    {
        controls.Player.Enable();
    }
   
    void OnDisable()
    {
        controls.Player.Disable();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // Apply sense
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Putting a clamp so that you can't rotate the entire way
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Horizontal rotation of the player body
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
