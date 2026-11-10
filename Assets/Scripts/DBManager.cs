using UnityEngine;

public static class DBManager
{
    public static string username;

    public static string personajeSeleccionado;
    //inicializando en un valor no valido en la base 
    //de datos para indicar que no hay personaje seleccionado
    public static int idPersonajeSeleccionado = -1;

    public static bool LoggedIn => !string.IsNullOrEmpty(username);

    public static bool PersonajeSeleccionado => idPersonajeSeleccionado != -1;

    public static void SeleccionarPersonaje(int id, string nombre)
    {
        idPersonajeSeleccionado = id;
        personajeSeleccionado = nombre;
    }

    public static void LimpiarPersonajeSeleccionado()
    {
        idPersonajeSeleccionado = -1;
        personajeSeleccionado = null;
    }

    public static void LogOut()
    {
        username = null;
        LimpiarPersonajeSeleccionado();
    }
}
