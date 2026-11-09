using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class cargarPersonajes : MonoBehaviour
{
    //elementos del prefab del boton personaje
    public GameObject botonPersonajePrefab;
    public Transform contenedorPersonajes;
    public void Start()
    {
        Debug.Log("CargarPersonajes: Start() ejecutado");
        Debug.Log("CargarPersonajes: Usuario en DBManager = [" + DBManager.username + "]");
        CallLoadCharacter();
    }
    public void CallLoadCharacter()
    {
        StartCoroutine(LoadingPlayerCharacter());
    }

    IEnumerator LoadingPlayerCharacter()
    {
        WWWForm form = new WWWForm();
        string username = DBManager.username;

        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/consultaPersonaje.php", form))
        {
            yield return www.SendWebRequest();

            // Comprueba si la solicitud fue exitosa
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Character load request failed: " + www.error);
                yield break;
            }

            // Procesa la respuesta del servidor
            string response = www.downloadHandler.text;

            PersonajesResponse datos = JsonUtility.FromJson<PersonajesResponse>(response);

            if (datos.status == 0)
            {
                Debug.Log("Personajes recibidos: " + datos.personajes.Length);

                CrearBotonesPersonajes(datos.personajes);
            }
            else
            {
                Debug.LogError("Error al cargar personajes: " + datos);
            }
        }
    }
    //metodo para crear los botones de los personajes en la UI
    void CrearBotonesPersonajes(PersonajeData[] personajes)
    {
        foreach (PersonajeData personaje in personajes)
        {
            GameObject nuevoBoton = Instantiate(
                botonPersonajePrefab,
                contenedorPersonajes
            );

            PersonajeUI personajeUI = nuevoBoton.GetComponent<PersonajeUI>();

            personajeUI.Configurar(personaje);
        }
    }
}
