using System.Collections;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject Flower;
    [SerializeField] private int radius;

    [SerializeField] private int spawnCooldown;

    [SerializeField] private int maxFlowers;
    private int currentAmountFlowers;

    private bool isWaitingForSpawn;

    void Start()
    {
        currentAmountFlowers = transform.childCount;
        isWaitingForSpawn = false;
    }

    void Update()
    {
        if (!isWaitingForSpawn)
        {
            currentAmountFlowers = transform.childCount;
            if (currentAmountFlowers < maxFlowers)
            {
                isWaitingForSpawn = true;
                StartCoroutine(DelayFlowerSpawn());
            }
        }
    }

    IEnumerator DelayFlowerSpawn()
    {
        yield return new WaitForSeconds(spawnCooldown);
        SpawnFlower();
        isWaitingForSpawn = false;
    }

    private void SpawnFlower()
    {
        Instantiate(Flower, GetNewPosition(), transform.rotation, transform);
        currentAmountFlowers++;
    }

    private Vector3 GetNewPosition()
    {
        float randX = Random.Range(radius * -1, radius) + transform.position.x;
        float randZ = Random.Range(radius * -1, radius) + transform.position.z;
        float heightAtPoint = Terrain.activeTerrain.SampleHeight(new Vector3(randX, transform.position.y, randZ));
        return new(randX, heightAtPoint, randZ);
    }
}
