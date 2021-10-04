using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_damage;
    [SerializeField]
    private float speedDefaultRun;
    [SerializeField]
    private float speedRotation;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private GameObject RotationGroup;
    [SerializeField]
    private GameObject swordParticlesObject;
    [SerializeField]
    private GameObject swordCol;

    private Rigidbody2D rb2;
    private Animator anim;
    private ParticleSystem swordParticles;

    private bool swordAttacking;

    public float Speed => speedDefaultRun;

    public float Damage => m_damage;

    private void Awake()
    {
        swordParticles = swordParticlesObject.GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
    }


    public void Run(Vector2 direction)
    {
        rb2.velocity = direction * Speed;
    }

    public void RotateToDirection(Vector2 mouse)
    {
        Vector2 direction = mouse - (Vector2)transform.position;
        if (Vector2.Distance(mouse, transform.position) < minDistance)
        {
            return;
        }
        float angle;
        if (direction.x > 0)
        {
            angle = -Vector3.Angle(Vector3.up, direction);
        }
        else
        {
            angle = Vector3.Angle(Vector3.up, direction);
        }
        RotationGroup.transform.rotation = Quaternion.RotateTowards(RotationGroup.transform.rotation,
        Quaternion.Euler(0, 0, angle), speedRotation * Time.deltaTime);
    }

    public void SwordAttack()
    {
        if (swordAttacking)
        {
            return;
        }
        swordAttacking = true;
        anim.SetTrigger("Attack");
        swordParticles.Play();
        swordCol.SetActive(true);
    }

    public void AttackAnimEnd()
    {
        swordCol.SetActive(false);
        swordAttacking = false;
    }
}
