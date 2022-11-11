<template>

  <body>
    <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${keeps.img})` }">
      <div class=" text-end p-1">
        <i @click="deleteKeep()" v-if="keeps.creatorId == profile.id" title="Delete"
          class="mdi mdi-delete-forever selectable text-danger"></i>
      </div>
      <div class="card-body">
      </div>
      <div class="card-footer outline d-flex justify-content-between text-shadow">
        <h3 @click="getKeepById()" class="selectable" data-bs-toggle="modal"
          :data-bs-target="`#vaultKeep${activeKeep.id}`">
          {{ keeps.name }}
        </h3>
        <router-link :to="{ name: 'Profile', params: { id: keeps.creatorId } }">
          <img :title=profile.name :src=profile.picture alt="profile img" class="profile-img">
        </router-link>
      </div>
    </div>
  </body>

  <!-- View Keep Modal -->
  <div class="modal fade" :id="`vaultKeep${activeKeep.id}`" tabindex="-1" aria-labelledby="Vault Keep"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="no-pad modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6 no-pad">
                <img :src=activeKeep.img alt="Keep Image" class="img-fluid">
              </div>
              <div class="col-md-6 text-center d-flex flex-column justify-content-between px-5 py-1">
                <div class="d-flex justify-content-center">
                  <h5 class="mdi mdi-eye-outline">&nbsp{{ activeKeep.views }}&nbsp&nbsp</h5>
                  <h5 class="mdi mdi-alpha-k-box-outline">&nbsp{{ activeKeep.kept }}</h5>
                </div>
                <div>
                  <h1>{{ activeKeep.name }} </h1>
                  <p class="text-start">{{ activeKeep.description }} Lorem ipsum dolor sit amet consectetur adipisicing
                    elit.
                    Accusamus animi
                    illo nulla, maxime repudiandae saepe molestiae alias excepturi odio, quo officiis repellendus,
                    corrupti rem est vero iste non blanditiis? Odit.</p>
                </div>
                <!-- Add Keep to Vault -->
                <div class="d-flex justify-content-between">
                  <div class="my-5 d-flex gap-3">
                    <div class="dropdown" v-if="profile.id">
                      <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                        aria-expanded="false">
                        Dropdown button
                      </button>
                      <ul class="dropdown-menu">
                        <li v-for="v in vaults" :key="v.id">
                          <KeepModalDropdown :vaults="v" @click="addKeepToVault(v.id)" />
                        </li>
                      </ul>
                    </div>
                  </div>
                  <router-link :to="{ name: 'Profile', params: { id: keeps.creatorId } }">
                    <img :src=activeKeep.creator?.picture alt="profile picture" :title=activeKeep.creator?.name
                      class="profile-img mt-5">
                  </router-link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultKeepsService } from "../services/VaultKeepsService";
import Pop from "../utils/Pop";
import KeepModalDropdown from "./KeepModalDropdown.vue";

export default {
  props: {
    keeps: { type: Object, required: true }
  },
  setup(props) {
    return {
      profile: computed(() => AppState.account),
      vault: computed(() => AppState.vaults),
      vaults: computed(() => AppState.activeVault),
      activeKeep: computed(() => AppState.activeKeep),
      async deleteKeep() {
        try {
          if (await Pop.confirm("Delete Keep?"))
            await keepsService.deleteKeep(props.keeps.id);
        }
        catch (error) {
          Pop.error(error, "Deleting Keep");
        }
      },

      async addKeepToVault(vaultId) {
        try {
          const vaultKeep = {
            vaultId: vaultId,
            keepId: AppState.activeKeep.id
          }
          await vaultKeepsService.addKeepToVault(vaultKeep)
        }
        catch (error) {
          Pop.error(error, "Adding keep to vault");
        }
      },

      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keeps.id)
        } catch (error) {
          Pop.error(error, "Get Keep By Id")
        }
      }
    };
  },
  components: { KeepModalDropdown }
}
</script>


<style lang="scss" scoped>
.bgimg {
  height: 35vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.profile-img {
  border-radius: 50%;
  max-height: 5vh;
  min-width: 5vh;
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}

.text-shadow {
  text-shadow: 1px 1px 5px #ff00e1, 0 0 1em #8888f7, 0 0 0.2em #ffffff;
}
</style>