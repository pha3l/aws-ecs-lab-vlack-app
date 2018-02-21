<template>
  <div id="app">
    <template v-if="user">
      <div class="sidebar">
        <h3>{{user}}</h3>
        <h4>Channels</h4>
        <router-link class="new-channel-link" to="/channel/new"><i class="fas fa-plus-square"></i></router-link>
        <router-link
          v-for="channel in channels"
          :key="channel.id"
          :to="{name: 'Channel', params: {id: channel.id}}"
        >
          # {{channel.name}}
        </router-link>
      </div>

      <div class="content">
        <router-view/>
      </div>
    </template>

    <template v-else>
      <div class="sign-in-form">
        <h1>Sign In</h1>
        <input type="text" v-model="userName" /><br>
        <button @click="login()" :disabled="userName === ''">Login</button>
      </div>
    </template>
  </div>
</template>

<script>
export default {
  name: 'App',

  data: () => {
    return {
      userName: ''
    }
  },

  computed: {
    channels () {
      return this.$store.state.channels
    },

    user () {
      return this.$store.state.user
    }
  },

  created () {
    console.log(process.env)
    const loggedInUser = window.sessionStorage.getItem('vlackUser')
    if (loggedInUser) {
      this.$store.commit('setUser', loggedInUser)
    }
    this.getChannels()
  },

  methods: {
    getChannels () {
      this.$axios.get('/channel')
        .then((response) => {
          this.$store.commit('setChannels', response.data)
        })
    },

    login () {
      this.$store.commit('setUser', this.userName)
    }
  }
}
</script>
