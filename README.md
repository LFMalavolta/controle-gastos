# Controle de Gastos



Sistema fullstack para controle de gastos residenciais.



## Tecnologias utilizadas

- Backend: .NET 7 (Web API)
- Frontend: React + TypeScript
- Banco de dados: SQLite

## 📋 Funcionalidades
- Cadastro de pessoas
- Cadastro de categorias
- Cadastro de transações
- Regras de negócio:
  - Menores de idade só podem ter despesas
  - Categorias respeitam tipo (despesa/receita)
- Relatórios:
  - Totais por pessoa
  - Totais por categoria (opcional)

## Como executar o projeto
### Backend
```bash
cd Backend
dotnet run

API disponível em -> http://localhost:5140
```

### Frontend
```bash
cd frontend
npm install
npm start

Aplicação disponível em -> http://localhost:3000
```
