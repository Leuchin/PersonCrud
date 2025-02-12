<template>
  <div class="person-list-card">
    <h2>Lista de Pessoas</h2>

    <!-- Tabela de pessoas -->
    <table>
      <thead>
        <tr>
          <th>Nome</th>
          <th>Idade</th>
          <th>Biografia</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="person in persons"
          :key="person.id"
          :class="{
            'highlight-minor': person.age < 18
          }"
        >
          <td>{{ person.name }}</td>
          <td>{{ person.age }}</td>
          <td>{{ person.biography }}</td>
          <td>
            <!-- Botão para editar (emite evento para o pai) -->
            <button class="btn edit" @click="editPerson(person)">Editar</button>
            <!-- Botão para deletar -->
            <button class="btn delete" @click="deletePerson(person.id)">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';

export default defineComponent({
  name: 'PersonList',
  emits: ['edit'], 
  data() {
    return {
      persons: [] as PersonItem[],
    };
  },
  methods: {
    async fetchPersons() {
      try {
        const response = await api.get('/persons');
        this.persons = response.data;
      } catch (error) {
        console.error('Erro ao buscar lista de Pessoas:', error);
      }
    },

   
    async deletePerson(id?: string) {
      if (!id) return;
      if (!confirm('Tem certeza que deseja excluir esta pessoa?')) return;

      try {
        await api.delete(`/persons/${id}`);
        await this.fetchPersons(); 
      } catch (error) {
        console.error('Erro ao deletar Pessoa:', error);
      }
    },

    
    editPerson(person: PersonItem) {
      this.$emit('edit', person);
    },
  },
  
  mounted() {
    this.fetchPersons();
  },
});
</script>

<style scoped>
.person-list-card {
  max-width: 800px;
  margin: 0 auto 30px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  padding: 24px;
}

.person-list-card h2 {
  margin-bottom: 20px;
  font-size: 1.6rem;
  color: #333;
  text-align: center;
}

table {
  width: 100%;
  border-collapse: collapse;
  font-size: 0.95rem;
}

thead tr {
  background-color: #007bff;
  color: #fff;
}

th, td {
  padding: 12px;
  border-bottom: 1px solid #ddd;
  text-align: left;
}

tbody tr:hover {
  background-color: #f8f9fa;
}

.highlight-minor {
  background-color: #fff3cd !important; /* amarelinho p/ menores de 18 */
}

td:last-child {
  width: 150px;
}

.btn {
  border: none;
  border-radius: 4px;
  padding: 8px 14px;
  margin-right: 6px;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background-color 0.2s, transform 0.2s;
}

.btn:hover {
  opacity: 0.9;
}

.btn:active {
  transform: scale(0.96);
}

.btn.edit {
  background-color: #17a2b8;
  color: #fff;
}

.btn.delete {
  background-color: #dc3545;
  color: #fff;
}
</style>
