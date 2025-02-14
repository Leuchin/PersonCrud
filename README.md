# Criação de um CRUD com frontend

Um projeto simples, onde uma pessoa X colocará seu nome, idade e biografia.
Podendo atualizar e apagar.

## Backend

Feito em .Net 9, usando Entity Framework core e também SQLite.
O uso do SQLite é para não ter a necessidade de um servidor.
Diferente do SQL comum o SQLite é mais compacto e não tem necessidade de um servidor.
Sendo mais fácil de rodar sem a necessidade de adicionais.

## Frontend

Feito em Vue 3 + Typescript com o vite.

### Pré-Requisitos para teste

.Net 9,
Node.js 22
Npm

## Para utilizar o CRUD e testa-lo

O Crud precisará de 2 terminais para rodar o frontend e o backend;
Inicialmente no terminal 1, na pasta PersonCRUD use o comando "npm install".
Após no terminal 1 use "cd frontend" e usar o comando "npm install".
No terminal 2 use "cd backend" e usar o comando "npm install".
No terminal 1 para rodar use "npm run dev", e no terminal 2 use "dotnet run".

# Vue 3 + TypeScript + Vite

This template should help get you started developing with Vue 3 and TypeScript in Vite. The template uses Vue 3 `<script setup>` SFCs, check out the [script setup docs](https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup) to learn more.

Learn more about the recommended Project Setup and IDE Support in the [Vue Docs TypeScript Guide](https://vuejs.org/guide/typescript/overview.html#project-setup).
