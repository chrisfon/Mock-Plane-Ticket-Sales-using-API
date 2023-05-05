<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de secuencias</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="Countries"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
          </v-data-table>
        </v-card-text>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  data() {
    return {
      loading: false,
      Countries: [],
      dialog: false,
    };
  },
  mounted() {
    this.getSeqs();
  },
  methods: {

    async getSeqs() {
      this.loading = true;
      try {
        let response = await fetch("/api/Sequences", {
          method: "GET",
          redirect: "follow",
        });
        this.Countries = await response.json();
        this.Countries.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los paises",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    headers() {
      return [
        {
          text: "Secuencia",
          value: "sequence1",
        },
        {
          text: "Descripción",
          value: "description",
        },
        {
          text: "Prefijo",
          value: "prefix",
        },
        {
          text: "Rango inicial",
          value: "iniRange",
          align: "right",
        },
        {
          text: "Rango final",
          value: "endRange",
          align: "right",
        },
        { text: "Ultimo valor usado", value: "lastValue", align: "right" },
      ];
    },
  },
};
</script>