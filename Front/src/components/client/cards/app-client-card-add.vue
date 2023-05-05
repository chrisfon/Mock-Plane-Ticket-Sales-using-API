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
        <v-toolbar color="teal" dark>Agregar un aerolínea </v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="form.inputs.PAN"
            :counter="16"
            :rules="[
              (v) => !!v || 'Debe completar este campo',
              (v) => v.length === 16 || 'Verifique la tarjeta',
            ]"
            label="Ingrese su número de tarjeta"
            required
          >
          </v-text-field>
          <v-select
            v-model="form.inputs.franchise"
            :items="franchises"
            item-text="description"
            item-value="code"
            label="Seleccione la franquicia "
            required
          >
          </v-select>
          <v-text-field
            v-model="form.inputs.cvc"
            type="number"
            :counter="4"
            :rules="[
              (v) => !!v || 'Debe completar este campo',
              (v) =>
                (v.length > 0 && v.length < 4) ||
                'Verifique el código ingresado',
            ]"
            label="Ingrese el cvc"
            required
          >
          </v-text-field>
          <v-text-field-simplemask
            v-model="form.inputs.expiration"
            type="number"
            :counter="4"
            :rules="[
              (v) => !!v || 'Debe completar este campo',
              (v) => v.length == 6 || 'Verifique la fecha de expiración',
            ]"
            label="Ingrese la fecha de expiración de la tarjeta (MM/YYYY)"
            required

            v-bind:options="{
              inputMask: '##/####',
              outputMask: '##/####',
              empty: null,
              applyAfter: false,
            }"
          >
          </v-text-field-simplemask>
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
      franchises: [
        { code: "VIS", description: "Visa" },
        { code: "MSC", description: "Master card" },
        { code: "AMEX", description: "American Express" },
      ],
      form: {
        dialog: false,
        valid: true,
        inputs: {
          PAN: "",
          userName: sessionStorage.getItem("user"),
          cvc: "",
          expiration: "",
          franchise: "",
          sequence: 1,
        },
      },
    };
  },

  methods: {
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
        const res = await fetch("/api/Cards", requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("Cards", "I", this.form.inputs.PAN, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Aerolínea creado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("added");
        } else if (res.status === 409) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "Al parecer ya existe un aerolínea con este código",
            icon: "warning",
          });
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar el aerolínea",
            icon: "error",
          });
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el aerolínea",
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