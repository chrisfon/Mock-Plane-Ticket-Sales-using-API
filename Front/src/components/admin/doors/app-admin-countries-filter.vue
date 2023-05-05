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
    <v-card>
      <v-toolbar color="teal" dark>Seleccione una opción</v-toolbar>
      <v-card-text>
        <v-select
          @change="change"
          v-model="current"
          :items="options"
          item-text="description"
          item-value="code"
          label="Seleccione una opción"
          required
        >
        </v-select>
      </v-card-text>
    </v-card>
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
      options: [
        {
          code: "*ALL",
          description: "Todas",
        },
        {
          code: "0",
          description: "Abiertas",
        },
        {
          code: "1",
          description: "Cerradas",
        },
      ],
      current: "*ALL",
    };
  },

  methods: {
    move() {
      this.current = this.selected;
    },

    change() {
      this.$emit("change", this.current);
      this.dialog = false;
    },
  },
};
</script>