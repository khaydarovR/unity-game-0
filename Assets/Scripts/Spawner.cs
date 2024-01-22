using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObj;
    [SerializeField] private float maxCount = 1;
    [SerializeField] private float minDelaySec = 1;
    [SerializeField] private float maxDelaySec = 10;
    [SerializeField] private float spawnRadius = 10;
    int objCount = 0;
    private float timer = 0;

    void Start()
    {
        timer = Random.Range(minDelaySec, maxDelaySec);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && objCount < maxCount)
        {
            CreateObject();

            timer = Random.Range(minDelaySec, maxDelaySec);
        }
    }

    private void CreateObject()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = transform.position.y;
        Instantiate(spawnObj, randomPos, Quaternion.identity);
        objCount++;
    }
}
