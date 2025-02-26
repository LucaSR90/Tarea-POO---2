using System;

// Clase que representa un Motor con atributos de potencia y litros de aceite
class Motor
{
    private int litros_de_aceite;
    private int potencia;

    // Constructor que inicializa la potencia y deja los litros de aceite en 0
    public Motor(int potencia)
    {
        this.potencia = potencia;
        this.litros_de_aceite = 0;
    }

    // Métodos getter y setter para los atributos
    public int GetLitrosDeAceite() => litros_de_aceite;
    public int GetPotencia() => potencia;
    public void SetLitrosDeAceite(int litros) => litros_de_aceite = litros;
    public void SetPotencia(int potencia) => this.potencia = potencia;
}

// Clase que representa un Coche con marca, modelo, motor y costos de averías
class Coche
{
    private Motor motor;
    private string marca;
    private string modelo;
    private double precio_acumulado;
    private string ultimaAveria;

    // Constructor que inicializa la marca, el modelo y la potencia del motor
    public Coche(string marca, string modelo, int potenciaMotor)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.motor = new Motor(potenciaMotor);
        this.precio_acumulado = 0;
        this.ultimaAveria = "Ninguna";
    }

    // Métodos getter para los atributos
    public string GetMarca() => marca;
    public string GetModelo() => modelo;
    public double GetPrecioAcumulado() => precio_acumulado;
    public Motor GetMotor() => motor;
    public string GetUltimaAveria() => ultimaAveria;

    // Método para acumular costos de averías
    public void AcumularAveria(double importe, string averia)
    {
        precio_acumulado += importe;
        ultimaAveria = averia;
    }
}

// Clase que representa un Garaje que atiende coches
class Garaje
{
    private Coche cocheEnReparacion;
    private string averiaAsociada;
    private int cochesAtendidos;

    // Método para aceptar un coche en el garaje si no hay otro en reparación
    public bool AceptarCoche(Coche coche, string averia)
    {
        if (cocheEnReparacion != null)
        {
            return false; // Ya hay un coche en reparación
        }
        cocheEnReparacion = coche;
        averiaAsociada = averia;
        cochesAtendidos++;

        // Se genera un costo aleatorio de la avería
        double importeAveria = new Random().Next(100, 1000); // Entre 100 y 1000 pesos
        coche.AcumularAveria(importeAveria, averia);

        // Si la avería es "aceite", se incrementan los litros de aceite en 10
        if (averia == "aceite")
        {
            coche.GetMotor().SetLitrosDeAceite(coche.GetMotor().GetLitrosDeAceite() + 10);
        }
        return true;
    }

    // Método para devolver el coche y permitir la entrada de otro
    public void DevolverCoche()
    {
        cocheEnReparacion = null;
        averiaAsociada = null;
    }
}

// Clase principal que ejecuta la simulación del garaje y los coches
class PracticaPOO
{
    static void Main()
    {
        // Creación del garaje y dos coches
        Garaje garaje = new Garaje();
        Coche coche1 = new Coche("Toyota", "Corolla", 120);
        Coche coche2 = new Coche("Honda", "Civic", 130);

        string[] averias = { "aceite", "frenos", "motor" };
        Random random = new Random();

        // Se atienden los coches al menos dos veces en el garaje
        for (int i = 0; i < 2; i++)
        {
            while (!garaje.AceptarCoche(coche1, averias[random.Next(averias.Length)]))
                garaje.DevolverCoche();
            garaje.DevolverCoche();

            while (!garaje.AceptarCoche(coche2, averias[random.Next(averias.Length)]))
                garaje.DevolverCoche();
            garaje.DevolverCoche();
        }

        // Se muestra la información final de los coches
        Console.WriteLine($"Coche 1: {coche1.GetMarca()} {coche1.GetModelo()}, Motor: {coche1.GetMotor().GetPotencia()}HP, Costo en averías: {coche1.GetPrecioAcumulado():F2} pesos, Litros de aceite: {coche1.GetMotor().GetLitrosDeAceite()}, Última avería: {coche1.GetUltimaAveria()}");
        Console.WriteLine($"Coche 2: {coche2.GetMarca()} {coche2.GetModelo()}, Motor: {coche2.GetMotor().GetPotencia()}HP, Costo en averías: {coche2.GetPrecioAcumulado():F2} pesos, Litros de aceite: {coche2.GetMotor().GetLitrosDeAceite()}, Última avería: {coche2.GetUltimaAveria()}");
    }
}
