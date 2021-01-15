<template>
  <div>
    <h1>Notebook checker</h1>
    <v-app id="inspire">
      <v-container fluid>
        <v-row align="center">
          <v-col class="d-flex" cols="12" sm="6">
            <v-select
              :items="lehrberufe"
              v-model="selectedLehrberuf"
              label="Beruf"
            ></v-select>
          </v-col>
        </v-row>
        <v-row align="center">
          <v-col class="d-flex" cols="12" sm="6">
            <v-autocomplete
              :items="allModels"
              v-model="modell"
            ></v-autocomplete>
          </v-col>
        </v-row>
        <v-row align="center">
          <v-col class="d-flex" cols="12" sm="6">
            <v-btn color="accent" elevation="2" @click="compareNotebook"
              >Send</v-btn
            >
          </v-col>
        </v-row>
      </v-container>
      <div v-for="status in requirementEvaluation" :key="status.name">
        <v-alert v-bind:type="status.type"
          >{{ status.name }} ({{ status.specs }}): {{ status.message }}</v-alert
        >
      </div>
      <a href="https://noteb.com" style="font-weight: bold;color:#3f8f86;"
        >Powered by noteb.com</a
      >
    </v-app>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from "axios";
@Component
export default class NotebookChecker extends Vue {
  mounted() {
    fetch("https://localhost:44369/api/notebook/all")
      .then((res) => res.json())
      .then((json) => (this.allModels = json));
  }
  allModels = { type: Array };
  requirementEvaluation = [];
  notebook = { type: Object };
  // THIS SHOULD BE UPDATED AT A LATER POINT, SO IT ISN'T HARDCODED BUT RATHER PULLS ALL THE
  // DIFFERENT LEHRBERUFE FROM THE SERVER
  lehrberufe = [
    "Informatiker/in",
    "Elektroker/in",
    "Automobil-Mechatroniker/in",
  ];
  modell = "";
  selectedLehrberuf = "";
  isShown = false;
  async compareNotebook() {
    const result = await axios.get("https://localhost:44369/api/notebook", {
      params: {
        notebookModell: this.modell,
        lehrberuf: this.selectedLehrberuf,
      },
    });
    this.isShown = true;
    this.requirementEvaluation = result.data.statuses;
  }
}
</script>
