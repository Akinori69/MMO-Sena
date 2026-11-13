using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class CrearPersonaje : MonoBehaviour
{
    // Referencias a los elementos de interfaz utilizados durante la creación:
    // campo de texto para el nombre y controlador de selección de clase.
    [SerializeField] private TMP_InputField nombreInput;
    [SerializeField] private SeleccionBotonCreacion seleccionBoton;

    // Método asociado al botón de creación de personaje.
    // Valida el nombre introducido antes de iniciar la solicitud al servidor.
    public void Crear()
    {
        // Recupera el nombre introducido y elimina los espacios sobrantes.
        string nombre = nombreInput.text.Trim();

        // Impide iniciar la creación si el campo de nombre está vacío.
        if (nombre == "")
        {
            Debug.LogWarning("Debes introducir un nombre para el personaje.");
            return;
        }

        // Inicia la corrutina que envía los datos del nuevo personaje a PHP.
        StartCoroutine(CrearPersonajePHP(nombre));
    }

    // Envía al servidor los datos necesarios para registrar el nuevo personaje
    // y procesa la respuesta de la operación.
    IEnumerator CrearPersonajePHP(string nombre)
    {
        WWWForm form = new WWWForm();

        // Adjunta el usuario propietario, el nombre del personaje
        // y la clase seleccionada en la interfaz.
        form.AddField("username", DBManager.username);
        form.AddField("nombre", nombre);
        form.AddField("clase", seleccionBoton.ClaseSeleccionada);

        // Realiza la solicitud POST al archivo PHP encargado de crear el personaje.
        using (UnityWebRequest www = UnityWebRequest.Post(
            "http://localhost/sqlconnect/crearPersonaje.php", form))
        {
            yield return www.SendWebRequest();

            // Comprueba si ocurrió un error durante la comunicación con el servidor.
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al crear personaje: " + www.error);
                yield break;
            }

            // Recupera la respuesta del servidor y separa el código de estado
            // del mensaje informativo utilizando el tabulador como delimitador.
            string response = www.downloadHandler.text;
            string[] responseData = response.Split('\t');

            // El código 0 indica que el personaje fue creado correctamente.
            if (responseData.Length > 1 && responseData[0] == "0")
            {
                Debug.Log("Personaje creado: " + responseData[1]);
            }
            else
            {
                // Informa el motivo del fallo devuelto por el servidor PHP.
                Debug.LogWarning("No se pudo crear el personaje: " + response);
            }
        }
    }
}