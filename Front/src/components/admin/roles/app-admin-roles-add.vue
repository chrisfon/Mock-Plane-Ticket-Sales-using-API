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
        <v-toolbar color="teal" dark>Agregar un rol</v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="form.inputs.Description"
            :counter="50"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Descripción del rol"
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
    total: {
      required: true,
    },
  },
  data() {
    return {
      form: {
        dialog: false,
        valid: true,
        inputs: {
          Description: "",
          id: this.total,
        },
      },
    };
  },
  methods: {

    async saveData(){
      if (!this.form.valid) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      let aux = (1 + parseInt(this.total)).toString().padStart(6, "0");
      this.form.inputs.id = "ROL-" + aux;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch("/api/Roles", requestOptions);
        if ( res.status >= 200 && res.status <= 299) {
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Perfil creado exitosamente",
            icon: "success",
          });
          this.Log.add('Role','I',this.form.inputs.id,this.form.inputs);
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("added");
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar el perfil",
            icon: "error",
          });
        }
      } catch (e) {
        console.log(e)
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el perfil",
          icon: "error",
        });
      }
    },
    save(event) {
      event.preventDefault();
      event.stopPropagation();
      this.$refs.form.validate();
      this.$nextTick(()=>this.saveData())

    },
  },
};
</script>