
using UnityEngine;
using TMPro;

public class PersonajeUI : MonoBehaviour
{
    //script asociado al prefab del boton personaje 
    // para enseñar la informacion del personaje en el boton
    public TMP_Text nombreTexto;
    public TMP_Text nivelTexto;
    public TMP_Text claseTexto;

    public void Configurar(PersonajeData personaje)
    {
        nombreTexto.text = personaje.nombre;
        nivelTexto.text = "Nivel " + personaje.nivel;
        claseTexto.text = personaje.clase;
    }
}
