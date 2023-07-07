using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : ObstacleSpawner
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private float xAxisOutsideCameraPosition;

    private void Start()
    {
        xAxisOutsideCameraPosition = Random.Range(12, 24);
    }
    protected override void SpawnObject()
    {
        float offsetY = Random.Range(minY, maxY);

        Vector2 pos = new Vector2(this.transform.position.x + xAxisOutsideCameraPosition, this.transform.position.y + offsetY);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
