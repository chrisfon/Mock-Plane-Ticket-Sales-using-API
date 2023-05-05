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
        <v-toolbar color="teal" dark>Modificar una puerta</v-toolbar>
        <v-card-text class="mt-5">
          <v-text-field :value="door.code" disabled label="Código del puerta">
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
            label="Descripción del puerta"
            required
          >
          </v-text-field>
          <v-checkbox v-model="closed" label="¿Puerta abierta?"></v-checkbox>
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
    door: {
      type: Object,
      default: () => ({ 
        description: "",
        code: "",
      }),
    },
  },
  data() {
    return {
      closed: false,
      form: {
        dialog: false,
        valid: true,
        inputs: {
          description: "",
          code: "",
          country: null,
          sequence: 0,
          isClosed: 0,
        },
      },
      countries: [],
    };
  },
  mounted() {
    this.getCountries();
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
  methods: {
    async getCountries() {
      let response = await fetch("/api/Countries", {
        method: "GET",
        redirect: "follow",
      });
      this.countries = await response.json();
    },
    move() {
      this.form.inputs.description = this.door.description;
      this.form.inputs.code = this.door.code;
      this.form.inputs.country = this.door.country;
      this.form.inputs.isClosed = this.door.isClosed;
      this.closed = this.door.isClosed;
    },
    async save(event) {
      event.preventDefault();
      event.stopPropagation();
      if (!this.form.valid) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      this.form.inputs.code = this.door.code;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "PUT",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch("/api/Doors/"+this.door.code, requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("Doors", "U", this.form.inputs.code, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Puerta modificada exitosamente",
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
          text: "No se ha logrado almacenar el puerta",
          icon: "error",
        });
      }
    },
  },
};
</script>