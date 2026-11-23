using System.ComponentModel;
using System.Diagnostics;

namespace PracticaHilos;

public static class LanzarProceso
{
    public static async Task EjecutarAsync()
    {
        await ProbarAsync("dotnet", "--list-sdks");
        await ProbarAsync("dotnet", "--opcion-inventada");
        await ProbarAsync("programa_inventado", "");
    }

    private static async Task ProbarAsync(string exe, string parametros)
    {
        Console.WriteLine($"===== {exe} {parametros}");
        var config = new ProcessStartInfo
        {
            FileName = exe,
            Arguments = parametros,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        Process? proceso;
        try
        {
            proceso = Process.Start(config);
        }
        catch (Win32Exception e)
        {
            Console.WriteLine($"Excepción Win32Exception: {e.Message}\n");
            return;
        }

        using (proceso!)
        {
            Task<string> lecturaSalida = proceso.StandardOutput.ReadToEndAsync();
            Task<string> lecturaError = proceso.StandardError.ReadToEndAsync();
            await proceso.WaitForExitAsync(); // espera sin bloquear el hilo

            string salida = await lecturaSalida;
            string error = await lecturaError;
            if (!string.IsNullOrWhiteSpace(salida)) Console.WriteLine(salida.TrimEnd());
            if (!string.IsNullOrWhiteSpace(error)) Console.WriteLine("stderr: " + error.Trim().Split('\n')[0]);
            Console.WriteLine($"ExitCode = {proceso.ExitCode}\n");
        }
    }
}
