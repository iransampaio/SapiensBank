using System.Text.Json.Serialization;

public class Conta
{
    public int Numero { get; set; }
    public string Cliente { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
    public decimal Saldo { get; set; }
    public decimal Limite { get; set; }

    [JsonIgnore]
    public decimal SaldoDisponivel => Saldo + Limite;

    public Conta(int numero, string cliente, string cpf, string senha, decimal limite = 0)
    {
        Numero = numero;
        Cliente = cliente;
        Cpf = cpf;
        Senha = senha;
        Limite = limite;
    }

    // --- MÉTODOS NOVOS ADICIONADOS PARA A ATIVIDADE ---

    public void Depositar(decimal valor)
    {
        if (valor > 0)
        {
            Saldo += valor;
        }
    }

    public bool Sacar(decimal valor)
    {
        // Verifica se o valor é positivo e se tem saldo+limite suficiente
        if (valor > 0 && SaldoDisponivel >= valor)
        {
            Saldo -= valor;
            return true; // Saque deu certo
        }
        return false; // Saque deu errado
    }

    public void AumentarLimite(decimal valor)
    {
        if (valor > 0)
        {
            Limite += valor;
        }
    }

    public bool DiminuirLimite(decimal valor)
    {
        // Não podemos tirar mais limite do que a pessoa tem
        if (valor > 0 && valor <= Limite)
        {
            Limite -= valor;
            return true;
        }
        return false;
    }
}