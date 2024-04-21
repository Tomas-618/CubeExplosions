using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class InteractableCube : MonoBehaviour
{
    private Explodable _explodable;

    public Explodable Explodable => _explodable;

    private void Start() =>
        _explodable = GetComponent<Explodable>();
}
