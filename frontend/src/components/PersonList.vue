<template>
    <div class="Person-List-Container">
        <h2>Formulário Pessoa</h2>
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
                <tr v-for="Person in Persons" :key="Person.id" class="{
                    'table-danger': Person.age < 18,
                    'table-success': Person.age >= 18}">
                    <td>{{ Person.name }}</td>
                    <td>{{ Person.age }}</td>
                    <td>{{ Person.biography }}</td>
                    <!--Botões de edit, delete e Atualizar
                    -->
                    <td>
                        <button class="btn info" @click="openEdit(Person)">Editar</button>
                        <button class="btn danger" @click="openDelete(Person)">Deletar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <!--Transition de Edição-->
    <transition name="fade">
        <div class="overlay" v-if="showEdit">
            <div class="content">
                <h3>Edição de Pessoa</h3>

                    <div class="form-group">
                        <label>Nome</label>
                        <input type="text" 
                        v-model="editData.name"
                        placeholder="Nome da Pessoa"/>
                    </div>

                    <div class="form-group">
                        <label>Idade</label>
                        <input type="number" 
                        v-model="editData.age"
                        placeholder="Idade da Pessoa"/>
                    </div>

                    <div class="form-group">
                        <label>Biografia</label>
                        <textarea type="biography"
                            v-model="editData.biography"
                            placeholder="Biografia da Pessoa">
                        </textarea>
                    </div>

                    <div class="edit-buttons">
                        <button class="btn info" @click="confirmEdit()">Atualizar</button>
                        <button class="btn warning" @click="closeEdit()">Cancelar</button>
                </div>
            </div>
        </div>
    </transition>

    <!--Transition Exclusão-->
    <transition name="fade">
        <div class="overlay" v-if="showDelete">
            <div class="content">
                <h3>Confirmação de Exclusão</h3>
                <p>Deseja realmente <strong>excluir {{ selectedPerson?.name }}</strong></p>
                <button class="btn danger" @click="confirmDelete()">Sim</button>
                <button class="btn warning" @click="closeDelete()">Não</button>
            </div>
        </div>
    </transition>
</template>


<!-- Script em Typescript-->
<script lang="ts">
import { defineComponent } from 'vue';
import api from '../services/api.ts';
import type { PersonItem } from '../models/PersonItem.ts';
import Swal from 'sweetalert2';

export default defineComponent({
    name: 'PersonList',
    emit:['editPesons'],
    data() {
        return {
            Persons: [] as PersonItem[],

            showEdit: false,
            editData: {
                id: '',
                name: '',
                age: 0,
                biography: '',
            } as PersonItem,

            showDelete: false,
            selectedPerson: null as PersonItem | null,
        };
    },
        methods: {
        async fetchPersons() {
            try {
                const response = await api.get('/persons');
                this.Persons = response.data;
            } catch (error) {
                console.error(error);
                Swal.fire('Erro', 'Erro ao buscar pessoas', 'error');
            }
        },
        openEdit(person: PersonItem) {
            this.editData = { ...person };
            this.showEdit = true;
        },
        closeEdit() {
            this.showEdit = false;
        },
        async confirmEdit() {
            if (!this.editData.name || !this.editData.age || !this.editData.biography) {
                Swal.fire('Erro', 'Preencha todos os campos', 'error');
                return;
            }
            try {
                await api.put(`/persons/${this.editData.id}`, this.editData);
                Swal.fire('Sucesso', 'Pessoa atualizada com sucesso!', 'success');
                this.fetchPersons();
                this.closeEdit();
            } catch (error) {
                console.error(error);
                Swal.fire('Erro', 'Erro ao atualizar pessoa', 'error');
            }
        },
        openDelete(person: PersonItem) {
            this.selectedPerson = person;
            this.showDelete = true;
        },
        closeDelete() {
            this.showDelete = false;
        },
        async confirmDelete() {
            try {
                await api.delete(`/persons/${this.selectedPerson?.id}`);
                Swal.fire(
                    'Sucesso', 
                    `A pessoa "${this.selectedPerson?.name}" foi excluída com sucesso.`,
                    'success');
                this.fetchPersons();
                this.closeDelete();
            } catch (error) {
                console.error(error);
                Swal.fire(
                    'Erro', 
                    'Erro ao excluir pessoa', 
                    'error');
            }
        },
    },
    mounted() {
        this.fetchPersons();
    },
});
   
</script>

<!-- Estilização em CSS-->'
<style scoped>
@keyframes fadeIn 
{
    0%{
        opacity: 0%;
        transform: translate(10px);
    }    
    100%{
        opacity: 100%;
        transform: translate(0px);
    }
}

.fade-enter-active, 
.fade-leave-active {
    transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to{
    opacity: 0;
}

.Person-List-Container {
    background-color: #242222;
    border: 1px solid antiquewhite;
    border-radius: 5px;
    padding: 16px 18px;
    margin-bottom: 30px;  
}

h2{
    margin-bottom: 20px;
    font-size:1.2rem;
    color: antiquewhite;
}

table{
    width: 100%;
    border-collapse: collapse;
    font-size:0.95rem;
}

th, td{
    border: 1px solid antiquewhite;
    padding: 8px;
    border-bottom: 1px solid antiquewhite;
}

th{
    background-color: antiquewhite;
    color: #242222;
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
.overlay{
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}
.content{
    background-color: #242222;
    border: 1px solid antiquewhite;
    border-radius: 5px;
    padding: 16px 18px;
    width: 300px;
    animation: fadeIn 0.5s;
}
.content h3{
    margin-bottom: 10px;
    font-size: 1.2rem;
    color: antiquewhite;
}
.form-group{
    margin-bottom: 15px;
    display: flex;
    flex-direction: column;
}   
.form-group label{
    font-weight: 600;
    margin-bottom: 6px;
    color: antiquewhite;
}   
.form-group input[type="text"],
.form-group textarea{
    border: 1px solid #ccc;
    border-radius: 4px;
    padding: 8px;
    font-size: 0.95rem;
}
.form-group input[type="number"]{
    border: 1px solid #ccc;
    border-radius: 4px;
    padding: 8px;
    font-size: 0.95rem;
}
.edit-buttons{
    display: flex;
    gap: 10px;
    margin-top: 10px;
}

</style>