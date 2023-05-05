<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de aerolíneas</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="body"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
            <template v-slot:item.country="{ item }">
              {{ item.country }}
              <appAdminCountriesShow
                :code="item.country"
              ></appAdminCountriesShow>
            </template>
            <template v-slot:item.actions="{ item }">
              <app-admin-Airlines-show
                :code="item.code"
              ></app-admin-Airlines-show>
              <app-admin-Airlines-edit
                :airline="item"
                @changed="getAirlines"
              ></app-admin-Airlines-edit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-admin-Airlines-add @added="getAirlines">
          </app-admin-Airlines-add>
          <app-admin-countries-filter
            selected="currentCountry"
            @change="changecountry"
          ></app-admin-countries-filter>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appAdminAirlinesShow from "./app-admin-airlines-show.vue";
import appAdminAirlinesAdd from "./app-admin-airlines-add.vue";
import appAdminAirlinesEdit from "./app-admin-airlines-edit.vue";
import appAdminCountriesShow from "./../countries/app-admin-countries-show.vue";
import appAdminCountriesFilter from "../countries/app-admin-countries-filter.vue";
export default {
  components: {
    appAdminAirlinesAdd,
    appAdminAirlinesEdit,
    appAdminAirlinesShow,
    appAdminCountriesShow,
    appAdminCountriesFilter,
  },
  data() {
    return {
      currentCountry: "*ALL",
      loading: false,
      Airlines: [],
      dialog: false,
    };
  },
  mounted() {
    this.getAirlines();
  },
  methods: {
    async editItem(item) {
      console.log(item);
    },
    changecountry(country) {
      this.currentCountry = country;
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar la aerolínea?",
          text: `la aerolínea ${item.description} será eliminada del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Airlines/${item.code}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add('Airline','D',item.code,item);
              this.$swal.fire(
                "Aerolínea eliminada!",
                "La aerolínea ha sido borrado del sistema",
                "success"
              );
              this.getAirlines();
            } else {
              this.$swal.fire(
                "Aerolínea no eliminada!",
                "Ha ocurrido un error al eliminar al Aerolínea",
                "error"
              );
            }
          }
        });
    },
    async getAirlines() {
      this.loading = true;
      try {
        let response = await fetch("/api/Airlines", {
          method: "GET",
          redirect: "follow",
        });
        this.Airlines = await response.json();
        this.Airlines.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los aerolíneas",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    body() {
      return this.Airlines.filter(
        (airline) =>
          this.currentCountry === "*ALL" ||
          airline.country === this.currentCountry
      );
    },

    headers() {
      return [
        {
          text: "Código de aerolínea",
          value: "code",
        },
        {
          text: "País de la aerolínea",
          value: "country",
        },
        {
          text: "Descripción dla aerolínea",
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