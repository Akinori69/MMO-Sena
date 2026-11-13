using System;

// Permite que las clases puedan convertirse desde y hacia formatos serializados,
// facilitando el intercambio de información entre Unity y el servidor PHP.
[Serializable]
public class PersonajeData
{
    // Identificador único del personaje en la base de datos (llave primaria).
    public int id;

    // Nombre asignado al personaje durante su creación.
    public string nombre;

    // Nombre de la clase a la que pertenece el personaje.
    public string clase;

    // Nivel actual del personaje almacenado en la base de datos.
    public string nivel;
}

// Estructura utilizada para interpretar la respuesta JSON de PHP
// al consultar los personajes asociados a una cuenta.
[Serializable]
public class PersonajesResponse
{
    // Código de estado devuelto por PHP: 0 indica una consulta exitosa.
    public int status;

    // Arreglo que contiene los datos de todos los personajes recuperados.
    public PersonajeData[] personajes;
}

// Estructura utilizada para interpretar la respuesta JSON
// del servidor después de solicitar la eliminación de un personaje.
[Serializable]
public class EliminarPersonajeResponse
{
    // Código de estado que indica si la operación fue exitosa o produjo un error.
    public int status;

    // Mensaje descriptivo enviado por PHP con el resultado de la operación.
    public string mensaje;
}