using Mirror;
using UnityEngine;

public class ReceiveDamage : NetworkBehaviour 
{
    [SerializeField]
    private int maxHealth = 10;

    [SyncVar(hook = nameof(SetHealth))]
    private int _currentHealth;

    [SerializeField]
    private string enemyTag;

    [SerializeField]
    private bool destroyOnDeath;

    [SerializeField]
    private HealthBar healthBar;
    
    private Vector2 _initialPosition;
    private bool _hasHealthBar;

    // Use this for initialization
    private void Awake()
    {
        _hasHealthBar = healthBar != null;
    }

    private void Start () 
    {
        _currentHealth = maxHealth;
        _initialPosition = transform.position;
        if (_hasHealthBar)
        {
            healthBar.SetMaxHealth(_currentHealth);
        }
    }

    private void OnTriggerEnter2D (Collider2D triggeredCollider) 
    {
        if(triggeredCollider.CompareTag(enemyTag)) 
        {
            TakeDamage(1);
            Destroy(triggeredCollider.gameObject);
        }
    }

    private void TakeDamage (int amount) 
    {
        if(isServer) 
        {
            _currentHealth -= amount;

            if(_currentHealth <= 0) 
            {
                if(destroyOnDeath) 
                {
                    Destroy(gameObject);
                } 
                else 
                {
                    _currentHealth = maxHealth;
                    RpcRespawn();
                }
            }
        }
    }

    private void SetHealth(int oldHealth, int newHealth)
    {
        if (_hasHealthBar)
        {
            healthBar.SetHealth(newHealth);
        }
    }

    [ClientRpc]
    private void RpcRespawn () 
    {
        transform.position = _initialPosition;
    }
}