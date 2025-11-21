using static System.Console;
using System.Linq; // Importante para buscar na lista

public class UX
{
    private readonly Banco _banco;
    private readonly string _titulo;

    public UX(string titulo, Banco banco)
    {
        _titulo = titulo;
        _banco = banco;
    }

    public void Executar()
    {
        CriarTitulo(_titulo);
        WriteLine(" [1] Criar Conta");
        WriteLine(" [2] Listar Contas");
        WriteLine(" [3] Efetuar Saque");     // Funcionalidade Faltante
        WriteLine(" [4] Efetuar Depósito");  // Funcionalidade Faltante
        WriteLine(" [5] Aumentar Limite");   // Funcionalidade Faltante
        WriteLine(" [6] Diminuir Limite");   // Funcionalidade Faltante
        
        ForegroundColor = ConsoleColor.Red;
        WriteLine("\n [9] Sair");
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        Write(" Digite a opção desejada: ");
        var opcao = ReadLine() ?? "";
        ForegroundColor = ConsoleColor.White;
        
        switch (opcao)
        {
            case "1": CriarConta(); break;
            case "2": MenuListarContas(); break;
            case "3": MenuSacar(); break;          // Novo
            case "4": MenuDepositar(); break;      // Novo
            case "5": MenuAumentarLimite(); break; // Novo
            case "6": MenuDiminuirLimite(); break; // Novo
        }
        
        if (opcao != "9")
        {
            Executar();
        }
        else 
        {
            // Salva apenas ao sair
            _banco.SaveContas();
        }
    }

    private void CriarConta()
    {
        CriarTitulo(_titulo + " - Criar Conta");
        Write(" Numero:  ");
        var numero = Convert.ToInt32(ReadLine());
        Write(" Cliente: ");
        var cliente = ReadLine() ?? "";
        Write(" CPF:     ");
        var cpf = ReadLine() ?? "";
        Write(" Senha:   ");
        var senha = ReadLine() ?? "";
        Write(" Limite:  ");
        var limite = Convert.ToDecimal(ReadLine());

        var conta = new Conta(numero, cliente, cpf, senha, limite);
        _banco.Contas.Add(conta);

        CriarRodape("Conta criada com sucesso!");
    }

    private void MenuListarContas()
    {
        CriarTitulo(_titulo + " - Listar Contas");
        foreach (var conta in _banco.Contas)
        {
            WriteLine($" Conta: {conta.Numero} - {conta.Cliente}");
            WriteLine($" Saldo: {conta.Saldo:C} | Limite: {conta.Limite:C}");
            WriteLine($" Saldo Disponível: {conta.SaldoDisponivel:C}\n");
        }
        CriarRodape();
    }

    // --- NOVOS MÉTODOS IMPLEMENTADOS ---

    private void MenuSacar()
    {
        CriarTitulo(_titulo + " - Saque");
        var conta = BuscarConta(); // Usa o método auxiliar para achar a conta
        
        if (conta != null)
        {
            Write(" Valor do Saque: ");
            var valor = Convert.ToDecimal(ReadLine());
            
            bool sucesso = conta.Sacar(valor);
            if (sucesso)
                CriarRodape("Saque realizado com sucesso!");
            else
                CriarRodape("Erro: Saldo insuficiente ou valor inválido.");
        }
    }

    private void MenuDepositar()
    {
        CriarTitulo(_titulo + " - Depósito");
        var conta = BuscarConta();
        
        if (conta != null)
        {
            Write(" Valor do Depósito: ");
            var valor = Convert.ToDecimal(ReadLine());
            
            conta.Depositar(valor);
            CriarRodape("Depósito realizado com sucesso!");
        }
    }

    private void MenuAumentarLimite()
    {
        CriarTitulo(_titulo + " - Aumentar Limite");
        var conta = BuscarConta();
        
        if (conta != null)
        {
            Write(" Valor para aumentar: ");
            var valor = Convert.ToDecimal(ReadLine());
            
            conta.AumentarLimite(valor);
            CriarRodape($"Novo limite: {conta.Limite:C}");
        }
    }

    private void MenuDiminuirLimite()
    {
        CriarTitulo(_titulo + " - Diminuir Limite");
        var conta = BuscarConta();
        
        if (conta != null)
        {
            Write(" Valor para diminuir: ");
            var valor = Convert.ToDecimal(ReadLine());
            
            bool sucesso = conta.DiminuirLimite(valor);
            if (sucesso)
                CriarRodape($"Novo limite: {conta.Limite:C}");
            else
                CriarRodape("Erro: Valor inválido ou maior que o limite atual.");
        }
    }

    // Método auxiliar para não repetir código de busca
    private Conta? BuscarConta()
    {
        Write(" Digite o Número da Conta: ");
        int numero = Convert.ToInt32(ReadLine());

        // Procura na lista de contas do banco
        foreach(var c in _banco.Contas)
        {
            if (c.Numero == numero) return c;
        }

        CriarRodape("Conta não encontrada!");
        return null;
    }

    // --- MÉTODOS VISUAIS ---

    private void CriarLinha()
    {
        WriteLine("-------------------------------------------------");
    }

    private void CriarTitulo(string titulo)
    {
        Clear();
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(" " + titulo);
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
    }

    private void CriarRodape(string? mensagem = null)
    {
        CriarLinha();
        ForegroundColor = ConsoleColor.Green;
        if (mensagem != null)
            WriteLine(" " + mensagem);
        Write(" ENTER para continuar");
        ForegroundColor = ConsoleColor.White;
        ReadLine();
    }
}