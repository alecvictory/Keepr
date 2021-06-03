<template>
  <div class="vaultKeep card rounded shadow">
    <div title="Open Keep Modal">
      <img class="card-img img-fluid"
           :src="keepProp.img"
           alt=""
           style="max-width:25rem;"
           @click="getKeepById"
           type=""
           data-toggle="modal"
           :data-target="`#keep-modal` + keepProp.id"
      >
      <div class="card-img-overlay d-flex justify-content-around text-white">
        <div class="" @click="getKeepById">
          {{ keepProp.name }}
        </div>
        <div>
          <router-link :to="{name: 'ProfilePage', params: { id: keepProp.creatorId }}">
            <img class="small-profile-image rounded-circle" :src="keepProp.img" alt="">
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
import $ from 'jquery'

export default {
  name: 'VaultKeep',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      vault: computed(() => AppState.vault),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keepProp.id)
          await keepsService.getKeepsByVaultId(props.keepProp.vaultKeepId)
          $('#vaultKeep').modal('show')
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
