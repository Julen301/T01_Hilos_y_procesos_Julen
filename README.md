# PracticaHilos

Práctica de hilos, tareas y procesos en C# (.NET 8). Un único programa de consola con un menú; cada reto está en su propia clase:

- `PrimerHilo.cs` → Reto 1: crear un hilo, `Join` en distintos momentos e Id de cada hilo.
- `CondicionCarrera.cs` → Reto 2: contador compartido sin protección, con `lock` y con `Interlocked`.
- `TareasYTiempo.cs` → Reto 3: descargas simuladas en secuencia, con `Task.WhenAll` y con `Thread.Sleep`.
- `LanzarProceso.cs` → Reto 4: ejecutar `dotnet --list-sdks`, leer su salida y comprobar `ExitCode`.

## Cómo ejecutarlo

```bash
cd PracticaHilos
dotnet run            # muestra el menú
dotnet run -- 2       # lanza directamente el reto 2
dotnet run -- 2 1000000   # reto 2 con un millón de vueltas por hilo
```
