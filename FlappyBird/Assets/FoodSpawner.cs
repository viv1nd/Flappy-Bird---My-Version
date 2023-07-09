using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : ObstacleSpawner
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private float randomYposition;
    private float xAxisOutsideCameraPosition;

    
    private void Start()
        {
            xAxisOutsideCameraPosition = Random.Range(12, 24);
        }

    protected override void SpawnObject()
    {
        randomYposition = Random.Range(minY, maxY);
        Vector2 pos = new Vector2(this.transform.position.x + xAxisOutsideCameraPosition, this.transform.position.y + randomYposition);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);


    }
}

