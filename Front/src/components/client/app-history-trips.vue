<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Historial de viajes</v-card-title>
        </v-card>
        <v-card-text>
          <v-data-table :headers="headers" :items="trips" class="elevation-1">
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-dialog v-model="dialog.show" width="500">
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                text
                color="teal"
                outlined
                rounded
                v-bind="attrs"
                v-on="on"
                @click="dialog.show = true"
                >Buscar por fecha</v-btn
              >
            </template>

            <v-card>
              <v-card-title class="text-h5 grey lighten-2">
                Búsqueda de viajes por fecha
              </v-card-title>

              <v-card-text>
                <v-form>
                  <v-text-field
                    prepend-icon="mdi-calendar"
                    name="date"
                    label="Ingrese fecha de búsqueda"
                    type="date"
                    v-model="dialog.form.date"
                  ></v-text-field>
                </v-form>
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text color="teal" outlined rounded>Buscar</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "app-history-trips",
  computed: {
    headers() {
      return [
        {
          text: "Fecha del viaje",
          value: "dateTrip",
        },
        {
          text: "Hora del viaje",
          value: "timeTrip",
        },
        {
          text: "Origen del viaje",
          value: "fromTrip",
        },
        {
          text: "Destino del viaje",
          value: "toTrip",
        },
        {
          text: "Línea de bus",
          value: "lineTrip",
        },
        {
          text: "Costo del viaje",
          value: "costTrip",
          align: "right",
        },
      ];
    },
  },
  data() {
    return {
      dialog: {
        show: false,
        form: {
          date: new Date().toISOString().split("T")[0],
        },
      },
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
          timeTrip: "14:29:56",
          fromTrip: "Pavas",
          toTrip: "San José",
          lineTrip:
            "INTERLÍNEA URUCA - PAVAS - ESCAZÚ,SAN JOSÉ - PAVAS - LOMAS DEL RÍO",
          costTrip: "295",
        },
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
          timeTrip: "14:29:56",
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
