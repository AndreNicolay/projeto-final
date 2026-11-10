# Projeto Final: Produtos

Sistema com API em C# (.NET 10), frontend em Vue 3 e banco MySQL no alwaysdata.

## Como rodar
1. Instale Docker Desktop e Git.
2. Clone o repositório.
3. Copie `.env.example` para `.env` e preencha com os dados do seu banco.
4. No painel do alwaysdata, rode `banco/schema.sql` no phpMyAdmin.
5. Na raiz, rode: `docker compose up --build`
6. Abra http://localhost:3000 (site) e http://localhost:8080 (API).

## Estrutura
- `backend/`: API em C# com Dapper
- `frontend/`: Vue 3 servido pelo nginx
- `banco/schema.sql`: criação das tabelas
- `compose.yaml`: liga os dois containers
