<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn v-bind="attrs" v-on="on" color="teal" class="ma-2 white--text" fab>
        <v-icon dark> mdi-plus </v-icon>
      </v-btn>
    </template>
    <v-form ref="form" v-model="form.valid" lazy-validation @submit="save">
      <v-card>
        <v-toolbar color="teal" dark>Agregar un puerta </v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="form.inputs.code"
            :counter="3"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Código del puerta"
            required
          >
          </v-text-field>
          <v-select
            v-model="form.inputs.country"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione un país"
            required
          >
          </v-select>
          <v-text-field
            v-model="form.inputs.description"
            :counter="20"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Descripción del puerta"
            required
          >
          </v-text-field>

          <v-checkbox
            v-model="closed"
            label="¿Puerta abierta?"
          ></v-checkbox>

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
  data() {
    return {
      form: {
        dialog: false,
        valid: true,
        inputs: {
          description: "",
          country: null,
          code: "",
          isClosed: 0,
          sequence: 3,
        },
      },
      closed : false,
      countries: [],
    };
  },

  watch : {
    closed(value){
      if(value){
        this.form.inputs.isClosed = 1
      }else{
        this.form.inputs.isClosed = 0
      }
    }
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
    async saveData() {
      if (!this.form.valid) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch("/api/Doors", requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add('Doors','I',this.form.inputs.code,this.form.inputs); 
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "puerta creado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("added");
        } else if (res.status === 409) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "Al parecer ya existe un puerta con este código",
            icon: "warning",
          });
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar el puerta",
            icon: "error",
          });
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el puerta",
          icon: "error",
        });
      }
    },
    save(event) {
      event.preventDefault();
      event.stopPropagation();
      this.$refs.form.validate();
      this.$nextTick(() => this.saveData());
    },
  },
};
</script>