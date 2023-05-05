<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Gestión de puertas</v-card-title>
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
            <template v-slot:item.isclosed="{ item }">
              <v-checkbox v-model="item.isClosed" readonly></v-checkbox>
            </template>
            <template v-slot:item.actions="{ item }">
              <!--<app-admin-Doors-show :code="item.code"></app-admin-Doors-show>-->
              <app-admin-Doors-edit
                :door="item"
                @changed="getDoor"
              ></app-admin-Doors-edit>
              <v-icon small @click="deleteItem(item)" color="red">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <app-admin-Doors-add @added="getDoor"> </app-admin-Doors-add>
          <app-admin-doors-filter :selected="current" @change="change"></app-admin-doors-filter>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
//import appAdminDoorsShow from "./app-admin-doors-show.vue";
import appAdminDoorsAdd from "./app-admin-doors-add.vue";
import appAdminDoorsEdit from "./app-admin-doors-edit.vue";
import appAdminCountriesShow from "../countries/app-admin-countries-show.vue";
import appAdminDoorsFilter from "./app-admin-countries-filter.vue";
export default {
  components: {
    appAdminDoorsAdd,
    appAdminDoorsEdit,
    //appAdminDoorsShow,
    appAdminCountriesShow,
    appAdminDoorsFilter
  },
  data() {
    return {
      current : "*ALL",
      loading: false,
      Door: [],
      dialog: false,
    };
  },
  mounted() {
    this.getDoor();
  },
  methods: {

    change(option){
      this.current = option;
    },

    async editItem(item) {
      console.log(item);
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el puerta?",
          text: `El puerta ${item.description} será eliminada del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/Doors/${item.code}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.Log.add('Doors','D',item.code,item);
              this.$swal.fire(
                "Puerta eliminada!",
                "La puerta ha sido borrado del sistema",
                "success"
              );
              this.getDoor();
            } else {
              this.$swal.fire(
                "Puerta no eliminada!",
                "Ha ocurrido un error al eliminar al Puerta",
                "error"
              );
            }
          }
        });
    },
    async getDoor() {
      this.loading = true;
      try {
        let response = await fetch("/api/Doors", {
          method: "GET",
          redirect: "follow",
        });
        this.Door = await response.json();
        this.Door.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los puertas",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {

    body(){
      return this.Door.filter(door=>{
        if(this.current === "*ALL"){
          return true;
        }
        if(this.current === '1' && door.isClosed === 1){
          return true;
        }
        if(this.current === '0' && door.isClosed === 0){
          return true;
        }
        return false;
      })
    },

    headers() {
      return [
        {
          text: "Código de puerta",
          value: "code",
        },
        {
          text: "País de la puerta",
          value: "country",
        },
        {
          text: "Descripción del puerta",
          value: "description",
        },
        {
          text: "Sequence ID",
          value: "seqId",
        },
        {
          text: "Cerrada?",
          value: "isclosed",
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