<template>
  <div class="keep card rounded shadow" v-if="keepProp">
    <!-- COME BACK HERE -->
    <div title="Open Keep Modal"
         @click="getKeepById"
         type=""
         class=""
         data-toggle="modal"
         :data-target="`#keep-modal` + keepProp.id"
    >
      <img class="card-img img-fluid" :src="keepProp.img" alt="" style="max-width:20rem;">
      <div>
        {{ keepProp.name }}
      </div>
    </div>
    <router-link :to="{name: 'ProfilePage', params: { id: keepProp.creator.id }}">
      <img class="small-profile-image rounded-circle" :src="keepProp.creator.picture" alt="">
    </router-link>
    <KeepModal />
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'
import $ from 'jquery'

export default {
  name: 'Keep',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      keep: computed(() => AppState.keep),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      router,
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keepProp.id)
          $('#keep-modal').modal('show')
        } catch (error) {
          Notification.toast('Error: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.small-profile-image{
  height: 2rem;
  width: 2rem;
}
</style>
