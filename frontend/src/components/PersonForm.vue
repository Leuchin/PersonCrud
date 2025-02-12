<template>
  <div class="person-form-card">
    <h2>{{ isEditMode ? 'Editar Pessoa' : 'Novas Pessoa' }}</h2>

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

// Importa o SweetAlert2
import Swal from 'sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

export default defineComponent({
  name: 'PersonForm',
  props: {
    // Recebe a pessoa a editar, se houver
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
    // Se 'personToEdit' vier com um id, entramos em modo edição
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
      // Define se é criação ou edição
      const operation = this.isEditMode ? 'editar' : 'criar';
      const confirmText = this.isEditMode ? 'Sim, atualizar!' : 'Sim, criar!';

      // Pede confirmação com SweetAlert2
      const result = await Swal.fire({
        title: `Confirmar ${operation}?`,
        text: `Deseja realmente ${operation} esta pessoa?`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#007bff',  // Azul
        cancelButtonColor: '#868e96',   // Cinza
        confirmButtonText: confirmText,
        cancelButtonText: 'Cancelar',
      });

      if (!result.isConfirmed) {
        // Usuário cancelou
        return;
      }

      try {
        if (this.isEditMode && this.personData.id) {
          // PUT: atualizar
          await api.put(`/persons/${this.personData.id}`, this.personData);

          // Alerta de sucesso
          Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Pessoa atualizada com sucesso!',
            timer: 2000,
            timerProgressBar: true,
            showConfirmButton: false,
          });
        } else {
          // POST: criar nova
          const nowUtcIso = new Date().toISOString();
          const newPerson = {
            ...this.personData,
            timerStart: nowUtcIso, // se precisar de data
          };
          await api.post('/persons', newPerson);

          Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Pessoa criada com sucesso!',
            timer: 2000,
            timerProgressBar: true,
            showConfirmButton: false,
          });
        }

        // Emite evento para recarregar a lista
        this.$emit('refresh-list');
        this.clearForm();

      } catch (error) {
        console.error('Erro ao enviar Pessoa:', error);

        Swal.fire({
          icon: 'error',
          title: 'Ops...',
          text: 'Falha ao enviar Pessoa. Verifique se o backend está rodando e se o modelo está correto.',
        });
      }
    },
  },
});
</script>

<style scoped>
/* Animação fade-in */
@keyframes fadeInForm {
  0% {
    opacity: 0;
    transform: translateY(8px);
  }
  100% {
    opacity: 1;
    transform: translateY(0);
  }
}

/* Container principal do formulário */
.person-form-card {
  max-width: 1000px;
  margin: 0 auto 30px;
  background-color: #f7f9fa;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  padding: 24px;
  animation: fadeInForm 0.4s ease-in-out;
}

.person-form-card h2 {
  margin-bottom: 24px;
  font-size: 1.6rem;
  color: #333;
  text-align: center;
  letter-spacing: 0.5px;
}

/* Agrupamento dos campos */
.form-group {
  margin-bottom: 16px;
  display: flex;
  flex-direction: column;
}

/* Rótulos */
.form-group label {
  font-weight: 600;
  margin-bottom: 6px;
  color: #444;
}

/* Campos de texto e textarea */
.form-group input[type='text'],
.form-group input[type='number'],
.form-group textarea {
  background-color: #fff;
  color: #333;
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 10px;
  font-size: 1rem;
  transition: border-color 0.2s, background-color 0.2s;
}

/* Placeholder com cor mais clara */
.form-group input::placeholder,
.form-group textarea::placeholder {
  color: #999;
}

/* Foco no campo */
.form-group input:focus,
.form-group textarea:focus {
  border-color: #007bff;
  outline: none;
  background-color: #fefefe;
}

/* Tamanho mínimo da textarea e redimensionamento vertical */
textarea {
  resize: vertical;
  min-height: 100px;
}

/* Botões */
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
  transition: background-color 0.2s, transform 0.2s, box-shadow 0.2s;
}

/* Hover e active */
.btn:hover {
  opacity: 0.9;
}
.btn:active {
  transform: scale(0.97);
}

/* Botão principal (Criar/Atualizar) */
.btn.primary {
  background-color: #007bff;
  color: #fff;
}

/* Botão secundário (Limpar) */
.btn.secondary {
  background-color: #868e96;
  color: #fff;
}

/* Responsividade básica */
@media (max-width: 600px) {
  .person-form-card {
    padding: 16px;
  }
  .btn {
    padding: 8px 14px;
    font-size: 0.9rem;
  }
}
</style>
