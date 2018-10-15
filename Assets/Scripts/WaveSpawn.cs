using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawn : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] wave;
    private int nextWave = 0;

    public Transform[] SpawnPoints;

    public Transform[] Platforms;

    public float timeBetweenWaves = 10f;
    public float waveCountdown;
    public float bossCountDown = 4f;

    int scene = 0;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        wave[0].rate = 1;
        if (SceneManager.GetActiveScene().name == "duel")
            scene = 1;
        else if (SceneManager.GetActiveScene().name == "Level 1")
            scene = 2;
        else if (SceneManager.GetActiveScene().name == "Level 2")
            scene = 3;
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            enabled = false;
        }
        if(state == SpawnState.WAITING)
        {
            NextWave();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(wave[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void NextWave()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 > wave.Length - 1)
        {
            //Level Over
            Debug.Log("WAVE OVER");
            if (scene == 1)
                nextWave = 0;
            else if(scene == 2 || scene == 3)
            {
                state = SpawnState.WAITING;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    Master.CompletedGame();
                    enabled = false;
                }
                else
                    return;
            }
            else if(scene == 0)
            {
                //Call Boss
                state = SpawnState.WAITING;
                if (bossCountDown <= 0)
                {
                    bossCountDown = 50;
                    for (int i = 0; i < Platforms.Length; i++)
                        Destroy(Platforms[i].gameObject);
                    BossLevel.SpawnBoss();
                }
                else if(bossCountDown == 50)
                {
                    state = SpawnState.WAITING;
                    if (GameObject.FindGameObjectWithTag("Enemy") == null)
                    {
                        Master.CompletedGame();
                        enabled = false;
                    }
                    else
                        return;
                }
                else
                    bossCountDown -= Time.deltaTime;
            }
        }
        nextWave++;
        wave[nextWave].count = wave[nextWave - 1].count + 1; 
        wave[nextWave].rate = wave[nextWave - 1].rate + 0.2f;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform sp = SpawnPoints[ Random.Range(0, SpawnPoints.Length) ];
        Instantiate(enemy, sp.position, sp.rotation);
    }
}
