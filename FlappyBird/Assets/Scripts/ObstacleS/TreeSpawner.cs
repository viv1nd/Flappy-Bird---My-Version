using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : ObstacleSpawner
{
    [SerializeField] private float fixedY = -1;
    private float xAxisOutsideCameraPosition;

    private void Start()
    {
        xAxisOutsideCameraPosition = Random.Range(12, 24);
    }

    protected override void SpawnObject()
    {
        Vector2 pos = new Vector2(this.transform.position.x + xAxisOutsideCameraPosition, fixedY);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
