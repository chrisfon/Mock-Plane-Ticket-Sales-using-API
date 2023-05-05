<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de perfiles</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="roles"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
            <template v-slot:item.actions="{ item }">
              <app-admin-roles-show :rol="item.id"></app-admin-roles-show>
              <app-admin-roles-edit
                :rol="item"
                @changed="getRoles"
              ></app-admin-roles-edit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-admin-roles-add :total="total" @added="getRoles">
          </app-admin-roles-add>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appAdminRolesShow from "./app-admin-roles-show.vue";
import appAdminRolesAdd from "./app-admin-roles-add.vue";
import appAdminRolesEdit from "./app-admin-roles-edit.vue";
export default {
  components: { appAdminRolesAdd, appAdminRolesEdit, appAdminRolesShow },
  data() {
    return {
      loading: false,
      roles: [],
      dialog: false,
    };
  },
  mounted() {
    this.getRoles();
  },
  methods: {
    async editItem(item) {
      console.log(item);
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el rol?",
          text: `El rol de ${item.description} será eliminado del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Roles/${item.id}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add('Role','D',item.id,item);
              this.$swal.fire(
                "Rol eliminado!",
                "El rol ha sido borrado del sistema",
                "success"
              );
              this.getRoles();
            } else {
              this.$swal.fire(
                "Rol no eliminado!",
                "Ha ocurrido un error al eliminar al rol",
                "error"
              );
            }
          }
        });
    },
    async getRoles() {
      this.loading = true;
      try {
        let response = await fetch("/api/Roles", {
          method: "GET",
          redirect: "follow",
        });
        this.roles = await response.json();
        this.roles.sort();
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
    total() {
      return this.roles
        .map((e) => {
          let aux = e.id.split("-");
          return parseInt(aux[1]);
        })
        .reduce((a, e) => {
          if (e > a) {
            return e;
          }
          return a;
        }, 0);
    },
    headers() {
      return [
        {
          text: "id",
          value: "id",
        },
        {
          text: "Descripción del rol",
          value: "description",
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