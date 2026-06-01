using UnityEngine;

public class PreyGoat : MonoBehaviour
{
    public Transform player;

    public float detectionRange = 20f;
    public float fleeSpeed = 30f;


    public float groundCheckHeight = 5f;
    public float groundOffset = 0.1f;
    public LayerMask groundLayer;
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            RunAwayFromPlayer();
        }

        StickToGround();
    }

    void RunAwayFromPlayer()
    {
        Vector3 directionAway = transform.position - player.position;

        // Ignore up/down direction so the goat only runs across the ground
        directionAway.y = 0f;

        if (directionAway == Vector3.zero)
        {
            return;
        }

        directionAway = directionAway.normalized;

        transform.position += directionAway * fleeSpeed * Time.deltaTime;

        transform.forward = directionAway;
    }

    void StickToGround()
    {
        RaycastHit[] hits;

        Vector3 rayStart = transform.position + Vector3.up * groundCheckHeight;

        Debug.DrawRay(rayStart, Vector3.down * groundCheckHeight * 2f, Color.green);

        hits = Physics.RaycastAll(
            rayStart,
            Vector3.down,
            groundCheckHeight * 2f,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );

        foreach (RaycastHit hit in hits)
        {
            // Ignore this goat's own colliders
            if (hit.collider.transform.root == transform.root)
            {
                continue;
            }

            Vector3 newPosition = transform.position;
            newPosition.y = hit.point.y + groundOffset;
            transform.position = newPosition;

            return;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}