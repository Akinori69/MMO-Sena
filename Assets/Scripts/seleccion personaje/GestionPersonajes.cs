using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GestionPersonajes : MonoBehaviour
{
    [Header("Interfaz")]

    // Referencias a los elementos de interfaz que muestran el personaje seleccionado
    // y permiten iniciar la partida o eliminarlo.
    public TMP_Text personajeSeleccionadoTexto;
    public Button botonJugar;
    public Button botonEliminar;

    // Conserva la referencia al botón del personaje seleccionado
    // para poder identificarlo y eliminarlo de la interfaz posteriormente.
    private PersonajeUI personajeUISeleccionado;

    // Inicializa el estado de la interfaz al entrar en la escena.
    // Limpia cualquier selección anterior y deshabilita las acciones
    // hasta que el usuario seleccione un personaje.
    private void Start()
    {
        DBManager.LimpiarPersonajeSeleccionado();

        personajeUISeleccionado = null;

        personajeSeleccionadoTexto.text = "Ningún personaje seleccionado";

        botonJugar.interactable = false;
        botonEliminar.interactable = false;
    }

    // Recibe la selección realizada desde un botón de personaje.
    // Guarda sus identificadores en DBManager y actualiza los elementos de interfaz.
    public void SeleccionarPersonaje(PersonajeData personaje, PersonajeUI boton)
    {
        // Almacena el ID y el nombre del personaje seleccionado para utilizarlos
        // en las siguientes operaciones del juego.
        DBManager.SeleccionarPersonaje(
            personaje.id,
            personaje.nombre
        );

        // Conserva la referencia al botón seleccionado.
        personajeUISeleccionado = boton;

        // Actualiza el texto informativo de la interfaz.
        personajeSeleccionadoTexto.text =
            "Personaje seleccionado: " + personaje.nombre;

        // Habilita las acciones disponibles para un personaje seleccionado.
        botonJugar.interactable = true;
        botonEliminar.interactable = true;

        // Registra los datos de la selección para facilitar la depuración.
        Debug.Log("Personaje seleccionado: " + personaje.nombre);
        Debug.Log("ID seleccionado: " + personaje.id);
    }

    // Comprueba que exista un personaje seleccionado antes de iniciar
    // el procedimiento de eliminación en el servidor.
    public void EliminarPersonajeSeleccionado()
    {
        if (!DBManager.PersonajeSeleccionado)
        {
            Debug.LogWarning("No hay ningún personaje seleccionado.");
            return;
        }

        // Inicia la comunicación asíncrona con PHP para eliminar el personaje.
        StartCoroutine(EliminarPersonaje());
    }

    // Envía la solicitud de eliminación al servidor y actualiza la interfaz
    // dependiendo de si la operación fue exitosa o produjo algún error.
    IEnumerator EliminarPersonaje()
    {
        // Recupera el ID del personaje seleccionado y el usuario propietario.
        int idPersonaje = DBManager.idPersonajeSeleccionado;
        string username = DBManager.username;

        // Deshabilita temporalmente el botón para evitar solicitudes repetidas.
        botonEliminar.interactable = false;

        WWWForm form = new WWWForm();

        // Adjunta los datos necesarios para identificar al personaje y verificar su propietario.
        form.AddField("id_personaje", idPersonaje);
        form.AddField("username", username);

        // Ejecuta la solicitud POST al archivo PHP encargado de eliminar el personaje.
        using (UnityWebRequest www = UnityWebRequest.Post(
            "http://localhost/sqlconnect/eliminarPersonaje.php",
            form))
        {
            yield return www.SendWebRequest();

            // Comprueba si ocurrió un error durante la comunicación con el servidor.
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al eliminar personaje: " + www.error);
                botonEliminar.interactable = true;
                yield break;
            }

            // Convierte la respuesta JSON del servidor en un objeto de respuesta.
            string response = www.downloadHandler.text;

            EliminarPersonajeResponse datos =
                JsonUtility.FromJson<EliminarPersonajeResponse>(response);

            // Comprueba que la respuesta recibida tenga un formato válido.
            if (datos == null)
            {
                Debug.LogError("Respuesta inválida del servidor: " + response);
                botonEliminar.interactable = true;
                yield break;
            }

            // El estado 0 confirma que el personaje fue eliminado correctamente en MySQL.
            if (datos.status == 0)
            {
                Debug.Log("Personaje eliminado: " + DBManager.personajeSeleccionado);

                // Elimina de la interfaz el botón correspondiente al personaje eliminado.
                if (personajeUISeleccionado != null)
                {
                    Destroy(personajeUISeleccionado.gameObject);
                }

                // Limpia los datos de selección almacenados para evitar referencias
                // a un personaje que ya no existe en la base de datos.
                DBManager.LimpiarPersonajeSeleccionado();

                personajeUISeleccionado = null;

                // Restablece los elementos visuales y deshabilita las acciones disponibles.
                personajeSeleccionadoTexto.text =
                    "Ningún personaje seleccionado";

                botonJugar.interactable = false;
                botonEliminar.interactable = false;
            }
            else
            {
                // Informa el motivo del fallo y vuelve a habilitar el botón
                // para permitir que el usuario pueda intentar nuevamente.
                Debug.LogError("No se pudo eliminar el personaje: " + datos.mensaje);
                botonEliminar.interactable = true;
            }
        }
    }
}