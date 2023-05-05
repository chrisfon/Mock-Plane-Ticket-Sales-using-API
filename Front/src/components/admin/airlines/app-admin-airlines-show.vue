<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn v-bind="attrs" v-on="on" color="teal" small icon @click="getInfo">
        <v-icon dark> mdi-eye </v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-toolbar color="teal" dark>Información de la aerolínea</v-toolbar>
      <v-card-text>
        <v-text-field :value="code" readonly label="Código de la aerolínea"> </v-text-field>
        <v-text-field
          readonly
          v-model="form.inputs.country"
          label="País de la aerolínea"
        >
        </v-text-field>
        <v-text-field
          readonly
          v-model="form.inputs.description"
          label="Descripción de la aerolínea"
        >
        </v-text-field>
      </v-card-text>
      <v-card-actions class="justify-end">
        <v-btn text @click="form.dialog = false" outlined rounded color="red"
          >Cerrar</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script>
export default {
  props: {
    code: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      form: {
        dialog: false,
        inputs: {
          description: "",
          code: "",
          country : ""
        },
      },
    };
  },

  methods: {
    async getInfo() {
      try {
        let response = await fetch("/api/Airlines/", {
          method: "GET",
          redirect: "follow",
        });
        let airlines = await response.json();
        let airline = airlines.find(e=>e.code === this.code);
        this.form.inputs.code = airline.code;
        this.form.inputs.description = airline.description;
        this.form.inputs.country = airline.country;
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrcodeo un error",
          text: "Ha ocurrcodeo un error al recuperar la información",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>