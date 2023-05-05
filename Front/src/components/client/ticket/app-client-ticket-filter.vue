<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn v-bind="attrs" v-on="on" color="teal" class="ma-2 white--text" fab>
        <v-icon dark> mdi-magnify </v-icon>
      </v-btn>
    </template>
    <v-container fluid>
      <v-card>
        <v-toolbar color="teal" dark>Filtrado</v-toolbar>
        <v-card-text>
          <v-select
            v-model="filter.origin"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione el país de origen"
            required
          >
          </v-select>
          <v-select
            v-model="filter.to"
            :items="countries"
            item-text="description"
            item-value="code"
            label="Seleccione el país de destino"
            required
          >
          </v-select>
          <v-text-field
            v-model="filter.date"
            type="date"
            label="Ingrese la fecha de salida"
          >
          </v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text @click="change" outlined rounded color="teal"
              >Confirmar</v-btn
            >
          </v-card-actions>
        </v-card-actions>
      </v-card>
    </v-container>
  </v-dialog>
</template>
<script>
export default {
  props: {
    selected: {
      type: String,
      default: "*ALL",
    },
  },
  data() {
    return {
      dialog: false,
      countries: [],
      current: "*ALL",
      filter: {
        origin: "*ALL",
        to: "*ALL",
        date: "",
      },
    };
  },
  mounted() {
    this.getCountries();
  },

  methods: {
    move() {
      this.current = this.selected;
    },

    change() {
      this.$emit("change", this.filter);
      this.dialog = false;
    },

    async getCountries() {
      let response = await fetch("/api/Countries", {
        method: "GET",
        redirect: "follow",
      });
      this.countries = await response.json();
      this.countries.unshift({
        code: "*ALL",
        description: "Todos",
      });
    },
  },
};
</script>