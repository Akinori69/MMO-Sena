using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI passwordField;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        string username = nameField.text
            .Replace("\u200B", "")
            .Trim();

        string password = passwordField.text
            .Replace("\u200B", "")
            .Trim();

        form.AddField("username", username);
        form.AddField("password", password);

        Debug.Log("Username enviado: [" + nameField.text + "]");
        Debug.Log("Password enviada: [" + passwordField.text + "]");

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest();

            // Comprueba si la solicitud fue exitosa
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Login request failed: " + www.error);
                yield break;
            }

            // Procesa la respuesta del servidor
            string response = www.downloadHandler.text;
            string[] responseData = response.Split('\t');

            if (responseData.Length > 1 && responseData[0] == "0")
            {
                Debug.Log("User login successful");

                DBManager.username = nameField.text;
                // Carga la escena "elección personaje"
                SceneManager.LoadScene("elección personaje");
            }
            else
            {
                Debug.Log("User login failed. Error #" + response);
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 4 && passwordField.text.Length >= 4);
    }
}
