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

        RaycastHit[] hitsInfo = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

        InteractableCube cube = TryGetInteractableCube(hitsInfo);

        if (cube == null)
            return;

        Transform cubeTransform = cube.transform;

        if (_spawner.TrySpawnInPoint(cubeTransform.position) == false)
        {
            int impulseFactor = 8;
            int radiusFactor = 6;

            cube.Explodable.Explode(cubeTransform.localScale.magnitude * impulseFactor,
                cubeTransform.localScale.magnitude * radiusFactor);
        }
    }

    private InteractableCube TryGetInteractableCube(in RaycastHit[] hitsInfo)
    {
        foreach (RaycastHit hitInfo in hitsInfo)
        {
            if (hitInfo.transform.TryGetComponent(out InteractableCube cube))
            {
                return cube;
            }
        }

        return null;
    }
}
