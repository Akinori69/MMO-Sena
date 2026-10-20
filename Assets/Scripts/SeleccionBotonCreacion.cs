using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionBotonCreacion : MonoBehaviour
{
    private const int Opciones = 9;

    [SerializeField] private Button[] botones = new Button[Opciones];
    [SerializeField] private TMP_Text[] textos = new TMP_Text[Opciones];
    [SerializeField] private Image[] sprites = new Image[Opciones];
    [SerializeField] private GameObject[] personajes = new GameObject[Opciones];

    private void Awake()
    {
        for (int indice = 0; indice < botones.Length; indice++)
        {
            int indiceSeleccionado = indice;

            if (botones[indice] != null)
            {
                botones[indice].onClick.AddListener(() => Seleccionar(indiceSeleccionado));
            }
        }

        Seleccionar(0);
    }

    public void Seleccionar(int indice)
    {
        if (indice < 0 || indice >= Opciones)
        {
            Debug.LogWarning($"El indice {indice} no corresponde a una opcion valida.", this);
            return;
        }

        ActualizarVisibilidad(textos, indice);
        ActualizarVisibilidad(sprites, indice);
        RestablecerRotacion(personajes, indice);
        ActualizarVisibilidad(personajes, indice);
    }

    private static void RestablecerRotacion(GameObject[] elementos, int indiceSeleccionado)
    {
        if (elementos == null || indiceSeleccionado >= elementos.Length || elementos[indiceSeleccionado] == null)
        {
            return;
        }

        elementos[indiceSeleccionado].transform.localRotation = Quaternion.identity;
    }

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
