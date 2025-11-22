using UnityEngine;
using System.Collections.Generic;


public class TargetManager : MonoBehaviour
{
    private List<TargetPiece> pieces = new List<TargetPiece>();
    private HashSet<TargetPiece> fallen = new HashSet<TargetPiece>();


    private void Awake()
    {
        foreach (var p in FindObjectsOfType<TargetPiece>()) pieces.Add(p);
    }


    public void PrepareForShot()
    {
        fallen.Clear();
        // guardar estados iniciales si se necesita
    }


    public void OnPieceFallen(TargetPiece piece)
    {
        if (!fallen.Contains(piece)) fallen.Add(piece);
    }


    // Después del impacto: espera un pequeño tiempo para que la física se estabilice
    public int EvaluateAfterShot()
    {
        // Aquí podríamos esperar unos frames; para la demo devolvemos la cantidad recogida
        return fallen.Count;
    }
}