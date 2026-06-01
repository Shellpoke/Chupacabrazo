using UnityEngine;
using UnityEngine.Splines;

public class EnemyPath : MonoBehaviour
{
    public SplineContainer path;
    public float speed = 3f;
    [Range(0f, 1f)]
    public float startProgress = 0f;
    private float progress = 0f;

    void Start()
    {
        progress = startProgress; //sets each farmer to its starting position (check in editor)
    }


    void Update()
    {
        if (path == null)
        {
            return;
        }

        progress += speed * Time.deltaTime / path.CalculateLength();

        if (progress > 1f)
        {
            progress = 0f;
        }

        Vector3 position = path.EvaluatePosition(progress);
        Vector3 tangent = path.EvaluateTangent(progress);

        transform.position = position;

        if (tangent != Vector3.zero)
        {
            transform.forward = tangent.normalized;
        }
    }
}