<template>
  <v-expansion-panel>
    <v-expansion-panel-header>
      <v-row align="center" class="spacer" no-gutters>
        <v-col cols="4" sm="2" md="1">
          <v-avatar size="36px">
            <v-icon color="teal" v-text="'mdi-bus-clock'"></v-icon>
          </v-avatar>
        </v-col>

        <v-col class="hidden-xs-only" sm="5" md="3">
          <strong v-html="'Resumenes de viajes'"></strong>
        </v-col>

        <v-col class="text-no-wrap" cols="5" sm="3">
          <strong v-html="''"></strong>
        </v-col>
        <v-col class="grey--text text-truncate hidden-sm-and-down">
          &mdash; Aquí puedes revisar los ultimos 10 viajes realizados
        </v-col>
      </v-row>
    </v-expansion-panel-header>

    <v-expansion-panel-content>
      <v-divider></v-divider>
      <v-card-text>
        <v-timeline align-top dense>
          <v-timeline-item color="teal" small v-for="(trip,k) in trips" :key="k">
            <v-row class="pt-1">
              <v-col cols="3">
                <strong>{{trip.fromTrip}} -> {{trip.toTrip}}</strong>
                <div class="text-caption">{{trip.dateTrip}}@{{trip.timeTrip}}</div>
              </v-col>
              <v-col cols="9">
                
                
              </v-col>
            </v-row>
          </v-timeline-item>
        </v-timeline>
      </v-card-text>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>
<script>
export default {
  name: "app-client-last-teen",
  computed: {
  },
  data() {
    return {
      trips: [
        {
          dateTrip: "2022-06-07",
          timeTrip: "14:29:56",
          fromTrip: "Pavas",
          toTrip: "San José",
          lineTrip:
            "INTERLÍNEA URUCA - PAVAS - ESCAZÚ,SAN JOSÉ - PAVAS - LOMAS DEL RÍO",
          costTrip: "295",
        },
        {
          dateTrip: "2022-06-07",
          timeTrip: "11:29:56",
          fromTrip: "Pavas",
          toTrip: "San José",
          lineTrip:
            "INTERLÍNEA URUCA - PAVAS - ESCAZÚ,SAN JOSÉ - PAVAS - LOMAS DEL RÍO",
          costTrip: "295",
        },
        {
          dateTrip: "2022-06-05",
          timeTrip: "13:29:56",
          fromTrip: "Pavas",
          toTrip: "San José",
          lineTrip:
            "INTERLÍNEA URUCA - PAVAS - ESCAZÚ,SAN JOSÉ - PAVAS - LOMAS DEL RÍO",
          costTrip: "295",
        },
        {
          dateTrip: "2022-06-05",
          timeTrip: "09:29:56",
          fromTrip: "Pavas",
          toTrip: "San José",
          lineTrip:
            "INTERLÍNEA URUCA - PAVAS - ESCAZÚ,SAN JOSÉ - PAVAS - LOMAS DEL RÍO",
          costTrip: "295",
        },
      ],
    };
  },
};
</script>   