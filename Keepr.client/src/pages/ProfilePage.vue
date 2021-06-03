<template>
  <div v-if="state.loading === true && !state.activeProfile">
    <h1>Loading...</h1>
  </div>
  <div class="profile container-fluid">
    <div class="row m-5">
      <div class="col-2">
        <img :src="state.activeProfile.picture" alt="">
      </div>
      <div class="col">
        <h1>
          {{ state.activeProfile.name }}
        </h1>
        <h3>
          <p>Vaults: {{ state.vaults.length }} </p>
        </h3>
        <h3>
          <p>Keeps: {{ state.keeps.length }}</p>
        </h3>
        {{}}
      </div>
    </div>
    <div class="row m-5">
      <h2 class="mr-4">
        Vaults
      </h2>
      <button title="Open Create Vault Form"
              type="button"
              class="btn btn-success"
              data-toggle="modal"
              data-target="#new-vault-form"
              v-if="state.account.id === route.params.id"
      >
        <i class="fa fa-plus" aria-hidden="true"></i>
      </button>
    </div>
    <div class="row">
      <div class="col">
        <div class="card-columns m-2">
          <Vault v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" />
        </div>
      </div>
    </div>
    <div class="row m-5">
      <h2 class="mr-4">
        Keeps
      </h2>
      <button title="Open Create Keep Form"
              type="button"
              class="btn btn-success"
              data-toggle="modal"
              data-target="#new-keep-form"
              v-if="state.account.id === route.params.id"
      >
        <i class="fa fa-plus" aria-hidden="true"></i>
      </button>
    </div>
    <div class="row">
      <div class="col">
        <div class="card-columns m-2">
          <Keep v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
        </div>
      </div>
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
import { profilesService } from '../services/ProfilesService'
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
      activeProfile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      await keepsService.getKeepsByProfileId(route.params.id)
      await vaultsService.getVaultsByProfileId(route.params.id)
      await profilesService.getProfileById(route.params.id)
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
