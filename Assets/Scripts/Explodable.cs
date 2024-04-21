using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explodable : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start() =>
        _rigidbody = GetComponent<Rigidbody>();

    public void Explode(in float impulse, in float radius)
    {
        Collider[] others = Physics.OverlapSphere(_rigidbody.position, radius);

        foreach (Collider other in others)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(impulse, _rigidbody.position, radius, 0, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
