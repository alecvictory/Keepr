<template>
  <div class="Vault container-fluid" v-if="state.activeVault">
    <div class="row">
      <h1>
        {{ state.activeVault.name }}
      </h1>
    </div>
    <div class="row">
      <div>
        <p>Keeps:</p>  {{ state.vaultKeeps.length }}
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="card-columns m-2">
          <VaultKeep v-for="keep in state.vaultKeeps" :key="keep.id" :keep-prop="keep" />
        </div>
      </div>
    </div>
    <div>
      <button type="button" class="btn btn-danger" data-dismiss="modal" v-if="state.account.id == state.activeVault.creatorId" @click="removeVault">
        Delete
      </button>
    </div>
  </div>
</template>

<script>
import { computed, reactive, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'VaultPage',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        Notification.toast('Error: ' + error, 'warning')
      }
    })
    return {
      state,
      route,
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
