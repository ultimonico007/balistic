using UnityEngine;
using UnityEngine.UI;

public class apuntado : MonoBehaviour
{
    [Header("Sliders")]
    public Slider verticalSlider;     // Apunta hacia arriba/abajo
    public Slider horizontalSlider;   // Gira a izquierda/derecha

    [Header("Rangos de movimiento")]
    public float minVertical = -10f;  // Grados
    public float maxVertical = 60f;

    public float minHorizontal = -90f;
    public float maxHorizontal = 90f;

    private void Update()
    {
        float v = Mathf.Lerp(minVertical, maxVertical, verticalSlider.value);
        float h = Mathf.Lerp(minHorizontal, maxHorizontal, horizontalSlider.value);

        // Rotación final del cañón
        transform.localRotation = Quaternion.Euler(v, h, 0f);
    }
}
