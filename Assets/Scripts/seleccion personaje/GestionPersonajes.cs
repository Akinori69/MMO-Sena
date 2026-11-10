using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GestionPersonajes : MonoBehaviour
{
    [Header("Interfaz")]
    public TMP_Text personajeSeleccionadoTexto;
    public Button botonJugar;
    public Button botonEliminar;

    private PersonajeUI personajeUISeleccionado;

    private void Start()
    {
        DBManager.LimpiarPersonajeSeleccionado();

        personajeUISeleccionado = null;

        personajeSeleccionadoTexto.text = "Ningún personaje seleccionado";

        botonJugar.interactable = false;
        botonEliminar.interactable = false;
    }

    // Seleccionar un personaje desde su prefab
    public void SeleccionarPersonaje(PersonajeData personaje, PersonajeUI boton)
    {
        DBManager.SeleccionarPersonaje(
            personaje.id,
            personaje.nombre
        );

        personajeUISeleccionado = boton;

        personajeSeleccionadoTexto.text =
            "Personaje seleccionado: " + personaje.nombre;

        botonJugar.interactable = true;
        botonEliminar.interactable = true;

        Debug.Log("Personaje seleccionado: " + personaje.nombre);
        Debug.Log("ID seleccionado: " + personaje.id);
    }

    // Eliminar el personaje seleccionado
    public void EliminarPersonajeSeleccionado()
    {
        if (!DBManager.PersonajeSeleccionado)
        {
            Debug.LogWarning("No hay ningún personaje seleccionado.");
            return;
        }

        StartCoroutine(EliminarPersonaje());
    }

    IEnumerator EliminarPersonaje()
    {
        int idPersonaje = DBManager.idPersonajeSeleccionado;
        string username = DBManager.username;

        botonEliminar.interactable = false;

        WWWForm form = new WWWForm();

        form.AddField("id_personaje", idPersonaje);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(
            "http://localhost/sqlconnect/eliminarPersonaje.php",
            form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al eliminar personaje: " + www.error);
                botonEliminar.interactable = true;
                yield break;
            }

            string response = www.downloadHandler.text;

            EliminarPersonajeResponse datos =
                JsonUtility.FromJson<EliminarPersonajeResponse>(response);

            if (datos == null)
            {
                Debug.LogError("Respuesta inválida del servidor: " + response);
                botonEliminar.interactable = true;
                yield break;
            }

            if (datos.status == 0)
            {
                Debug.Log("Personaje eliminado: " + DBManager.personajeSeleccionado);

                // Eliminar el botón del personaje de la interfaz
                if (personajeUISeleccionado != null)
                {
                    Destroy(personajeUISeleccionado.gameObject);
                }

                // Limpiar selección
                DBManager.LimpiarPersonajeSeleccionado();

                personajeUISeleccionado = null;

                personajeSeleccionadoTexto.text =
                    "Ningún personaje seleccionado";

                botonJugar.interactable = false;
                botonEliminar.interactable = false;
            }
            else
            {
                Debug.LogError("No se pudo eliminar el personaje: " + datos.mensaje);
                botonEliminar.interactable = true;
            }
        }
    }
}
