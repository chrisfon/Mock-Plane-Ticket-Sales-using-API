<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn v-bind="attrs" v-on="on" color="amber" small icon @click="getUser">
        <v-icon dark> mdi-pencil </v-icon>
      </v-btn>
    </template>
    <v-form ref="form" v-model="form.valid" lazy-validation @submit="save">
      <v-card>
        <v-toolbar color="teal" dark>Modificar un usuario</v-toolbar>
        <v-card-text>
          <v-text-field
            disabled
            v-model="form.inputs.userName"
            :counter="30"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Usuario"
            required
          >
          </v-text-field>
          <v-select
            v-model="form.inputs.role"
            :items="roles"
            item-text="description"
            item-value="id"
            label="Seleccione un rol"
            required
          >
          </v-select>

          <v-text-field
            type="email"
            v-model="form.inputs.email"
            :counter="256"
            :rules="emailRules"
            label="Correo electronico"
            required
          >
          </v-text-field>
          <v-text-field
            v-model="form.inputs.secQuestion"
            :counter="100"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Pregunta de seguridad"
            required
          >
          </v-text-field>
          <v-text-field
            v-model="form.inputs.secAnswer"
            :counter="50"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Respuesta de seguridad"
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
    userName: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      emailRules: [
        (v) => !!v || "Debe completar este campo",
        (v) =>
          !v ||
          /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
          "E-mail debe ser válido",
      ],
      form: {
        dialog: false,
        valid: true,
        inputs: {
          userName: "",
          //RoleNavigation: null,
          role: "",
          password: "",
          email: "",
          secQuestion: "",
          secAnswer: "",
        },
      },
      roles: [],
    };
  },
  mounted() {
    this.getRoles();
  },
  methods: {
    async getUser() {
      try {
        let response = await fetch("/api/Users/" + this.userName, {
          method: "GET",
          redirect: "follow",
        });
        const user = await response.json();
        Object.keys(this.form.inputs).forEach(key=>{
          if(key in user){
            this.form.inputs[key] = user[key]
          }
        })
      } catch (e) {
        console.error(e)
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Al parecer ha ocurrido un erorr al recuperar la información del usuario :(",
          icon: "warning",
        });
      }
    },

    async getRoles() {
      let response = await fetch("/api/Roles", {
        method: "GET",
        redirect: "follow",
      });
      this.roles = await response.json();
    },

    async saveData() {
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      //this.form.inputs.role = this.form.inputs.RoleNavigation.id;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "PUT",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      this.$refs.form.resetValidation();
      try {
        const res = await fetch("/api/Users/"+this.userName, requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.Log.add("User", "U", this.form.inputs.userName, this.form.inputs);
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Usuario modificado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.form.dialog = false;
          this.$emit("changed");
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado modificando la información",
            icon: "error",
          });
        }
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado modificar la información",
          icon: "error",
        });
      }
    },

    save(event) {
      event.preventDefault();
      event.stopPropagation();
      this.$refs.form.validate();
      this.$nextTick(() => {
        if (!this.form.valid) {
          return;
        }
        this.saveData();
      });
    },
  },
};
</script>