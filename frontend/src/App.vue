<template>
  <!--Navbar-->
  <Navbar/>

  


  <!--Container-->
  <div id="app">
    <div class="navigation">
      <navigation/>
    </div>
    <div class="content">
      <router-view/>

      <h1>Lista de pessoas</h1>

    <PersonForm
    :personToEdit="personBeingEdited"
     @refresh-list="refreshPersonList"
    />

    <PersonList
      ref="personList"
      @edit-person="onEditPerson"
    />

    </div>

  </div>
  
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import Navbar from './components/navbar.vue';
import PersonForm from './components/PersonForm.vue';
import PersonList from './components/PersonList.vue';
import type { PersonItem } from './models/PersonItem.ts';
import navigation from './components/Navigation.vue';

export default defineComponent({
  name: 'App',
  components: {
    Navbar,
    PersonForm,
    navigation,
    PersonList
  },
  setup() {
    const personList = ref<InstanceType<typeof PersonList> | null>(null);
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
div #app{
  width:100%;
  height: 100vh;
  color:antiquewhite;
  display: flex;
}
div .navigation{
  display: flex;
  width: 3px;
  flex: 1 1 0;
  padding: 6rem;
}
div .content{
  margin:8px;
  display: inline-block;
  border: 0.1px solid antiquewhite;
  border-radius: 25px;
}
</style>