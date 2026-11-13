using UnityEngine;

public static class DBManager
{
    // Almacena el nombre de la cuenta autenticada durante la sesión de juego.
    public static string username;

    // Guarda el nombre del personaje seleccionado por el usuario.
    public static string personajeSeleccionado;

    // Inicializa el identificador con un valor que no corresponde a ningún personaje
    // de la base de datos, indicando que actualmente no existe una selección activa.
    public static int idPersonajeSeleccionado = -1;

    // Propiedad que indica si existe una cuenta autenticada.
    // Devuelve true cuando el nombre de usuario contiene un valor válido.
    public static bool LoggedIn => !string.IsNullOrEmpty(username);

    // Propiedad que comprueba si existe un personaje seleccionado.
    // El valor -1 representa la ausencia de una selección.
    public static bool PersonajeSeleccionado => idPersonajeSeleccionado != -1;

    // Registra el ID y el nombre del personaje seleccionado.
    // Estos datos quedan disponibles para otros componentes durante la sesión.
    public static void SeleccionarPersonaje(int id, string nombre)
    {
        idPersonajeSeleccionado = id;
        personajeSeleccionado = nombre;
    }

    // Restablece los datos del personaje seleccionado.
    // Se utiliza al cambiar de selección, cerrar sesión o eliminar un personaje.
    public static void LimpiarPersonajeSeleccionado()
    {
        idPersonajeSeleccionado = -1;
        personajeSeleccionado = null;
    }

    // Cierra la sesión actual eliminando los datos de la cuenta
    // y cualquier selección de personaje almacenada.
    public static void LogOut()
    {
        username = null;
        LimpiarPersonajeSeleccionado();
    }
}