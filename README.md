# 📝 Gerenciador de Tarefas (To-Do List) com SQLite

Um aplicativo de console em C# robusto para gerenciamento de tarefas diárias, desenvolvido como parte do aprendizado no 2º Semestre da ETEC. O projeto marca a minha primeira transição de armazenamento em memória temporária para persistência real em banco de dados relacional utilizando **SQLite**.

---

## 🚀 Funcionalidades

O sistema conta com um CRUD completo executado diretamente no banco de dados:
1. **Adicionar Tarefa:** Cria um novo registro na tabela com status inicial pendente.
2. **Remover Tarefa:** Exclui permanentemente uma tarefa do banco utilizando o `ID`.
3. **Listar Tarefas Pendentes:** Filtra e exibe apenas o que ainda precisa ser feito.
4. **Listar Todas as Tarefas:** Mostra o panorama geral do banco (concluídas e incompletas).
5. **Concluir Tarefa:** Atualiza o status de uma tarefa pendente para concluída (`UPDATE`).
6. **Finalizar Aplicação:** Encerra o loop do console com segurança.

---

## 🛠️ Tecnologias e Aprendizados

* **C# (.NET):** Estrutura lógica, tratamento de erros com `try-catch`, manipulação de strings e loops de controle.
* **SQLite / Microsoft.Data.Sqlite:** Criação automatizada de tabelas (`CREATE TABLE IF NOT EXISTS`), comandos parametrizados para evitar SQL Injection, leitura de dados com `ExecuteReader()` e manipulações com `ExecuteNonQuery()`.
* **Git & GitHub:** Versionamento de código profissional utilizando boas práticas de `.gitignore` para blindar arquivos binários e o banco de dados local (`*.db`).

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado na sua máquina.

### Passo a Passo
1. Clone este repositório:
   ```bash
   git clone [https://github.com/MarujoEn/GerenciadorTarefas-SQLite.git](https://github.com/MarujoEn/GerenciadorTarefas-SQLite.git)