using System;

//script para gestionar los botones que despliega la 
// interfaz como prefabs sobre los personajes del jugador

[Serializable]
public class PersonajeData
{
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

