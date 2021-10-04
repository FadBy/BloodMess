using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_strong;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float speedDefault;
    [SerializeField]
    private float m_maxHealth;
    private float m_currentHealth;

    private HealthBar healthBar;

    private Player player;
    private Rigidbody2D rb;
    private Transform target;
    private SpriteRenderer sprite;
    
    public float Speed => speedDefault;

    public float Strong => m_strong;

    private float DistanceFromPlayer => Vector2.Distance(transform.position, player.transform.position);

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = Loader.Instance.Player.GetComponent<Player>();
        target = player.transform;
    }

    private void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    private void Update()
    {
        if (DistanceFromPlayer <= attackRange)
        {
            GoToTarget(target);
        }
        else
        {
            Idle();
        }
    }

    private void GoToTarget(Transform target)
    {
        Vector2 direction = target.transform.position - transform.position;
        rb.velocity = direction * Speed;
        sprite.flipX = direction.x < 0;
    }

    private void Idle()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            TakeDamage(LevelManager.Instance.Player.Damage);
        }
    }

    private void TakeDamage(float hp)
    {
        m_currentHealth -= hp;
        if (m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        healthBar.ChangeHealth(m_maxHealth, m_currentHealth);
    }
}
