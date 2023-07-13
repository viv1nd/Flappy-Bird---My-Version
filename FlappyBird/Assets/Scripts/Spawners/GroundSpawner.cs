using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : ObstacleSpawner
{
    [SerializeField] private float fixedY = -1;
    private float xAxisOutsideCameraPosition;
    

    protected override void Start()
    {
        base.Start();
        xAxisOutsideCameraPosition = 17;
    }

    protected override void SpawnObject()
    {
        Vector2 pos = new Vector2(this.transform.position.x + xAxisOutsideCameraPosition, fixedY);
        
        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
