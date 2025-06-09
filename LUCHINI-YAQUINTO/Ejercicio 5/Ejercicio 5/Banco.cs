using System.Collections.Generic;

public class Banco
{
    private Dictionary<string, CuentaBancaria> cuentas;

    public Banco()
    {
        cuentas = new Dictionary<string, CuentaBancaria>();
    }

    public void AgregarCuenta(CuentaBancaria cuenta)
    {
        if (!cuentas.ContainsKey(cuenta.NumeroCuenta))
        {
            cuentas.Add(cuenta.NumeroCuenta, cuenta);
        }
    }

    public bool Depositar(string numeroCuenta, decimal monto)
    {
        if (monto <= 0 || !cuentas.ContainsKey(numeroCuenta))
            return false;

        var cuenta = cuentas[numeroCuenta];
        return cuenta.ModificarSaldo(monto);
    }

    public bool Extraer(string numeroCuenta, decimal monto)
    {
        if (monto <= 0 || !cuentas.ContainsKey(numeroCuenta))
            return false;

        var cuenta = cuentas[numeroCuenta];
        return cuenta.ModificarSaldo(-monto);
    }

    public bool Transferencia(string cuentaOrigen, string cuentaDestino, decimal monto)
    {
        if (monto <= 0 ||
            !cuentas.ContainsKey(cuentaOrigen) ||
            !cuentas.ContainsKey(cuentaDestino))
            return false;

        var origen = cuentas[cuentaOrigen];
        var destino = cuentas[cuentaDestino];

        if (origen.ObtenerSaldo() < monto)
            return false;

        if (origen.ModificarSaldo(-monto))
        {
            destino.ModificarSaldo(monto);
            return true;
        }

        return false;
    }

    public decimal ConsultarSaldo(string numeroCuenta)
    {
        if (cuentas.TryGetValue(numeroCuenta, out var cuenta))
        {
            return cuenta.ObtenerSaldo();
        }
        return -1;
    }
}