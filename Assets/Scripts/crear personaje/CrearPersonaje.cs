using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class CrearPersonaje : MonoBehaviour
{
    [SerializeField] private TMP_InputField nombreInput;
    [SerializeField] private SeleccionBotonCreacion seleccionBoton;

    public void Crear()
    {
        string nombre = nombreInput.text.Trim();

        if (nombre == "")
        {
            Debug.LogWarning("Debes introducir un nombre para el personaje.");
            return;
        }

        StartCoroutine(CrearPersonajePHP(nombre));
    }

    IEnumerator CrearPersonajePHP(string nombre)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", DBManager.username);
        form.AddField("nombre", nombre);
        form.AddField("clase", seleccionBoton.ClaseSeleccionada);

        using (UnityWebRequest www = UnityWebRequest.Post(
            "http://localhost/sqlconnect/crearPersonaje.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al crear personaje: " + www.error);
                yield break;
            }

            string response = www.downloadHandler.text;
            string[] responseData = response.Split('\t');

            if (responseData.Length > 1 && responseData[0] == "0")
            {
                Debug.Log("Personaje creado: " + responseData[1]);
            }
            else
            {
                Debug.LogWarning("No se pudo crear el personaje: " + response);
            }
        }
    }
}
