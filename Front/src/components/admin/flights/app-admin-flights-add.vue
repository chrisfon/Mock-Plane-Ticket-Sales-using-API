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
        <v-toolbar color="teal" dark>Agregar un vuelo </v-toolbar>
        <v-card-text>
          <v-select
            v-model="form.inputs.origin"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione el país de origen"
            required
          >
          </v-select>

          <v-select
            v-model="form.inputs.to"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione el país de destino"
            required
          >
          </v-select>

          <v-select
            v-model="form.inputs.airline"
            :items="airlines"
            item-text="description"
            item-value="code"
            label="Seleccione la aerolinea"
            required
          >
          </v-select>

          <v-row>
            <v-col>
              <v-text-field
                v-model="form.inputs.dateDep"
                type="date"
                label="Fecha de salida"
                required
              >
              </v-text-field>
            </v-col>
            <v-col>
              <v-text-field
                v-model="form.inputs.timeDep"
                type="time"
                step="1"
                label="Hora de salida"
                required
              >
              </v-text-field>
            </v-col>
          </v-row>

          <v-row>
            <v-col>
              <v-text-field
                v-model="form.inputs.dateArrive"
                type="date"
                label="Fecha de llegada"
                required
              >
              </v-text-field>
            </v-col>
            <v-col>
              <v-text-field
                v-model="form.inputs.timeArrive"
                type="time"
                step="1"
                label="Hora de llegada"
                required
              >
              </v-text-field>
            </v-col>
          </v-row>
          <v-text-field
            v-model="form.inputs.cost"
            :rules="[
              (v) => !!v || 'Debe completar este campo',
              (v) => v >= 1 || 'Debe ingresar el valor del vuelo',
              (v) => v < 999999 || 'Valor máximo 999.999 USD',
            ]"
            type="number"
            label="Valor del vuelo (USD)"
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
  data() {
    return {
      form: {
        dialog: false,
        valid: true,
        inputs: {
          code : '',
          sequence :  4,
          origin: "",
          to: "",
          airline: "",
          dateDep: "",
          timeDep: "",
          dateArrive: "",
          timeArrive: "",
          cost: 0,
          status : '1'
        },
      },
      countries: [],
      airlines: [],
    };
  },

  mounted() {
    this.getCountries();
    this.getAirlines();
  },

  methods: {
    async getCountries() {
      let response = await fetch("/api/Countries", {
        method: "GET",
        redirect: "follow",
      });
      this.countries = await response.json();
    },
    async getAirlines() {
      let response = await fetch("/api/Airlines", {
        method: "GET",
        redirect: "follow",
      });
      this.airlines = await response.json();
    },
    async saveData() {
      if (!this.form.valid) {
        return;
      }
      this.form.inputs.code = new Date().getTime().toString();
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
        const res = await fetch("/api/Flights", requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("Flights", "I", this.form.inputs.code, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "vuelo creado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("added");
        } else if (res.status === 409) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "Al parecer ya existe un vuelo con este código",
            icon: "warning",
          });
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar el vuelo",
            icon: "error",
          });
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el vuelo",
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