using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _xaxisLimit;

    private void Update()
    {
        this.transform.position -= Vector3.right * _speed * Time.deltaTime;

        if (this.transform.position.x < _xaxisLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
