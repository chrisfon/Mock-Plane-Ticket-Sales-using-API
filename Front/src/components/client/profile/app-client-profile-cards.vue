<template>
  <v-expansion-panel>
    <v-expansion-panel-header>
      <v-row align="center" class="spacer" no-gutters>
        <v-col cols="4" sm="2" md="1">
          <v-avatar size="36px">
            <v-icon color="teal" v-text="'mdi-credit-card'"></v-icon>
          </v-avatar>
        </v-col>

        <v-col class="hidden-xs-only" sm="5" md="3">
          <strong v-html="'Métodos de pago registrados'"></strong>
        </v-col>

        <v-col class="text-no-wrap" cols="5" sm="3">
          <strong v-html="''"></strong>
        </v-col>
        <v-col class="grey--text text-truncate hidden-sm-and-down">
          &mdash; Aquí puedes gestionar sus métodos de pago
        </v-col>
      </v-row>
    </v-expansion-panel-header>

    <v-expansion-panel-content>
      <v-divider></v-divider>
      <v-card-text>
        <v-row>
          <v-col>
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
                      >Añadir método de pago
                    </template>
                    <v-form
                      ref="form"
                      v-model="form.valid"
                      lazy-validation
                      @submit="saveCard"
                    >
                      <v-card>
                        <v-card-title class="text-h5 grey lighten-2">
                          Registrar tarjeta
                        </v-card-title>

                        <v-card-text>
                          <v-text-field
                            v-model="form.tarjetahabiente"
                            :counter="50"
                            :rules="formRules.name"
                            label="Tarjetahabiente (tal como aparece en la tarjeta)"
                            required
                          ></v-text-field>
                          <v-text-field
                            v-model="form.PAN"
                            :counter="16"
                            :rules="formRules.card"
                            label="Ingrese el número de la tarjeta"
                            required
                          ></v-text-field>
                          <v-text-field
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
                          </v-row>
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
          <v-col v-for="(card, k) in cards" :key="k">
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title>
                  {{ card.PAN }}
                  <v-btn
                    @click="deleteCard(card.id)"
                    text
                    icon
                    color="red lighten-2"
                    dark
                  >
                    <v-icon dark left> mdi-delete </v-icon>
                  </v-btn>
                </v-list-item-title>
                <v-list-item-subtitle>{{
                  card.franquicia
                }}</v-list-item-subtitle>
                <v-list-item-subtitle>{{
                  card.tarjetahabiente
                }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
        </v-row>
      </v-card-text>
    </v-expansion-panel-content>
  </v-expansion-panel>
</template>
<script>
export default {
  name: "app-client-prfile-cards",
  methods: {
    saveCard(event) {
      event.stopPropagation();
      event.preventDefault();
      this.$refs.form.validate();
      this.cards.push({
        id: this.cards.length,
        franquicia: "Visa",
        tarjetahabiente: this.form.tarjetahabiente,
        PAN: "**** **** **** " + this.form.PAN.substring(12),
      });
      this.$refs.form.resetValidation();
      this.form.dialog = false;
      this.$swal.fire({
        title: "Acción realizada con éxito",
        text: "La tarjeta ha sido agregada al sistema!",
        icon: "success",
      });
    },
    deleteCard(id) {
      this.$swal
        .fire({
          title: "¿Esta seguro?",
          text: "La tarjeta será removida del sistema!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Confirmar!",
          cancelButtonText: "Cancelar",
        })
        .then((result) => {
          if (result.isConfirmed) {
            this.cards = this.cards.filter((card) => card.id !== id);
            this.$swal.fire(
              "Tarjeta eliminada!",
              "La tarjeta ha sido borrada del sistema",
              "success"
            );
          }
        });
    },
  },

  computed: {
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
        name: [
          (v) => !!v || "Por favor ingrese el nombre del tarjetahabiente",
          (v) => (v && v.length >= 15) || "Ingrese el nombre",
        ],
        card: [
          (v) => !!v || "La tarjeta es requerida",
          (v) => (v && v.length === 16) || "Longitud de tarjeta invalida",
          (v) => (v && !isNaN(v)) || "Ingrese una tarjeta valida",
        ],
        cvv: [
          (v) => !!v || "CVV es un valor requerido",
          (v) => (v && v.length === 3) || "Longitud del cvv invalida",
          (v) => (v && !isNaN(v)) || "Ingrese una cvv valida",
        ],
      },
      form: {
        PAN: "",
        tarjetahabiente: "",
        valid: true,
        dialog: false,
      },
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
    };
  },
};
</script>
