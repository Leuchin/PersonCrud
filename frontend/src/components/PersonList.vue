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
          :class="{ 'highlight-minor': person.age < 18 }"
        >
          <td>{{ person.name }}</td>
          <td>{{ person.age }}</td>
          <td>{{ person.biography }}</td>
          <td>
            <!-- Botão para editar (abre modal de edição) -->
            <button class="btn edit" @click="openEditModal(person)">
              Editar
            </button>
            <!-- Botão para excluir (abre modal de confirmação) -->
            <button class="btn delete" @click="openDeleteModal(person)">
              Excluir
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <!-- MODAL DE EDIÇÃO -->
  <transition name="fade">
    <div class="modal-overlay" v-if="showEditModal">
      <div class="modal-content">
        <h3>Edição de Pessoa</h3>

        <div class="form-group">
          <label>Nome</label>
          <input 
            type="text"
            v-model="editData.name"
            placeholder="Nome da pessoa"
          />
        </div>

        <div class="form-group">
          <label>Idade</label>
          <input 
            type="number"
            v-model="editData.age"
            placeholder="Idade"
          />
        </div>

        <div class="form-group">
          <label>Biografia</label>
          <textarea 
            v-model="editData.biography"
            placeholder="Biografia..."
          ></textarea>
        </div>

        <div class="modal-buttons">
          <button class="btn primary" @click="confirmEdit">
            Salvar
          </button>
          <button class="btn secondary" @click="closeEditModal">
            Cancelar
          </button>
        </div>
      </div>
    </div>
  </transition>

  <!-- MODAL DE CONFIRMAÇÃO PARA EXCLUIR -->
  <transition name="fade">
    <div class="modal-overlay" v-if="showDeleteModal">
      <div class="modal-content">
        <h3>Confirmar exclusão</h3>
        <p>
          Tem certeza que deseja excluir
          <strong>{{ selectedPerson?.name }}</strong>?
        </p>

        <div class="modal-buttons">
          <button class="btn delete" @click="confirmDelete">
            Excluir
          </button>
          <button class="btn secondary" @click="closeDeleteModal">
            Cancelar
          </button>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Swal from 'sweetalert2';    
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';

export default defineComponent({
  name: 'PersonList',
  data() {
    return {
      persons: [] as PersonItem[],

    
      showEditModal: false,
      editData: {
        id: '',
        name: '',
        age: 0,
        biography: '',
      } as PersonItem,

   
      showDeleteModal: false,
      selectedPerson: null as PersonItem | null,
    };
  },
  methods: {
  
    async fetchPersons() {
      try {
        const response = await api.get('/persons');
        this.persons = response.data;
      } catch (error) {
        console.error('Erro ao buscar lista de Pessoas:', error);
        Swal.fire('Erro', 'Falha ao carregar a lista de pessoas.', 'error');
      }
    },

    openEditModal(person: PersonItem) {
      this.editData = { ...person }; 
      this.showEditModal = true;
    },
    closeEditModal() {
      this.showEditModal = false;
      this.editData = { id: '', name: '', age: 0, biography: '' };
    },
    async confirmEdit() {
      if (!this.editData.id) return;
      try {
       
        await api.put(`/persons/${this.editData.id}`, this.editData);
    
        Swal.fire('Sucesso', 'Pessoa atualizada com sucesso!', 'success');

        this.closeEditModal();
        this.fetchPersons();
      } catch (error) {
        console.error('Erro ao editar Pessoa:', error);
        Swal.fire('Erro', 'Falha ao atualizar os dados da Pessoa.', 'error');
      }
    },

    openDeleteModal(person: PersonItem) {
      this.selectedPerson = person;
      this.showDeleteModal = true;
    },
    closeDeleteModal() {
      this.showDeleteModal = false;
      this.selectedPerson = null;
    },
    async confirmDelete() {
      if (!this.selectedPerson?.id) return;
      try {
        await api.delete(`/persons/${this.selectedPerson.id}`);
        Swal.fire(
          'Excluído!',
          `A pessoa "${this.selectedPerson.name}" foi excluída com sucesso.`,
          'success'
        );
        this.closeDeleteModal();
        this.fetchPersons();
      } catch (error) {
        console.error('Erro ao deletar Pessoa:', error);
        Swal.fire('Erro', 'Falha ao excluir Pessoa.', 'error');
      }
    },
  },
  mounted() {
    this.fetchPersons();
  },
});
</script>

