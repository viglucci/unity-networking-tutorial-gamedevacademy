using Mirror;
using UnityEngine;

public class MoveShip : NetworkBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        var movement = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(movement * speed, 0.0f);
    }
}