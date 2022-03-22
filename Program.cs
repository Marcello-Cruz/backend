using System;
using Exercicio_tarefas.db;

bool sair = false;
while (!sair)
{
  string opcao = UI.SelecionaOpcaoEmMenu();

  switch (opcao)
  {
    case "L": ListarTodasAsTarefas(); break;
    case "P": ListarTarefasPendentes(); break;
    case "I": ListarTarefasPorId(); break;
    case "D": ListarTarefasPorDescricao(); break;
    case "N": IncluirNovaTarefa(); break;
    case "A": AlterarDescricaoDaTarefa(); break;
    case "C": ConcluirTarefa(); break;
    case "E": ExcluirTarefa(); break;

    case "S":
      sair = true;
      break;

    default:
      UI.ExibeErro("\nOpção não reconhecida.");
      break;
  }

  Console.Write("\nPressione uma tecla para continuar...");
  Console.ReadKey();
}

void ListarTodasAsTarefas()
{
  UI.ExibeDestaque("\n-- Listar todas as Tarefas ---");


  using (var _db = new tarefasContext())
  {
    var tarefas = _db.Tarefas.ToList<Tarefa>();

    Console.WriteLine($"{tarefas.Count()} tarefa(s) encontrada(s).");

    foreach (var tarefa in tarefas)
    {
      Console.WriteLine($"Id #{tarefa.Id}: {tarefa.Descricao} [{(tarefa.Concluida ? "X" : " ")}] ");
    }

  }

}

void ListarTarefasPendentes()
{
  UI.ExibeDestaque("\n-- Listar Tarefas Pendentes ---");

  using (var _db = new tarefasContext())
  {
    var tarefas = _db.Tarefas.Where(t => !t.Concluida)
                             .ToList<Tarefa>();

    Console.WriteLine($"{tarefas.Count()} tarefa(s) encontrada(s).");

    foreach (var tarefa in tarefas)
    {
      Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");
    }
  }
}

void ListarTarefasPorDescricao()
{
  UI.ExibeDestaque("\n-- Listar Tarefas por Descrição ---");
  string descricao = UI.SelecionaDescricao();

  using (var _db = new tarefasContext())
  {
    var tarefas = _db.Tarefas.Where(t => t.Descricao.Contains(descricao))
                             .ToList<Tarefa>();

    Console.WriteLine($"{tarefas.Count()} tarefa(s) encontrada(s) com os valor(es) \"{descricao}\".");

    foreach (var tarefa in tarefas)
    {
      Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");
    }
  }

}

void ListarTarefasPorId()
{
  UI.ExibeDestaque("\n-- Listar Tarefas por Id ---");
  int id = UI.SelecionaId();

  using (var _db = new tarefasContext())
  {
    var tarefas = _db.Tarefas.Find(id);


    if (tarefas == null)
    {
      Console.WriteLine($"Tarefa não encontrada(s).");
      return;
    }
    Console.WriteLine($"[{(tarefas.Concluida ? "X" : "")}]#{tarefas.Id}: {tarefas.Descricao}");
  }
}

void IncluirNovaTarefa()
{
  UI.ExibeDestaque("\n-- Incluir Nova Tarefa ---");
  string descricao = UI.SelecionaDescricao();

  if (string.IsNullOrEmpty(descricao))
  {
    UI.ExibeErro("Não é possivel incluir tarefa sem descrição");
    return;
  }

  using (var _db = new tarefasContext())
  {
    var tarefa = new Tarefa()
    {
      Descricao = descricao,
    };
    _db.Tarefas.Add(tarefa);
    _db.SaveChanges();

    Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");
  }
}

void AlterarDescricaoDaTarefa()
{
  UI.ExibeDestaque("\n-- Alterar Descrição da Tarefa ---");
  int id = UI.SelecionaId();
  string descricao = UI.SelecionaDescricao();

  if (string.IsNullOrEmpty(descricao))
  {
    UI.ExibeErro("Não é permitido deixar uma tarefa sem descrição");
    return;
  }

  using (var _db = new tarefasContext())
  {

    var tarefa = _db.Tarefas.Find(id);

    if (tarefa == null)
    {
      Console.WriteLine($"Tarefa não encontrada.");
      return;
    }

    tarefa.Descricao = descricao;
    _db.SaveChanges();

    Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");

  }
}

void ConcluirTarefa()
{
  UI.ExibeDestaque("\n-- Concluir Tarefa ---");
  int id = UI.SelecionaId();

  using (var _db = new tarefasContext())
  {

    var tarefa = _db.Tarefas.Find(id);

    if (tarefa == null)
    {
      Console.WriteLine($"Tarefa não encontrada.");
      return;
    }

    if (tarefa.Concluida)
    {
      UI.ExibeErro($"Tarefa já concluida.");
      return;
    }

    tarefa.Concluida = true;
    _db.SaveChanges();

    Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");
  }
}

void ExcluirTarefa()
{
  UI.ExibeDestaque("\n-- Excluir Tarefa ---");
  int id = UI.SelecionaId();

  using (var _db = new tarefasContext())
  {
    var tarefa = _db.Tarefas.Find(id);

    if (tarefa == null)
    {
      Console.WriteLine($"Tarefa não encontrada.");
      return;
    }

    _db.Tarefas.Remove(tarefa);
    _db.SaveChanges();

    Console.WriteLine($"[{(tarefa.Concluida ? "X" : "")}]#{tarefa.Id}: {tarefa.Descricao}");
  }
}