<template>
  <div class="keep card rounded shadow" v-if="vaultProp" @click="getVaultById()">
    <img class="card-img img-fluid" :src="vaultProp.img" alt="" style="max-width:20rem;">
    <div>
      {{ vaultProp.name }}
    </div>
    <img class="small-profile-image rounded-circle" :src="vaultProp.creator.picture" alt="">
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'

export default {
  name: 'Vault',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      vault: computed(() => AppState.vault),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      router,
      async getVaultById() {
        try {
          await vaultsService.getVaultById(props.vaultProp.id)
          router.push(({ name: 'VaultPage', params: { id: props.vaultProp.id } }))
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
