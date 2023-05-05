<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de paises</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="Countries"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
            <template v-slot:item.actions="{ item }">
              <app-admin-Countries-show :code="item.code"></app-admin-Countries-show>
              <app-admin-Countries-edit
                :country="item"
                @changed="getCountries"
              ></app-admin-Countries-edit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-admin-Countries-add  @added="getCountries">
          </app-admin-Countries-add>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appAdminCountriesShow from "./app-admin-countries-show.vue";
import appAdminCountriesAdd from "./app-admin-countries-add.vue";
import appAdminCountriesEdit from "./app-admin-countries-edit.vue";
export default {
  components: { appAdminCountriesAdd, appAdminCountriesEdit, appAdminCountriesShow },
  data() {
    return {
      loading: false,
      Countries: [],
      dialog: false,
    };
  },
  mounted() {
    this.getCountries();
  },
  methods: {
    async editItem(item) {
      console.log(item);
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el país?",
          text: `El país ${item.description} será eliminado del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Countries/${item.code}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add('Country','D',item.code,item);
              this.$swal.fire(
                "País eliminado!",
                "El Country ha sido borrado del sistema",
                "success"
              );
              this.getCountries();
            } else {
              this.$swal.fire(
                "País no eliminado!",
                "Ha ocurrido un error al eliminar al País",
                "error"
              );
            }
          }
        });
    },
    async getCountries() {
      this.loading = true;
      try {
        let response = await fetch("/api/Countries", {
          method: "GET",
          redirect: "follow",
        });
        this.Countries = await response.json();
        this.Countries.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los paises",
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
          text: "Código de país",
          value: "code",
        },
        {
          text: "Descripción del país",
          value: "description",
        },
         {
          text: "Sequence ID",
          value: "seqId",
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