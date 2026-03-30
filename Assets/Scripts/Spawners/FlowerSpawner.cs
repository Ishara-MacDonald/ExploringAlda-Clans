using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject Flower;
    [SerializeField] private int radius;

    [SerializeField] private int spawnCooldown;
    [SerializeField] private float countDown;

    [SerializeField] private int maxFlowers;
    private int currentAmountFlowers;

    private bool isWaitingForSpawn;

    void Start()
    {
        Instantiate(Flower, GetNewPosition(), transform.rotation, transform);
        Instantiate(Flower, GetNewPosition(), transform.rotation, transform);
        Instantiate(Flower, GetNewPosition(), transform.rotation, transform);
        Instantiate(Flower, GetNewPosition(), transform.rotation, transform);
        isWaitingForSpawn = false;
    }

    void Update()
    {

    }

    private Vector3 GetNewPosition()
    {
        float randX = Random.Range(radius * -1, radius) + transform.position.x;
        float randZ = Random.Range(radius * -1, radius) + transform.position.z;
        return new(randX, transform.position.y, randZ);
    }
}
