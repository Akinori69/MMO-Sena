using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonExit : MonoBehaviour, IPointerClickHandler
{
    // Detecta el clic sobre el objeto y ejecuta el procedimiento de cierre
    // de la aplicación o de la sesión de ejecución del Editor.
    public void OnPointerClick(PointerEventData eventData)
    {
        QuitApplication();
    }

    // Finaliza la ejecución dependiendo del entorno en el que se encuentre Unity.
    private void QuitApplication()
    {
#if UNITY_EDITOR
        // Dentro del Editor, detiene el modo Play sin cerrar Unity.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // En una compilación ejecutable, cierra la aplicación.
        Application.Quit();
#endif
    }
}