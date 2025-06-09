using System;
public class CuentaBancaria
{
    private string numeroCuenta;
    private decimal saldo;
    private string titular;

    public CuentaBancaria(string numero, string titularCuenta, decimal saldoInicial = 0)
    {
        numeroCuenta = numero;
        titular = titularCuenta;
        saldo = saldoInicial;
    }

    public string NumeroCuenta => numeroCuenta;
    public string Titular => titular;

    public decimal ObtenerSaldo()
    {
        return saldo;
    }

    public bool ModificarSaldo(decimal monto)
    {
        if (saldo + monto < 0)
            return false;

        saldo += monto;
        return true;
    }
}