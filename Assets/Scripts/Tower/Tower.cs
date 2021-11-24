using Extensions;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Enemy Target { get; private set; }
    public bool canShoot = true;
    public float range = 2.5f;
    public float shootTime = 0.5f;
    public int damage = 10;
    float lastShootTime;

    void Start()
    {
        lastShootTime = 0f;
    }

    void Update()
    {
        if (GameManager.Instance.Paused) return;
        lastShootTime += Time.deltaTime;

        if (Target && !Target.transform.gameObject.IsDestroyed())
        {
            if (TargetInRange())
            {
                FaceTarget();
                if (canShoot && lastShootTime > shootTime)
                    Shoot();
            }
            else
                Target = null;
        }
        else
            Retarget();
    }

    void FaceTarget()
    {
        float dx = transform.position.x - Target.transform.position.x;
        float dy = transform.position.y - Target.transform.position.y;

        float angle = Mathf.Atan2(-dx, dy) * Mathf.Rad2Deg;
        while (angle < 0f) angle += 360f;
        float z = transform.eulerAngles.z;
        while (z < 0f) z += 360f;

        float newAngle = Mathf.Lerp(z, angle,
            Time.deltaTime * Config.Instance.towerRotationSpeed);

        transform.eulerAngles = new Vector3(0f, 0f, newAngle);
    }

    bool TargetInRange() => Vector2.Distance(Target.transform.position, transform.position) < range;

    void Retarget()
    {
        float closestDistance = range;
        Enemy closestEnemy = null;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        Target = closestEnemy;
    }

    void Shoot()
    {
        lastShootTime = 0f;

        Target.Health -= damage;
    }
}
