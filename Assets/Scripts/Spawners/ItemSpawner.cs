using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private int radius;

    [SerializeField] private int spawnCooldown;

    [SerializeField] private int maxItems;
    [SerializeField] private int StartAmount;
    private int currentAmount;

    private bool isWaitingForSpawn;

    void Start()
    {
        currentAmount = transform.childCount;
        isWaitingForSpawn = false;
        if (StartAmount > 0)
        {
            for (int i = 0; i < StartAmount; i++)
                SpawnItem();
        }
    }

    void Update()
    {
        if (!isWaitingForSpawn)
        {
            currentAmount = transform.childCount;
            if (currentAmount < maxItems)
            {
                isWaitingForSpawn = true;
                StartCoroutine(DelayItemSpawn());
            }
        }
    }

    IEnumerator DelayItemSpawn()
    {
        yield return new WaitForSeconds(spawnCooldown);
        SpawnItem();
        isWaitingForSpawn = false;
    }

    private void SpawnItem()
    {
        GameObject newObj = new(itemData.itemName);

        newObj.AddComponent<OverworldItem>();
        newObj.transform.SetPositionAndRotation(GetNewPosition(), transform.rotation);
        newObj.transform.parent = transform;

        newObj.GetComponent<OverworldItem>().SetDetails(itemData, "Pick up");
        currentAmount++;
    }

    private Vector3 GetNewPosition()
    {
        float randX = Random.Range(radius * -1, radius) + transform.position.x;
        float randZ = Random.Range(radius * -1, radius) + transform.position.z;
        float heightAtPoint = Terrain.activeTerrain.SampleHeight(new Vector3(randX, transform.position.y, randZ));
        return new(randX, heightAtPoint, randZ);
    }
}
