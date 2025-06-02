using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sistema Bancario");

        // Crear banco
        Banco miBanco = new Banco();

        // Crear cuentas
        var cuenta1 = new CuentaBancaria("001", "Juan Pérez", 1000);
        var cuenta2 = new CuentaBancaria("002", "María Gómez", 500);
        var cuenta3 = new CuentaBancaria("003", "Carlos López");

        // Agregar cuentas al banco
        miBanco.AgregarCuenta(cuenta1);
        miBanco.AgregarCuenta(cuenta2);
        miBanco.AgregarCuenta(cuenta3);

        // Operaciones de prueba
        Console.WriteLine("\nDepositando $200 en cuenta 001:");
        if (miBanco.Depositar("001", 200))
            Console.WriteLine("Depósito exitoso");
        else
            Console.WriteLine("Depósito fallido");

        Console.WriteLine("\nExtrayendo $100 de cuenta 002:");
        if (miBanco.Extraer("002", 100))
            Console.WriteLine("Extracción exitosa");
        else
            Console.WriteLine("Extracción fallida");

        Console.WriteLine("\nIntentando extraer $1000 de cuenta 003:");
        if (miBanco.Extraer("003", 1000))
            Console.WriteLine("Extracción exitosa");
        else
            Console.WriteLine("Extracción fallida (fondos insuficientes)");

        Console.WriteLine("\nTransferencia de $300 de cuenta 001 a cuenta 002:");
        if (miBanco.Transferencia("001", "002", 300))
            Console.WriteLine("Transferencia exitosa");
        else
            Console.WriteLine("Transferencia fallida");

        // Mostrar saldos finales
        Console.WriteLine("\nSaldos finales:");
        Console.WriteLine($"Cuenta 001: {miBanco.ConsultarSaldo("001"):C}");
        Console.WriteLine($"Cuenta 002: {miBanco.ConsultarSaldo("002"):C}");
        Console.WriteLine($"Cuenta 003: {miBanco.ConsultarSaldo("003"):C}");

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}