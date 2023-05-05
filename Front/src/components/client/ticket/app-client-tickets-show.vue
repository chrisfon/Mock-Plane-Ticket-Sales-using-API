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
      <v-toolbar color="teal" dark>Información del país</v-toolbar>
      <v-card-text>
        <v-text-field :value="code" readonly label="Código del país"> </v-text-field>
        <v-text-field
          readonly
          v-model="form.inputs.description"
          label="Descripción del país"
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
        },
      },
    };
  },

  methods: {
    async getInfo() {
      this.form.inputs.code = this.code;
      try {
        let response = await fetch("/api/Countries/"+this.code, {
          method: "GET",
          redirect: "follow",
        });
        let country = await response.json();
        this.form.inputs.code = country.code;
        this.form.inputs.description = country.description;
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