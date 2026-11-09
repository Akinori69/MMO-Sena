using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class cargarPersonajes : MonoBehaviour
{
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
            string[] responseData = response.Split('\t');
            Debug.Log("CargarPersonajes: Respuesta PHP = " + response);

            if (responseData.Length > 1 && responseData[0] == "0")
            {
                Debug.Log("Character load successful");

                DBManager.personajeSeleccionado = responseData[1];
                // Carga la escena "juego"
                //SceneManager.LoadScene("juego");
            }
        }
    }
}
