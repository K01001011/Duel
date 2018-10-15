using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]
	public class PlayerStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int CurHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Constr()
        {
            CurHealth = maxHealth;
        }
    }

    public PlayerStats playerStats = new PlayerStats();

    [SerializeField]
    private Status status;

    void Start()
    {
        playerStats.Constr();
        if (status != null)
        {
            status.SetHealth(playerStats.CurHealth, playerStats.maxHealth);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.CurHealth -= damage;
        if(playerStats.CurHealth <= 0)
        {
            Master.KillPlayer(this);
        }
        status.SetHealth(playerStats.CurHealth, playerStats.maxHealth);
    }

    public void RepairPlayer(int Health)
    {
        playerStats.CurHealth += Health;
        status.SetHealth(playerStats.CurHealth, playerStats.maxHealth);
    }
}
