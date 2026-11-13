using UnityEngine;
using UnityEngine.InputSystem;

public class RotateByMouse : MonoBehaviour
{
    // Velocidad con la que rota el objeto al desplazar el ratón.
    public float speed = 10f;

    // Indica si el usuario mantiene activa la interacción de rotación.
    private bool isRotating = false;

    // Almacena la posición horizontal anterior del ratón para calcular su desplazamiento.
    private float starMousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Se ejecuta en cada frame para detectar la interacción del ratón
    // y aplicar la rotación correspondiente al objeto.
    // Update is called once per frame
    void Update()
    {
        // Comprueba que exista un dispositivo de ratón conectado.
        if (Mouse.current == null)
        {
            // Si no hay un ratón conectado, no realizar ninguna operación.
            return;
        }

        // Recupera la posición actual del cursor en la pantalla.
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Detecta si se presiona el botón izquierdo del ratón por primera vez.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Activa el estado de rotación y registra la posición horizontal inicial.
            isRotating = true;
            starMousePosition = mousePosition.x;
        }
        // Detecta cuándo se libera el botón izquierdo para finalizar la interacción.
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // Desactiva la rotación hasta que se produzca una nueva pulsación.
            isRotating = false;
        }
        // Si el usuario mantiene activa la rotación, calcula el desplazamiento del cursor.
        else if (isRotating)
        {
            // Calcula la diferencia horizontal entre la posición actual y la anterior del ratón.
            float mouseDelta = mousePosition.x - starMousePosition;

            // Convierte el desplazamiento del ratón en un valor de rotación,
            // teniendo en cuenta la velocidad configurada y el tiempo entre frames.
            float rotationAmount = mouseDelta * speed * Time.deltaTime;

            // Aplica la rotación al objeto alrededor de su eje Y local,
            // utilizando el vector descendente como dirección de giro.
            transform.Rotate(Vector3.down, rotationAmount);

            // Actualiza la posición de referencia para calcular el desplazamiento del siguiente frame.
            starMousePosition = mousePosition.x;
        }
    }
}