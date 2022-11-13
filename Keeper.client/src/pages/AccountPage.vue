<template>
  <main class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-sm-12 col-md-8 d-flex flex-column justify-content-end align-items-center bgimg my-4"
        :style="{ backgroundImage: `url(${account.coverImg})` }">
        <img :src=account.picture alt="profile pic" class="profile-img">
      </div>
      <div class="col-12 text-center">
        <i data-bs-toggle="modal" data-bs-target="#editAccount" title="Edit Account"
          class="mdi mdi-cog selectable text-dark"></i>
      </div>
      <div class="col-12 d-flex justify-content-center">
        <h1>{{ account.name }}</h1>
      </div>
      <div class="col-md-2 d-flex justify-content-center bg-light round">
        <h4>{{ vault.length }} Vaults</h4>
      </div>
      <h3 v-if="vault.length > 0">Vaults</h3>
      <div class="col-sm-6 col-md-3 my-4" v-for="v in vault" :key="v.id">
        <VaultCard :vault="v" />
      </div>
      <div class="row">
        <!-- NOTE keeps not setup on the backend for account page -->
        <h3 v-if="keeps.length > 0">Keeps</h3>
        <div class="col-sm-6 col-md-3 my-4" v-for="k in keeps" :key="k.id">
          <!-- <KeepCard :keeps="k" /> -->
        </div>
      </div>
    </div>
  </main>

  <!-- Modal -->
  <form @submit.prevent="editAccount()">
    <div class="modal fade" id="editAccount" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Edit Account</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
              title="Close Modal"></button>
          </div>
          <div class="modal-body">
            <div class="form-floating">
              <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="name">
              <label for="name">Name...</label>
            </div>
            <div class="form-floating my-3">
              <input v-model="editable.picture" type="url" class="form-control" id="profilePicture"
                placeholder="profile picture">
              <label for="profilePicture">Picture Url...</label>
            </div>
            <div class="form-floating">
              <input v-model="editable.coverImg" type="url" class="form-control" id="profileCoverImage"
                placeholder="profile coverImage">
              <label for="profileCoverImage">Cover Image Url...</label>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>
  </form>

</template>

<script>
import { Modal } from "bootstrap"
import { computed, onMounted, ref } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import KeepCard from "../components/KeepCard.vue"
import VaultCard from "../components/VaultCard.vue"
import { accountService } from "../services/AccountService"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/vaultsService"
import Pop from "../utils/Pop"
export default {
  setup() {
    const route = useRoute();
    const editable = ref({})

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults();
      }
      catch (error) {
        Pop.error(error, "Getting Account Vaults");
      }
    }

    async function getKeepById() {
      try {
        await keepsService.getKeepById()
      } catch (error) {
        Pop.error(error.message, "Getting keep by id")
      }
    }

    onMounted(() => {
      getMyVaults();
    });
    return {
      editable,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.accountVaults),
      keeps: computed(() => AppState.keeps),

      async editAccount() {
        try {
          const formData = editable.value
          await accountService.editAccount(formData)
          Modal.getOrCreateInstance('#editAccount').hide()
        } catch (error) {
          Pop.error(error, "Editing Account")
        }
      }

    };
  },
  components: { VaultCard, KeepCard }
}
</script>

<style scoped>
.round {
  border-radius: 2rem;
  width: 15vh;
}

.profile-img {
  border-radius: 50%;
  max-height: 12vh;
  min-height: 12vh;
  max-width: 12vh;
  min-width: 12vh;
}

.bgimg {
  height: 25vh;
  background-position: center;
  background-size: cover;
  border-radius: 1rem;
}
</style>
