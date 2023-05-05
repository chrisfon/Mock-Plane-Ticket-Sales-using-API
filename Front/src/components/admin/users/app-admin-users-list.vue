<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de usuarios </v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="Users"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
            <template v-slot:item.role="{ item }">
              {{ item.role }}
              <appAdminRolesShow :rol="item.role"></appAdminRolesShow>
            </template>
            <template v-slot:item.actions="{ item }">
              <app-admin-Users-edit
                :userName="item.userName"
                @changed="getUsers"
              ></app-admin-Users-edit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-admin-Users-add @added="getUsers"> </app-admin-Users-add>
          <v-btn  color="teal" class="ma-2 white--text" fab @click="getUsers">
        <v-icon dark> mdi-reload </v-icon>
      </v-btn>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appAdminUsersAdd from "./app-admin-users-add.vue";
import appAdminUsersEdit from "./app-admin-users-edit.vue";
import appAdminRolesShow from "./../roles/app-admin-roles-show.vue";
export default {
  components: {
    appAdminUsersAdd,
    appAdminUsersEdit,
    appAdminRolesShow,
  },
  data() {
    return {
      loading: false,
      Users: [],
      dialog: false,
    };
  },
  mounted() {
    this.getUsers();
  },
  methods: {
    async editItem(item) {
      console.log(item);
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el usuario?",
          text: `El usuario ${item.userName} será eliminado del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Users/${item.userName}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {

              this.Log.add('User','D',item.userName,item);
              this.$swal.fire(
                "Usuario eliminado!",
                "El usuario ha sido borrado del sistema",
                "success"
              );
              this.getUsers();
            } else {
              this.$swal.fire(
                "Usuario no eliminado!",
                "Ha ocurrido un error al eliminar al usuario",
                "error"
              );
            }
          }
        });
    },
    async getUsers() {
      this.loading = true;
      try {
        let response = await fetch("/api/users", {
          method: "GET",
          redirect: "follow",
        });
        this.Users = await response.json();
        this.Users = this.Users.map(e=>{
          e.password = "******";
          e.secAnswer = "******";
          return e;
        })
        this.Users.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los perfiles",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    headers() {
      return [
        {
          text: "Usuario",
          value: "userName",
        },
        {
          text: "Rol",
          value: "role",
        },
        {
          text: "Correo",
          value: "email",
        },
        {
          text: "Acciones",
          value: "actions",
          sortable: false,
        },
      ];
    },
  },
};
</script>