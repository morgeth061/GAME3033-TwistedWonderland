using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerVolume : MonoBehaviour
{
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public Vector3 GetPositionInBounds()
    {
        if (!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        Bounds boxBounds = boxCollider.bounds;
        return new Vector3(Random.Range(boxBounds.min.x, boxBounds.max.x), transform.position.y, Random.Range(boxBounds.min.z, boxBounds.max.z));

    }

}