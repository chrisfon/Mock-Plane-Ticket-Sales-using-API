<template>
  <div>
    <v-app-bar
      app
      absolute
      color="#43a047"
      dark
      prominent
      src="./../../assets/image/wallpaper.jpg"
    >
      <v-app-bar-nav-icon
        v-show="!!profile"
        @click="drawer = true"
      ></v-app-bar-nav-icon>

      <v-toolbar-title>My Flight</v-toolbar-title>
      <template v-slot:img="{ props }">
        <v-img
          v-bind="props"
          gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
        ></v-img>
      </template>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" temporary app>
      <v-list nav dense>
        <v-list-item-group
          active-class="deep-purple--text text--accent-4"
          v-model="selected"
        >
          <v-list-item
            v-for="(option, k) in options"
            :key="k"
            @click="clickMethod(option)"
          >
            <v-list-item-icon>
              <v-icon>{{ option.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-title>{{ option.label }}</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>
<script>
export default {
  name: "app-side-bar",
  props: {
    profile: {
      required: true,
      type: String,
    },
  },
  data() {
    return {
      selected: 1,
      drawer: false,
    };
  },
  methods: {
    clickMethod(opt) {
      if (opt.selected === 999) {
        this.$emit("logoff");
        sessionStorage.clear();
      }
      this.redirect(opt);
    },

    redirect(opt) {
      this.selected = opt.selected;
      this.$router.push({
        path: opt.path,
      });
    },
  },
  computed: {
    options() {
      if (this.profile === "ROL-000001") {
        return [
          {
            icon: "mdi-account-supervisor-circle-outline",
            label: "Gestión de usuarios",
            path: "/admin/users",
            selected: 0,
          },
          {
            icon: "mdi-security",
            label: "Gestión de perfiles",
            path: "/admin/roles",
            selected: 1,
          },
          {
            icon: "mdi-form-textbox-password",
            label: "Cambio de contraseña",
            path: "/admin/change-password",
            selected: 2,
          },
          {
            icon: "mdi-map",
            label: "Gestión de países",
            path: "/admin/countries",
            selected: 3,
          },
          {
            icon: "mdi-airplane-takeoff",
            label: "Gestión de aerolíneas",
            path: "/admin/airlines",
            selected: 4,
          },
          {
            icon: "mdi-door",
            label: "Gestión de puertas",
            path: "/admin/doors",
            selected: 5,
          },
          {
            icon: "mdi-notebook-edit",
            label: "Bitácora",
            path: "/admin/journal",
            selected: 6,
          },
                    {
            icon: "mdi-airplane",
            label: "Vuelos",
            path: "/admin/flights",
            selected: 7,
          },
          {
            icon: "mdi-bug",
            label: "Log de errores",
            path: "/admin/logs",
            selected: 8,
          },
          {
            icon: "mdi-view-sequential",
            label: "Sequencias",
            path: "/admin/sequences",
            selected: 9,
          },
          /*{
            icon: "mdi-bus",
            label: "Mi historial de viajes",
            path: "/client/history/trips",
            selected: 2,
          },*/
          {
            icon: "mdi-logout",
            label: "Cerrar sesión",
            path: "/",
            selected: 999,
          },
        ];
      } else {
        return [
          {
            icon: "mdi-credit-card",
            label: "Gestión de tarjetas",
            path: "/client/cards",
            selected: 1,
          },
          {
            icon: "mdi-shopping",
            label: "Buscar tickets",
            path: "/client/search",
            selected: 2,
          },
          {
            icon: "mdi-clipboard-list",
            label: "Compras y reservaciones",
            path: "/client/historical",
            selected: 3,
          },
          {
            icon: "mdi-form-textbox-password",
            label: "Cambio de contraseña",
            path: "/admin/change-password",
            selected: 4,
          },
          {
            icon: "mdi-logout",
            label: "Cerrar sesión",
            path: "/",
            selected: 999,
          },
        ];
      }
    },
  },
};
</script>