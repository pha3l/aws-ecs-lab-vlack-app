<template>
  <div class="new-channel-form">
    <h1>New Channel: {{channelName}}</h1>
    <input type="text" v-model="channelName" />
    <button v-on:click="createChannel" v-bind:disabled="channelName === ''">Create</button>
  </div>
</template>

<script>
export default {
  name: 'ChannelForm',
  data: () => {
    return {
      channelName: ''
    }
  },

  created () {
    console.log(this.$store)
  },

  methods: {
    createChannel () {
      this.$axios.post('/channel', {Name: this.channelName})
        .then((response) => {
          this.$store.commit('addChannel', response.data)
          this.$router.push('/')
        })
    }
  }
}
</script>
