using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    private Rigidbody _rb;
    private IController _controller;

    public float speed;
    public Vector3 limit;
    private Camera _camera;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _inventory = new Inventory(20, 0);
        _controller = new PlayerController(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _inventory.SortByName();
        if (Input.GetKeyDown(KeyCode.P))
            _inventory.SortByQuantity();
        if (Input.GetKeyDown(KeyCode.L))
            _inventory.SortByType();
    }
    
    private void FixedUpdate()
    {
        _controller.OnUpdate();
    }
    
    public void OnMove(Vector3 dir)
    {
        limit = dir * speed;
        limit = Vector3.ClampMagnitude(limit, speed);
        _rb.AddForce(_camera.transform.forward * limit.z);
        _rb.AddForce( _camera.transform.right * limit.x);
        transform.forward = new Vector3(_camera.transform.forward.x, transform.forward.y, _camera.transform.forward.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<IPickeable>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IPickeable pickeable = other.GetComponent<IPickeable>();
                pickeable.Pick(_inventory);
            }
        }
    }
}
