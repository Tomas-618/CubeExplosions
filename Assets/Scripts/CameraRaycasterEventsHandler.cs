using UnityEngine;

public class CameraRaycasterEventsHandler : MonoBehaviour
{
    [SerializeField] private CameraRaycaster _entity;

    private void OnEnable() =>
        _entity.HittedOnCube += ExplodeCube;

    private void OnDisable() =>
        _entity.HittedOnCube -= ExplodeCube;

    private void ExplodeCube(ExplodableCube cube)
    {
        Transform cubeTransform = cube.transform;

        int impulseFactor = 8;
        int radiusFactor = 6;

        cube.Explode(cubeTransform.localScale.magnitude * impulseFactor,
            cubeTransform.localScale.magnitude * radiusFactor);
    }
}
