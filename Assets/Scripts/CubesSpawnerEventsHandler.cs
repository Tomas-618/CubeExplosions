using UnityEngine;

public class CubesSpawnerEventsHandler : MonoBehaviour
{
    [SerializeField] private CubesSpawner _entity;

    private void OnEnable() =>
        _entity.OnFailToSpawnInCube += OnFailToSpawn;

    private void OnDisable() =>
        _entity.OnFailToSpawnInCube -= OnFailToSpawn;

    private void OnFailToSpawn(InteractableCube cube)
    {
        Transform cubeTransform = cube.transform;

        int impulseFactor = 8;
        int radiusFactor = 6;

        cube.Explodable.Explode(cubeTransform.localScale.magnitude * impulseFactor,
            cubeTransform.localScale.magnitude * radiusFactor);
    }
}
