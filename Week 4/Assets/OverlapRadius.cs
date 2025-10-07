using UnityEngine;

public class OverlapRadius : MonoBehaviour
{
    float radius = 12;
    void ExplosionDamage(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("AddDamage");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
