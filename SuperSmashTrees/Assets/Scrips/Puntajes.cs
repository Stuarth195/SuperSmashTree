using System;
using System.Collections.Generic;

public class SistemaPuntajes
{
    // Diccionario para almacenar los puntajes de cada jugador
    private Dictionary<string, int> puntajesJugadores;

    public SistemaPuntajes()
    {
        puntajesJugadores = new Dictionary<string, int>();
    }

    // Añadir un nuevo jugador al sistema
    public void RegistrarJugador(string nombreJugador)
    {
        if (!puntajesJugadores.ContainsKey(nombreJugador))
        {
            puntajesJugadores.Add(nombreJugador, 0);
            Console.WriteLine($"Jugador {nombreJugador} registrado con éxito.");
        }
        else
        {
            Console.WriteLine($"El jugador {nombreJugador} ya existe.");
        }
    }

    // Aumentar el puntaje de un jugador
    public void AumentarPuntaje(string nombreJugador, int puntos)
    {
        if (puntajesJugadores.ContainsKey(nombreJugador))
        {
            puntajesJugadores[nombreJugador] += puntos;
            Console.WriteLine($"Se añadieron {puntos} puntos a {nombreJugador}. Puntaje total: {puntajesJugadores[nombreJugador]}");
        }
        else
        {
            Console.WriteLine($"El jugador {nombreJugador} no está registrado.");
        }
    }

    // Reducir el puntaje de un jugador (por ejemplo, por autodestrucción)
    public void ReducirPuntaje(string nombreJugador, int puntos)
    {
        if (puntajesJugadores.ContainsKey(nombreJugador))
        {
            puntajesJugadores[nombreJugador] = Math.Max(0, puntajesJugadores[nombreJugador] - puntos);
            Console.WriteLine($"Se restaron {puntos} puntos a {nombreJugador}. Puntaje total: {puntajesJugadores[nombreJugador]}");
        }
        else
        {
            Console.WriteLine($"El jugador {nombreJugador} no está registrado.");
        }
    }

    // Obtener el puntaje actual de un jugador
    public int ObtenerPuntaje(string nombreJugador)
    {
        if (puntajesJugadores.TryGetValue(nombreJugador, out int puntaje))
        {
            return puntaje;
        }
        return -1; // Retorna -1 si el jugador no existe
    }

    // Reiniciar todos los puntajes a cero
    public void ReiniciarPuntajes()
    {
        var jugadores = new List<string>(puntajesJugadores.Keys);
        foreach (var jugador in jugadores)
        {
            puntajesJugadores[jugador] = 0;
        }
        Console.WriteLine("Todos los puntajes han sido reiniciados a cero.");
    }
}
