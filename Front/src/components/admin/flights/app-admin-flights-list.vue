<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Buscador</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="body"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
          <template v-slot:item.airline="{ item }">
              {{ item.airline }}
              <appAdminAirlinesShow
                :code="item.airline"
              ></appAdminAirlinesShow>
            </template>
            <template v-slot:item.origin="{ item }">
              {{ item.origin }}
              <appAdminCountriesShow
                :code="item.origin"
              ></appAdminCountriesShow>
            </template>
            <template v-slot:item.to="{ item }">
              {{ item.to }}
              <appAdminCountriesShow :code="item.to"></appAdminCountriesShow>
            </template>
            <template v-slot:item.cost="{ item }">
              {{ item.cost }} USD</template
            >
            <template v-slot:item.dep="{ item }">
              {{ item.dateDep.split("T")[0] }}@{{ item.timeDep }}</template
            >
            <template v-slot:item.arrive="{ item }">
              {{ item.dateArrive.split("T")[0] }}@{{
                item.timeArrive
              }}</template
            >
            <template v-slot:item.actions="{ item }">
              <appAdminFlightsEdit :item="item" @changed="getFlights"></appAdminFlightsEdit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0"> </v-card-actions>
        <appAdminFlightsAdd @added="getFlights"></appAdminFlightsAdd>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appAdminCountriesShow from "./../countries/app-admin-countries-show.vue";
import appAdminFlightsAdd from "./app-admin-flights-add.vue";
import appAdminFlightsEdit from "./app-admin-flights-edit.vue";
import appAdminAirlinesShow from "../airlines/app-admin-airlines-show.vue";
export default {
  components: {
    appAdminCountriesShow,
    appAdminFlightsAdd,
    appAdminFlightsEdit,
    appAdminAirlinesShow
  },
  data() {
    return {
      currentCountry: "*ALL",
      current: null,
      loading: false,
      Airlines: [],
      dialog: false,
      filter: {
        origin: "*ALL",
        to: "*ALL",
        date: "",
      },
    };
  },
  mounted() {
    this.getFlights();
  },
  methods: {
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el vuelo?",
          text: `el vuelo ${item.code} será eliminada del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Flights/${item.code}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add("Flights", "D", item.code, item);
              this.$swal.fire(
                "vuelo eliminada!",
                "el vuelo ha sido borrado del sistema",
                "success"
              );
              this.getFlights();
            } else {
              this.$swal.fire(
                "vuelo no eliminada!",
                "Ha ocurrido un error al eliminar al vuelo",
                "error"
              );
            }
          }
        });
    },

    change(filter) {
      this.filter = filter;
    },
    async editItem(item) {
      console.log(item);
    },
    changecountry(country) {
      this.currentCountry = country;
    },

    async getFlights() {
      this.loading = true;
      try {
        let response = await fetch("/api/Flights", {
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
      return this.Airlines.filter((airline) => {
        if (airline.status === "2") {
          return false;
        }
        if (
          this.filter.origin !== "*ALL" &&
          airline.origin !== this.filter.origin
        ) {
          return false;
        }
        if (this.filter.to !== "*ALL" && airline.to !== this.filter.to) {
          return false;
        }
        if (
          this.filter.date !== "" &&
          airline.dateDep.split("T")[0] !== this.filter.date
        ) {
          return false;
        }
        return true;
      });
    },

    headers() {
      return [
        {
          text : 'Aerolinea',
          value : 'airline' 
        },
        {
          text: "Origin",
          value: "origin",
        },
        {
          text: "Destino",
          value: "to",
        },
        {
          text: "Salida",
          value: "dep",
        },
        {
          text: "Llegada",
          value: "arrive",
        },
        {
          text: "Costo",
          value: "cost",
        },
         {
          text: "Sequence ID",
          value: "seqId",
        },

        {
          text: "Acciones",
          value: "actions",
          align: "center",
          sortable: false,
        },
      ];
    },
  },
};
</script>