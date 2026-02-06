using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{

    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnRange = -1;

    public InputActionAsset inputActions;
    private InputAction clickAction;

    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.FindActionMap("Player").Enable();
        }
    }
    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.FindActionMap("Player").Disable();
        }
    }
    private void Awake()
    {
        targetRb = GetComponent<Rigidbody>();
        if (inputActions != null)
        {
            clickAction = inputActions.FindAction("Click");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickAction != null && clickAction.WasPressedThisFrame())
        {
            CheckForClick();
        }
    }

    private void CheckForClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnRange);
    }
}
