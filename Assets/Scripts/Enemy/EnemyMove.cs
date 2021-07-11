using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Vector3[] Waypoints;

    //loop through waypoints or stop when we hit end?
    [SerializeField] bool Loop = true;

    //speed to move, essentially units per second
    [SerializeField] float speed = 2.0f;

    //current index
    private int CurrentIdx = 0;
    private Vector3 CurrentWaypoint = Vector3.zero;
    private bool isOnHold = false;
    private bool isGettingNextPoint = false;

    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        CurrentWaypoint = Waypoints[CurrentIdx];
    }

    // Update is called once per frame
    void Update()
    {
        
        //if we are supposed to move
        if (CurrentWaypoint != Vector3.zero)
        {
            if (!isOnHold)
            {
                //check if we have not hit the next waypoint yet
                if (transform.position != CurrentWaypoint)
                {
                    //move towards current waypoint
                    transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint, speed * Time.deltaTime);
                    enemyManager.GetAnimator().SetFloat("Speed", speed * Time.deltaTime);
                }
                else
                {
                    if (!isGettingNextPoint)
                    {
                        //current waypoint hit, get next point
                        StartCoroutine(GetNextWaypoint());
                    }
                    enemyManager.GetAnimator().SetFloat("Speed", 0f);
                }

            } else
            {
                enemyManager.GetAnimator().SetFloat("Speed", 0f);
            }

        }
        else
        {
            isStopped();
        }
    }

    IEnumerator GetNextWaypoint()
    {
        isOnHold = true;
        isGettingNextPoint = true;
        yield return new WaitForSeconds(3);
        //increment the current index
        CurrentIdx++;

        //if current index is greater than the length of our waypoints
        if (CurrentIdx >= Waypoints.Length)
        {
            //if looping, reset currentIndex
            if (Loop)
            {
                CurrentIdx = CurrentIdx % Waypoints.Length;
            }
        }

        //now, if there is a waypoint
        if (CurrentIdx < Waypoints.Length)
        {
            //get next waypoint
            CurrentWaypoint = Waypoints[CurrentIdx];
            //also likely set direction here as well from current position to next waypoint we just pulled
        }
        else
        {
            //reset current waypoint to zero so update doesn't keep processing
            CurrentWaypoint = Vector3.zero;
        }

        isOnHold = false;
        isGettingNextPoint = false;
        if (CurrentWaypoint.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        
    }

    public void Stop ()
    {
        isOnHold = true;
    }

    public void Continue ()
    {
        isOnHold = false;
    }

    public bool isStopped ()
    {
        return isGettingNextPoint;
    }
}
