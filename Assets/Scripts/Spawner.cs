using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnObjs;
    [SerializeField] private List<int> waveEnemyCount;
    [SerializeField] private float spawnRadius = 10;
    [SerializeField] private int waveDelaySec = 10;

    int waveObjCount = 0;
    int currentWaveCount = 0;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    void Update()
    {

    }

    IEnumerator StartWaves()
    {
        while (currentWaveCount < waveEnemyCount.Count)
        {
            CreateObjects(currentWaveCount);
            currentWaveCount++;
            yield return new WaitForSeconds(waveDelaySec);
        }
        yield return null;
    }

    private void CreateObjects(int wave)
    {
        while (waveObjCount < waveEnemyCount[wave])
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = transform.position.y;
            var randNum = Random.Range(0, spawnObjs.Count);
            Instantiate(spawnObjs[randNum], randomPos, Quaternion.identity);
            waveObjCount++;
        }
        waveObjCount = 0;
    }
}