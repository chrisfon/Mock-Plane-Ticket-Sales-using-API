<template>
  <v-expansion-panel>
    <v-expansion-panel-header>
      <v-row align="center" class="spacer" no-gutters>
        <v-col cols="4" sm="2" md="1">
          <v-avatar size="36px">
            <v-icon color="teal" v-text="'mdi-account-circle'"></v-icon>
          </v-avatar>
        </v-col>

        <v-col class="hidden-xs-only" sm="5" md="3">
          <strong v-html="firstName"></strong>
        </v-col>

        <v-col class="text-no-wrap" cols="5" sm="3">
          <strong v-html="'Bienvenido a BusPayment'"></strong>
        </v-col>
        <v-col class="grey--text text-truncate hidden-sm-and-down">
          &mdash; Aquí puedes revisar la información relacionada a tu cuenta
        </v-col>
      </v-row>
    </v-expansion-panel-header>

    <v-expansion-panel-content>
      <v-divider></v-divider>
      <v-card-text>
        <v-row>
          <v-col>
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title>Nombre</v-list-item-title>
                <v-list-item-subtitle>{{ name }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col>
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title>Correo asociado</v-list-item-title>
                <v-list-item-subtitle>{{ mail }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col>
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title>Dinero en el monedero</v-list-item-title>
                <v-list-item-subtitle>{{ totalMoney }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col>
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title
                  >Total de viajes en la semana</v-list-item-title
                >
                <v-list-item-subtitle>{{ weekTrips }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
        </v-row>
      </v-card-text>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>
<script>
export default {
  name: "app-client-prfile-data",
  computed: {
    firstName() {
      return this.name.split(" ")[0];
    },
  },
  data() {
    return {
      name: "Nahomi Villalobos Cascante",
      mail: "nao.villa@gmail.com",
      weekTrips: 16,
      totalMoney: "5,000",
    };
  },
};
</script>   