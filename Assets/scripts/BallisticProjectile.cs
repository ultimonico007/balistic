using UnityEngine;


public class BallisticProjectile : MonoBehaviour
{
    public Rigidbody rb;
    public float mass = 1f;


    private ShotLogger logger;
    private float launchTime;
    private bool launched = false;


    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        logger = FindObjectOfType<ShotLogger>();
    }


    public void Configure(float mass)
    {
        this.mass = mass;
        if (rb != null) rb.mass = mass;
    }


    // angleDegrees in degrees, force is an arbitrary multiplier (tune in inspector)
    public void Launch(float angleDegrees, float force)
    {
        if (rb == null) return;
        rb.mass = mass;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();


        Vector3 dir = Quaternion.Euler(-angleDegrees, 0f, 0f) * Vector3.forward; // assumes forward is shoot direction
                                                                                 // If 2D or different axis, adapt direction calculation.
        float initialSpeed = force / Mathf.Max(0.01f, mass); // tuneable relationship


        rb.linearVelocity = dir.normalized * initialSpeed;
        launchTime = Time.time;
        launched = true;


        if (logger != null) logger.OnProjectileLaunched(this, launchTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!launched) return;
        launched = false;


        float flightTime = Time.time - launchTime;
        Vector3 contactPoint = collision.contacts.Length > 0 ? collision.contacts[0].point : transform.position;
        float relSpeed = collision.relativeVelocity.magnitude;
        float impulse = collision.impulse.magnitude; // total impulse applied to this rigidbody in the collision


        if (logger != null) logger.RegisterImpact(flightTime, contactPoint, relSpeed, impulse, collision.gameObject);


        // Optionally: destroy proyectile after impact
        Destroy(gameObject, 1f);
    }
}