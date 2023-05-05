<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <app-client-historical-purchase></app-client-historical-purchase>
        <appClientHistoricalReservation class="mt-6"></appClientHistoricalReservation>
        <v-card-text max-width="600">
        </v-card-text>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import appClientHistoricalPurchase from './app-client-historical-purchase.vue';
import appClientHistoricalReservation from './app-client-historical-reservation.vue';
export default {
    name : 'app-client-historical',
    components : {
        appClientHistoricalPurchase,
        appClientHistoricalReservation
    }
}
</script>