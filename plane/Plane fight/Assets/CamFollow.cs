using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public List<Transform> targets;
    public Transform targetTransform;
    public float smoothSpeed = 0.2f;
    Vector3 offset = new Vector3(0f, 0f, -30f);

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetCenter()+offset, smoothSpeed);
    }

    Vector3 GetCenter()
    {
        targets.RemoveAll(item => item == null);

        if(targets.Count == 1) {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
