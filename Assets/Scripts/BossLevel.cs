using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour {

    public static BossLevel bossLevel;
    public Transform enemy;
    public Transform SpawnPoint;

    public void Awake()
    {
        if (bossLevel == null)
        {
            bossLevel = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<BossLevel>();
        }
    }

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            enabled = false;
        }
    }
	
    public static void SpawnBoss()
    {
        Instantiate(bossLevel.enemy, bossLevel.SpawnPoint.position, bossLevel.SpawnPoint.rotation);
        bossLevel.enabled = false;
    }
}
