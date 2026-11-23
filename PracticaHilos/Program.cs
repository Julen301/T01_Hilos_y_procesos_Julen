namespace PracticaHilos;

// Punto de entrada: se elige el reto por argumento (dotnet run -- 2) o por menú.
internal class Program
{
    private static async Task Main(string[] args)
    {
        string opcion = args.Length > 0 ? args[0] : PedirOpcion();

        switch (opcion)
        {
            case "1": PrimerHilo.Ejecutar(); break;
            case "2": CondicionCarrera.Ejecutar(args.Length > 1 ? int.Parse(args[1]) : 100_000); break;
            case "3": await TareasYTiempo.EjecutarAsync(); break;
            case "4": await LanzarProceso.EjecutarAsync(); break;
            default: Console.WriteLine("Opción no válida"); break;
        }
    }

    private static string PedirOpcion()
    {
        Console.WriteLine("1) Mi primer hilo");
        Console.WriteLine("2) Condición de carrera");
        Console.WriteLine("3) Tareas y tiempo");
        Console.WriteLine("4) Lanzar un proceso");
        Console.Write("Elige reto: ");
        return Console.ReadLine() ?? "";
    }
}
