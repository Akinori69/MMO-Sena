using UnityEngine;

public static class DBManager
{
    public static string username;
    public static string dinero;

    public static string personajeSeleccionado;

    public static bool LoggedIn => username != null;
    public static void LogOut()
    {
        username = null;
    }
}
