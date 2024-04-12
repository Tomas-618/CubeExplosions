using System;
using UnityEngine;

public class InteractableCube : MonoBehaviour
{
    [SerializeField] private Explodable _explodable;

    private CubesSpawner _spawner;
    private Transform _transform;

    private void OnMouseDown()
    {
        //float radius = _transform.localScale;
        float radius = 3;

        _spawner.SpawnAlongCircle(_transform.position, radius);
        _explodable.Explode();
    }

    private void Start() =>
        _transform = transform;

    public void Init(CubesSpawner spawner) =>
        _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));

    
}
