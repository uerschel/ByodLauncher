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
            :items="lehrberufe"
            v-model="selectedLehrberuf"
            label="Beruf"
          ></v-select>
        </v-col>
      </v-row>
      <v-row align="center">
         <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-text-field
      label="Notebook modell"
      :rules="rules"
      v-model="modell"
      hide-details="auto"
    ></v-text-field>       
        </v-col>
      </v-row>
      <v-row align="center">
        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
    <v-btn
  color="accent"
  elevation="2"
  @click="compareNotebook"
>Send</v-btn>
        
        </v-col>

      </v-row>
    </v-container>
  </v-app>
</div>
    
</template>

<script lang="ts">

    import {Component, Vue} from "vue-property-decorator";
    import axios from 'axios';
    @Component
    export default class NotebookChecker extends Vue {
      created(){
        axios.get("https://localhost:44369/api/notebook/all").then(models => { this.allModels.push(models)});
      }
      allModels = [];
      // THIS SHOULD BE UPDATED AT A LATER POINT, SO IT ISN'T HARDCODED BUT RATHER PULLS ALL THE 
      // DIFFERENT LEHRBERUFE FROM THE SERVER
        lehrberufe = ["Informatiker/in", "Elektroker/in", "Automobil-Mechatroniker/in"];
        rules = [
        value => !!value || 'Required.',
        value => (value && value.length >= 3) || 'Min 3 characters',
      ];
      modell = "";
      selectedLehrberuf = "";
      async compareNotebook() {
        try{
          const result = await axios.get("https://localhost:44369/api/notebook", {
            params: {
              notebookModell: this.modell,
              //lehrberuf: this.selectedLehrberuf
            }
          });
          console.log(result);
        } catch(err){
          
        }
      }
    }
</script>