public class UI
{
    public static void ExibeDestaque(string texto)
    {
        var corLetraAnterior = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(texto);
        Console.ForegroundColor = corLetraAnterior;
    }

    public static void ExibeErro(string texto)
    {
        var corLetraAnterior = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(texto);
        Console.ForegroundColor = corLetraAnterior;
    }

    public static string SelecionaOpcaoEmMenu()
    {
        Console.Clear();
        ExibeDestaque("-- Tarefas --\n");
        Console.WriteLine("[L]istar todas as Tarefas");
        Console.WriteLine("Listar Tarefas [P]endentes");
        Console.WriteLine("Listar Tarefa por [I]d");
        Console.WriteLine("Listar Tarefas por [D]escrição");
        Console.WriteLine("Incluir [N]ova Tarefa");
        Console.WriteLine("[A]lterar Descrição da Tarefa");
        Console.WriteLine("[C]oncluir Tarefa");
        Console.WriteLine("[E]xcluir Tarefa");
        Console.WriteLine("[S]air");
        Console.Write("\nDigite a sua opção: ");

        string entrada = (Console.ReadLine() ?? "").ToUpper().Trim();
        return entrada.Length < 2 ? entrada : entrada.Substring(0, 1);
    }

    public static int SelecionaId()
    {
        Console.Write("Digite o Id desejado: ");
        string entrada = (Console.ReadLine() ?? "").Trim();

        int id;
        bool digitouNumero = Int32.TryParse(entrada, out id);

        return digitouNumero ? id : -1;
    }

    public static string SelecionaDescricao()
    {
        Console.Write("Digite a Descrição desejada: ");
        return (Console.ReadLine() ?? "").Trim();
    }
}