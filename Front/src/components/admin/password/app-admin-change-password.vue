<template>
  <v-container fluid>
    <v-row dense align="center" justify="center">
      <v-col cols="6">
        <v-form
          ref="form"
          v-model="form.valid"
          lazy-validation
          @submit.prevent="change"
        >
          <v-card>
            <v-toolbar color="teal" justify="center" dark
              >Cambio de contraseña</v-toolbar
            >
            <v-card-text>
              <v-text-field
                :type="form.show.show1 ? 'text' : 'password'"
                v-model="form.inputs.currentPassword"
                :rules="rules.currentPassword"
                :counter="30"
                label="Contraseña actual"
                :append-icon="form.show.show1 ? 'mdi-eye-off' : 'mdi-eye'"
                @click:append="form.show.show1 = !form.show.show1"
                required
              >
              </v-text-field>
              <v-text-field
                :type="form.show.show2 ? 'text' : 'password'"
                v-model="form.inputs.newPassword"
                :counter="30"
                :rules="rules.newPassword"
                label="Nueva contraseña"
                :append-icon="form.show.show2 ? 'mdi-eye-off' : 'mdi-eye'"
                @click:append="form.show.show2 = !form.show.show2"
                required
              >
              </v-text-field>

              <v-text-field
                :type="form.show.show3 ? 'text' : 'password'"
                v-model="form.inputs.confirmPassword"
                :counter="30"
                :rules="rules.confirmPassword"
                label="Confirmación de contraseña"
                :append-icon="form.show.show3 ? 'mdi-eye-off' : 'mdi-eye'"
                @click:append="form.show.show3 = !form.show.show3"
                required
              >
              </v-text-field>
            </v-card-text>
            <v-card-actions>
              <v-btn
                type="submit"
                text
                @click="change"
                outlined
                rounded
                color="teal"
                >Confirmar</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "app-admin-change-password",
  data() {
    return {
      currentUser: null,
      form: {
        valid: false,
        inputs: {
          currentPassword: "",
          newPassword: "",
          confirmPassword: "",
        },
        show: {
          show1: false,
          show2: false,
          show3: false,
        },
      },
      rules: {
        currentPassword: [(v) => !!v || "Este campo es requerido"],
        newPassword: [
          (v) => !!v || "Este campo es requerido",
          (v) =>
            v.length >= 8 || "Contraseña debe contener al menos 8 caracteres",
        ],
        confirmPassword: [
          (v) => !!v || "Este campo es requerido",
          (v) =>
            v === this.form.inputs.newPassword ||
            "Confirmación no coincide con la nueva contraseña",
        ],
      },
    };
  },

  mounted() {
    this.getCurrentPassword();
  },

  methods: {
    change(event) {
      event.preventDefault();
      event.stopPropagation();
      this.$refs.form.validate();
      this.$nextTick(() => this.changePassword());
    },

    async changePassword() {
      if (!this.form.valid) {
        return;
      }

      try {
        if (this.form.inputs.currentPassword !== this.currentUser.password) {
          this.$swal.fire({
            title: "Ha ocurrido un error",
            text: "La contraseña actual no es correcta",
            icon: "error",
          });
        } else {
          this.currentUser.password = this.form.inputs.newPassword;
          var myHeaders = new Headers();
          myHeaders.append("Content-Type", "application/json");
          var raw = JSON.stringify(this.currentUser);
          var requestOptions = {
            method: "PUT",
            headers: myHeaders,
            body: raw,
            redirect: "follow",
          };
          const res = await fetch("/api/Users/"+this.currentUser.userName, requestOptions);
          if (res.status >= 200 && res.status <= 299) {
            this.$swal.fire({
              title: "Acción realizada con éxito",
              text: "Contraseña cambiada exitosamente",
              icon: "success",
            });
            this.$refs.form.reset();
            this.$refs.form.resetValidation();
            this.getCurrentPassword();
          }
        }
      } catch (e) {
        console.error(e)
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Intentelo nuevamente",
          icon: "error",
        });
      }
    },

    async getCurrentPassword() {
      const aux = sessionStorage.getItem("user");
      let response = await fetch("/api/Users/" + aux, {
        method: "GET",
        redirect: "follow",
      });
      const user = await response.json();
      delete user.cards;
      delete user.reservations;
      delete user.roleNavigation;
      this.currentUser = user;
    },
  },
};
</script>