<template>
<div>
    <h1>Notebook checker</h1>
 <v-app id="inspire">
    <v-container fluid>
      <v-row align="center">
        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-select
            :items="items"
            label="Beruf"
          ></v-select>
        </v-col> 
      </v-row>
    </v-container>
  </v-app>
</div>
    
</template>

<script lang="ts">

    import {Component, Vue} from "vue-property-decorator";
    @Component
    export default class NotebookChecker extends Vue {
        items = ["Informatiker/in", "Automechatroniker/in"];
    }
</script>