using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;


public class ShotLogger : MonoBehaviour
{
    public string fileName = "shot_log.csv";
    private string fullPath;
    private float lastLaunchTime;


    private TargetManager targetManager;


    private void Awake()
    {
        fullPath = Path.Combine(Application.persistentDataPath, fileName);
        targetManager = FindObjectOfType<TargetManager>();


        // If file doesn't exist, write headers
        if (!File.Exists(fullPath))
        {
            var header = "timestamp,flight_time,impact_x,impact_y,impact_z,relative_speed,impulse, pieces_down,score";
            File.AppendAllText(fullPath, header + "\n", Encoding.UTF8);
        }
    }


    public void OnProjectileLaunched(BallisticProjectile projectile, float launchTime)
    {
        lastLaunchTime = launchTime;
        if (targetManager != null) targetManager.PrepareForShot();
    }


    public void RegisterImpact(float flightTime, Vector3 impactPoint, float relativeSpeed, float impulse, GameObject hitObject)
    {
        int piecesDown = 0;
        if (targetManager != null)
        {
            piecesDown = targetManager.EvaluateAfterShot();
        }


        int score = CalculateScore(relativeSpeed, piecesDown, impulse);


        string timeStamp = System.DateTime.Now.ToString("s");
        string line = string.Format("{0},{1:F3},{2:F3},{3:F3},{4:F3},{5:F3},{6:F3},{7},{8}",
        timeStamp, flightTime,
        impactPoint.x, impactPoint.y, impactPoint.z,
        relativeSpeed, impulse, piecesDown, score);


        File.AppendAllText(fullPath, line + "\n", Encoding.UTF8);


        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null) ui.ShowShotReport(timeStamp, flightTime, impactPoint, relativeSpeed, impulse, piecesDown, score);
    }


    private int CalculateScore(float relSpeed, int piecesDown, float impulse)
    {
        // Ejemplo simple: combinar factores. Ajustar fórmula según criterio.
        int s = Mathf.Clamp(Mathf.RoundToInt(piecesDown * 100 + impulse * 0.5f + relSpeed * 2f), 0, 10000);
        return s;
    }
}