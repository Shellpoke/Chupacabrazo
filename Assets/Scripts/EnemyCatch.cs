using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerNearby : MonoBehaviour
{
    public Transform player;
    public float killRange = 20f; //Set hitbox size
    public Vector3 hitboxOffset = Vector3.zero; //Set hitbox offset to match better the model


    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //catch trigger
        if (distanceToPlayer <= killRange)
        {
            Debug.Log("Don Farmer: asi te queria agarrar maldito bellaco!");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 hitboxCenter = transform.position + transform.TransformDirection(hitboxOffset);

        Gizmos.DrawWireSphere(hitboxCenter, killRange);
    }
}