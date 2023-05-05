<template>
  <div id="inspire">
    <v-main>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="teal">
                <v-toolbar-title>Ingrese sus credenciales</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form @submit.prevent="login">
                  <v-text-field
                    v-model="form.userName"
                    prepend-icon="mdi-shield-account"
                    name="login"
                    label="Login"
                    type="text"
                    required
                  ></v-text-field>
                  <v-text-field
                    v-model="form.password"
                    id="password"
                    prepend-icon="mdi-lock"
                    name="password"
                    label="Password"
                    type="password"
                    required
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <app-admin-Users-add
                  btn="client"
                  role="ROL-000002"
                  @added="userAdded"
                >
                </app-admin-Users-add>
                <v-btn @click="login" color="teal" dark to="/"
                  >Iniciar sesión</v-btn
                >
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-main>
  </div>
</template>

<script>
import appAdminUsersAdd from "../admin/users/app-admin-users-add.vue";

export default {
  name: "app-ogin",

  components: { appAdminUsersAdd },

  createAccount() {},

  mounted() {
    const aux = sessionStorage.getItem("profile");
    if (aux !== null) {
      switch (aux) {
        case "ROL-000001":
          return this.goAdmin();
      }
    }
  },

  data() {
    return {
      form: {
        userName: "",
        password: "",
      },
    };
  },

  methods: {
    userAdded() {
      this.$swal.fire({
        title: "Usuario creado con éxito",
        text: "Inicie sesión",
        icon: "success",
      });
    },
    async login(event) {
      event.preventDefault();
      event.stopPropagation();
      let response = await fetch("/api/Users/" + this.form.userName, {
        method: "GET",
        redirect: "follow",
      });
      const user = await response.json();
      if (user.password !== this.form.password) {
        this.$swal.fire({
          title: "Ha ocurrido un error en el inicio de sesión",
          text: "Por favor, verifique sus credenciales",
          icon: "error",
        });
      } else {
        sessionStorage.setItem("user", user.userName);
        this.redirect(user.role);
      }
    },

    redirect(profile = "ROL-000001") {
      sessionStorage.setItem("profile", profile);
      if (profile === "ROL-000001") {
        this.goAdmin();
      }else{
        this.goClient();
      }
    },
    goAdmin() {
      this.$router.push({
        path: "/admin/roles",
      });
      this.$emit("loged", "ROL-000001");
    },

    goClient() {
      this.$router.push({
        path: "/client/cards",
      });
      this.$emit("loged", "ROL-000002");
    },

  },
};
</script>

<style></style>
