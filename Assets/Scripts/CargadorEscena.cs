using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CargadorEscena : MonoBehaviour, IPointerClickHandler
{
    [Header("Escena a cargar")]
    [SerializeField] private string nombreEscena;

#if UNITY_EDITOR
    [Tooltip("Arrastra la escena desde el Project para asociarla desde el Inspector.")]
    [SerializeField] private SceneAsset escenaACargar;
#endif

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (escenaACargar != null)
            nombreEscena = escenaACargar.name;
    }
#endif

    public void OnPointerClick(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(nombreEscena))
        {
            Debug.LogWarning("No hay ninguna escena asociada a CargadorEscena.");
            return;
        }

        SceneManager.LoadScene(nombreEscena);
    }
}
