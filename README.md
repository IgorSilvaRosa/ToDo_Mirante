# ToDo_Mirante - API

Gerenciamento de criação, alteração, exclusão e listagem de tarefas

## Como executar

### Pré requisitos
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- Visual Studio 2022 ou Vs Code

### Clonar Projeto

```
git clone https://github.com/IgorSilvaRosa/ToDo_Mirante.git
cd ToDo_Mirante

dotnet restore
dotnet run --project ToDo.API
```

Para acessa via Swagger
https://localhost:7073/swagger

## Endpoints

### Criar Tarefa
POST /api/Tarefa

Request:
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "status": 0,
  "dataVencimento": "2025-06-20T17:26:18.292Z"
}

Response:
- 201 Created

{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "status": 0,
  "dataVencimento": "2025-06-20T17:26:18.292Z"
}

### Erros
- 400 Bad Request -> Status inválido.
- 400 Bad Request -> Título obrigatório

### Atualizar Tarefa
PUT /api/Tarefa/{id}

Request:
{
  "id": 1,
  "titulo": "string",
  "descricao": "string",
  "status": 0,
  "dataVencimento": "2025-06-20T17:26:18.292Z"
}

Response:
- 204 

### Erros
- 400 Bad Request -> Status inválido.
- 400 Bad Request -> Dados inválidos.
- 400 Bad Request -> Insira um Id maior que zero.
- 400 Bad Request -> Os Ids estão divergentes id:{id} e TarefaDto: {dto.Id}"
- 400 Bad Request -> Favor inserir o título da tarefa.

### Deletar Tarefa
DELETE /api/Tarefa/{id}

Request:
api/Tarefa/1

Response:
- 204 

### Erros
- 400 Bad Request -> Insira um Id maior que zero.

### Listar Tarefas
GET /api/Tarefa

Request:
api/Tarefa

Response:
- 200 

Exemplo
[
  {
    "id": 2,
    "titulo": "IGOR",
    "descricao": "string",
    "status": 0,
    "dataVencimento": "2025-06-19T20:30:39.689"
  },
  {
    "id": 8,
    "titulo": "string",
    "descricao": "string",
    "status": 0,
    "dataVencimento": "2025-06-20T17:26:18.292"
  }
]

### Filtrar Tarefas (Status, DataVencimento)
GET /api/Tarefa/Filtro

Request:
api/Tarefa/filtro?Status=0&DataVencimento=2025-06-20T17%3A26%3A18.292' 

Response:
- 200 

Exemplo
[
  {
    "id": 8,
    "titulo": "string",
    "descricao": "string",
    "status": 0,
    "dataVencimento": "2025-06-20T17:26:18.292"
  }
]

