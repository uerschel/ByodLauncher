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
          <v-autocomplete :items="allModels" v-model="modell"></v-autocomplete>      
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
  <v-alert
  v-bind:type="ram.status"
  v-bind:v-show="isShown"
>{{ram.status}}</v-alert>
  </v-app>
</div>
</template>


<script lang="ts">

    import {Component, Vue} from "vue-property-decorator";
    import axios from 'axios';
    @Component
    export default class NotebookChecker extends Vue {
      mounted(){
        fetch("https://localhost:44369/api/notebook/all").then(res => res.json()).then(json => this.allModels = json);
      } 
      allModels = {type: Array};
      // THIS SHOULD BE UPDATED AT A LATER POINT, SO IT ISN'T HARDCODED BUT RATHER PULLS ALL THE 
      // DIFFERENT LEHRBERUFE FROM THE SERVER
      lehrberufe = ["Informatiker/in", "Elektroker/in", "Automobil-Mechatroniker/in"];
      modell = "";
      selectedLehrberuf = "";
      isShown = false;
      ram = {status: ""};
      async compareNotebook() {
          const result = await axios.get("https://localhost:44369/api/notebook", {
            params: {
              notebookModell: this.modell,
              lehrberuf: this.selectedLehrberuf
            }
          });
          const data = result.data;
          this.ram.status = "success";
          this.isShown = true;
          console.log(data);
      }
      

    }
</script>