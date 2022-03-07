using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float nextWaypointDist = 3.0f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody rb;
    public FPSController player;
    public bool GuardCanShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<FPSController>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       rb.AddRelativeForce( Vector3.up * (rb.mass * Mathf.Abs(Physics.gravity.y) ) );


        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint]) - rb.position.normalized;
        Vector3 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        Vector3 relativePos = target.position - transform.position;  
     //replace TARGET with what they should "look at"
     Quaternion rotation = Quaternion.LookRotation(relativePos);
     transform.rotation = rotation;

        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void PlayerNotInStart()
    {

    }

}
