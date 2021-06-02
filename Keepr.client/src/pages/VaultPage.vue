<template>
  <div class="Vault container-fluid" v-if="state.activeVault">
    <div class="row">
      {{ state.activeVault.name }}
    </div>
    <div class="row">
      {{ state.activeVault.keeps }}
    </div>
    <div class="">
      <button type="button" class="btn btn-danger" data-dismiss="modal" @click="removeVault">
        Delete
      </button>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
export default {
  name: 'VaultPage',
  props: {
  },
  setup() {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    })
    return {
      state,
      router,
      async removeVault() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, Remove Vault')) {
            await vaultsService.removeVault(state.activeVault.id)
            Notification.toast('Successfully Deleted Vault', 'success')
          }
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      }
    }
  }
}
</script>
