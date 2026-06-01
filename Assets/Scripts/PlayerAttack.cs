using UnityEngine;

public class PlayerBite : MonoBehaviour
{
    public float biteRange = 10f;
    public int biteDamage = 1;

    void Update()
    {
        if (Input.GetButtonDown("Bite"))
        {
            Bite();
        }
    }

    void Bite()
    {
        Debug.Log("Bite!");

        RaycastHit hit;

        Vector3 rayStart = transform.position + Vector3.up;

        if (Physics.Raycast(rayStart, transform.forward, out hit, biteRange))
        {
            Debug.Log("Hit: " + hit.collider.name);

            GoatBitten GoatBitten = hit.collider.GetComponentInParent<GoatBitten>();

            if (GoatBitten != null)
            {
                GoatBitten.TakeBite(biteDamage);
            }
            else
            {
                Debug.Log("Hit object does not have PreyGoat script.");
            }
        }
        else
        {
            Debug.Log("Bite missed.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 rayStart = transform.position + Vector3.up;

        Gizmos.DrawLine(rayStart, rayStart + transform.forward * biteRange);
    }
}