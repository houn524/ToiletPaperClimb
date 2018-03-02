using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcokSpawnManager : MonoBehaviour {

    public float spawnInterval = 1.0f;
    public GameObject[] rockPrefabs;

    private ToiletPaper toiletPaper;

    private bool spawnDelay = false;

    private float randomInterval;
    private float spawnLength;

	// Use this for initialization
	void Start () {
        toiletPaper = GameObject.Find("ToiletPapers").GetComponent<ToiletPaper>() as ToiletPaper;
	}
	
	// Update is called once per frame
	void Update () {
        if(!spawnDelay)
        {
            Instantiate(rockPrefabs[Random.Range(0, rockPrefabs.Length - 1)],
                new Vector3(Random.Range(-1.0f, 1.0f), 2.75f), Quaternion.identity);

            randomInterval = Random.Range(((spawnInterval - 3.0f) >= 0) ? spawnInterval - 3.0f : 0, spawnInterval + 3.0f);
            spawnLength = toiletPaper.leftLength - randomInterval;
            spawnDelay = true;
        }
        else
        {
            if (toiletPaper.leftLength <= spawnLength)
                spawnDelay = false;
        }
	}
}
