<template>
  <div class="create-vault-modal container-fluid
       modal"
       id="new-vault-form"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Create Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <form @submit.prevent="createVault">
          <div class="modal-body container-fluid modalScroll modal-height">
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="title">Title:</label>
                  <input type="text"
                         class="form-control"
                         id="title"
                         v-model="state.newVault.name"
                         required
                  >
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="imgUrl">Image URL:</label>
                  <input type="text"
                         class="form-control"
                         id="imgUrl"
                         v-model="state.newVault.imgUrl"
                  >
                </div>
                <div class="col-12">
                  <div class="form-group">
                    <label for="description">Description:</label>
                    <input type="text"
                           class="form-control"
                           id="description"
                           v-model="state.newVault.description"
                           required
                    >
                  </div>
                </div>
                <div class="col-12">
                  <div class="form-group">
                    <label for="isPrivate">Private Vault?</label>
                    <input @click="state.checkbox = !state.checkbox"
                           type="checkbox"
                           class="form-control"
                           id="isPrivate"
                           v-model="state.newVault.isPrivate"
                    >
                  </div>
                </div>
              </div>
              <div class="row mt-4">
                <div class="modal-footer d-flex justify-content-end col-12">
                  <button type="submit" class="btn btn-success">
                    Create
                  </button>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
import $ from 'jquery'

export default {
  name: 'CreateVaultModal',
  setup() {
    const state = reactive({
      newVault: {},
      vault: computed(() => AppState.vault),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      checkbox: true
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#new-vault-form').modal('hide')
          Notification.toast('Successfully Created Vault', 'success')
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
