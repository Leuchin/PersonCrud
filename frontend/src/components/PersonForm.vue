<template>
  <div class="person-form-card">
    <h2>{{ isEditMode ? 'Editar Pessoa' : 'Nova Pessoa' }}</h2>

    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="name">Nome</label>
        <input
          type="text"
          id="name"
          v-model="personData.name"
          required
          placeholder="Digite o nome"
        />
      </div>

      <div class="form-group">
        <label for="age">Idade</label>
        <input
          type="number"
          id="age"
          v-model="personData.age"
          required
          placeholder="Digite a idade"
        />
      </div>

      <div class="form-group">
        <label for="biography">Biografia</label>
        <textarea
          id="biography"
          v-model="personData.biography"
          required
          placeholder="Fale sobre a pessoa"
        ></textarea>
      </div>

      <div class="buttons">
        <button type="submit" class="btn primary">
          {{ isEditMode ? 'Atualizar' : 'Criar' }}
        </button>
        <button type="button" class="btn secondary" @click="clearForm">
          Limpar
        </button>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, watch } from 'vue';
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';

export default defineComponent({
  name: 'PersonForm',
  props: {
    // Se quiser editar uma pessoa existente, passe por aqui
    personToEdit: {
      type: Object as PropType<PersonItem | null>,
      default: null,
    },
  },
  emits: ['refresh-list'],
  data() {
    return {
      isEditMode: false,
      personData: {
        name: '',
        age: 0,
        biography: '',
      } as PersonItem,
    };
  },
  watch: {
    // Sempre que 'personToEdit' mudar, atualize o formulário
    personToEdit: {
      immediate: true,
      handler(newVal: PersonItem | null) {
        if (newVal && newVal.id) {
          this.isEditMode = true;
          this.personData = { ...newVal };
        } else {
          this.isEditMode = false;
          this.clearForm();
        }
      },
    },
  },
  methods: {
    clearForm() {
      this.isEditMode = false;
      this.personData = {
        name: '',
        age: 0,
        biography: '',
      };
    },

    async handleSubmit() {
      try {
        if (this.isEditMode && this.personData.id) {
          // PUT: atualizar
          await api.put(`/persons/${this.personData.id}`, this.personData);
          alert('Pessoa atualizada com sucesso!');
        } else {
          // POST: criar nova
          const nowUtcIso = new Date().toISOString();
          const newPerson = {
            ...this.personData,
            timerStart: nowUtcIso, // se quiser um campo de data
          };

          await api.post('/persons', newPerson);
          alert('Pessoa criada com sucesso!');
        }

        // Emite evento para que quem usar este form possa recarregar a lista
        this.$emit('refresh-list');
        this.clearForm();
      } catch (error) {
        console.error('Erro ao enviar Pessoa:', error);
        alert(
          'Falha ao enviar Pessoa. Verifique se o backend está rodando e se o modelo está correto.'
        );
      }
    },
  },
});
</script>

<style scoped>
/* Container principal do formulário */
.person-form-card {
  max-width: 600px;
  margin: 0 auto 30px;
  background-color: #f7f9fa;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  padding: 24px;
}

.person-form-card h2 {
  margin-bottom: 24px;
  font-size: 1.6rem;
  color: #333;
  text-align: center;
  letter-spacing: 0.5px;
}

.form-group {
  margin-bottom: 16px;
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 6px;
  color: #444;
}

.form-group input[type='text'],
.form-group input[type='number'],
.form-group textarea {
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 10px;
  font-size: 1rem;
  transition: border-color 0.2s;
}

.form-group input:focus,
.form-group textarea:focus {
  border-color: #007bff;
  outline: none;
}

textarea {
  resize: vertical;
  min-height: 100px;
}

.buttons {
  display: flex;
  gap: 12px;
  margin-top: 20px;
  justify-content: flex-end;
}

.btn {
  border: none;
  border-radius: 4px;
  padding: 10px 18px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.2s, transform 0.2s;
}

.btn:hover {
  opacity: 0.9;
}

.btn:active {
  transform: scale(0.97);
}

.btn.primary {
  background-color: #007bff;
  color: #fff;
}

.btn.secondary {
  background-color: #868e96;
  color: #fff;
}
</style>
