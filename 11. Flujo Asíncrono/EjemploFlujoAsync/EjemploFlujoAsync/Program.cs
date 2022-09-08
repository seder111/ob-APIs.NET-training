using EjemploFlujoAsync;
using System.Diagnostics;

// Iniciamos un contador de tiempo - SINCRONO
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine("\n*************************************************");
Console.WriteLine("\nBienvenido a la Calculadora de Hipotecas Síncrona");
Console.WriteLine("\n*************************************************");


// aniosVidaLaboral
// esTipoContratoIndefinido
// sueldoNeto
// gastosMensuales
// hipotecaConcedida

var aniosVidaLaboral = CalculadoraHipotecaSync.ObtenerAniosVidaLaboral();
Console.WriteLine($"\nAños de vida laboral: {aniosVidaLaboral}");

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\nSueldo neto obtenido: {sueldoNeto} Euros");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"\nGastos mensuales obtenidos: {gastosMensuales} Euros");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformacionPacaCondecerHipoteca(
    aniosVidaLaboral, esTipoContratoIndefinido, sueldoNeto, gastosMensuales, cantidadSolicitada: 60000, aniosPagar: 35
    );

var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");

stopwatch.Stop();

Console.WriteLine($"\nLa operación síncrona ha durado: {stopwatch.Elapsed} segundos.");

// Reiniciamos el contador de tiemp - asíncrono

stopwatch.Restart();
Console.WriteLine("\n**************************************************");
Console.WriteLine("\nBienvenido a la Calculadora de Hipotecas Asíncrona");
Console.WriteLine("\n**************************************************");

Task<int> aniosVidaLaboralTask = CalculadoraHipotecaAsync.ObtenerAniosVidaLaboral();
Task<bool> esTipoContratoIndefinidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> gastosMensualesTask = CalculadoraHipotecaAsync.ObtenerGastosMensuales();


var analisisHipotecaTasks = new List<Task>
{
    aniosVidaLaboralTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    gastosMensualesTask
};

while (analisisHipotecaTasks.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTasks);

    if(tareaFinalizada == aniosVidaLaboralTask)
    {
        Console.WriteLine($"\nAños de Vida Laboral Obtenidos: {aniosVidaLaboralTask.Result} Años");
    } else if (tareaFinalizada == esTipoContratoIndefinidoTask)
    {
        Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinidoTask.Result}");
    } else if (tareaFinalizada == sueldoNetoTask)
    {
        Console.WriteLine($"\nSueldo neto obtenido: {sueldoNetoTask.Result} Euros");
    } else if (tareaFinalizada == gastosMensualesTask)
    {
        Console.WriteLine($"\nGastos mensuales obtenidos: {gastosMensualesTask.Result} Euros");
    }

    analisisHipotecaTasks.Remove(tareaFinalizada); // En cada vuelta eliminamos la tareaFinalizada hasta vaciar la lista y salir del bucle
}

var hipotecaAsyncConcedida = CalculadoraHipotecaAsync.AnalizarInformacionPacaCondecerHipoteca(
    aniosVidaLaboralTask.Result, esTipoContratoIndefinidoTask.Result, sueldoNetoTask.Result, gastosMensualesTask.Result, cantidadSolicitada: 60000, aniosPagar: 35
    );

var ResultadoAsync = hipotecaAsyncConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis Finalizado. Su solicitud de hipoteca ha sido: {ResultadoAsync}");

stopwatch.Stop();

Console.WriteLine($"\nLa operación asíncrona ha durado: {stopwatch.Elapsed} segundos.");