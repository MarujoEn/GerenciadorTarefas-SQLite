using Microsoft.Data.Sqlite;
using SQLitePCL;

string conexaoString = "Data Source=tarefas.db";

using (var conexao = new SqliteConnection(conexaoString))
{
    conexao.Open(); 

    string criarTabelaSql = @"
        CREATE TABLE IF NOT EXISTS Tarefas (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Concluida INTEGER NOT NULL DEFAULT 0
        );";

    using (var comando = conexao.CreateCommand())
    {
        comando.CommandText = criarTabelaSql; 
        
        comando.ExecuteNonQuery();
    } 

}

try
{


    while (true)
    {
        Console.WriteLine("Digite qual opção você deseja: ");
        Console.WriteLine("1 - Adicionar Tarefa a Lista.");
        Console.WriteLine("2 - Remover Tarefa da Lista.");
        Console.WriteLine("3 - Listar tarefas pendentes.");
        Console.WriteLine("4 - Listar todas as tarefas.");
        Console.WriteLine("5 - Concluir tarefa");
        Console.WriteLine("6 - Finalizar aplicação");

        string opcao = Console.ReadLine()!;

        if (opcao == "1")
        {
            Console.WriteLine("Digite sua nova tarefa: ");
            string NovaTarefa = Console.ReadLine()!;

            if (string.IsNullOrEmpty(NovaTarefa))
            {
                Console.WriteLine("O nome da tarefa não pode ser vazio.");
            }

            else
            {
                 Console.WriteLine("Tem certeza? (Y/N)");
                string confirmacao = Console.ReadLine()!.Trim().ToUpper();

                if (confirmacao == "Y")
                {
                    using(var conexao = new SqliteConnection(conexaoString))
                    {
                        conexao.Open();

                        using(var comando = conexao.CreateCommand())
                        {
                            comando.CommandText = "INSERT INTO Tarefas (Nome, Concluida) VALUES (@nome, 0)";
                            comando.Parameters.AddWithValue("@nome", NovaTarefa);

                            comando.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Tarefa salva com sucesso.");
                }
                else
                {
                    Console.WriteLine("Operação cancelada. Voltando ao menu...");
                }
            }

            
        }

        else if (opcao == "2")
        {
            bool temTarefa = false;

            using (var conexao = new SqliteConnection(conexaoString))
            {
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT Id, Nome, Concluida FROM Tarefas";

                    using(var leitor = comando.ExecuteReader())
                    {
                        

                        while(leitor.Read())
                        {
                            temTarefa = true;

                            int Id = leitor.GetInt32(0);
                            string Nome = leitor.GetString(1);
                            bool Concluida = leitor.GetInt32(2) == 1;

                            string statusTexto = Concluida ? "[Concluida]" : "[Incompleta]";

                            Console.WriteLine($"{Id} - {Nome} - {statusTexto}");

                        }
                        
                    }

                }

                if (!temTarefa)
                {
                    Console.WriteLine("Não há nada para ser listado, lista vazia.");
                }

                else
                {
                    using(var comando = conexao.CreateCommand())
                    {

                        Console.WriteLine("Digite o ID da tarefa que deseja excluir: ");
                                string exclusaoEscolhida = Console.ReadLine()!.Trim();

                                Console.WriteLine("Tem certeza que deseja excluir esta tarefa? (Y/N)");
                                string confirmacao = Console.ReadLine()!.Trim().ToUpper();

                                if (confirmacao == "Y")
                                {

                                    comando.CommandText = "DELETE FROM Tarefas WHERE Id = @id";
                                    comando.Parameters.AddWithValue("@id", exclusaoEscolhida);

                                    comando.ExecuteNonQuery();

                                    Console.WriteLine("Tarefa excluida com sucesso.");
                                }
                                else
                                {
                                    Console.WriteLine("Operação cancelada. Voltando ao menu...");
                                }
                    }
                }
            }
        }

        else if (opcao == "3")
        {
             using (var conexao = new SqliteConnection(conexaoString))
            {
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT Id, Nome, Concluida FROM Tarefas WHERE Concluida = 0";

                    using(var leitor = comando.ExecuteReader())
                    {
                        bool temTarefa = false;

                        while(leitor.Read())
                        {
                            temTarefa = true;

                            int Id = leitor.GetInt32(0);
                            string Nome = leitor.GetString(1);

                            Console.WriteLine($"{Id} - {Nome} - [Incompleta]");
                        }

                        if (!temTarefa)
                        {
                            Console.WriteLine("Não há nada para ser listado, pendentes concluidas.");
                        }
                    }

                }
            }
        }

        else if (opcao == "4")
        {
            using (var conexao = new SqliteConnection(conexaoString))
            {
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT Id, Nome, Concluida FROM Tarefas";

                    using(var leitor = comando.ExecuteReader())
                    {
                        bool temTarefa = false;

                        while(leitor.Read())
                        {
                            temTarefa = true;

                            int Id = leitor.GetInt32(0);
                            string Nome = leitor.GetString(1);
                            bool Concluida = leitor.GetInt32(2) == 1;

                            string statusTexto = Concluida ? "[Concluida]" : "[Incompleta]";

                            Console.WriteLine($"{Id} - {Nome} - {statusTexto}");
                        }

                        if (!temTarefa)
                        {
                            Console.WriteLine("Não há nada para ser listado, lista vazia.");
                        }
                    }

                }
            }
        }

        else if (opcao == "5")
        {
            bool temTarefa = false;

            using (var conexao = new SqliteConnection(conexaoString))
            {
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT Id, Nome, Concluida FROM Tarefas WHERE Concluida = 0";

                    using(var leitor = comando.ExecuteReader())
                    {
                        

                        while(leitor.Read())
                        {
                            temTarefa = true;

                            int Id = leitor.GetInt32(0);
                            string Nome = leitor.GetString(1);

                            Console.WriteLine($"{Id} - {Nome} - [Incompleta]");

                        }
                        
                    }

                }

                if (!temTarefa)
                {
                    Console.WriteLine("Não há nada para ser listado, pendentes concluidas.");
                }

                else
                {
                    using(var comando = conexao.CreateCommand())
                    {

                        Console.WriteLine("Digite o ID da tarefa que deseja concluir: ");
                                string conclusaoEscolhida = Console.ReadLine()!.Trim();

                                Console.WriteLine("Tem certeza que deseja concluir esta tarefa? (Y/N)");
                                string confirmacao = Console.ReadLine()!.Trim().ToUpper();

                                if (confirmacao == "Y")
                                {

                                    comando.CommandText = "UPDATE Tarefas SET Concluida = 1 WHERE Id = @id";
                                    comando.Parameters.AddWithValue("@id", conclusaoEscolhida);

                                    comando.ExecuteNonQuery();

                                    Console.WriteLine("Tarefa concluída com sucesso.");
                                }
                                else
                                {
                                    Console.WriteLine("Operação cancelada. Voltando ao menu...");
                                }
                    }
                }
            }
        }

        else if (opcao == "6")
        {
            break;
        }
    }

}
catch (System.Exception)
{
    Console.WriteLine("Erro encontrado. Reinicie a aplicação");
}