# gRPC Transactions Feedback

Este é um projeto simples para exemplificar gRPC em C#, parte da DM113. O **client** envia valores de transação e o **server** responde com o status (aprovada ou rejeitada) e o saldo atualizado.

- **client**: Envia uma sequência de valores de transação ao servidor e exibe o status de cada transação e o saldo atualizado.
- **server**: Mantém um saldo inicial e processa as transações enviadas pelo client, respondendo se a transação foi aprovada ou rejeitada e atualizando o saldo.

## Como executar o projeto:

Após clonado o repositório, abra o .sln do projeto e execute o comando abaixo no Package Manager Console:

```bash
dotnet restore
```
- Execute o server, por padrão ele utilizará a porta 5005. 
- Execute o client.

Observe que o client envia uma sequência de transações e o server "processa" e retorna se a transação foi aprovada ou rejeitada.

