using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Sets distance of camera from player, can be edited on inspector.
    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);
    private Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        // Gets information about the player's transform component.
        Target = GameObject.Find("GoatVanilla").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Sets camera behind the target.
        this.transform.position = Target.TransformPoint(CamOffset);
        this.transform.LookAt(Target);
    }
}
