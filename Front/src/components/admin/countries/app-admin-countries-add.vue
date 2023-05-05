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
        <v-toolbar color="teal" dark>Agregar un país </v-toolbar>
        <v-card-text>
                    <v-text-field
            v-model="form.inputs.code"
            :counter="3"
            :rules="[(v) => !!v || 'Debe completar este campo',v=>v.length == 3 || 'Utilice el código de 3 caracteres']"
            label="Código del país"
            required
          >
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



  data() {
    return {
      form: {
        dialog: false,
        valid: true,
        inputs: {
          description: "",
          code : "",
          sequence : 2,
          
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
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      try {
        const res = await fetch("/api/Countries", requestOptions);  
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add('Country','I',this.form.inputs.code,this.form.inputs); 
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "País creado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.$refs.form.resetValidation();
          this.form.dialog = false;
          this.$emit("added");
        } else if (res.status === 409) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "Al parecer ya existe un país con este código",
            icon: "warning",
          });
        }else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar el país",
            icon: "error",
          });
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar el país",
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