<template>
    <div class="Person-List-Container">
        <h2>Formulário Pessoa</h2>
        <table>
            <thead>
                <tr>
                    <th>Nome</th>
                    <th>Idade</th>
                    <th>Biografia</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="Person in Persons" :key="Person.id" class="{
                    'table-danger': Person.age < 18,
                    'table-success': Person.age >= 18}">
                    <td>{{ Person.name }}</td>
                    <td>{{ Person.age }}</td>
                    <td>{{ Person.biography }}</td>
                    <!--Botões de edit, delete e Atualizar
                    -->
                    <td>
                        <button class="btn info" @click="editPerson(Person)">Editar</button>
                        <button class="btn danger" @click="deletePerson(Person.id)">Deletar</button>
                        <button class= "btn warning" @click = "renewPerson(Person.id)">Renovar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<!-- Script em Typescript-->
<script lang="ts">
import { defineComponent } from 'vue';
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';

export default defineComponent({
    name: 'PersonList',
    emit:['editPesons'],
    data() {
        return {
            Persons: [] as PersonItem[],

        };
    },
        methods: {
        async fetchPersons() {
            try {
                const response = await api.get('/person');
                this.Persons = response.data;
            } catch (error) {
                console.error(error);
            }
        },
        async deletePerson(id?: number) {
            if(!id) return;
            try{
                await api.delete(`/person/${id}`);
                this.fetchPersons();
            } catch (error) {
                console.error(error);
            }
        },
        async renewPerson(id?: number) {
            if(!id) return;
            try{
                await api.put(`/person/${id}/renew`);
                this.fetchPersons();
            } catch (error) {
                console.error(error);
            }
        },
        editPerson(Person: PersonItem) {
            this.$emit('edit', Person);
        },
    },
    mounted() {
        this.fetchPersons();
    },
});
   
</script>

<!-- Estilização em CSS-->'
<style scoped>
.Person-List-Container {
    background-color: antiquewhite;
    border: 1px solid #242222;
    border-radius: 5px;
    padding: 16px 18px;
    margin-bottom: 30px;  
}

h2{
    margin-bottom: 20px;
    font-size:1.2rem;
    color: #242222;
}

table{
    width: 100%;
    border-collapse: collapse;
    font-size:0.95rem;
}

th, td{
    border: 1px solid #242222;
    padding: 8px;
    border-bottom: 1px solid #242222;
}

th{
    background-color: #242222;
    color: #fff;
    border: none;
}
.btn {
  border: none;
  border-radius: 4px;
  padding: 6px 12px;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background-color 0.2s, box-shadow 0.2s;
}

.btn.info {
  background-color: #17a2b8;
  color: #fff;
}
.btn.info:hover {
  background-color: #138496;
}

.btn.danger {
  background-color: #dc3545;
  color: #fff;
}
.btn.danger:hover {
  background-color: #c82333;
}

.btn.warning {
  background-color: #ffc107;
  color: #333;
}
.btn.warning:hover {
  background-color: #e0a800;
}
</style>