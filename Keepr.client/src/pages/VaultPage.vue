<template>
  <div class="Vault container-fluid">
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
import $ from 'jquery'
export default {
  name: 'VaultPage',
  props: {
  },
  setup() {
    const router = useRouter()
    const state = reactive({
      keep: computed(() => AppState.keep),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      router,
      async getVaultById() {
        try {
          await vaultsService.getVaultById()
          $('#keep-modal').modal('show')
        } catch (error) {
          Notification.toast('Error: ' + error, 'error')
        }
      }
    }
  }
}
</script>
