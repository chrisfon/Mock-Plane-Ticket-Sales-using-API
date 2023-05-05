<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Bitácora </v-card-title>
        </v-card>
        <v-card-text max-width="600">
          <v-data-table
            :headers="headers"
            :items="body"
            :loading="loading"
            :single-expand="singleExpand"
            item-key="id"
            :expanded.sync="expanded"
            class="elevation-1"
            show-expand
          >
            <template #expanded-item="{ headers, item }">
              <td :colspan="headers.length">
                <v-card class="mt-5 mb-5">
                  <v-toolbar color="teal" dark
                    >Información del registro</v-toolbar
                  >
                  <v-card-title> Llave alterada </v-card-title>
                  <v-card-subtitle>
                    {{ item.description }}
                  </v-card-subtitle>
                  <v-card-title> información alterada </v-card-title>
                  <v-card-subtitle>
                    {{ item.detail }}
                  </v-card-subtitle>
                </v-card>
              </td>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-btn color="teal" class="ma-2 white--text" fab @click="getjournals">
            <v-icon dark> mdi-reload </v-icon>
          </v-btn>
        </v-card-actions>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  data() {
    return {
      singleExpand: false,
      expanded: [],
      loading: false,
      journals: [],
      dialog: false,
    };
  },
  mounted() {
    this.getjournals();
  },
  methods: {
    actionDesc(action) {
      switch (action) {
        case "D":
          return "Delete";
        case "I":
          return "Insert";
        default:
          return "Update";
      }
    },

    getDateTime(id) {
      return new Date(id).toLocaleString();
    },

    async editItem(item) {
      console.log(item);
    },
    deleteItem(item) {
      this.$swal
        .fire({
          title: "¿Esta seguro de que desea eliminar el usuario?",
          text: `El usuario ${item.userName} será eliminado del sistema`,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            const response = await fetch(`/api/journals/${item.userName}`, {
              method: "DELETE",
              redirect: "follow",
            });
            if (response.status >= 200 && response.status <= 299) {
              this.$swal.fire(
                "Usuario eliminado!",
                "El usuario ha sido borrado del sistema",
                "success"
              );
              this.getjournals();
            } else {
              this.$swal.fire(
                "Usuario no eliminado!",
                "Ha ocurrido un error al eliminar al usuario",
                "error"
              );
            }
          }
        });
    },
    async getjournals() {
      this.loading = true;
      try {
        let response = await fetch("/api/Journals", {
          method: "GET",
          redirect: "follow",
        });
        this.journals = await response.json();
        this.journals.sort((x,y)=>y.id -x.id);
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los registros",
          icon: "error",
        });
      } finally {
        this.loading = false;
      }
    },
  },
  computed: {
    body() {
      return this.journals.map((journal) => {
        return {
          ...journal,
          datetime: this.getDateTime(journal.id),
          actiondesc: this.actionDesc(journal.action),
        };
      });
    },

    headers() {
      return [
        {
          text: "Usuario",
          value: "userName",
        },
        {
          text: "Fecha y hora",
          value: "datetime",
        },
        {
          text: "Tabla",
          value: "recordCode",
        },
        {
          text: "Acción",
          value: "actiondesc",
        },
        { text: "", value: "data-table-expand" },
      ];
    },
  },
};
</script>