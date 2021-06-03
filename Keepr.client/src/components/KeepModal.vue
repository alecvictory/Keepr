<template>
  <div class="keep-modal container-fluid
       modal"
       v-if="state.activeKeep"
       id="keep-modal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="row">
          <div class="col">
            <img :src="state.activeKeep.img" alt="" class="img-fluid p-2 rounded" style="border-radius: 1rem ;">
          </div>
          <div class="col">
            <div class="row">
              <div class="col">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
            </div>
            <div class="row pb-4">
              <div class="col d-flex justify-content-center">
                <i class="fa fa-eye pr-2"></i><span class="pr-5">{{ state.activeKeep.views }}</span>
                <i class="fab fa-kaggle pr-2"></i><span class="pr-5">{{ state.activeKeep.keeps }}</span>
                <i class="fa fa-share pr-2"></i><span class="">{{ state.activeKeep.shares }}</span>
              </div>
            </div>
            <div class="row pb-4">
              <div class="col d-flex justify-content-center">
                <h2>{{ state.activeKeep.name }}</h2>
              </div>
            </div>
            <div class="row">
              <div class="col d-flex justify-content-center">
                <p>{{ state.activeKeep.description }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer justify-content-around">
          <form @submit.prevent="addVaultKeep">
            <div class="dropdown p-2">
              <select class="form-select" aria-labelledby="dropdownMenuButton" style="border: 1px gray solid;" v-model="state.newVaultKeep.vaultId">
                <option v-for="vault in state.accountVaults" :key="vault.id" :value="vault.id">
                  {{ vault.name }}
                </option>
              </select>
              <button type="submit" class="btn btn-success btn-sm">
                <i class="fas fa-plus-circle"></i>
              </button>
            </div>
          </form>
          <button type="button" class="btn btn-primary btn-sm" v-if="state.account.id == state.activeKeep.creatorId" @click="removeKeepFromVault">
            Remove from Vault
          </button>
          <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" v-if="state.account.id == state.activeKeep.creatorId" @click="removeKeep">
            <i class="fa fa-trash" aria-hidden="true"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'
import { useRouter } from 'vue-router'
// import $ from 'jquery'

export default {
  name: 'KeepModal',
  setup() {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      newVaultKeep: {},
      accountVaults: computed(() => AppState.accountVaults),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    })
    return {
      state,
      router,
      async removeKeep() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, Remove Keep')) {
            await keepsService.removeKeep(state.activeKeep.id)
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      },
      async addVaultKeep() {
        try {
          state.newVaultKeep.keepId = state.activeKeep.id
          await keepsService.addVaultKeep(state.newVaultKeep)
          Notification.toast('Successfully Added Keep', 'success')
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
