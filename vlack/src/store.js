export default {
  state: {
    channels: [],
    user: null
  },
  mutations: {
    setChannels (state, channelList) {
      state.channels = channelList
    },

    addChannel (state, channel) {
      state.channels.push(channel)
    },

    setUser (state, username) {
      state.user = username
      window.sessionStorage.setItem('vlackUser', username)
    },

    clearUser (state) {
      state = null
      window.sessionStorage.clear('vlackUser')
    }
  }
}
