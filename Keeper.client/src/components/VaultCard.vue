<template>

  <body>
    <div class="card bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${vault.img})` }">
      <div class="text-end">
        <i @click="deleteVault()" title="Delete Vault" v-if="vault.creatorId == profile.id"
          class="mdi mdi-delete-forever text-danger selectable"></i>
      </div>
      <div class="card-body">
      </div>
      <div class="card-footer outline d-flex text-shadow justify-content-between">
        <router-link v-if="vault.id" :to="{ name: 'Vault', params: { id: vault.id } }">
          <h3 class="selectable text-light">
            {{ vault.name }}
          </h3>
        </router-link>
        <i class="mdi mdi-lock text-light fs-5" title="Private Vault" v-if="vault.isPrivate"></i>
      </div>
    </div>
  </body>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup(props) {

    return {
      profile: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      async deleteVault() {
        try {
          if (await Pop.confirm("Delete Vault?"))
            await vaultsService.deleteVault(props.vault.id);
        } catch (error) {
          Pop.error(error, "Deleting Vault")
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
body {
  text-transform: uppercase;
  letter-spacing: 7px;
}

.bgimg {
  height: 35vh;
  max-width: 45vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}
</style>