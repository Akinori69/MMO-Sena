using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CargarPersonajes : MonoBehaviour
{
    // Referencias al prefab del botón de personaje y al contenedor
    // donde se generarán dinámicamente los personajes disponibles.
    public GameObject botonPersonajePrefab;
    public Transform contenedorPersonajes;

    // Controlador encargado de gestionar la selección de personajes.
    public GestionPersonajes gestionPersonajes;

    // Al iniciar el componente, muestra información de depuración
    // y solicita automáticamente los personajes asociados al usuario.
    public void Start()
    {
        Debug.Log("CargarPersonajes: Start() ejecutado");
        Debug.Log("CargarPersonajes: Usuario en DBManager = [" + DBManager.username + "]");

        CallLoadCharacter();
    }

    // Inicia la corrutina encargada de consultar los personajes en PHP.
    public void CallLoadCharacter()
    {
        StartCoroutine(LoadingPlayerCharacter());
    }

    // Envía el nombre del usuario al servidor y procesa la lista de personajes recibida.
    IEnumerator LoadingPlayerCharacter()
    {
        WWWForm form = new WWWForm();

        // Recupera el usuario almacenado durante el inicio de sesión.
        string username = DBManager.username;

        // Adjunta el nombre de usuario a la solicitud POST.
        form.AddField("username", username);

        // Realiza la consulta al archivo PHP encargado de recuperar los personajes.
        using (UnityWebRequest www = UnityWebRequest.Post(
            "http://localhost/sqlconnect/consultaPersonaje.php", form))
        {
            yield return www.SendWebRequest();

            // Comprueba si ocurrió algún error durante la comunicación con el servidor.
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Character load request failed: " + www.error);
                yield break;
            }

            // Convierte la respuesta JSON del servidor en un objeto de tipo PersonajesResponse.
            string response = www.downloadHandler.text;

            PersonajesResponse datos =
                JsonUtility.FromJson<PersonajesResponse>(response);

            // Verifica que la respuesta sea válida y que PHP haya devuelto el estado exitoso.
            if (datos != null && datos.status == 0)
            {
                Debug.Log("Personajes recibidos: " + datos.personajes.Length);

                // Genera un botón de interfaz por cada personaje recuperado de la base de datos.
                CrearBotonesPersonajes(datos.personajes);
            }
            else
            {
                // Informa si el servidor devolvió un error o una respuesta inesperada.
                Debug.LogError("Error al cargar personajes: " + response);
            }
        }
    }

    // Crea dinámicamente los botones de los personajes y configura sus datos visuales
    // y su conexión con el controlador de selección.
    void CrearBotonesPersonajes(PersonajeData[] personajes)
    {
        foreach (PersonajeData personaje in personajes)
        {
            // Instancia el prefab dentro del contenedor de la interfaz.
            GameObject nuevoBoton = Instantiate(
                botonPersonajePrefab,
                contenedorPersonajes
            );

            // Recupera el componente encargado de mostrar la información del personaje.
            PersonajeUI personajeUI =
                nuevoBoton.GetComponent<PersonajeUI>();

            // Transfiere los datos del personaje y la referencia al gestor de selección.
            personajeUI.Configurar(personaje, gestionPersonajes);
        }
    }
}