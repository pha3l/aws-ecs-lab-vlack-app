<template>
  <div class="message" :class="{starred: message.starred}">
    <b>{{message.user}}</b> <span>{{message.createdAt}}</span>
    <p>{{message.body}}</p>
    <a @click="star"><i class="far fa-star"></i></a>
  </div>
</template>

<script>
export default {
  name: 'Message',
  props: ['message'],

  methods: {
    star () {
      this.$emit('star')
    }
  }
}
</script>
