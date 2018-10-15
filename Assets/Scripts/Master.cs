using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour {

    public static Master master;
    public GameObject GameOver;
    public GameObject NextLevel;
    public new AudioSource audio;
    public Text text;

    public static void EndGame()
    {
        master.GameOver.SetActive(true);
    }

    public static void CompletedGame()
    {
        master.NextLevel.SetActive(true);
    }

    private void Awake()
    {
        if(master == null)
        {
            master = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Master>();
        }
        master.text.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        //GameOver
        EndGame();
    }

    public static void KillEnemy(Enemies enemy)
    {
        Transform clone = Instantiate(enemy.deathParticles, enemy.transform.position, Quaternion.identity) as Transform;
        master.audio.Play();
        Destroy(clone.gameObject, 5f);
        Destroy(enemy.gameObject);
        int score = int.Parse(master.text.text);
        score++;
        master.text.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
    }

    public static void DestroyPortion(OnCollect portion)
    {
        Destroy(portion.gameObject);
    }
}
