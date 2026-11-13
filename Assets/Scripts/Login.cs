using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    // Referencias a los campos de texto donde el usuario introduce sus credenciales.
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI passwordField;

    // Botón que permite iniciar el proceso de autenticación.
    public Button submitButton;

    // Inicia la corrutina encargada de enviar las credenciales al servidor PHP.
    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    // Construye y envía la solicitud de autenticación, esperando la respuesta del servidor.
    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();

        // Limpia caracteres invisibles y espacios sobrantes de las credenciales.
        string username = nameField.text
            .Replace("\u200B", "")
            .Trim();

        string password = passwordField.text
            .Replace("\u200B", "")
            .Trim();

        // Adjunta las credenciales al formulario que recibirá PHP mediante POST.
        form.AddField("username", username);
        form.AddField("password", password);

        // Muestra en consola las credenciales originales para facilitar la depuración.
        Debug.Log("Username enviado: [" + nameField.text + "]");
        Debug.Log("Password enviada: [" + passwordField.text + "]");

        // Ejecuta la petición HTTP y espera a que finalice la comunicación con PHP.
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest();

            // Comprueba si la solicitud fue exitosa.
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Login request failed: " + www.error);
                yield break;
            }

            // Separa el código de respuesta del mensaje enviado por PHP.
            string response = www.downloadHandler.text;
            string[] responseData = response.Split('\t');

            // El código 0 indica que las credenciales fueron verificadas correctamente.
            if (responseData.Length > 1 && responseData[0] == "0")
            {
                Debug.Log("User login successful");

                // Guarda el nombre del usuario para utilizarlo en otras escenas.
                DBManager.username = nameField.text;

                // Carga la escena "elección personaje" después de autenticar al usuario.
                SceneManager.LoadScene("elección personaje");
            }
            else
            {
                // Informa el código o mensaje de error devuelto por el servidor.
                Debug.Log("User login failed. Error #" + response);
            }
        }
    }

    // Habilita el botón de inicio de sesión únicamente cuando ambos campos
    // contienen al menos cuatro caracteres.
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 4 && passwordField.text.Length >= 4);
    }
}