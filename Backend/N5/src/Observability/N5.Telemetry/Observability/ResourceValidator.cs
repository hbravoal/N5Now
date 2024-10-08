namespace N5.Telemetry.Observability;

using System.Diagnostics;

public class ResourceValidator
{
    // Umbral constante para el uso de recursos (50%)
    private const float CpuThreshold = 50.0f; 
    private const float MemoryThreshold = 50.0f; 

    /// <summary>
    /// Verifica si el uso de CPU y memoria está por debajo de los umbrales definidos.
    /// </summary>
    /// <returns>True si se puede publicar, de lo contrario False.</returns>
    public bool CanPublish()
    {
        return GetCpuUsage() < CpuThreshold && GetMemoryUsage() < MemoryThreshold;
    }

    /// <summary>
    /// Obtiene el porcentaje actual de uso de CPU.
    /// </summary>
    /// <returns>Uso de CPU en porcentaje.</returns>
    private float GetCpuUsage()
    {
        using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
        {
            // Para obtener el valor actual, se necesita realizar una llamada inicial
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000); // Esperar un segundo para obtener el próximo valor
            return cpuCounter.NextValue();
        }
    }

    /// <summary>
    /// Obtiene el porcentaje actual de uso de memoria.
    /// </summary>
    /// <returns>Uso de memoria en porcentaje.</returns>
    private float GetMemoryUsage()
    {
        var process = Process.GetCurrentProcess();
        long workingSet = process.WorkingSet64; // Memoria usada por el proceso
        long totalMemory = process.PrivateMemorySize64; // Memoria total privada

        // Calcular el porcentaje de uso de memoria
        return (float)(workingSet * 100) / totalMemory;
    }
}
