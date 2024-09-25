# PartilhaAPI

O **PartilhaAPI** é um aplicativo projetado para facilitar a divisão de contas entre amigos. Semelhante ao Split Wise, o aplicativo permite que os usuários cadastrem amigos, registrem transações e acompanhem quanto cada um deve.

## Funcionalidades

- **Cadastro de Usuários**: Usuários podem se registrar no aplicativo.
- **Adição de Amigos**: Usuários podem adicionar outros usuários como amigos.
- **Registro de Transações**: Usuários podem criar transações, definindo quem pagou e quem deve.
- **Divisão Igualitária**: O valor total de cada transação é dividido igualmente entre todos os membros selecionados na transação.
- **Visualização de Saldos**: Usuários podem ver o total que devem e quanto cada amigo deve a eles.

## Regras de Negócio

- Os usuários podem emprestar ou pegar emprestado de outros usuários.
- Cada transação é registrada com um pagador e uma lista de membros envolvidos.
- O sistema calcula automaticamente quanto cada usuário deve aos outros com base nas transações registradas.
- Não há opção de quitar dívidas; o sistema apenas registra transações.

## Entidades

### User
Representa um usuário do aplicativo.

- **Id**: UUID (chave primária)
- **Name**: Nome do usuário
- **Email**: Email único do usuário

### Friend
Representa a relação de amizade entre usuários.

- **Id**: UUID (chave primária)
- **UserId**: UUID (FK do usuário que adicionou o amigo)
- **FriendId**: UUID (FK do amigo adicionado)

### Transaction
Representa uma transação registrada por um usuário.

- **Id**: UUID (chave primária)
- **Description**: Descrição da transação (ex: "Uber")
- **TotalAmount**: Valor total da transação
- **CreatedAt**: Data de criação da transação
- **CreatedById**: UUID (FK do usuário que criou a transação)
- **PaidById**: UUID (FK do usuário que pagou a transação)

### TransactionMember
Representa cada membro envolvido em uma transação.

- **Id**: UUID (chave primária)
- **TransactionId**: UUID (FK referenciando Transaction)
- **UserId**: UUID (FK referenciando User)
- **AmountOwed**: Valor que o membro deve

## Estrutura do Banco de Dados

A estrutura do banco de dados pode ser visualizada no [dbdiagram.io](https://dbdiagram.io). 
Abaixo está o código utilizado para criar o diagrama:

```sql
Table User {
  Id uuid [pk, unique]
  Name varchar(100) [not null]
  Email varchar(255) [not null, unique]
}

Table Friend {
  Id uuid [pk, unique]
  UserId uuid [not null]
  FriendId uuid [not null]
}

Table Transaction {
  Id uuid [pk, unique]
  Description varchar(255) [not null]
  TotalAmount decimal(18, 2) [not null]
  CreatedAt datetime [default: `now()`]
  CreatedById uuid [not null]
  PaidById uuid [not null]
}

Table TransactionMember {
  Id uuid [pk, unique]
  TransactionId uuid [not null]
  UserId uuid [not null]
  AmountOwed decimal(18, 2) [not null]
}

// Relacionamentos
Ref: Friend.UserId > User.Id
Ref: Friend.FriendId > User.Id
Ref: Transaction.CreatedById > User.Id
Ref: Transaction.PaidById > User.Id
Ref: TransactionMember.TransactionId > Transaction.Id
Ref: TransactionMember.UserId > User.Id
```

### Tecnologias Utilizadas
- .NET Core: Para o desenvolvimento da API.
- Entity Framework Core: Para o gerenciamento do banco de dados.
- PostgreSQL: Para armazenamento de dados.
- Firebase Authentication: Para autenticação de usuários.

### Instalação
- Clone o repositório.
- Configure a string de conexão para o PostgreSQL no arquivo appsettings.json.
- Execute as migrações do Entity Framework para criar o banco de dados.
- Inicie a aplicação.

### Contribuição
- Contribuições são bem-vindas! Sinta-se à vontade para abrir um pull request ou relatar problemas.

### Licença
- Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.


### entity framework commands

```
Add-Migration "StartMigration"
Update-Database
```