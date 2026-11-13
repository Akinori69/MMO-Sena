using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonajeUI : MonoBehaviour
{
    // Referencias a los elementos TextMeshPro del prefab que muestran
    // el nombre, nivel y clase del personaje.
    public TMP_Text nombreTexto;
    public TMP_Text nivelTexto;
    public TMP_Text claseTexto;

    // Almacena los datos del personaje representado por este botón
    // y la referencia al controlador encargado de gestionar su selección.
    private PersonajeData personaje;
    private GestionPersonajes gestor;

    // Recibe los datos del personaje y el controlador principal,
    // actualiza la información visual del prefab y registra su evento de selección.
    public void Configurar(PersonajeData datos, GestionPersonajes controlador)
    {
        personaje = datos;
        gestor = controlador;

        // Actualiza los textos del botón utilizando la información recibida desde PHP.
        nombreTexto.text = personaje.nombre;
        nivelTexto.text = "Nivel " + personaje.nivel;
        claseTexto.text = personaje.clase;

        // Asocia el evento de clic del botón con el método de selección.
        GetComponent<Button>().onClick.AddListener(Seleccionar);
    }

    // Notifica al gestor que el usuario ha seleccionado este personaje,
    // enviándole sus datos y la referencia al botón correspondiente.
    private void Seleccionar()
    {
        gestor.SeleccionarPersonaje(personaje, this);
    }
}