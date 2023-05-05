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
      <v-toolbar color="teal" dark>Información del rol</v-toolbar>
      <v-card-text>
        <v-text-field :value="rol" readonly label="Id del rol"> </v-text-field>
        <v-text-field
          readonly
          v-model="form.inputs.description"
          label="Descripción del rol"
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
    rol: {
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
          id: "",
        },
      },
    };
  },

  methods: {
    async getInfo() {
      try {
        let response = await fetch("/api/Roles/", {
          method: "GET",
          redirect: "follow",
        });
        let aux = await response.json();
        let rol = aux.find((e) => e.id === this.rol);
        this.form.inputs.id = this.rol;
        this.form.inputs.description = rol.description;
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar la información",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>