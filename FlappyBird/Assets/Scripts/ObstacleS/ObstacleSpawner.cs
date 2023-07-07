using UnityEngine;
/// <summary>
/// It is parent class for all the spawning obstacles
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] protected GameObject _prefab;
    [SerializeField, Range(0f,5f)] private float _time;
    //[SerializeField] private float _yaxisRange;

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
    
    protected virtual void SpawnObject()
    {
        //float offsetY = Random.Range(-_yaxisRange, _yaxisRange);

        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
