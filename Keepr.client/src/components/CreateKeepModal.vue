<template>
  <div class="create-keep-modal container-fluid
       modal"
       id="new-keep-form"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Create Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <form @submit.prevent="createKeep">
          <div class="modal-body container-fluid modalScroll modal-height">
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <label for="title">Title:</label>
                  <input type="text"
                         class="form-control"
                         id="title"
                         v-model="state.newKeep.name"
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
                         v-model="state.newKeep.img"
                         required
                  >
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="description">Description:</label>
                  <input type="text"
                         class="form-control"
                         id="description"
                         v-model="state.newKeep.description"
                         required
                  >
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="tags">Tags</label>
                  <input type="text"
                         class="form-control"
                         id="tags"
                         v-model="state.newKeep.tags"
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
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'
import $ from 'jquery'

export default {
  name: 'CreateKeepModal',
  setup() {
    const state = reactive({
      newKeep: {},
      keep: computed(() => AppState.keep),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          state.newKeep = {}
          $('#new-keep-form').modal('hide')
          Notification.toast('Successfully Created Keep', 'success')
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
