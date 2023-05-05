<template>
  <v-expansion-panel>
    <v-expansion-panel-header>
      <v-row align="center" class="spacer" no-gutters>
        <v-col cols="4" sm="2" md="1">
          <v-avatar size="36px">
            <v-icon color="teal" v-text="'mdi-account-cash-outline'"></v-icon>
          </v-avatar>
        </v-col>

        <v-col class="hidden-xs-only" sm="5" md="3">
          <strong v-html="'Monedero'"></strong>
        </v-col>

        <v-col class="text-no-wrap" cols="5" sm="3">
          <strong v-html="''"></strong>
        </v-col>
        <v-col class="grey--text text-truncate hidden-sm-and-down">
          &mdash; Aquí puedes gestionar tu monedero virtual
        </v-col>
      </v-row>
    </v-expansion-panel-header>

    <v-expansion-panel-content>
      <v-divider></v-divider>
      <v-card-text>
        <v-row>
          <v-col cols="3">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title>
                  <v-dialog v-model="form.dialog" width="500">
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        text
                        icon
                        v-on="on"
                        v-bind="attrs"
                        color="teal lighten-2"
                        dark
                      >
                        <v-icon large dark left> mdi-card-plus </v-icon> </v-btn
                      >Añadir fondos al monedero
                    </template>
                    <v-form
                      ref="form"
                      v-model="form.valid"
                      lazy-validation
                      @submit="addMoney"
                    >
                      <v-card>
                        <v-card-title class="text-h5 grey lighten-2">
                          Añadir fondos al monedero virtual
                        </v-card-title>

                        <v-card-text>
                          <v-select
                            v-model="form.cardSelected"
                            :items="cardsList"
                            item-text="label"
                            required
                            item-value="id"
                            label="Seleccione una tarjeta"
                          ></v-select>
                          <!-- <v-text-field
                            v-model="form.cvv"
                            :counter="3"
                            type="password"
                            :rules="formRules.cvv"
                            label="Ingrese el CVV"
                            required
                          ></v-text-field>
                         <v-row>
                            <v-col>
                              <v-select
                                v-model="form.monthSelected"
                                :items="months"
                                required
                                label="Mes de vencimiento"
                              ></v-select>
                            </v-col>
                            <v-col>
                              <v-select
                                v-model="form.yearSelected"
                                :items="years"
                                required
                                label="Año de vencimiento"
                              ></v-select>
                            </v-col>
                          </v-row>-->
                          <v-text-field
                            v-model="form.total"
                            type="number"
                            :rules="formRules.total"
                            label="Ingrese el total a recargar"
                            required
                          ></v-text-field>
                        </v-card-text>

                        <v-divider></v-divider>

                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn
                            type="submit"
                            text
                            color="teal"
                            outlined
                            rounded
                            >Confirmar</v-btn
                          >
                          <v-btn
                            @click="form.dialog = false"
                            color="red lighten-2"
                            outlined
                            rounded
                            >Cancelar</v-btn
                          >
                        </v-card-actions>
                      </v-card>
                    </v-form>
                  </v-dialog>
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col cols="3">
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title
                  >Total de dinero en el monedero</v-list-item-title
                >
                <v-list-item-subtitle>{{ moneyFormat }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col>
            <v-timeline align-top dense>
              <v-timeline-item
                color="teal"
                small
                v-for="(payment, k) in payments"
                :key="k"
              >
                <v-row class="pt-1">
                  <v-col cols="3">
                    <strong>{{ payment.total }} {{ payment.card }} </strong>
                    <div class="text-caption">
                      {{ payment.date }}
                    </div>
                  </v-col>
                  <v-col cols="9"> </v-col>
                </v-row>
              </v-timeline-item>
            </v-timeline>
          </v-col>
        </v-row>
      </v-card-text>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>
<script>
export default {
  name: "app-client-prfile-add-money",
  methods: {
    addMoney(event) {
      event.stopPropagation();
      event.preventDefault();
      this.$refs.form.validate();
      this.totalMoney += 0 + this.form.total;
      this.form.dialog = false;
      this.$swal.fire({
        title: "Acción realizada con éxito",
        text: "Los fondos han sido agregada al monedero!",
        icon: "success",
      });
      this.payments.unshift({
        date: `${
          new Date().toISOString().split("T")[0]
        }@${new Date().toLocaleTimeString()}`,
        card: this.cards[this.form.cardSelected].PAN,
        total: this.form.total.toLocaleString("eu-US"),
      });
    },
  },
  computed: {
    moneyFormat() {
      return this.totalMoney.toLocaleString("en-US");
    },
    cardsList() {
      return this.cards.map((card) => ({
        label: `${card.PAN} ${card.franquicia}`,
        ...card,
      }));
    },
    months() {
      return Array.from({ length: 12 }, (k, v) =>
        (v + 1).toString().padStart(2, "0")
      );
    },
    years() {
      const currentYear = new Date().getFullYear() - 2000;
      return Array.from({ length: 5 }, (k, v) => (v + currentYear).toString());
    },
  },
  data() {
    return {
      formRules: {
        cvv: [
          (v) => !!v || "CVV es un valor requerido",
          (v) => (v && v.length === 3) || "Longitud del cvv invalida",
          (v) => (v && !isNaN(v)) || "Ingrese una cvv valida",
        ],
        total: [
          (v) => !!v || "Ingrese el total a recargar",
          (v) => (v && !isNaN(v)) || "Ingrese el total a recargar",
          (v) =>
            (v && parseInt(v) > 0 && parseInt(v) <= 10000) ||
            "Ingrese un valor valido",
        ],
      },
      form: {
        valid: true,
        dialog: false,
        cardSelected: "",
        monthSelected: "",
        yearSelected: "",
        cvv: "",
        month: "",
        year: "",
        total: 1000,
      },
      name: "Nahomi Villalobos Cascante",
      totalMoney: 5000,
      cards: [
        {
          id: 0,
          franquicia: "Mastercard",
          PAN: "**** **** **** 1234",
          tarjetahabiente: "Nahomi Villalobos C.",
        },
        {
          id: 1,
          franquicia: "Visa",
          PAN: "**** **** **** 7889",
          tarjetahabiente: "Nahomi Villalobos",
        },
        {
          id: 2,
          franquicia: "Mastercard",
          PAN: "**** **** **** 6666",
          tarjetahabiente: "Nahomi Villalobos Cascante",
        },
      ],
      payments: [
        {
          date: "2000-03-01@12:00:00",
          card: "**** **** **** 6666",
          total: "1,000",
        },
        {
          date: "2000-02-01@12:00:00",
          card: "**** **** **** 6666",
          total: "1,000",
        },
        {
          date: "2000-01-01@12:00:00",
          card: "**** **** **** 6666",
          total: "1,000",
        },
      ],
    };
  },
};
</script>   