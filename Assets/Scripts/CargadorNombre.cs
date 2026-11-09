using UnityEngine;
using TMPro;

public class CargadorNombre : MonoBehaviour
{
    public TMP_Text nombreJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nombreJugador.text = "prueba";
        if (DBManager.LoggedIn)
        {
            nombreJugador.text = "Bienvenido: " + DBManager.username;
        }
    }
}
