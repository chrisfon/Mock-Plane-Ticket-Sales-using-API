<template>
  <v-app>
    <app-side-bar :profile="currentProfile" @logoff="logoff"></app-side-bar>
    <!-- Sizes your content based upon application components -->
    <v-main class="main-container">
      <!-- Provides the application the proper gutter -->
      <v-container fluid>
        <!-- If using vue-router -->
        <router-view @loged="login"></router-view>
      </v-container>
    </v-main>
    <app-footer></app-footer>

  </v-app>
</template>

<script>
import AppSideBar from './components/general/app-side-bar.vue';
import AppFooter from './components/general/app-footer.vue';
export default {
  name: "App",
  components: {
    AppSideBar, AppFooter
  },

  mounted(){
    const aux = sessionStorage.getItem('profile');
    if(aux !== null){
      this.currentProfile = aux;
    }
  },

  data: () => ({
    currentProfile : ''
  }),
  methods : {
    logoff(){
      this.currentProfile = ''
    },
    login(profile){
      this.currentProfile = profile
    }
  }
};
</script>
