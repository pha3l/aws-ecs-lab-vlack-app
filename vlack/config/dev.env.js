'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  HTTP_ENDPOINT: '"http://localhost:8080/api"',
  WS_ENDPOINT: '"http://localhost:8080/socket"'
})
