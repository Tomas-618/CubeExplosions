using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private void Start() =>
        GetComponent<MeshRenderer>().material.color = RandomUtils.GetRandomColor();
}
