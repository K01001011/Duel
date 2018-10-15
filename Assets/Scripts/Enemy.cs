using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemy : MonoBehaviour {

    public Transform target;

    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathOver = false;

    public float nextPointDist = 3;

    private int currentWayPoint = 0;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.Log("No Target");
            this.enabled = false;
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            this.enabled = false;
            yield return false;
        }
        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        

        yield return new WaitForSeconds(1f/updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        if (path == null)
            return;

        if(currentWayPoint >= path.vectorPath.Count)
        {
            if (pathOver)
                return;
            pathOver = true;
            return;
        }
        pathOver = false;

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);

        if(Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nextPointDist)
        {
            currentWayPoint++;
            return;
        }
    }
}
