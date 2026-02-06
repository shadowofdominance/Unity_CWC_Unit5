using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate;

    private void Start()
    {
        StartCoroutine(SpawnTarget());
    }
    private IEnumerator SpawnTarget()
    {
        spawnRate = Random.Range(1, 3);
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
