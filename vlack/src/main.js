// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Vuex from 'vuex'
import App from './App'
import router from './router'

import axios from 'axios'
import * as signalR from '@aspnet/signalr-client'

import applicationState from './store'

// Axios Setup
Vue.prototype.$axios = axios.create({
  baseURL: location.protocol + '//' + location.hostname + ':' + process.env.HTTP_ENDPOINT_PORT + process.env.HTTP_ENDPOINT_PATH
})

// SignalR Setup
Vue.prototype.$signalR = signalR

// Styles
require('./assets/styles/application.scss')

Vue.config.productionTip = false

Vue.use(Vuex)
const store = new Vuex.Store(applicationState)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
