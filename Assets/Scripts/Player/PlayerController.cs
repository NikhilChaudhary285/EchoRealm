using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody _rb;
    private bool _isGrounded;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * moveSpeed;
        _rb.linearVelocity = new Vector3(move.x, _rb.linearVelocity.y, move.z);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision col) => _isGrounded = true;
    private void OnCollisionExit(Collision col) => _isGrounded = false;
}