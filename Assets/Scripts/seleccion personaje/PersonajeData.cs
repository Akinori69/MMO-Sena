using System;

[Serializable]
public class PersonajeData
{
    public int id;
    public string nombre;
    public string clase;
    public string nivel;
}

[Serializable]
public class PersonajesResponse
{
    public int status;
    public PersonajeData[] personajes;
}

[Serializable]
public class EliminarPersonajeResponse
{
    public int status;
    public string mensaje;
}
