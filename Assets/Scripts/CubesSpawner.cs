using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _minCubesCount;
    [SerializeField, Min(0)] private int _maxCubesCount;

    [SerializeField] private InteractableCube _entity;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;
    [SerializeField] private float _spawnHeight;

    private Transform _transform;
    private int _chanceOfSpawn;
    private int _cubeDivideFactor;

    public int RandomCubesCount => Random.Range(_minCubesCount, _maxCubesCount + 1);

    private void OnValidate()
    {
        if (_maxCubesCount <= _minCubesCount)
            _maxCubesCount = _minCubesCount + 1;

        if (_maxPosition <= _minPosition)
            _maxPosition = _minPosition + 1;
    }

    private void Start()
    {
        _transform = transform;
        _chanceOfSpawn = RandomUtils.MaxPercent;
        _cubeDivideFactor = 2;

        SpawnInRandomRange(RandomCubesCount, _minPosition, _maxPosition);
    }

    public bool TrySpawnInPoint(Vector3 point)
    {
        if (CanSpawnAlongCircle(_chanceOfSpawn) == false)
            return false;

        int randomCubesCount = RandomCubesCount;

        foreach (InteractableCube entity in Spawn(point, randomCubesCount))
            entity.transform.localScale /= _cubeDivideFactor;

        _chanceOfSpawn /= 2;
        _cubeDivideFactor *= 2;

        return true;
    }

    private bool CanSpawnAlongCircle(in int chanceOfSpawn) =>
        RandomUtils.IsSuccess(chanceOfSpawn);

    private void SpawnInRandomRange(in int cubesCount, float minPosition, float maxPosition)
    {
        Spawn(() => _transform.position + new Vector3(Random.Range(minPosition, maxPosition),
            _spawnHeight,
            Random.Range(minPosition, maxPosition)),
            cubesCount);
    }

    private void Spawn(System.Func<Vector3> pointInfo, in int cubesCount)
    {
        if (cubesCount < 0)
            throw new System.ArgumentOutOfRangeException(cubesCount.ToString());

        for (int i = 0; i < cubesCount; i++)
            Instantiate(_entity, pointInfo.Invoke(), Quaternion.identity);
    }

    private IEnumerable<InteractableCube> Spawn(Vector3 point, int cubesCount)
    {
        if (cubesCount < 0)
            throw new System.ArgumentOutOfRangeException(cubesCount.ToString());

        for (int i = 0; i < cubesCount; i++)
        {
            InteractableCube entity = Instantiate(_entity, point, Quaternion.identity);

            yield return entity;
        }
    }
}
