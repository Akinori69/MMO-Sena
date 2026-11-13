using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionBotonCreacion : MonoBehaviour
{
    // Cantidad de clases disponibles para la creación de personajes.
    private const int Opciones = 9;

    // Este script guarda un índice entre 0 y 8, correspondiente a los identificadores
    // del 1 al 9 utilizados en PHP para asociar cada personaje con su clase.
    // PHP recibe este índice y determina la clase correspondiente para asignar sus valores básicos.

    // Almacena la clase seleccionada. Su valor inicial es 0 y solamente puede modificarse
    // desde este componente, permitiendo que otros scripts consulten la selección.
    public int ClaseSeleccionada { get; private set; } = 0;

    // Referencias a los botones, textos, imágenes y modelos 3D asociados a cada clase.
    // Los arreglos mantienen la correspondencia entre elementos mediante sus índices.
    [SerializeField] private Button[] botones = new Button[Opciones];
    [SerializeField] private TMP_Text[] textos = new TMP_Text[Opciones];
    [SerializeField] private Image[] sprites = new Image[Opciones];
    [SerializeField] private GameObject[] personajes = new GameObject[Opciones];

    // Registra los eventos de selección en cada botón y establece la primera clase
    // como selección predeterminada al inicializar el componente.
    private void Awake()
    {
        for (int indice = 0; indice < botones.Length; indice++)
        {
            // Guarda el índice actual para que cada botón conserve su propia selección
            // cuando se ejecute posteriormente el evento de clic.
            int indiceSeleccionado = indice;

            if (botones[indice] != null)
            {
                botones[indice].onClick.AddListener(() => Seleccionar(indiceSeleccionado));
            }
        }

        // Selecciona automáticamente la primera clase al iniciar la interfaz.
        Seleccionar(0);
    }

    // Actualiza la clase seleccionada y sincroniza los elementos visuales
    // para mostrar únicamente los componentes correspondientes a dicha opción.
    public void Seleccionar(int indice)
    {
        // Comprueba que el índice recibido corresponda a una de las nueve clases disponibles.
        if (indice < 0 || indice >= Opciones)
        {
            Debug.LogWarning($"El indice {indice} no corresponde a una opcion valida.", this);
            return;
        }

        // Asigna el índice seleccionado para enviarlo posteriormente a PHP.
        ClaseSeleccionada = indice;

        // Actualiza los textos e imágenes y muestra el modelo 3D correspondiente.
        ActualizarVisibilidad(textos, indice);
        ActualizarVisibilidad(sprites, indice);
        RestablecerRotacion(personajes, indice);
        ActualizarVisibilidad(personajes, indice);
    }

    // Restablece la rotación local del modelo seleccionado para evitar
    // que conserve una orientación modificada durante selecciones anteriores.
    private static void RestablecerRotacion(GameObject[] elementos, int indiceSeleccionado)
    {
        if (elementos == null || indiceSeleccionado >= elementos.Length || elementos[indiceSeleccionado] == null)
        {
            return;
        }

        elementos[indiceSeleccionado].transform.localRotation = Quaternion.identity;
    }

    // Activa exclusivamente el componente correspondiente al índice seleccionado.
    // Esta versión genérica permite trabajar con distintos tipos de componentes de Unity.
    private static void ActualizarVisibilidad<T>(T[] elementos, int indiceSeleccionado) where T : Component
    {
        if (elementos == null)
        {
            return;
        }

        for (int indice = 0; indice < elementos.Length; indice++)
        {
            if (elementos[indice] != null)
            {
                elementos[indice].gameObject.SetActive(indice == indiceSeleccionado);
            }
        }
    }

    // Sobrecarga del método anterior destinada a elementos GameObject.
    // Permite controlar directamente la visibilidad de los modelos 3D de cada clase.
    private static void ActualizarVisibilidad(GameObject[] elementos, int indiceSeleccionado)
    {
        if (elementos == null)
        {
            return;
        }

        for (int indice = 0; indice < elementos.Length; indice++)
        {
            if (elementos[indice] != null)
            {
                elementos[indice].SetActive(indice == indiceSeleccionado);
            }
        }
    }
}