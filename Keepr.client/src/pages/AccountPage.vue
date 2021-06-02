<template>
  <div v-if="state.loading === true">
    <h1>Loading...</h1>
  </div>
  <div class="account container-fluid">
    <div class="row">
      <div class="col"></div>
      <div class="col">
        <img class="rounded" :src="account.picture" alt="" />
        {{ account.name }}
      </div>
    </div>
    <div class="row">
      <h2 class="mr-2">
        Create Vault
      </h2>
      <button title="Open Create Vault Form"
              type="button"
              class="btn btn-success"
              data-toggle="modal"
              data-target="#new-vault-form"
      >
        <i class="fa fa-plus" aria-hidden="true"></i>
      </button>
    </div>
    <div class="row">
      <Vault v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" />
    </div>
    <div class="row">
      <h2 class="mr-2">
        Create Keep
      </h2>
      <button title="Open Create Keep Form"
              type="button"
              class="btn btn-success"
              data-toggle="modal"
              data-target="#new-keep-form"
      >
        <i class="fa fa-plus" aria-hidden="true"></i>
      </button>
    </div>
    <div class="row">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
    </div>
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
  name: 'AccountPage',
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
      user: computed(() => AppState.user)
    })
    onMounted(async() => {
      await keepsService.getKeepsByProfileId(route.params.id)
      await vaultsService.getVaultsByProfileId(route.params.id)
      state.loading = false
    })
    return {
      state,
      route,
      account: computed(() => AppState.account),
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
