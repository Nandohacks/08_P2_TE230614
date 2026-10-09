//Ejercicio 8

namespace Ejercicio08_ControlTanque
{
    // Clase que representa el modelo y simulación del tanque de líquido
    public class TanqueLiquido
    {
        // Propiedades de la clase
        public double CapacidadMaxima { get; set; }
        public double VolumenInicial { get; set; }
        public double VolumenAgregado { get; set; }
        public double VolumenActual { get; private set; }

        // Constructor por defecto
        public TanqueLiquido() { }

        // Constructor parametrizado
        public TanqueLiquido(double capacidadMaxima, double volumenInicial, double volumenAgregado)
        {
            CapacidadMaxima = capacidadMaxima;
            VolumenInicial = volumenInicial;
            VolumenAgregado = volumenAgregado;
            VolumenActual = volumenInicial;
        }

        // Método para obtener el porcentaje de llenado actual
        public double CalcularPorcentajeLlenado()
        {
            if (CapacidadMaxima <= 0) return 0.0; // Protección contra división entre cero
            return (VolumenActual / CapacidadMaxima) * 100.0;
        }

        // Método para realizar la simulación del llenado por ciclo mediante un ciclo while
        public void SimularLlenado()
        {
            int ciclo = 0;
            Console.WriteLine("\n==================================================");
            Console.WriteLine($"CAPACIDAD MÁXIMA DEL TANQUE: {CapacidadMaxima:F2} L");
            Console.WriteLine($"VOLUMEN INICIAL: {VolumenInicial:F2} L ({CalcularPorcentajeLlenado():F2}%)");
            Console.WriteLine("==================================================");
            Console.WriteLine("INICIANDO PROCESO DE LLENADO:\n");

            // Si el tanque ya inicia con o supera la capacidad máxima
            if (VolumenActual >= CapacidadMaxima)
            {
                Console.WriteLine("El tanque ya se encuentra completamente lleno.");
                Console.WriteLine($"Volumen actual: {VolumenActual:F2} L | Llenado: {CalcularPorcentajeLlenado():F2}%");
                return;
            }

            // Ciclo while para llenar el tanque de forma progresiva
            while (VolumenActual < CapacidadMaxima)
            {
                ciclo++;
                VolumenActual += VolumenAgregado;

                // Restricción: El volumen nunca debe superar la capacidad máxima
                if (VolumenActual > CapacidadMaxima)
                {
                    VolumenActual = CapacidadMaxima;
                }

                double porcentajeActual = CalcularPorcentajeLlenado();
                Console.WriteLine($"  -> Ciclo {ciclo}: Volumen = {VolumenActual:F2} L | Llenado = {porcentajeActual:F2}%");
            }

            Console.WriteLine("\n==================================================");
            Console.WriteLine($"¡TANQUE LLENO AL 100%!");
            Console.WriteLine($"Proceso completado con éxito en {ciclo} ciclos.");
            Console.WriteLine("==================================================\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ejercicio 8 - Control de Llenado de Tanque";

            Console.WriteLine("==================================================");
            Console.WriteLine("   SISTEMA DE CONTROL Y LLENADO DE TANQUE");
            Console.WriteLine("==================================================\n");

            // 1. Lectura y validación de la Capacidad Máxima (evita divisiones entre cero y valores <= 0)
            double capMax = LeerDoublePositivoStrict("Ingrese la capacidad máxima del tanque (L): ");

            // 2. Lectura y validación del Volumen Inicial (evita valores < 0 y mayores a la capacidad máxima)
            double volIni = LeerVolumenInicialValido("Ingrese el volumen inicial de líquido (L): ", capMax);

            // 3. Lectura y validación del Volumen Agregado por ciclo (evita ciclos infinitos al exigir un valor > 0)
            double volAgr = LeerDoublePositivoStrict("Ingrese el volumen a agregar por ciclo (L): ");

            // Creación/Instanciación del objeto 'tanque' a partir de la clase TanqueLiquido
            TanqueLiquido tanque = new TanqueLiquido(capMax, volIni, volAgr);

            // Ejecución de la simulación
            tanque.SimularLlenado();

            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Método de lectura para números flotantes estrictamente mayores a cero
        static double LeerDoublePositivoStrict(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (double.TryParse(entrada, out valor) && valor > 0)
                {
                    return valor;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Error: Ingrese un valor numérico estrictamente mayor a 0.");
                Console.ResetColor();
            }
        }

        // Método de lectura para el volumen inicial respetando los límites del tanque
        static double LeerVolumenInicialValido(string mensaje, double limiteMaximo)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (double.TryParse(entrada, out valor) && valor >= 0 && valor <= limiteMaximo)
                {
                    return valor;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Error: Ingrese un valor numérico entre 0 y {limiteMaximo:F2} L.");
                Console.ResetColor();
            }
        }
    }
}