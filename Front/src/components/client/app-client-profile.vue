<template>
  <v-container fluid>
    <v-row justify="center">
      <v-expansion-panels popout>
        <app-client-profile-data></app-client-profile-data>
        <app-client-profile-add-money></app-client-profile-add-money>
        <app-client-profile-last-teen></app-client-profile-last-teen>
        <app-client-profile-cards></app-client-profile-cards>
      </v-expansion-panels>
    </v-row>
  </v-container>
</template>
<script>
import appClientProfileData from "./profile/app-client-profile-data.vue";
import appClientProfileLastTeen from "./profile/app-client-profile-last-teen.vue";
import appClientProfileCards from "./profile/app-client-profile-cards.vue";
import appClientProfileAddMoney from "./profile/app-client-profile-add-money.vue";
import AppClientProfileAddMoney from "./profile/app-client-profile-add-money.vue";
export default {
  components: {
    appClientProfileData,
    appClientProfileLastTeen,
    appClientProfileCards,
    appClientProfileAddMoney,
    AppClientProfileAddMoney,
  },
  data: () => ({
    messages: [
      {
        color: "green",
        icon: "mdi-account-multiple",
        name: "Monedero",
        new: 1,
        total: 3,
        title: "Twitter",
      },
      {
        color: "amber",
        icon: "mdi-tag",
        name: "Promos",
        new: 2,
        total: 4,
        title: "Shop your way",
        exceprt: "New deals available, Join Today",
      },
    ],
    lorem:
      "Lorem ipsum dolor sit amet, at aliquam vivendum vel, everti delicatissimi cu eos. Dico iuvaret debitis mel an, et cum zril menandri. Eum in consul legimus accusam. Ea dico abhorreant duo, quo illum minimum incorrupte no, nostro voluptaria sea eu. Suas eligendi ius at, at nemore equidem est. Sed in error hendrerit, in consul constituam cum.",
  }),
};
</script>