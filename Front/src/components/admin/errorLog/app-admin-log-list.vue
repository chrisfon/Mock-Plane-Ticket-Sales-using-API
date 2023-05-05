<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="mx-auto" color="teal" dark>
          <v-card-title>Log de errores </v-card-title>
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
                    >Información del error</v-toolbar
                  >
                  <v-card-title> Log del error </v-card-title>
                  <v-card-subtitle>
                    {{ item.detail }}
                  </v-card-subtitle>
                </v-card>
              </td>
            </template>
          </v-data-table>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-btn color="teal" class="ma-2 white--text" fab @click="getlogs">
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
      logs: [],
      dialog: false,
    };
  },
  mounted() {
    this.getlogs();
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

    async getlogs() {
      this.loading = true;
      try {
        let response = await fetch("/api/ErrorLogs", {
          method: "GET",
          redirect: "follow",
        });
        this.logs = await response.json();
        this.logs.sort();
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
      return this.logs.map((log) => {
        return {
          ...log,
          datetime: this.getDateTime(log.id),
          preview: log.detail.substring(0,150)+'...'
        };
      });
    },

    headers() {
      return [
        {
          text: "Fecha y hora",
          value: "datetime",
        },
        {
          text: "Vista Previa",
          value: "preview",
        },
        { text: "", value: "data-table-expand" },
      ];
    },
  },
};
</script>