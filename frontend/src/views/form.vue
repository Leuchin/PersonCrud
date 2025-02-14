<template>
    <!--Navbar-->
    <Navbar/>
  
    <!--Container-->
    <div id="app">
      <div class="navigation">
        <navigation/>
      </div>
      <div class="content">
  
        <h1>Lista de pessoas</h1>
  
      <PersonForm
      :personToEdit="personBeingEdited"
       @refresh-list="refreshPersonList"
      />
  
      
  
      </div>
  
    </div>
    
  </template>
  
<script lang="ts">
import { defineComponent, ref } from 'vue';
import Navbar from '../components/navbar.vue';
import PersonForm from '../components/PersonForm.vue';
import type { PersonItem } from '../models/PersonItem.ts';
import navigation from '../components/Navigation.vue';
import PersonList from '../components/PersonList.vue';
  
export default defineComponent({
    name: 'App',
    components: {
      Navbar,
      PersonForm,
      PersonList,
      navigation
    },
setup() {
    const personList = ref<InstanceType<typeof personList> | null>(null);
    const personBeingEdited = ref<PersonItem | null>(null);
  
    function onEditPerson(person: PersonItem) {
        personBeingEdited.value = {...person};
    }
  
    function refreshPersonList() {
        if (personList.value) {
            personList.value.fetchPersons();
        }
        personBeingEdited.value = null;
      }
      return {
        personBeingEdited,
        onEditPerson,
        refreshPersonList,
      };
    },
});
</script>
<style>
 
div .content {
    margin-left: 260px;
    padding: 20px;
    display: inline-block;
    border-radius: 25px;
}
</style>