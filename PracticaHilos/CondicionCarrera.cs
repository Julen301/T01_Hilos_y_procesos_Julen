namespace PracticaHilos;

public static class CondicionCarrera
{
    private static int _total;
    private static readonly object _candado = new();

    public static void Ejecutar(int vueltas)
    {
        Console.WriteLine($"Cada hilo suma {vueltas} veces. Debería salir {vueltas * 2}.");

        Medir("SIN PROTECCIÓN", vueltas, () => _total++);
        Medir("LOCK", vueltas, () => { lock (_candado) _total++; });
        Medir("INTERLOCKED", vueltas, () => Interlocked.Increment(ref _total));
    }

    private static void Medir(string modo, int vueltas, Action sumarUno)
    {
        var resultados = new List<int>();
        var reloj = System.Diagnostics.Stopwatch.StartNew();

        for (int prueba = 0; prueba < 5; prueba++)
        {
            _total = 0;
            var hilos = new[] { new Thread(Bucle), new Thread(Bucle) };
            foreach (var h in hilos) h.Start();
            foreach (var h in hilos) h.Join();
            resultados.Add(_total);
        }

        reloj.Stop();
        Console.WriteLine($"{modo,-15} -> {string.Join(" | ", resultados)}   ({reloj.ElapsedMilliseconds} ms las 5 pruebas)");

        void Bucle()
        {
            for (int i = 0; i < vueltas; i++) sumarUno();
        }
    }
}
