using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    private Camera _camera;

    public event Func<InteractableCube, bool> OnCubeHit;

    private void Start() =>
        _camera = GetComponent<Camera>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        RaycastFromCursor(out InteractableCube cube);

        if (cube == false)
            return;

        OnCubeHit?.Invoke(cube);
    }

    private void RaycastFromCursor(out InteractableCube cube)
    {
        RaycastHit[] hitsInfo = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

        cube = GetCubeOrNull(hitsInfo);
    }

    private InteractableCube GetCubeOrNull(in RaycastHit[] hitsInfo)
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
