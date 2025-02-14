<template>
    <div id="list">
        <navigation/>
        <PersonList
            ref="personList"
      />
    </div>
</template>

<script setup lang="ts">

import navigation from '../components/Navigation.vue';
import PersonList from '../components/PersonList.vue';

</script>