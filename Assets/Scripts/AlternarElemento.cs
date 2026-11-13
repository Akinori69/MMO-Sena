using UnityEngine;
using UnityEngine.EventSystems;

public class AlternarElemento : MonoBehaviour, IPointerClickHandler
{
    // Referencia al elemento de interfaz cuya visibilidad se desea alternar.
    [SerializeField] private GameObject elementoUI;

    // Detecta los clics realizados sobre el objeto que contiene este componente
    // y comprueba si el elemento asociado debe activarse o desactivarse.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Comprueba que exista una referencia asignada desde el Inspector.
        if (elementoUI == null)
        {
            Debug.LogError("No se ha asignado el elemento UI.", this);
            return;
        }

        // Determina el estado actual del elemento para ejecutar la acción contraria.
        if (elementoUI.activeSelf)
        {
            Debug.Log("Desactivando elemento UI");
            DesactivarElemento();
        }
        else
        {
            Debug.Log("Activando elemento UI");
            ActivarElemento();
        }
    }

    // Desactiva el elemento de interfaz asociado al componente.
    private void DesactivarElemento()
    {
        elementoUI.SetActive(false);
    }

    // Activa el elemento de interfaz asociado al componente.
    private void ActivarElemento()
    {
        elementoUI.SetActive(true);
    }
}