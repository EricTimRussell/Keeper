<template>
  <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${vault.img})` }">
    <i @click="deleteVault()" title="Delete Vault" v-if="vault.creatorId == profile.id"
      class="mdi mdi-delete-forever text-danger selectable text-end p-2"></i>
    <div class="card-body">
    </div>
    <div class="card-footer outline d-flex text-shadow justify-content-between">
      <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
        <h3 class="selectable">
          {{ vault.name }}
        </h3>
      </router-link>
      <i class="mdi mdi-lock text-light fs-5" v-if="vault.isPrivate"></i>
    </div>
  </div>
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
      // TODO update page when vault is deleted
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

.text-shadow {
  text-shadow: 1px 1px 5px #ff00e1, 0 0 1em #8888f7, 0 0 0.2em #ffffff;
}
</style>