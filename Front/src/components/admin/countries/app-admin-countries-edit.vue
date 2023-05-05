<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
        v-bind="attrs"
        v-on="on"
        color="amber"
        small
        icon
        @click="form.inputs.description = country.description"
      >
        <v-icon dark> mdi-pencil </v-icon>
      </v-btn>
    </template>
    <v-form ref="form" v-model="form.valid" lazy-validation @submit="save">
      <v-card>
        <v-toolbar color="teal" dark>Modificar un país</v-toolbar>
        <v-card-text>
          <v-text-field :value="country.code" disabled label="Código del país">
          </v-text-field>
          <v-text-field
            v-model="form.inputs.description"
            :counter="50"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Descripción del país"
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
    country: {
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
          sequence : 0
        },
      },
    };
  },

  methods: {
    async save(event) {
      event.preventDefault();
      event.stopPropagation();
      if (!this.form.valid) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      this.form.inputs.code = this.country.code;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "PUT",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch("/api/Countries/"+this.country.code, requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("Country", "U", this.country.code, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "País modificado exitosamente",
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
          text: "No se ha logrado almacenar el país",
          icon: "error",
        });
      }
    },
  },
};
</script>