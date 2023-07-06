
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _time;
    [SerializeField] private float _yaxisRange;

    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _time)
        {
            SpawnObject();

            _elapsedTime = 0f;
        }
    }

    private void SpawnObject()
    {
        float offsetY = Random.Range(-_yaxisRange, _yaxisRange);

        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + offsetY);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
