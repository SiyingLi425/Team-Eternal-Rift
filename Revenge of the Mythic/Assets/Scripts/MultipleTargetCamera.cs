using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();
    public Vector3 offset;
    public float smoothTime = 0.5f;

    private Vector3 velocity;
    private List<GameObject> players;
    private bool ran = false;
    private void Start()
    {

    }
    private void LateUpdate()
    {
        if(ran == false)
        {
            players = GameObject.FindGameObjectsWithTag("Player").ToList();
 
            for (int i = 0; i < players.Count; i++)
            {
                targets.Add(players[i].transform);
            }

            ran = true;
        }
        if(targets.Count == 0)
        {
            return;
        }
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
