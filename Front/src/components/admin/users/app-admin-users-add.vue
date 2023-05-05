<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
        v-if="btn == 'admin'"
        v-bind="attrs"
        v-on="on"
        color="teal"
        class="ma-2 white--text"
        fab
      >
        <v-icon dark> mdi-plus </v-icon>
      </v-btn>
      <v-btn v-else color="teal" dark v-bind="attrs" v-on="on"
        >Crear cuenta cliente</v-btn
      >
    </template>
    <v-form ref="form" v-model="form.valid" lazy-validation @submit="save">
      <v-card>
        <v-toolbar color="teal" dark>Agregar un usuario</v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="form.inputs.userName"
            :counter="30"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Usuario"
            required
          >
          </v-text-field>
          <v-select
            v-if="btn == 'admin'"
            v-model="form.inputs.role"
            :items="roles"
            item-text="description"
            item-value="id"
            label="Seleccione un rol"
            required
          >
          </v-select>
          <v-text-field
            type="password"
            v-model="form.inputs.password"
            :counter="30"
            :rules="[(v) => !!v || 'Debe completar este campo']"
            label="Contraseña (temporal)"
            required
          >
          </v-text-field>
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
          <v-text-field
            v-if="btn != 'admin' && !validCaptcha"
            required
            type="text"
            label="Ingrese los números y letras del siguiente captcha"
            v-model="inputValue"
          />
          <appCaptchaCode
            v-if="btn != 'admin' && !validCaptcha"
            :value="inputValue"
            :count="6"
            @isValid="checkValidCaptcha"
          >
            <template #icon>
              <span style="color: blue">with Custom Text Or Icon</span>
            </template>
          </appCaptchaCode>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn type="submit" :disabled="btn != 'admin' && !validCaptcha" text @click="save" outlined rounded color="teal"
            >Confirmar</v-btn
          >
          <v-btn text @click="cancel" outlined rounded color="red"
            >Cancelar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>
<script>
import AppCaptchaCode from "./../../general/app-captcha-code.vue";
export default {
  components: { AppCaptchaCode },
  props: {
    btn: {
      type: String,
      default: "admin",
    },
    role: {
      type: String,
      default: "ROL-000002",
    },
  },
  data() {
    return {
      inputValue: "",
      validCaptcha : false,
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

    cancel(){
      this.form.dialog = false;
      this.validCaptcha = false;
      this.inputValue = ''
    },

    checkValidCaptcha(value) {
      this.validCaptcha = value;
    },

    async getRoles() {
      let response = await fetch("/api/Roles", {
        method: "GET",
        redirect: "follow",
      });
      this.roles = await response.json();
    },

    async saveData() {
      if (this.btn !== "admin") {
        this.form.inputs.role = "ROL-000002";
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      //this.form.inputs.role = this.form.inputs.RoleNavigation.id;
      var raw = JSON.stringify(this.form.inputs);
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };
      this.$refs.form.resetValidation();
      try {
        const res = await fetch("/api/Users", requestOptions);
        if (res.status >= 200 && res.status <= 299) {
          this.form.inputs.password = "***************";
          this.form.inputs.secAnswer = "**************";
          this.Log.add(
            "User",
            "I",
            this.form.inputs.userName,
            this.form.inputs
          );
          this.$swal.fire({
            title: "Acción realizada con éxito",
            text: "Usuario agregado exitosamente",
            icon: "success",
          });
          this.$refs.form.reset();
          this.form.dialog = false;
          this.$emit("added");
        } else if (res.status === 409) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "Al parecer ya existe un usuario con este nombre de usuario :(",
            icon: "warning",
          });
        } else {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "No se ha logrado almacenar la información",
            icon: "error",
          });
        }
      } catch (e) {
        console.error(e);
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "No se ha logrado almacenar la información",
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