using System;

class Motor
{
    private int litros_de_aceite;
    private int potencia;

    public Motor(int potencia)
    {
        this.potencia = potencia;
        this.litros_de_aceite = 0;
    }

    public int GetLitrosDeAceite() => litros_de_aceite;
    public int GetPotencia() => potencia;

    public void SetLitrosDeAceite(int litros) => litros_de_aceite = litros;
    public void SetPotencia(int potencia) => this.potencia = potencia;
}

class Coche
{
    private Motor motor;
    private string marca;
    private string modelo;
    private double precio_acumulado;

    public Coche(string marca, string modelo, int potenciaMotor)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.motor = new Motor(potenciaMotor);
        this.precio_acumulado = 0;
    }

    public string GetMarca() => marca;
    public string GetModelo() => modelo;
    public double GetPrecioAcumulado() => precio_acumulado;
    public Motor GetMotor() => motor;

    public void AcumularAveria(double importe)
    {
        precio_acumulado += importe;
    }
}

class Garaje
{
    private Coche cocheEnReparacion;
    private string averiaAsociada;
    private int cochesAtendidos;

    public bool AceptarCoche(Coche coche, string averia)
    {
        if (cocheEnReparacion != null)
        {
            return false; // Ya hay un coche en reparación
        }
        cocheEnReparacion = coche;
        averiaAsociada = averia;
        cochesAtendidos++;
        double importeAveria = new Random().NextDouble() * 1000;
        coche.AcumularAveria(importeAveria);
        if (averia == "aceite")
        {
            coche.GetMotor().SetLitrosDeAceite(coche.GetMotor().GetLitrosDeAceite() + 10);
        }
        return true;
    }

    public void DevolverCoche()
    {
        cocheEnReparacion = null;
        averiaAsociada = null;
    }
}

class PracticaPOO
{
    static void Main()
    {
        Garaje garaje = new Garaje();
        Coche coche1 = new Coche("Toyota", "Corolla", 120);
        Coche coche2 = new Coche("Honda", "Civic", 130);

        string[] averias = { "aceite", "frenos", "motor" };
        Random random = new Random();

        for (int i = 0; i < 2; i++)
        {
            while (!garaje.AceptarCoche(coche1, averias[random.Next(averias.Length)]))
                garaje.DevolverCoche();
            garaje.DevolverCoche();

            while (!garaje.AceptarCoche(coche2, averias[random.Next(averias.Length)]))
                garaje.DevolverCoche();
            garaje.DevolverCoche();
        }

        Console.WriteLine($"Coche 1: {coche1.GetMarca()} {coche1.GetModelo()}, Costo en averías: {coche1.GetPrecioAcumulado():F2}");
        Console.WriteLine($"Coche 2: {coche2.GetMarca()} {coche2.GetModelo()}, Costo en averías: {coche2.GetPrecioAcumulado():F2}");
    }
}
