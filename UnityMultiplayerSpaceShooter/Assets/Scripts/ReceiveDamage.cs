using System;
using Mirror;
using UnityEngine;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 10;

    [SyncVar(hook = nameof(OnHealthUpdate))] private int _currentHealth;

    [SerializeField] private string enemyTag;

    [SerializeField] private bool destroyOnDeath;

    [SerializeField] private HealthBar healthBar;
    
    [SerializeField] private GameObject carcass;

    private Vector2 _initialPosition;
    private bool _hasHealthBar;
    private static readonly int DeathAniTrigger = Animator.StringToHash("death");
    private Animator _animator;

    // Use this for initialization
    private void Awake()
    {
        _hasHealthBar = healthBar != null;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        _initialPosition = transform.position;
        if (_hasHealthBar)
        {
            healthBar.SetMaxHealth(_currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        if (!triggeredCollider.CompareTag(enemyTag)) return;
        
        var damageToDeal = enemyTag == "Bullet" ? 5 : 1;
            
        TakeDamage(damageToDeal);

        Destroy(triggeredCollider.gameObject);
    }

    private void TakeDamage(int amount)
    {
        if (!isServer) return;
        
        _currentHealth -= amount;

        if (_currentHealth > 0) return;
            
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            _currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    private void OnDestroy()
    {
        if (carcass == null) return;

        var ourTransform = transform;
        var startPosition = ourTransform.position;
        var originalVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        
        var explosion = Instantiate(carcass, startPosition, ourTransform.rotation);
        // TODO: why is velocity always zero?
        explosion.GetComponent<Rigidbody2D>().velocity = originalVelocity;
        
        explosion.GetComponent<Animator>().SetTrigger(DeathAniTrigger);
    }

    private void OnHealthUpdate(int oldHealth, int newHealth)
    {
        if (_hasHealthBar)
        {
            healthBar.SetHealth(newHealth);
        }

        // if (newHealth == 0)
        // {
        //     GetComponent<BoxCollider2D>().enabled = false;
        //
        //     if (_animator != null)
        //     {
        //         _animator.SetTrigger(DeathAniTrigger);
        //     }
        //     else
        //     {
        //         Destroy(gameObject);
        //     }
        // }
    }

    // [ClientRpc]
    // private void RpcOnDeath()
    // {
    //     GetComponent<BoxCollider2D>().enabled = false;
    //
    //     if (_animator != null)
    //     {
    //         _animator.SetTrigger(DeathAniTrigger);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    [ClientRpc]
    private void RpcRespawn()
    {
        transform.position = _initialPosition;
    }
}