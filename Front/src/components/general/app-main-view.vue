<template>
  <v-row dense align="center" justify="center">
    <v-col cols="4" v-for="(option,k) in options" :key="k">
        <v-card   :color="option.color" dark>
          <v-card-title class="text-h5"> {{option.title}} </v-card-title>

          <v-card-subtitle>{{option.description}}</v-card-subtitle>
          <v-card-actions>
            <v-btn text> Ingrese aquí </v-btn>
          </v-card-actions>
        </v-card>
    </v-col>
  </v-row>
</template>
<script>
export default {
  name: "app-main-view",
  data() {
    return {
      options: [
        {
          title: "Ingresar como un cliente",
          description: `Si usted es un cliente, y desea realizar un pago, realizar una recarga de su monedero virtual, revisar su historial de 
                  viajes o revisar su historial de recargas...`,
          color: "teal darken-1",
        },
        {
          title: "Ingresar como un trabajador",
          description: `Si usted es un encargado administrativo o un chofer de bus...`,
          color: "cyan darken-1",
        },
      ],
    };
  },
};
</script>