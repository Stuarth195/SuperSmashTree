using UnityEngine;
using System.Collections.Generic;
using System;

public class RandomNumbers : MonoBehaviour
{
    [Header("Rango de posiciones en X")]
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Rango de posiciones en Y")]
    public float minY = 5f;
    public float maxY = 10f;

    [Header("Rango de posiciones en Z")]
    public float minZ = -10f;
    public float maxZ = 10f;

    [Header("Rango de números aleatorios")]
    public int minNum = 1;
    public int maxNum = 1000;

    [Header("Configuración de tiempo")]
    public float periodoTotalMinutos = 0.5f; // Cuánto dura todo el proceso (en minutos)
    public float intervaloMinSegundos = 1f;  // Mínimo intervalo en segundos
    public float intervaloMaxSegundos = 5f;  // Máximo intervalo en segundos

    private float _timerIntervalo;
    private float _timerTotal;
    private float _intervaloActual;
    private bool _activo = false;

    private void Start()
    {
        _timerIntervalo = 0f;
        _timerTotal = 0f;
        _activo = true;

        // Iniciar con un intervalo aleatorio
        _intervaloActual = UnityEngine.Random.Range(intervaloMinSegundos, intervaloMaxSegundos);
        Debug.Log($"[Inicio] Intervalo inicial: {_intervaloActual} segundos");
    }

    private void Update()
    {
        if (!_activo)
            return;

        _timerIntervalo += Time.deltaTime;
        _timerTotal += Time.deltaTime;

        // Si ha pasado el intervalo actual, ejecutar funciones
        if (_timerIntervalo >= _intervaloActual)
        {
            EjecutarFunciones();
            _timerIntervalo = 0f;

            // Cambiar el intervalo actual aleatoriamente
            _intervaloActual = UnityEngine.Random.Range(intervaloMinSegundos, intervaloMaxSegundos);
            Debug.Log($"[Nuevo intervalo] Próxima ejecución en: {_intervaloActual} segundos");
        }

        // Si ha pasado el tiempo total, detener
        if (_timerTotal >= periodoTotalMinutos * 60f) // Convertir minutos a segundos
        {
            Debug.Log("Tiempo total cumplido. Deteniendo ejecución.");
            _activo = false;
        }
    }

    private void EjecutarFunciones()
    {
        CreateRandomNumber();
        CreateRandomNumberList();
        CreateSphere();
    }

    public void CreateRandomNumber()
    {
        int randomNumber = UnityEngine.Random.Range(minNum, maxNum);
        Debug.Log("Random Number: " + randomNumber);
    }

    public void CreateRandomNumberList()
    {
        List<int> randomNumbers = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            randomNumbers.Add(UnityEngine.Random.Range(minNum, maxNum));
        }
        Debug.Log("Random Numbers List: " + string.Join(", ", randomNumbers));
    }

    public void CreateSphere()
    {
        float randomPositionX = UnityEngine.Random.Range(minX, maxX);
        float randomPositionY = UnityEngine.Random.Range(minY, maxY);
        float randomPositionZ = UnityEngine.Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
        Debug.Log("Random Position: " + spawnPosition);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = spawnPosition;

        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = true;
    }
}
