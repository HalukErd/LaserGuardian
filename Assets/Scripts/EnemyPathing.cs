using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    //config parameters
    WaveConfig waveConfig;
    List<Transform> waypoints;
    
    int wayPointIndex = 0;

	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[wayPointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if(wayPointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPos, movementThisFrame);
            if(transform.position == targetPos)
            {
                wayPointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
