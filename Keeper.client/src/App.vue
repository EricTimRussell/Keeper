<template>
  <header>
    <div>
      <Navbar />
    </div>
  </header>
  <main>
    <router-view />
  </main>

  <!-- create keep modal -->
  <div class="modal fade" id="keep" tabindex="-1" aria-labelledby="Keep Modal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Create</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <CreateKeep id="" />
        </div>
      </div>
    </div>
  </div>

  <!-- Create Vault modal -->
  <div class="modal fade" id="vault" tabindex="-1" aria-labelledby="Vault Modal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5">Create</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <CreateVault />
        </div>
      </div>
    </div>
  </div>

</template>






<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateKeep from "./components/CreateKeep.vue"
import CreateVault from "./components/CreateVault.vue"
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      profile: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { Navbar, CreateKeep, CreateVault }
}
</script>
<style lang="scss">
.no-pad {
  padding: 0% !important
}

@import "./assets/scss/main.scss";
</style>
