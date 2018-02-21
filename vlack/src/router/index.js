import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import ChannelForm from '@/components/ChannelForm'
import Channel from '@/components/Channel'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/channel/new',
      name: 'ChannelForm',
      component: ChannelForm
    },
    {
      path: '/channel/:id',
      name: 'Channel',
      component: Channel
    }
  ]
})
