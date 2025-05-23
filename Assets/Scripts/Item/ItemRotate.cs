
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 30f);
    }
}
