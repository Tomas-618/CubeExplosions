using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explodable : MonoBehaviour
{
    [SerializeField, Min(0)] private float _impulse;
    [SerializeField, Min(0)] private float _radius;

    private Rigidbody _rigidbody;

    private void Reset()
    {
        _impulse = 16;
        _radius = 6;
    }

    private void Start() =>
        _rigidbody = GetComponent<Rigidbody>();

    public void Explode()
    {
        Collider[] others = Physics.OverlapSphere(_rigidbody.position, _radius);

        foreach (Collider other in others)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_impulse, _rigidbody.position, _radius, 0, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
