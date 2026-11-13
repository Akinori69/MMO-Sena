using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CargadorEscena : MonoBehaviour, IPointerClickHandler
{
    [Header("Escena a cargar")]

    // Nombre de la escena que se cargará al interactuar con el objeto.
    // Se utiliza una cadena de texto para identificarla mediante SceneManager.
    [SerializeField] private string nombreEscena;

#if UNITY_EDITOR
    // Referencia exclusiva del Editor que permite asociar una escena
    // directamente desde la ventana Project mediante el Inspector.
    [Tooltip("Arrastra la escena desde el Project para asociarla desde el Inspector.")]
    [SerializeField] private SceneAsset escenaACargar;
#endif

#if UNITY_EDITOR
    // Se ejecuta cuando se modifican los valores del componente en el Inspector.
    // Sincroniza automáticamente el nombre de la escena seleccionada con nombreEscena.
    private void OnValidate()
    {
        if (escenaACargar != null)
            nombreEscena = escenaACargar.name;
    }
#endif

    // Detecta la interacción del usuario y solicita la carga de la escena asociada.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Comprueba que se haya asignado un nombre de escena antes de intentar cargarla.
        if (string.IsNullOrEmpty(nombreEscena))
        {
            Debug.LogWarning("No hay ninguna escena asociada a CargadorEscena.");
            return;
        }

        // Carga la escena indicada utilizando su nombre registrado en Build Settings.
        SceneManager.LoadScene(nombreEscena);
    }
}