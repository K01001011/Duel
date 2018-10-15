using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour {

    public Transform portions;
    public Transform[] SpawnPoints;

    float waitTime = 8f;
    float timeDelay = 3f;

    // Use this for initialization
    void Start () {
        StartCoroutine(Spawner());
	}
	
	// Update is called once per frame
	void Update () {
        waitTime = Random.Range(7f, 13f);
        timeDelay = Random.Range(3f, 6f);
	}

    IEnumerator Spawner()
    {
        while(true)
        {
            Transform sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Transform healthPortion = Instantiate(portions, sp.position, sp.rotation);
            Destroy(healthPortion.gameObject, timeDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
