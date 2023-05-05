<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de tarjetas</v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="body"
            :loading="loading"
            item-key="id"
            class="elevation-1"
          >
            <template v-slot:item.actions="{ item }">
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-Client-Card-add @added="getCards"> </app-Client-Card-add>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appClientCardAdd from "./app-client-card-add.vue";

export default {
  components: {
    appClientCardAdd,
  },
  data() {
    return {
      currentCountry: "*ALL",
      loading: false,
      Card: [],
      dialog: false,
    };
  },
  mounted() {
    this.getCards();
  },
  methods: {
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar la tarjeta?",
          text: `la tarjeta será eliminada del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Cards/${item.card}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add("Card", "D", item.card, item);
              this.$swal.fire(
                "Aerolínea eliminada!",
                "La tarjeta ha sido borrado del sistema",
                "success"
              );
              this.getCards();
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
    async getCards() {
      this.loading = true;
      try {
        let response = await fetch("/api/Cards", {
          method: "GET",
          redirect: "follow",
        });
        this.Card = await response.json();
        this.Card = this.Card.filter(
          (card) => card.userName === sessionStorage.getItem("user")
        );
        this.Card.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar las tarjetas",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    body() {
      return this.Card.map((Card) => ({
        franchise:
          Card.franchise === "VIS"
            ? "Visa"
            : Card.franchise === "MSC"
            ? "Master Card"
            : "American Express",
        pan: "**** **** **** " + Card.pan.substring(12),
        card: Card.pan,
      }));
    },

    headers() {
      return [
        {
          text: "número de tarjeta",
          value: "pan",
        },
        {
          text: "Franquicia",
          value: "franchise",
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