<template>
  <div class="vue_client_recaptcha_icon" @click="resetCaptcha">
    <img src="https://www.freeiconspng.com/uploads/black-refresh-icon-png-9.png" style="width:60px">
    <canvas id="captcha_canvas" class="captcha_canvas" ref="captcha_canvas">{{this.code}}</canvas>
  </div>
</template>
<script>
import { h } from "vue";
export default {
  props: {
    value: {
      type: String,
      default: "",
    },
    chars: {
      type: String,
      default: "bcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
    },
    count: {
      type: Number,
      default: 10,
    },
    hideLines: {
      default: false,
    },
    customTextColor: {
      default: "",
    },
    width: {
      default() {
        return this.count * 30;
      },
    },
    height: {
      default: 50,
    },
    canvasClass: {
      default: "",
    },
    icon: {
      default: "refresh",
    },
  },
  data() {
    return {
      captcha_canvas: null,
      code: "",
      canvasWidth: this.width,
      canvasHeight: this.height,
    };
  },

  mounted() {
    this.captcha_canvas = this.$refs;
    this.captcha();
  },
  render() {
    return h("div", { class: "vue_client_recaptcha" }, [
      h(
        "div",
        {
          class: "vue_client_recaptcha_icon",
          onClick: () => this.resetCaptcha(),
        },
        [
          h(this.slots.icon ? this.slots.icon : "img", {
            src: "https://www.freeiconspng.com/uploads/black-refresh-icon-png-9.png",
            style: "width:60px",
          }),
        ]
      ),
      h(
        "canvas",
        {
          id: "captcha_canvas",
          class: `captcha_canvas ${this.canvasClass}`,
          ref: "captcha_canvas",
        },
        this.code
      ),
    ]);
  },
  methods: {
    captcha() {
      const captcha_canvas = this.$refs["captcha_canvas"];
      captcha_canvas.width = this.canvasWidth;
      captcha_canvas.height = this.canvasHeight;
      let ctx = captcha_canvas.getContext("2d");
      for (let i = 0; i < this.count; i++) {
        let sIndex = Math.floor(Math.random() * this.chars.length);
        let sDeg = (Math.random() * 30 * Math.PI) / 180;
        let cTxt = this.chars[sIndex];
        this.code += cTxt;
        /* props.chars = cTxt.toLowerCase(); */
        let x = 10 + i * 25;
        let y = 30 + Math.random() * 8;
        ctx.font = "bold 28px 微软雅黑";
        ctx.translate(x, y);
        if (this.customTextColor) {
          ctx.fillStyle = this.customTextColor;
        } else {
          ctx.fillStyle = this.randomColor();
        }
        ctx.rotate(sDeg);
        ctx.fillText(cTxt, 0, 0);
        ctx.rotate(-sDeg);
        ctx.translate(-x, -y);
      }
      if (!this.hideLines) {
        this.createLines();
      }
      this.sendValueToParent();
    },
    createLines() {
      const captcha_canvas = this.$refs["captcha_canvas"];
      let ctx = captcha_canvas.getContext("2d");
      for (let i = 0; i < this.count; i++) {
        ctx.strokeStyle = this.randomColor();
        ctx.beginPath();
        ctx.moveTo(
          Math.random() * this.canvasWidth,
          Math.random() * this.canvasHeight
        );
        ctx.lineTo(
          Math.random() * this.canvasWidth,
          Math.random() * this.canvasHeight
        );
        ctx.stroke();
      }
    },
    sendValueToParent() {
      this.$emit("getCode", this.code);
    },

    randomColor() {
      /* this function can generate random color for letters and dirtyLines */
      let r = Math.floor(Math.random() * 256);
      let g = Math.floor(Math.random() * 256);
      let b = Math.floor(Math.random() * 256);
      return "rgb(" + r + "," + g + "," + b + ")";
    },
    resetCaptcha() {
      const captcha_canvas = this.$refs["captcha_canvas"];
      let ctx = captcha_canvas.getContext("2d");
      ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);
      this.code = "";
      this.captcha();
    },
  },
  watch: {
    value() {
      if (this.code && this.code === this.value) {
        this.$emit("isValid", true);
      } else {
        this.$emit("isValid", false);
      }
    },
  },
};
</script>
<style>
.vue_client_recaptcha {
  display: flex;
  justify-content: center;
  flex-direction: row;
}
.vue_client_recaptcha_icon {
  text-align: center;
  padding: 10px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #eee;
}
.vue_client_recaptcha_icon:hover {
  background-color: #cccccc;
}
.vue_client_recaptcha .captcha_canvas {
  background: #eee;
  padding: 10px 0px;
}
</style>