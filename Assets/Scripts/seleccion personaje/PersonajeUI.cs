using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonajeUI : MonoBehaviour
{
    // Elementos visuales del prefab
    public TMP_Text nombreTexto;
    public TMP_Text nivelTexto;
    public TMP_Text claseTexto;

    private PersonajeData personaje;
    private GestionPersonajes gestor;

    public void Configurar(PersonajeData datos, GestionPersonajes controlador)
    {
        personaje = datos;
        gestor = controlador;

        nombreTexto.text = personaje.nombre;
        nivelTexto.text = "Nivel " + personaje.nivel;
        claseTexto.text = personaje.clase;

        GetComponent<Button>().onClick.AddListener(Seleccionar);
    }

    private void Seleccionar()
    {
        gestor.SeleccionarPersonaje(personaje, this);
    }
}
