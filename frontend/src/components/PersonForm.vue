<template>
    <div class="Person-Container">
        <h2>{{ isEditMode ? 'Editar Pessoa' : 'Nova Pessoa' }}</h2>
            <form @submit.prevent="handleSubmit">
                <div class="form-group">
                    <label for="name">Nome</label>
                        <input 
                        type="text" 
                        id="name" 
                        v-model="PersonData.name" 
                        required
                        />
                    </div>

                    <div class="form-group">
                        <label for="age">Idade</label>
                        <input 
                        type="number" 
                        id="age" 
                        v-model="PersonData.age" 
                       required
                        />
                    </div>

                    <div class="form-group">
                        <label for="biography">Biografia</label>
                        <textarea 
                        id="biography" 
                        v-model="PersonData.biography" 
                        required
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
import { defineComponent, type PropType } from 'vue';
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';
import Swal from 'sweetalert2';



export default defineComponent({
  name: 'PersonForm',
  props: {
    pesonToEdit: {
      type: Object as PropType<PersonItem| null>,
      default: null,
    },
  },
  emits: ['refresh-list'],
  data() {
    return {
      isEditMode: false,
      PersonData: {
        name: '',
        age: 0,
        Biography: '',
      } as PersonItem,
    };
  },
  watch: {
    personToEdit: {
      immediate: true,
      handler(newVal: PersonItem | null) {
        if (newVal) {
          this.isEditMode = true;
          this.PersonData = { ...newVal };
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
      this.PersonData = {
        name: '',
        age: 0,
        biography: '',
      };
    },

    async handleSubmit() {
      try {
        if (this.isEditMode && this.PersonData.id) {
          // PUT
          await api.put(`/persons/${this.PersonData.id}`, this.PersonData);

          Swal.fire({
            title: 'Sucesso',
            text: 'Pessoa atualizada com sucesso!',
            icon: 'success',
            timer: 1500,
            timerProgressBar: true,
            showConfirmButton: false,
          });
          
        } else {
          // POST

          const newPerson = {
            ...this.PersonData,
          };

          await api.post('/persons', newPerson);
          Swal.fire({
            title: 'Sucesso',
            text: 'Pessoa criada com sucesso!',
            icon: 'success',
            timer: 1500,
            timerProgressBar: true,
            showConfirmButton: false,
          });
          this.$emit('refresh-list');
          this.clearForm();
        }

        this.$emit('refresh-list');
        this.clearForm();
        setTimeout(() => {
          window.location.reload();
        }, 1500);

      } catch (error) {
        console.error('Erro ao enviar Pessoa:', error);
        Swal.fire('Erro', 'Erro ao enviar Pessoa verifique se o backend está ligado', 'error'); 
      }
    },
  },
});
</script>

<style scoped>
.Person-Container {
  border: 1px solid antiquewhite;
  border-radius: 5px;
  padding: 16px 20px;
  margin-bottom: 30px;
  background: #272424;
}

h2 {
  margin-bottom: 20px;
  font-size: 1.4rem;
  color: #84ace9;
}

.form-group {
  margin-bottom: 15px;
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 6px;
  color: #84ace9;
}

.form-group input[type="text"],
.form-group textarea,
.form-group select,
.form-group input[type="number"] {
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 8px;
  font-size: 0.95rem;
}

.buttons {
  display: flex;
  gap: 10px;
  margin-top: 10px;
}

.btn {
  border: none;
  border-radius: 4px;
  padding: 8px 16px;
  font-size: 0.95rem;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn.primary {
  background-color: #0066cc;
  color: #fff;
}

.btn.primary:hover {
  background-color: #005bb5;
}

.btn.secondary {
  background-color: #f2f2f2;
  color: #413c3c;
}

.btn.secondary:hover {
  background-color: #e2e2e2;
}
</style>