using UnityEngine;
using UnityEngine.InputSystem;

public class RotateByMouse : MonoBehaviour
{
    public float speed = 10f; // Velocidad de rotación
    private bool isRotating = false; // Bandera para controlar si se está rotando
    private float starMousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current == null)
        {
            //si no hay un ratón conectado, no hacer nada
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame) // Detecta si se presiona el botón izquierdo del ratón
        {
            isRotating = true; // Activa la rotación
            starMousePosition = mousePosition.x; // Guarda la posición inicial del ratón
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame) // Detecta si se suelta el botón izquierdo del ratón
        {
            isRotating = false; // Desactiva la rotación
        }
        else if (isRotating) // Si se está rotando
        {
            float mouseDelta = mousePosition.x - starMousePosition; // Calcula el cambio del ratón
            float rotationAmount = mouseDelta * speed * Time.deltaTime; // Calcula la cantidad de rotación basada en el cambio del ratón y la velocidad
            transform.Rotate(Vector3.down, rotationAmount); // Aplica la rotación al objeto alrededor del eje Y
            starMousePosition = mousePosition.x; // Actualiza la posición inicial del ratón para la siguiente iteración
        }
    }
}
