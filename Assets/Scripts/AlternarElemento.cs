using UnityEngine;
using UnityEngine.EventSystems;

public class AlternarElemento : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject elementoUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (elementoUI == null)
        {
            Debug.LogError("No se ha asignado el elemento UI.", this);
            return;
        }

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

    private void DesactivarElemento()
    {
        elementoUI.SetActive(false);
    }

    private void ActivarElemento()
    {
        elementoUI.SetActive(true);
    }
}
