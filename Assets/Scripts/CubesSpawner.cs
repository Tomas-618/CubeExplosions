using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _minCount;
    [SerializeField, Min(0)] private int _maxCount;

    [SerializeField] private ExplodableCube _entity;
    [SerializeField] private CameraRaycaster _raycaster;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;
    [SerializeField] private float _spawnHeight;

    private Transform _transform;
    private int _chanceOfSpawn;

    public int RandomCount => Random.Range(_minCount, _maxCount + 1);

    private void OnValidate()
    {
        if (_maxCount <= _minCount)
            _maxCount = _minCount + 1;

        if (_maxPosition <= _minPosition)
            _maxPosition = _minPosition + 1;
    }

    private void OnEnable() =>
        _raycaster.HittedOnCube += TrySpawnInEntity;

    private void OnDisable() =>
        _raycaster.HittedOnCube -= TrySpawnInEntity;

    private void Start()
    {
        _transform = transform;
        _chanceOfSpawn = RandomUtils.MaxPercent;

        SpawnInRandomRange(RandomCount, _minPosition, _maxPosition);
    }

    public void TrySpawnInEntity(ExplodableCube entity)
    {
        if (CanSpawnAlongCircle(_chanceOfSpawn) == false)
            return;

        int divideFactor = 2;

        int randomCubesCount = RandomCount;

        foreach (ExplodableCube cube in Spawn(entity.transform.position, randomCubesCount))
        {
            Vector3 newScale = entity.transform.localScale / divideFactor;

            cube.transform.localScale = newScale;
        }

        _chanceOfSpawn /= divideFactor;
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

    private IEnumerable<ExplodableCube> Spawn(Vector3 point, int cubesCount)
    {
        if (cubesCount < 0)
            throw new System.ArgumentOutOfRangeException(cubesCount.ToString());

        for (int i = 0; i < cubesCount; i++)
        {
            ExplodableCube entity = Instantiate(_entity, point, Quaternion.identity);

            yield return entity;
        }
    }
}
