using UnityEngine;
using TMPro;

public class CargadorNombre : MonoBehaviour
{
    // Referencia al elemento TextMeshPro que mostrará el nombre del usuario
    // o el mensaje de bienvenida en la interfaz.
    public TMP_Text nombreJugador;

    // Se ejecuta una sola vez al inicializar el componente.
    // Establece un texto predeterminado y posteriormente comprueba
    // si existe una sesión iniciada para mostrar el nombre correspondiente.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Asigna un texto de prueba que se muestra cuando no hay una sesión iniciada.
        nombreJugador.text = "prueba";

        // Comprueba el estado de autenticación almacenado en DBManager.
        if (DBManager.LoggedIn)
        {
            // Muestra el nombre de la cuenta que inició sesión.
            nombreJugador.text = "Bienvenido: " + DBManager.username;
        }
    }
}