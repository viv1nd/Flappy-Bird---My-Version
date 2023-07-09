using UnityEngine;
/// <summary>
/// It is parent class for all the spawning obstacles
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    public static float gameSpeed = 1f;
    [SerializeField] protected GameObject _prefab;
    [SerializeField, Range(0f, 15f)] private float _time;
    [SerializeField] private int levelLength = 20;
    //[SerializeField] private float _yaxisRange;

    private float _elapsedTime;
    private int _levelStatus = 0;
    private void Update()
    {
        _elapsedTime += Time.deltaTime * gameSpeed;

        if (_elapsedTime > _time)
        {
            SpawnObject();
            _levelStatus++;
            _elapsedTime = 0f;
        }
        
        if(_levelStatus > levelLength)
        {
            gameSpeed = gameSpeed + 0.5f;
            _levelStatus = 0;
        }

    }

    protected virtual void SpawnObject()
    {
        //float offsetY = Random.Range(-_yaxisRange, _yaxisRange);

        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
