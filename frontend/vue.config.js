const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  productionSourceMap: true,
  transpileDependencies: true,
  configureWebpack: {
    devtool: 'source-map',
  },
})
