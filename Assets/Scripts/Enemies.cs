using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int CurHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 10;

        public void Constr()
        {
            CurHealth = maxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();

    public Transform deathParticles;

    [SerializeField]
    private Status status;

    void Start()
    {
        enemyStats.Constr();
        if(status != null)
        {
            status.SetHealth(enemyStats.CurHealth, enemyStats.maxHealth);
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.CurHealth -= damage;
        if (enemyStats.CurHealth <= 0)
        {
            Master.KillEnemy(this);
        }
        if (status != null)
        {
            status.SetHealth(enemyStats.CurHealth, enemyStats.maxHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if(player != null)
        {
            player.DamagePlayer(enemyStats.damage);
            DamageEnemy(10000);
        }
    }
}
