using UnityEngine;


public class TargetPiece : MonoBehaviour
{
    public Rigidbody rb;
    public Joint joint; // si tiene joint
    public float fallenYThreshold = 0.5f; // cuánto baja para considerarse derribada


    private Vector3 initialPosition;
    private bool reportedFallen = false;


    private TargetManager manager;


    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (joint == null) joint = GetComponent<Joint>();
        initialPosition = transform.position;
        manager = FindObjectOfType<TargetManager>();
    }


    private void FixedUpdate()
    {
        if (reportedFallen) return;
        if (transform.position.y < initialPosition.y - fallenYThreshold)
        {
            reportedFallen = true;
            manager?.OnPieceFallen(this);
        }


        // También detectar si el joint se rompió (joint == null o !joint.enabled)
        if (!reportedFallen && joint == null)
        {
            reportedFallen = true;
            manager?.OnPieceFallen(this);
        }
    }


    private void OnJointBreak(float breakForce)
    {
        if (!reportedFallen)
        {
            reportedFallen = true;
            manager?.OnPieceFallen(this);
        }
    }
}