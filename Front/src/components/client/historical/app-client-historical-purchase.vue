<template>
  <v-card class="mx-auto" color="teal">
    <v-card-title>Historico de compras</v-card-title>
    <v-card-text>
      <v-data-table
        :headers="headers"
        :items="body"
        :loading="loading"
        item-key="code"
        class="elevation-1"
      >
        <template v-slot:item.vuelo="{ item }">
          {{ item.country }}
          <app-admin-flights-show
            :ticket="item.ticket"
          ></app-admin-flights-show>
        </template>
      </v-data-table>
    </v-card-text>
  </v-card>
</template>
<script>
import appAdminFlightsShow from "./../../admin/flights/app-admin-flights-show.vue";
export default {
  name: "app-client-historical-purchase",
  components: {
    appAdminFlightsShow,
  },
  data() {
    return {
      loading: false,
      items: [],
      user: sessionStorage.getItem("user"),
    };
  },

  mounted() {
    this.getData();
  },

  methods: {
    async getData() {
      this.loading = true;
      try {
        let response = await fetch("/api/Purchases", {
          method: "GET",
          redirect: "follow",
        });
        this.items = await response.json();
        this.items.filter((p) => p.userName === this.user);
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar las compras",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    body() {
      return this.items.map((p) => {
        let aux = new Date(parseInt(p.code)).toISOString();
        let date = aux.split("T")[0];
        let time = aux.split("T")[1].split(".")[0];
        return {
          ...p,
          date: `${date}@${time}`,
          card: `**** **** **** ${p.card.substring(12)}`,
          ticket: p.ticket,
        };
      });
    },

    headers() {
      return [
        {
          text: "Fecha de compra",
          value: "date",
        },
        {
          text: "Tarjeta",
          value: "card",
        },
        {
          text: "Vuelo",
          value: "vuelo",
        },
         {
          text: "Sequence ID",
          value: "seqId",
        },
      ];
    },
  },
};
</script>