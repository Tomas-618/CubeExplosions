using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    private Camera _camera;

    public event Action<ExplodableCube> HittedOnCube;

    private void Start() =>
        _camera = GetComponent<Camera>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        RaycastFromCursor(out ExplodableCube cube);

        if (cube == false)
            return;

        HittedOnCube?.Invoke(cube);
    }

    private void RaycastFromCursor(out ExplodableCube cube)
    {
        RaycastHit[] hitsInfo = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

        cube = GetCubeOrNull(hitsInfo);
    }

    private ExplodableCube GetCubeOrNull(in RaycastHit[] hitsInfo)
    {
        foreach (RaycastHit hitInfo in hitsInfo)
        {
            if (hitInfo.transform.TryGetComponent(out ExplodableCube cube))
            {
                return cube;
            }
        }

        return null;
    }
}
