namespace PracticaHilos;

public static class PrimerHilo
{
    public static void Ejecutar()
    {
        Escribir("Main", $"soy el hilo principal");

        Console.WriteLine("\n>> Caso 1: sin Join");
        var trabajador = CrearHilo();
        trabajador.Start();
        Escribir("Main", "he lanzado el hilo y sigo con lo mío");
        Escribir("Main", $"¿el hilo sigue vivo? {trabajador.IsAlive}");
        while (trabajador.IsAlive) Thread.Sleep(50); // solo para que no se mezcle con el caso 2

        Console.WriteLine("\n>> Caso 2: Join justo después de Start");
        trabajador = CrearHilo();
        trabajador.Start();
        trabajador.Join();
        Escribir("Main", "este mensaje sale cuando el hilo ya ha terminado");

        Console.WriteLine("\n>> Caso 3: Join al final");
        trabajador = CrearHilo();
        trabajador.Start();
        Escribir("Main", "hago otras cosas mientras tanto");
        Thread.Sleep(1200);
        Escribir("Main", "ya he acabado lo mío, ahora espero al hilo");
        trabajador.Join();
        Escribir("Main", "todo terminado");
    }

    private static Thread CrearHilo() => new Thread(() =>
    {
        for (int n = 1; n <= 5; n++)
        {
            Escribir("Secundario", n.ToString());
            Thread.Sleep(500);
        }
    });

    private static void Escribir(string quien, string texto) =>
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} | {quien,-10} | id={Environment.CurrentManagedThreadId} | {texto}");
}
