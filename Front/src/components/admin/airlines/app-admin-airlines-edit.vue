<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn v-bind="attrs" v-on="on" color="amber" small icon @click="move">
        <v-icon dark> mdi-pencil </v-icon>
      </v-btn>
    </template>
    <v-form ref="form" v-model="form.valid" lazy-validation @submit="save">
      <v-card>
        <v-toolbar color="teal" dark>Modificar una aerolínea</v-toolbar>
        <v-card-text>
          <v-text-field
            :value="airline.code"
            disabled
            label="Código del aerolínea"
          >
          </v-text-field>
          <v-select
            v-model="form.inputs.country"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione un país"
            required
          ></v-select>
          <v-text-field
            v-model="form.inputs.description"
            :counter="50"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Descripción del aerolínea"
            required
          >
          </v-text-field>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn type="submit" text @click="save" outlined rounded color="teal"
            >Confirmar</v-btn
          >
          <v-btn text @click="form.dialog = false" outlined rounded color="red"
            >Cancelar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>
<script>
export default {
  props: {
    airline: {
      type: Object,
      default: () => ({
        description: "",
        code: "",
      }),
    },
  },
  data() {
    return {
      form: {
        dialog: false,
        valid: true,
        inputs: {
          description: "",
          code: "",
          country: null,
          sequence: 1,
        },
      },
      countries: [],
    };
  },
  mounted() {
    this.getCountries();
  },
  methods: {
    async getCountries() {
      let response = await fetch("/api/Countries", {
        method: "GET",
        redirect: "follow",
      });
      this.countries = await response.json();
    },
    move() {
      this.form.inputs.description = this.airline.description;
      this.form.inputs.code = this.airline.code;
      this.form.inputs.country = this.airline.country;
    },
    async save(event) {
      event.preventDefault();
      event.stopPropagation();
      if (!this.form.valid) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      this.form.inputs.code = this.airline.code;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "PUT",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch(
          "/api/Airlines/"+this.form.inputs.code ,
          requestOptions
        );
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("Airline", "U", this.form.inputs.code, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Aerolínea modificada exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("changed");
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el aerolínea",
          icon: "error",
        });
      }
    },
  },
};
</script>