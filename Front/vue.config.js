module.exports = {
  transpileDependencies: [
    'vuetify'
  ],
  devServer: {
    proxy: {
      "/api": {
        target: "http://localhost:56832/",
        changeOrigin: true,
        ws: false,
      },
    },
  },
}