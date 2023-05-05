<template>
  <v-container fluid>
    <v-row justify="center">
      <v-card>
        <v-card-title
          >El pago se efectuará automaticamente cuando el chofer escanee su
          código</v-card-title
        >
        <v-card-text justify="center">
          <v-row justify="center">
            <v-card class="mt-6 mb-6 mr-6 ml-6">
              <qr-code :text="qrText"></qr-code>
            </v-card>
          </v-row>
          <v-row justify="center">
            <v-card class="mt-6 mb-6 mr-6 ml-6">
              <v-btn icon color="teal lighten-2" dark @click="changeDate">
                <v-icon large dark> mdi-refresh </v-icon>
              </v-btn>
            </v-card>
          </v-row>
        </v-card-text>
      </v-card>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "app-client-pay",
  data() {
    return {
      currentDate: new Date(),
    };
  },
  methods: {
    changeDate() {
      this.currentDate = new Date();
    },
  },
  computed: {
    qrText() {
      return (
        "Proyecto de la U, en construcción :p" + this.currentDate.toISOString()
      );
    },
  },
};
</script>