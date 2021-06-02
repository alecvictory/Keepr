<template>
  <div v-if="state.loading === true">
    <h1>Loading...</h1>
  </div>
  <div class="profile container-fluid">
    <div class="row">
      <div class="col"></div>
      <div class="col">
        <!-- {{ profile.name }} -->
      </div>
    </div>
    <div class="row">
      <button title="Open Create Vault Form"
              type="button"
              class=""
              data-toggle="modal"
              data-target="#new-vault-form"
      >
        Create Vault
      </button>
    </div>
    <div class="row">
    </div>
    <div class="row">
      <button title="Open Create Keep Form"
              type="button"
              class=""
              data-toggle="modal"
              data-target="#new-keep-form"
      >
        Create Keep
      </button>
    </div>
    <div class="row">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
    </div>
    <h1>This is the user's profile</h1>
    <CreateVaultModal />
    <CreateKeepModal />
  </div>
</template>

<script>
import { computed, reactive, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'

export default {
  name: 'ProfilePage',
  props: {
    vaultProp: {
      type: Object,
      required: true
    },
    profile: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profiles: computed(() => AppState.profiles),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      await keepsService.getKeepsByProfileId(route.params.id)
      await vaultsService.getVaultsByProfileId(route.params.id)
      state.loading = false
    })
    return {
      state,
      route,
      async getVaultById() {
        try {
          await vaultsService.getVaultById(props.vaultProp.id)
        } catch (error) {
          Notification.toast('Error: ' + error, 'error')
        }
      }
    }
  }
}
</script>
