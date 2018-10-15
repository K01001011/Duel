using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public float fireRate = 0;
    public int Damage = 20;
    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public Transform MuzzleTrailPrefab;
    public Transform HitPrefab;
    float timeToSpawn = 0;
    public float spawnRate = 10;
    float timeToFire = 0;

    Transform firePoint;

    // Use this for initialization
    void Awake ()
    {
        firePoint = transform.Find("firePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }


    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemies enemy = hit.collider.GetComponent<Enemies>();
            if(enemy != null)
            {
                enemy.DamageEnemy(Damage);
                //Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
            }
        }
        if (Time.time >= timeToSpawn)
        {
            Vector3 hitPos;
            Vector3 hitNormal;
            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal);
            timeToSpawn = Time.time + 1 / spawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.transform.position, firePoint.transform.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if(lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1,hitPos);
        }
        Destroy(trail.gameObject, 0.04f);

        if(hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 1f);
        }

        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = Random.Range(0.2f, 0.4f);
        audio.Play();
        Transform clone = Instantiate(MuzzleTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(4f, 5f);
        clone.localScale = new Vector3(size * -1, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
