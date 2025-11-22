using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Slider angleSlider;
    public Slider forceSlider;
    public Dropdown massDropdown;
    public Button fireButton;
    public Text reportText;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;


    private void Start()
    {
        fireButton.onClick.AddListener(OnFireClicked);
    }


    private void OnFireClicked()
    {
        float angle = angleSlider != null ? angleSlider.value : 45f;
        float force = forceSlider != null ? forceSlider.value : 500f;
        float mass = 1f;


        if (massDropdown != null)
        {
            // assume options like "0.1", "0.5", "1", "2"
            float.TryParse(massDropdown.options[massDropdown.value].text, out mass);
        }


        SpawnAndLaunch(angle, force, mass);
    }


    private void SpawnAndLaunch(float angle, float force, float mass)
    {
        if (projectilePrefab == null || projectileSpawnPoint == null) return;
        var go = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        var proj = go.GetComponent<BallisticProjectile>();
        if (proj != null)
        {
            proj.Configure(mass);
            proj.Launch(angle, force);
        }
    }


    public void ShowShotReport(string timeStamp, float flightTime, Vector3 impactPoint, float relSpeed, float impulse, int piecesDown, int score)
    {
        if (reportText == null) return;
        reportText.text = string.Format(
        "Tiro: {0}\nTiempo de vuelo: {1:F2}s\nImpacto: ({2:F2}, {3:F2}, {4:F2})\nVel. Rel: {5:F2} m/s\nImpulso: {6:F2}\nPiezas derribadas: {7}\nPuntuación: {8}",
        timeStamp, flightTime, impactPoint.x, impactPoint.y, impactPoint.z, relSpeed, impulse, piecesDown, s