<style scoped>

@keyframes fadeIn {
  0% {
    opacity: 0;
    transform: translateY(8px);
  }
  100% {
    opacity: 1;
    transform: translateY(0);
  }
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}

.person-list-card {
  max-width: 1000px;
  margin: 0 auto 40px;
  background-color: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  padding: 24px 24px 32px;
  animation: fadeIn 0.4s ease-in-out;
  overflow-x: auto;
}

.person-list-card h2 {
  margin-bottom: 24px;
  font-size: 1.8rem;
  font-weight: 600;
  color: #333;
  text-align: center;
  letter-spacing: 1px;
}

table {
  width: 100%;
  border-collapse: collapse;
  border-radius: 8px;
  background-color: #f8f9fa;
  font-size: 0.95rem;
}

thead tr {
  background: linear-gradient(to right, #007bff, #0052a4);
  color: #fff;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

th, td {
  padding: 14px 16px;
  border-bottom: 1px solid #e1e5e9;
  text-align: left;
  white-space: nowrap;
}

tbody tr:nth-child(even) {
  background-color: #ffffff;
}
tbody tr:nth-child(odd) {
  background-color: #f3f5f7;
}

tbody tr {
  transition: background-color 0.2s, transform 0.2s;
}
tbody tr:hover {
  background-color: #eaf2fa;
  transform: scale(1.01);
}

.highlight-minor {
  background-color: #fff3cd !important;
}

td:last-child {
  min-width: 160px;
  text-align: center;
}


.btn {
  border: none;
  border-radius: 4px;
  padding: 8px 14px;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background-color 0.2s, transform 0.2s;
  margin-right: 6px;
  display: inline-flex;
  align-items: center;
  gap: 6px;
}

.btn:hover {
  opacity: 0.9;
}
.btn:active {
  transform: scale(0.95);
}

.btn.edit {
  background-color: #17a2b8;
  color: #fff;
}
.btn.delete {
  background-color: #dc3545;
  color: #fff;
}
@media (max-width: 600px) {
  th,
  td {
    padding: 10px 8px;
    font-size: 0.85rem;
  }
  .btn {
    padding: 6px 10px;
  }
  td:last-child {
    min-width: 120px;
  }
}


.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 999;
}

.modal-content {
  background: #ffffff;
  width: 90%;
  max-width: 480px;
  border-radius: 8px;
  padding: 24px;
  box-shadow: 0 8px 18px rgba(0, 0, 0, 0.1);
  position: relative;
  animation: fadeIn 0.4s ease-in-out;
}

.modal-content h3 {
  margin: 0 0 20px;
  font-size: 1.4rem;
  font-weight: 600;
  text-align: center;
  color: #333;
}

.modal-content p {
  margin-bottom: 16px;
  text-align: center;
}

.form-group {
  margin-bottom: 16px;
  display: flex;
  flex-direction: column;
}
.form-group label {
  font-weight: 600;
  margin-bottom: 5px;
  color: #444;
}
.form-group input,
.form-group textarea {
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 10px;
  font-size: 0.95rem;
  transition: border-color 0.2s;
}
.form-group input:focus,
.form-group textarea:focus {
  border-color: #007bff;
  outline: none;
}
.form-group textarea {
  resize: vertical;
  min-height: 80px;
}

.modal-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}

.btn.primary {
  background-color: #007bff;
  color: #fff;
}

.btn.secondary {
  background-color: #6c757d;
  color: #fff;
}
</style>
