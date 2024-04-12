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
    private Vector3 _position;
    private int _chanceOfSpawn;

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
        SpawnInRandomRange();
    }

    public void SpawnAlongCircle(in Vector3 centerPosition, float radius)
    {
        if (CanSpawnAlongCircle(_chanceOfSpawn) == false)
            return;

        const int MaxAngle = 360;

        int cubesCount = RandomCubesCount;

        float angleStep = MaxAngle / cubesCount * Mathf.Deg2Rad;

        Vector3 SetCurrentCirclePosition(int currentStep)
        {
            float angle = angleStep * currentStep;

            return new Vector3(Mathf.Cos(angle), _spawnHeight, Mathf.Sin(angle)) * radius;
        }

        _chanceOfSpawn /= 2;

        Spawn(SetCurrentCirclePosition, centerPosition, cubesCount);
    }

    private void SpawnInRandomRange()
    {
        Vector3 SetRandomPosition() =>
            new Vector3(Random.Range(_minPosition, _maxPosition), _spawnHeight, Random.Range(_minPosition, _maxPosition));

        Spawn(SetRandomPosition, RandomCubesCount);
    }

    private bool CanSpawnAlongCircle(in int chanceOfSpawn) =>
        RandomUtils.IsSuccess(chanceOfSpawn);

    private void Spawn(System.Func<Vector3> vectorInfo, in int cubesCount)
    {
        for (int i = 0; i < cubesCount; i++)
        {
            _position = vectorInfo.Invoke();

            InteractableCube entity = Instantiate(_entity, _transform.position + _position, Quaternion.identity);

            entity.Init(this);
        }
    }

    private void Spawn(System.Func<int, Vector3> vectorInfo, in Vector3 origin, in int cubesCount)
    {
        for (int i = 0; i < cubesCount; i++)
        {
            _position = vectorInfo.Invoke(i);

            _position.x += origin.x;
            _position.z += origin.z;

            InteractableCube entity = Instantiate(_entity, _position, Quaternion.identity);

            entity.Init(this);
        }
    }
}
