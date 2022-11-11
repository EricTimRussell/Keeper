<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-light px-3 height elevation-2 row">
    <div class="row">
      <div class="col-3 d-flex">
        <router-link class="navbar-brand" :to="{ name: 'Home' }">
          <button class="btn btn-secondary">Home</button>
        </router-link>
        <div v-if="account.id" class="btn-group" role="group" aria-label="Button group with nested dropdown">
          <div class="btn-group" role="group">
            <button type="button" class="btn btn-outline dropdown-toggle" data-bs-toggle="dropdown"
              aria-expanded="false">
              Create
            </button>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#vault">Vault</a></li>
              <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#keep">Keep</a></li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-6 d-flex justify-content-center">
        <img alt="logo" src="../assets/img/KeeprLogo.png" height="60" />
      </div>
      <div class="col-3">
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
          aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse d-flex justify-content-end" id="navbarText">
          <Login />
        </div>
      </div>
    </div>
  </nav>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import Login from './Login.vue'
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
.height {
  min-height: 8vh;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
