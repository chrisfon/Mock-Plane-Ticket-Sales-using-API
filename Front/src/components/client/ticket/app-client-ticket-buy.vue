<template>
  <v-dialog
    transition="dialog-bottom-transition"
    max-width="500"
    v-model="form.dialog"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
        v-bind="attrs"
        v-on="on"
        color="teal"
        class="ma-2 white--text"
        fab
        x-small
      >
        <v-icon dark> mdi-shopping </v-icon>
      </v-btn>
    </template>
    <v-form
      ref="form"
      v-model="form.valid"
      lazy-validation
      @submit="save"
      loading
    >
      <v-card>
        <v-toolbar color="teal" dark>{{ title }} </v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="options.total"
            type="number"
            :counter="2"
            :rules="[
              (v) => !!v || 'Debe completar este campo',
              (v) => v >= 1 || 'Debe comprar al menos un tiquete',
              (v) => v < 100 || 'Puedes comprar un máximo de 99 tiquetes',
            ]"
            label="Cantidad de tiquetes"
            required
          >
          </v-text-field>
          <v-text-field :value="costTotal" label="Total a pagar" disabled>
          </v-text-field>
          <v-radio-group v-model="options.buy">
            <v-radio :label="`Realizar reserva`" :value="2"></v-radio>
            <v-radio :label="`Realizar compra`" :value="1"></v-radio>
          </v-radio-group>
          <v-select
            v-if="options.buy === 1"
            v-model="options.card"
            :items="cards"
            item-text="description"
            item-value="code"
            label="Seleccione una tarjeta"
            :required="options.buy === 1"

          >
          </v-select>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn
            type="submit"
            text
            @click="save"
            outlined
            rounded
            color="teal"
            >{{ btnText }}</v-btn
          >
          <v-btn text @click="form.dialog = false" outlined rounded color="red"
            >Cancelar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-form>
    <v-overlay absolute :value="loadingOverlay">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </v-overlay>
  </v-dialog>
</template>
<script>
import { mixinBuyTicket } from "./mixin-buy-ticket";
import { mixinResTicket } from "./mixin-res-ticket";
export default {
  props: {
    item: {
      type: Object,
      required: true,
    },
  },

  mixins: [mixinBuyTicket, mixinResTicket],

  mounted() {
    this.getCards();
  },

  data() {
    return {
      loading: false,
      loadingOverlay: false,
      cards: [],
      form: {
        dialog: false,
        ccv: "",
        valid: false,
      },
      options: {
        total: 1,
        card: "",
        buy: 1,
      },
    };
  },
  computed: {
    title() {
      return this.options.buy === 1
        ? "Realizar una compra"
        : "Realizar una reserva";
    },
    btnText() {
      return this.options.buy === 1 ? "Confirmar compra" : "Confirmar reserva";
    },
    costTotal() {
      return this.cost();
    },
  },
  methods: {
    cost() {
      return this.options.total * this.item.cost;
    },

    async getCards() {
      this.loadingOverlay = true;
      try {
        let response = await fetch("/api/Cards", {
          method: "GET",
          redirect: "follow",
        });
        this.cards = await response.json();
        this.cards = this.cards
          .filter((card) => card.userName === sessionStorage.getItem("user"))
          .map((card) => {
            return {
              ...card,
              description:
                (card.franchise === "VIS"
                  ? "Visa"
                  : card.franchise === "MSC"
                  ? "Master Card"
                  : "American Express") +
                " " +
                ("**** **** **** " + card.pan.substring(12)),
              code: card.pan,
            };
          });
        this.cards.sort();
      } catch (e) {
        this.$swal.fire({
          title: "Ha ocurrido un error",
          text: "Ha ocurrido un error al recuperar los aerolíneas",
          icon: "error",
        });
      } finally {
        this.loadingOverlay = false;
      }
    },

    async save(event) {
      event.preventDefault();
      event.stopPropagation();
      this.$refs.form.validate();
      this.$nextTick(async () => {
        this.loading = true;
        if (this.options.buy === 1) {
          let res = await this.buytickets(
            this.options.card,
            this.options.total,
            this.item
          );
          if (res == this.options.total) {
            this.$swal.fire({
              title: "Acción realizada con éxito",
              text: "Todos tus tiquetes han sido comprados",
              icon: "success",
            });
          } else if (res == 0) {
            this.$swal.fire({
              title: "Ha ocurrido un error",
              text: `solo se han comprado ${res} de ${this.options.total} tiquetes`,
              icon: "error",
            });
          } else {
            this.$swal.fire({
              title: "Ha ocurrido un error",
              text: "No se ha logrado comprar los tiquetes",
              icon: "error",
            });
          }
        } else {
          let res = await this.restickets(
            this.options.card,
            this.options.total,
            this.item
          );
          if (res == this.options.total) {
            this.$swal.fire({
              title: "Acción realizada con éxito",
              text: "Todos tus tiquetes han sido reservados",
              icon: "success",
            });
          } else if (res == 0) {
            this.$swal.fire({
              title: "Ha ocurrido un error",
              text: `solo se han reservado ${res} de ${this.options.total} tiquetes`,
              icon: "error",
            });
          } else {
            this.$swal.fire({
              title: "Ha ocurrido un error",
              text: "No se ha logrado reservar los tiquetes",
              icon: "error",
            });
          }
        }
        this.form.dialog = false;
        this.loading = false;
      });
    },
  },
};
</script>