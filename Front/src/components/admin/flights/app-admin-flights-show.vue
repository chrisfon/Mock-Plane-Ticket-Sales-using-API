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
      <v-toolbar color="teal" dark>Información del vuelo</v-toolbar>
      <v-card-text>
        <v-text-field readonly v-model="form.inputs.airline" label="Aerolínea">
        </v-text-field>
        <v-text-field readonly v-model="form.inputs.origin" label="Origen">
        </v-text-field>
        <v-text-field readonly v-model="form.inputs.salida" label="Salida">
        </v-text-field>
        <v-text-field readonly v-model="form.inputs.to" label="destino">
        </v-text-field>
        <v-text-field readonly v-model="form.inputs.llegada" label="Llegada">
        </v-text-field>
        <v-text-field readonly v-model="form.inputs.cost" label="Costo">
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
  name : 'app-admin-flights-show',
  props: {
    code: {
      type: String,
      default: "",
    },
    ticket: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      form: {
        code: "",
        dialog: false,
        inputs: {

        },
      },
    };
  },

  methods: {
    async getFlight() {
      let response = await fetch("/api/Tickets/" + this.ticket, {
        method: "GET",
        redirect: "follow",
      });
      let res = await response.json();
      return  res.flight;
    },

    async getInfo() {
      try {
        let code = ''
        if(this.code === ''){
          code = await this.getFlight();
        }else{
          code = this.code;
        }
        let response = await fetch("/api/Flights/"+code, {
          method: "GET",
          redirect: "follow",
        });
        let vuelo = await response.json();
        this.form.inputs = vuelo;
        this.form.inputs.salida = this.form.inputs.dateDep.split('T')[0]+'@'+ this.form.inputs.timeDep
        this.form.inputs.llegada = this.form.inputs.dateArrive.split('T')[0]+'@'+ this.form.inputs.timeArrive
        this.form.inputs.cost = this.form.inputs.cost + ' USD'
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