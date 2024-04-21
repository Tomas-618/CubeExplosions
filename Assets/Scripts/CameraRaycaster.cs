using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private CubesSpawner _spawner;

    private Camera _camera;

    private void Start() =>
        _camera = GetComponent<Camera>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo) == false)
            return;

        if (hitInfo.transform.TryGetComponent(out InteractableCube cube) == false)
            return;

        Transform cubeTransform = cube.transform;

        int cubesSpawnRadius = 3;

        if (_spawner.TrySpawnAlongCircle(cubeTransform.position, cubesSpawnRadius) == false)
        {
            int impulseFactor = 8;
            int radiusFactor = 6;

            cube.Explodable.Explode(cubeTransform.localScale.magnitude * impulseFactor,
                cubeTransform.localScale.magnitude * radiusFactor);
        }
    }
}
