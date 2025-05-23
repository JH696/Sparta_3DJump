using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldCamera : MonoBehaviour
{
    public Transform target; // The target to follow

    private void LateUpdate()
    {
        if (target == null) return;

        float cameraDistanceY = 10f;

        transform.position = new Vector3(target.position.x, target.position.y + cameraDistanceY, target.position.z);
    }
}
