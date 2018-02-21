<template>
  <div class="channel">
    <div class="channel-header">
      <h3># {{name}}</h3>
    </div>

    <div class="messages">
      <message
        v-for="(message, i) in messages"
        :key="message.id"
        :message="message"
        @star="starMessage(i)"
      >
      </message>
    </div>

    <div class="new-message">
      <textarea
        v-model="newMessage"
        @keydown.enter.prevent="createMessage()"
        :placeholder="'New Message in #' + name"
      >
      </textarea>
    </div>
  </div>
</template>

<script>
import Message from './Message'

export default {
  name: 'Channel',

  data: () => {
    return {
      name: null,
      id: null,
      messages: [],
      newMessage: '',
      socketConnection: null
    }
  },

  watch: {
    '$route': function () {
      this.getChannel()
    },

    'messages': function () {
      setTimeout(() => {
        this.$el.querySelector('.messages').scrollTo(0, 10000000)
      }, 100)
    }
  },

  created () {
    this.getChannel()
  },

  beforeRouteUpdate (to, from, next) {
    this.socketConnection.stop()
    next()
  },

  beforeRouteLeave (to, from, next) {
    this.socketConnection.stop()
    next()
  },

  methods: {
    setupSocket () {
      const transport = this.$signalR.TransportType.WebSockets
      const socketEndpoint = location.protocol + '//' + location.hostname + ':' + process.env.WS_ENDPOINT_PORT + process.env.WS_ENDPOINT_PATH + '?channel_id='
      this.socketConnection = new this.$signalR.HubConnection(socketEndpoint + this.id, {transport: transport})
      this.socketConnection.on('broadcastMessage', (name, message) => {
        this.messages.push(message)
      })
      this.socketConnection.start()
    },

    createMessage () {
      if (this.newMessage !== '') {
        const messageParams = {
          user: this.$store.state.user,
          body: this.newMessage,
          ChannelId: this.id
        }
        this.$axios.post('/message', messageParams)
          .then((response) => {
            this.newMessage = ''
          })
      }
    },

    getChannel () {
      this.$axios.get('/channel/' + this.$route.params.id)
        .then((response) => {
          this.name = response.data.name
          this.id = response.data.id
          this.messages = response.data.messages
          this.setupSocket()
        })
    },

    starMessage (i) {
      this.messages[i].starred = true
    }
  },

  components: {
    'message': Message
  }
}
</script>
