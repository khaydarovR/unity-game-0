using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObj;
    [SerializeField] private float maxCount = 5;
    [SerializeField] private float timeDelaySec;
    [SerializeField] private float spawnRadius;
    int objCount = 0;
    private float timer = 0;

    void Start()
    {
        timer = timeDelaySec;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && objCount < maxCount)
        {
            CreateObject();

            timer = timeDelaySec;
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
