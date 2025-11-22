using UnityEngine;

public class shoot : MonoBehaviour
{
    public Transform spawnPoint;         // Punto donde sale el proyectil
    public GameObject projectilePrefab;  // Prefab del proyectil (con BallisticProjectile)

    public void Fire(float angle, float force, float mass)
    {
        if (spawnPoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Faltan referencias en el cañon.");
            return;
        }

        // Crear el proyectil en la boca del cañón
        GameObject go = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

        // Obtener el script del proyectil
        BallisticProjectile proj = go.GetComponent<BallisticProjectile>();

        if (proj != null)
        {
            proj.Configure(mass);      // Ajustar masa
            proj.Launch(angle, force); // Disparar
        }
        else
        {
            Debug.LogError("El prefab del proyectil no tiene BallisticProjectile.");
        }
    }
}