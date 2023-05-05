<template>
  <div class="sample-captcha">
    <input type="text" v-model="inputValue" />
    <!-- Hide Letters And Show NumbersOnly Without Lines -->
    <!-- Can Set Your Custom Icon Or Text With Slot -->
    <appCaptchaCode
      :value="inputValue"
      :count="6"
      @getCode="getCaptchaCode"
      @isValid="checkValidCaptcha"
    >
      <template #icon>
        <span style="color: blue">with Custom Text Or Icon</span>
      </template>
    </appCaptchaCode>
  </div>
</template>

<script>
import appCaptchaCode from "./app-captcha-code.vue";
export default {
  components: {
    appCaptchaCode,
  },

  data() {
    return {
      inputValue: "",
    };
  },
  methods: {
    getCaptchaCode(value) {
      /* you can access captcha code */
      console.log(value);
    },
    checkValidCaptcha(value) {
      /* expected return boolean if your value and captcha code are same return True otherwise return False */
      console.log(value);
    },
  },
};
</script>