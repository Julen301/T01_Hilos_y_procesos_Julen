using System.Diagnostics;

namespace PracticaHilos;

public static class TareasYTiempo
{
    public static async Task EjecutarAsync()
    {
        int[] duraciones = { 1, 2, 3 };
        var crono = new Stopwatch();

        // 1) Una detrás de otra
        crono.Start();
        foreach (int s in duraciones)
            Console.WriteLine(await DescargaAsync(s));
        crono.Stop();
        Console.WriteLine($"Secuencial: {crono.Elapsed.TotalSeconds:F2} s\n");

        // 2) Todas a la vez con WhenAll
        crono.Restart();
        string[] mensajes = await Task.WhenAll(duraciones.Select(DescargaAsync));
        crono.Stop();
        foreach (var m in mensajes) Console.WriteLine(m);
        Console.WriteLine($"Con WhenAll: {crono.Elapsed.TotalSeconds:F2} s\n");

        // 3) Extra: lo mismo pero con Thread.Sleep en vez de Task.Delay
        crono.Restart();
        await Task.WhenAll(duraciones.Select(DescargaConSleep));
        crono.Stop();
        Console.WriteLine($"WhenAll con Thread.Sleep: {crono.Elapsed.TotalSeconds:F2} s");
    }

    private static async Task<string> DescargaAsync(int segundos)
    {
        await Task.Delay(TimeSpan.FromSeconds(segundos));
        return $"Descarga de {segundos} s completada (hilo {Environment.CurrentManagedThreadId})";
    }

    // Sin await dentro: Thread.Sleep bloquea el hilo que llama, así que las "descargas" van en fila
    private static Task DescargaConSleep(int segundos)
    {
        Thread.Sleep(segundos * 1000);
        return Task.CompletedTask;
    }
}
