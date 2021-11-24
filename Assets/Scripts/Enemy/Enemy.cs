using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public enum EnemyType { Normal = 1, Boss = 2 }
    public const float SpawnScale = 0.6f;
    public readonly Vector2 HealthBarOffset = new Vector2(0f, 0.5f);

    //public EnemyType type = EnemyType.Normal;
    public float moveSpeed = 1f;
    public int baseDamage = 1;
    public float maxHealth = 100f;

    Slider healthBar;
    Path path;
    float health;

    public float Health
    {
        get { return health; }
        set
        {
            if (health <= 0f)
                return;
            health = value;
            if (health <= 0f) 
                Die(true);
            healthBar.gameObject.SetActive(health < maxHealth);
            healthBar.value = health / maxHealth;
        }
    }

    void Start()
    {
        health = maxHealth;
        path = new Path(transform.parent.GetComponent<Tile>());

        healthBar = Instantiate(UIManager.Instance.enemyHealthBarPrefab, UIManager.Instance.healthBarsParent);
        healthBar.transform.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + HealthBarOffset);
    }

    void Update()
    {
        if (GameManager.Instance.Paused || !path.Cur) return;
        
        Vector3 direction = path.Cur.transform.position - transform.position;
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, path.Cur.transform.position) < 0.01f)
        {
            transform.position = path.Cur.transform.position;
            path.Next();

            // check if a base tile was reached
            if (path.Cur == null)
                Die(false);
        }

        healthBar.transform.position = Camera.main.WorldToScreenPoint((Vector2) transform.position + HealthBarOffset);
    }

    public void Die(bool destroyed)
    {
        Destroy(gameObject);
        Destroy(healthBar.gameObject);

        // if it was destroyed by a tower, give money
        if (destroyed)
        {
            GameManager.Instance.Money += 10;
        }
        // otherwise, lower player health
        else
        {
            GameManager.Instance.Health -= baseDamage;
        }
    }
}