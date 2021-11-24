using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config Instance { get; private set; }

    [Range(1.5f, 10f)]
    public float towerRotationSpeed = 5f;
    public GameObject bulletPrefab;

    void Awake() => Instance = this;
}